using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

public partial class Test_Finnet : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(@"https://sandbox.finpay.co.id/servicescode/api/apiFinpay.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json";

            List<Items> items = new List<Items>();
            items.Add(new Items(1, "Flannel", "Size : 30", 200000, 2, 400000));
            
            recurring_data data = new recurring_data(3, 365000, 1, 10, "085720338927");

            Finnet finnet = new Finnet(null, null, null, null, null, 365000, "andreas.alexander7@gmail.com", "1", 1, "Andreas Alexander", "http://itwarmup.com/error.aspx", "0809-0001", items, "test", "TO2147", data, "http://itwarmup.com/complete.aspx", 1, "cc", "pay", "http://itwarmup.com/success.aspx", 5, 15);

            string AddInfo1 = finnet.add_info1 == null ? "" : finnet.add_info1 + "%";
            string AddInfo2 = finnet.add_info2 == null ? "" : finnet.add_info2 + "%";
            string AddInfo3 = finnet.add_info3 == null ? "" : finnet.add_info3 + "%";
            string AddInfo4 = finnet.add_info4 == null ? "" : finnet.add_info4 + "%";
            string AddInfo5 = finnet.add_info5 == null ? "" : finnet.add_info5 + "%";

            string stringRecData = "ARRAY";

            string stringItems = "ARRAY";

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

            Response.Write("<span style='color:#0000ff;font-size:18px;'>Signature algorhytm : </span><br/>" + signature + "<br/><br/>");

            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(signature));

                // Convert byte array to a string   
                string builder = BitConverter.ToString(bytes).Replace("-", "");
                finnet.mer_signature = builder;
            }

            Response.Write("<span style='color:#0000ff;font-size:18px;'>Signature algorhytm SHA256 :</span><br/>" + finnet.mer_signature + "<br/><br/>");

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(finnet, Formatting.Indented);
                streamWriter.Write(json);
                Response.Write("<span style='color:#0000ff;font-size:18px;'>JSON POST </span><br/>" + json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                dynamic result = JsonConvert.DeserializeObject(responseText);

                Response.Write("<br/><br/>" + result.status_code + "<br/>" + result.status_desc);
            }
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
            this.recType =_recType;
            this.recAmount = _recAmount;
            this.rectInterval = _rectInterval;
            this.recDue = _recDue;
            this.recNumber = _recNumber;
        }
    }

    public class add_info
    {
        public string DebitCardNo { get; set; }
        public string Last10DigitDebitCardNo { get; set; }
        public string Amount { get; set; }
        public string Invoice { get; set; }
        public string TokenResponse { get; set; }

        public add_info(string _DebitCardNo, string _Last10DigitDebitCardNo, string _Amount, string _Invoice, string _TokenResponse)
        {
            this.DebitCardNo = _DebitCardNo;
            this.Last10DigitDebitCardNo = _Last10DigitDebitCardNo;
            this.Amount = _Amount;
            this.Invoice = _Invoice;
            this.TokenResponse = _TokenResponse;
        }
    }
}