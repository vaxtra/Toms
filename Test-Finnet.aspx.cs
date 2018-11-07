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
            //var httpWebRequest = (HttpWebRequest)WebRequest.Create(@"https://sandbox.finpay.co.id/servicescode/api/apiFinpay.php");
            //httpWebRequest.ContentType = "application/json";
            //httpWebRequest.Method = "POST";
            //httpWebRequest.Accept = "application/json";

            //List<Items> items = new List<Items>();
            //items.Add(new Items(1, "Flannel", "Size : 30", 200000, 2, 400000));
            
            //recurring_data data = new recurring_data(3, 365000, 1, 10, "085720338927");

            //string info = "andreas";

            //Finnet finnet = new Finnet(info, null, null, null, null, 365000, "andreas.alexander7@gmail.com", "1", 1, "Andreas Alexander", "http://itwarmup.com/error.aspx", "0809-0001", items, "test", "TO2147", data, "http://itwarmup.com/complete.aspx", 1, "cc", "update", "http://itwarmup.com/success.aspx", 5, 15);

            //string AddInfo1 = finnet.add_info1 == null ? "" : finnet.add_info1 + "%";
            //string AddInfo2 = finnet.add_info2 == null ? "" : finnet.add_info2 + "%";
            //string AddInfo3 = finnet.add_info3 == null ? "" : finnet.add_info3 + "%";
            //string AddInfo4 = finnet.add_info4 == null ? "" : finnet.add_info4 + "%";
            //string AddInfo5 = finnet.add_info5 == null ? "" : finnet.add_info5 + "%";

            //string stringItems = "ARRAY";
           

            //string stringRecData = JsonConvert.SerializeObject(data);

            //string signature =
            //    (
            //    AddInfo1 +
            //    AddInfo2 +
            //    AddInfo3 +
            //    AddInfo4 +
            //    AddInfo5 +
            //    finnet.amount + "%" +
            //    finnet.cust_email + "%" +
            //    finnet.cust_id + "%" +
            //    finnet.cust_msisdn + "%" +
            //    finnet.cust_name + "%" +
            //    finnet.failed_url + "%" +
            //    finnet.invoice + "%" +
            //    stringItems + "%" + 
            //    finnet.merchant_id + "%" +
            //    stringRecData + "%" +
            //    finnet.return_url + "%" +
            //    finnet.rule_id + "%" +
            //    finnet.sof_id + "%" +
            //    finnet.sof_type + "%" +
            //    finnet.success_url + "%" +
            //    finnet.timeout + "%" +
            //    finnet.trans_date + "%").ToString().ToUpper() + "38-TO2147-01";

            //Response.Write("<span style='color:#0000ff;font-size:18px;'>Signature algorhytm : </span><br/>" + signature + "<br/><br/>");

            //// Create a SHA256   
            //using (SHA256 sha256Hash = SHA256Managed.Create())
            //{
            //    // ComputeHash - returns byte array  
            //    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(signature));

            //    // Convert byte array to a string   
            //    string builder = BitConverter.ToString(bytes).Replace("-", "");
            //    finnet.mer_signature = builder;
            //}

            //Response.Write("<span style='color:#0000ff;font-size:18px;'>Signature algorhytm SHA256 :</span><br/>" + finnet.mer_signature + "<br/><br/>");

            //using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            //{
            //    string json = JsonConvert.SerializeObject(finnet, Formatting.Indented);
            //    streamWriter.Write(json);
            //    Response.Write("<span style='color:#0000ff;font-size:18px;'>JSON POST </span><br/>" + json);
            //}
            //var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            //{
            //    var responseText = streamReader.ReadToEnd();
            //    dynamic result = JsonConvert.DeserializeObject(responseText);

            //    Response.Write("<br/><br/>" + result.status_code + "<br/>" + result.status_desc);
            //}
        }
    }
}