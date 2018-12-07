<%@ WebHandler Language="C#" Class="FinnetHanlder" %>

using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

public class FinnetHanlder : IHttpHandler
{
    public class ReceiveData
    {
        public string c { get; set; }
        public string m { get; set; }
        public object data { get; set; }
    }

    public class ResultData
    {
        public bool success { get; set; }
        public string message { get; set; }
        public object data { get; set; }

        public ResultData()
        {

        }

        public void Error(string msg, object data)
        {
            this.message = msg;
            this.data = data;
            this.success = false;
        }

        public void Success(string msg, object data)
        {
            this.message = msg;
            this.data = data;
            this.success = true;
        }
    }

    public class Status
    {
        public string status_code { get; set; }
        public string status_desc { get; set; }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            string c = string.Empty;
            string m = string.Empty;
            string jsonString = string.Empty;

            DataClassesDataContext db = new DataClassesDataContext();

            ResultData result = new ResultData();
            var jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            context.Request.InputStream.Position = 0;
            using (var inputStream = new System.IO.StreamReader(context.Request.InputStream))
            {
                jsonString = inputStream.ReadToEnd();
            }

            var receiveData = jsonSerializer.Deserialize<ReceiveData>(jsonString);
            if (receiveData.c == null)
            {
                result.Error("Error", null);
            }
            else
            {
                c = receiveData.c;
                m = receiveData.m;
                Dictionary<string, object> data = new Dictionary<string, object>();
                data = receiveData.data as Dictionary<string, object>;
                if (c != "finnet")
                    result.Error("class not found", null);
                else
                {
                    switch (m)
                    {
                        case "transaction":
                            {
                                //SUBMIT ORDER
                                Class_PaymentMethod pm = new Class_PaymentMethod();

                                Class_Order order = new Class_Order();
                                Class_Configuration _config = new Class_Configuration();
                                ReturnData resultSaveOrder = order.Object_SubmitOrder_veritrans(int.Parse(data["IDPaymentMethod"].ToString())) as ReturnData;
                                if (!resultSaveOrder.success)
                                    result.Error(resultSaveOrder.message, resultSaveOrder.data);

                                TBOrder detailOrder = order.GetDetail_ByIDOrder((resultSaveOrder.data as TBOrder).IDOrder);

                                var httpWebRequest = (HttpWebRequest)WebRequest.Create(@"https://sandbox.finpay.co.id/servicescode/api/apiFinpay.php");
                                httpWebRequest.ContentType = "application/json";
                                httpWebRequest.Method = "POST";
                                httpWebRequest.Accept = "application/json";

                                List<Items> items = new List<Items>();
                                foreach (var item in detailOrder.TBOrder_Details)
                                {
                                    items.Add(new Items(item.IDProduct, item.TBProduct.Name, item.Product_Name, (int)item.Price, item.Quantity, (int)(item.Quantity * item.Price)));
                                }

                                recurring_data recData = new recurring_data(3, (int)detailOrder.TotalPaid, 1, 10, detailOrder.TBCustomer.PhoneNumber);

                                Finnet finnet = new Finnet(
                                    detailOrder.TBCustomer.FirstName + " " + detailOrder.TBCustomer.LastName,
                                    detailOrder.TBCustomer.FirstName + " " + detailOrder.TBCustomer.LastName,
                                    detailOrder.TBCustomer.FirstName + " " + detailOrder.TBCustomer.LastName,
                                    detailOrder.TBCustomer.FirstName + " " + detailOrder.TBCustomer.LastName,
                                    detailOrder.TBCustomer.FirstName + " " + detailOrder.TBCustomer.LastName,
                                    (int)detailOrder.TotalPaid,
                                    detailOrder.TBCustomer.Email,
                                    detailOrder.IDCustomer.ToString(),
                                    detailOrder.IDCustomer,
                                    detailOrder.TBCustomer.FirstName + " " + detailOrder.TBCustomer.LastName,
                                    "http://itwarmup.com/error.aspx",
                                    detailOrder.Reference,
                                    items,
                                    "",
                                    "TO2147",
                                    recData,
                                    "http://itwarmup.com/complete.aspx",
                                    1,
                                    data["sof_id"].ToString(),
                                    data["sof_type"].ToString(),
                                    "http://itwarmup.com/success.aspx",
                                    5,
                                    detailOrder.DateInsert.Day);

                                string AddInfo1 = finnet.add_info1 == null ? "" : finnet.add_info1 + "%";
                                string AddInfo2 = finnet.add_info2 == null ? "" : finnet.add_info2 + "%";
                                string AddInfo3 = finnet.add_info3 == null ? "" : finnet.add_info3 + "%";
                                string AddInfo4 = finnet.add_info4 == null ? "" : finnet.add_info4 + "%";
                                string AddInfo5 = finnet.add_info5 == null ? "" : finnet.add_info5 + "%";

                                string stringItems = "ARRAY";


                                string stringRecData = "ARRAY";

                                string signature =
                                    (
                                    AddInfo1 +
                                    AddInfo2 +
                                    AddInfo3 +
                                    AddInfo4 +
                                    AddInfo5 +
                                    finnet.amount + "%" +
                                    finnet.cust_email + "%" +
                                    finnet.cust_id + "%" +
                                    finnet.cust_msisdn + "%" +
                                    finnet.cust_name + "%" +
                                    finnet.failed_url + "%" +
                                    finnet.invoice + "%" +
                                    stringItems + "%" +
                                    finnet.merchant_id + "%" +
                                    stringRecData + "%" +
                                    finnet.return_url + "%" +
                                    finnet.rule_id + "%" +
                                    finnet.sof_id + "%" +
                                    finnet.sof_type + "%" +
                                    finnet.success_url + "%" +
                                    finnet.timeout + "%" +
                                    finnet.trans_date + "%").ToString().ToUpper() + "38-TO2147-01";


                                string jsonPost = "";
                                // Create a SHA256   
                                using (SHA256 sha256Hash = SHA256Managed.Create())
                                {
                                    // ComputeHash - returns byte array  
                                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(signature));

                                    // Convert byte array to a string   
                                    string builder = BitConverter.ToString(bytes).Replace("-", "");
                                    finnet.mer_signature = builder;
                                }

                                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                                {
                                    string json = JsonConvert.SerializeObject(finnet, Formatting.Indented);
                                    streamWriter.Write(json);
                                    jsonPost = json;
                                }
                                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                                {
                                    var responseText = streamReader.ReadToEnd();
                                    Status resultData = JsonConvert.DeserializeObject<Status>(responseText);

                                    if (resultData.status_code == "00")
                                    {
                                        result.Success(resultData.status_code + " - " + resultData.status_desc, jsonPost);
                                    }
                                    else
                                    {
                                        result.Error(resultData.status_code + " - " + resultData.status_desc, jsonPost);
                                    }

                                }
                            }
                            break;
                        default: result.Error("endpoint not found", null); break;
                    }
                }
            }

            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.Response.Write(jsonSerializer.Serialize(result));
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    public class Finnet
    {
        public string add_info1 { get; set; }
        public string add_info2 { get; set; }
        public string add_info3 { get; set; }
        public string add_info4 { get; set; }
        public string add_info5 { get; set; }
        public int amount { get; set; }
        public string cust_email { get; set; }
        public string cust_id { get; set; }
        public int cust_msisdn { get; set; }
        public string cust_name { get; set; }
        public string failed_url { get; set; }
        public string invoice { get; set; }
        public List<Items> items { get; set; }
        public string merchant_id { get; set; }
        public string mer_signature { get; set; }
        public recurring_data recurring_data { get; set; }
        public string return_url { get; set; }
        public int rule_id { get; set; }
        public string sof_id { get; set; }
        public string sof_type { get; set; }
        public string success_url { get; set; }
        public int timeout { get; set; }
        public int trans_date { get; set; }

        public Finnet(string _add_info1, string _add_info2, string _add_info3, string _add_info4, string _add_info5, int _amount, string _cust_email, string _cust_id, int _cust_msisdn, string _cust_name, string _failed_url, string _invoice, List<Items> _items, string _mer_signature, string _merchant_id, recurring_data _recurring_data, string _return_url, int _rule_id, string _sof_id, string _sof_type, string _success_url, int _timeout, int _trans_date)
        {
            this.add_info1 = _add_info1;
            this.add_info2 = _add_info2;
            this.add_info3 = _add_info3;
            this.add_info4 = _add_info4;
            this.add_info5 = _add_info5;
            this.amount = _amount;
            this.cust_email = _cust_email;
            this.cust_id = _cust_id;
            this.cust_msisdn = _cust_msisdn;
            this.cust_name = _cust_name;
            this.failed_url = _failed_url;
            this.invoice = _invoice;
            this.items = _items;
            this.merchant_id = _merchant_id;
            this.mer_signature = _mer_signature;
            this.recurring_data = _recurring_data;
            this.return_url = _return_url;
            this.rule_id = _rule_id;
            this.sof_id = _sof_id;
            this.sof_type = _sof_type;
            this.success_url = _success_url;
            this.timeout = _timeout;
            this.trans_date = _trans_date;
        }
    }

    public class Items
    {
        public int id_product { get; set; }
        public string name { get; set; }
        public string varian { get; set; }
        public int price_per_unit { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }

        public Items(int _id_product, string _name, string _varian, int _price_per_unit, int _quantity, int _price)
        {
            this.id_product = _id_product;
            this.name = _name;
            this.varian = _varian;
            this.price_per_unit = _price_per_unit;
            this.quantity = _quantity;
            this.price = _price;
        }
    }

    public class recurring_data
    {
        public int recType { get; set; }
        public int recAmount { get; set; }
        public int rectInterval { get; set; }
        public int recDue { get; set; }
        public string recNumber { get; set; }

        public recurring_data(int _recType, int _recAmount, int _rectInterval, int _recDue, string _recNumber)
        {
            this.recType = _recType;
            this.recAmount = _recAmount;
            this.rectInterval = _rectInterval;
            this.recDue = _recDue;
            this.recNumber = _recNumber;
        }
    }
}