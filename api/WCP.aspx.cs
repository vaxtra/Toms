using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

[System.Web.Script.Services.ScriptService]
public partial class api_WCP : System.Web.UI.Page
{
    [WebMethod]
    public static response SaveWCPToken(string token, string clientKey)
    {
        //HttpContext.Current.Response.Write("asdasd");
        //HttpContext.Current.Response.Write(status_code + " " + status_message + " " + transaction_id + " " + masked_card + " " + order_id + " " + gross_amount + " " + payment_type + " " + transaction_time + " " + transaction_status + " " + fraud_status + " " + approval_code + " " + signature_key + " " + bank + " " + eci);
        try
        {
            if (clientKey == ConfigurationManager.AppSettings["Key"])
            {
                DataClassesDataContext db = new DataClassesDataContext();
                var clientToken = db.TBConfigurations.Where(x => x.Name.Contains("WCP_Token")).FirstOrDefault();
                if (clientToken != null)
                {
                    clientToken.Value = token;
                    db.SubmitChanges();

                    response result = new response("success", "Token Succesfully Updated", clientToken.Value);

                    return result;
                }
                else
                {
                    TBConfiguration config = new TBConfiguration();
                    config.Name = "WCP_Token";
                    config.Value = token;
                    config.DateInsert = DateTime.Now;
                    config.DateLastUpdate = DateTime.Now;
                    db.TBConfigurations.InsertOnSubmit(config);
                    db.SubmitChanges();

                    response result = new response("success", "Token Succesfully Inserted", config.Value);

                    return result;
                }
            }
            else
            {
                response result = new response("failed", "Invalid client key", null);

                return result;
            }
        }
        catch (Exception ex)
        {
            response result = new response("error", ex.Message, null);
            return result;
        }
    }

    [WebMethod]
    public static response GetProductWCP(string token, string clientKey)
    {
        try
        {
            Class_Product _prod = new Class_Product();
            DataClassesDataContext db = new DataClassesDataContext();
            var clientToken = db.TBConfigurations.Where(x => x.Name.Contains("WCP_Token")).FirstOrDefault();

            if (clientKey != ConfigurationManager.AppSettings["Key"])
                return new response("failed", "Invalid client key", null);

            if (token != clientToken.Value)
                return new response("failed", "Invalid client token", null);

                dynamic product = _prod.Dynamic_GetAll(db);;

                return new response("success", "Product fetched succesfully", product);
        }
        catch (Exception ex)
        {
            return new response("error", ex.Message, null);
        }
    }
}

public class response
{
    public string status { get; set; }
    public string message { get; set; }
    public dynamic data { get; set; }

    public response(string _status, string _message, dynamic _data)
    {
        this.status = _status;
        this.message = _message;
        this.data = _data;
    }
}
