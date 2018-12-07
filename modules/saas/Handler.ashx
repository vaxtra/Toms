<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

public class Handler : IHttpHandler
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
                if (c != "saas")
                    result.Error("class not found", null);
                else
                {
                    switch (m)
                    {
                        case "renew":
                            {
                                Class_SAAS _saas = new Class_SAAS();
                                dynamic addCartData = _saas.DYNAMIC_AddToCart_Renew(int.Parse(data["IDProduct"].ToString()), int.Parse(data["IDCustomer"].ToString()), int.Parse(data["IDCustomerProduct"].ToString()), int.Parse(data["IDProductCombination"].ToString()));
                                if (addCartData.success)
                                {
                                    result.Success(addCartData.message, addCartData.data);
                                }
                                else
                                {
                                    result.Error(addCartData.message, null);
                                }
                            }
                            break;
                        case "upgrade":
                            {
                                Class_SAAS _saas = new Class_SAAS();
                                dynamic addCartData = _saas.DYNAMIC_AddToCart_Upgrade(int.Parse(data["IDProduct"].ToString()), int.Parse(data["IDCustomer"].ToString()), int.Parse(data["IDCustomerProduct"].ToString()));
                                if (addCartData.success)
                                {
                                    result.Success(addCartData.message, addCartData.data);
                                }
                                else
                                {
                                    result.Error(addCartData.message, null);
                                }
                            }
                            break;
                        case "checkpackage":
                            {
                                Class_SAAS _saas = new Class_SAAS();
                                Class_Customer cust = new Class_Customer();
                                if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()] == null)
                                    result.Success("No Login Session", null);
                                else
                                {
                                    string token = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()].Value;
                                    var _result = cust.DecryptToken(token) as IDictionary<string, object>;
                                    result.Success("Package Data", _saas.GetCustomerProduct_ByIDCustomer_AndIDProduct(int.Parse(data["IDCustomer"].ToString()), int.Parse(data["IDProduct"].ToString())));
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

}