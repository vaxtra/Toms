<%@ WebHandler Language="C#" Class="ShippingCost" %>

using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;

public class ShippingCost : IHttpHandler
{

    public class ReceiveData
    {
        public string c { get; set; }
        public string m { get; set; }
        public object data { get; set; }
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
                if (c != "shipping")
                    result.Error("class not found", null);
                else
                {
                    switch (m)
                    {
                        case "province":
                            {
                                Class_Region region = new Class_Region();
                                result.Success("success", region.DYNAMIC_GetProvince_ByIDCountry(1));
                            }
                            break;
                        case "city":
                            {
                                if (data.ContainsKey("ID"))
                                {
                                    Class_Region region = new Class_Region();
                                    int idProvince = 0;
                                    int.TryParse(data["ID"].ToString(), out idProvince);
                                    result.Success("success", region.DYNAMIC_GetCity_ByIDProvince(idProvince));
                                }
                                else
                                    result.Error("ID is not specified", null);
                            }
                            break;
                        case "district":
                            {
                                if (data.ContainsKey("ID"))
                                {
                                    Class_Region region = new Class_Region();
                                    int idCity = 0;
                                    int.TryParse(data["ID"].ToString(), out idCity);
                                    result.Success("success", region.DYNAMIC_GetDistrict_ByIDCity(idCity));
                                }
                                else
                                    result.Error("ID is not specified", null);
                            }
                            break;
                        case "cost":
                            {
                                if (data.ContainsKey("ID"))
                                {
                                    int idDistrict = 0;
                                    int.TryParse(data["ID"].ToString(), out idDistrict);
                                    var cost = db.TBShippings.Where(x => x.IDCarrier == 1 && x.IDDistrict == idDistrict).Select(x => new { x.Price }).FirstOrDefault();
                                    result.Success("success", cost);

                                }
                                else
                                    result.Error("ID is not specified", null);
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
        //string id = context.Request["id"];

        //var jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        //var jsonString = String.Empty;

        //context.Request.InputStream.Position = 0;
        //using (var inputStream = new System.IO.StreamReader(context.Request.InputStream))
        //{
        //    jsonString = inputStream.ReadToEnd();
        //}

        //var emplList = jsonSerializer.Deserialize<System.Collections.Generic.List<Employee>>(jsonString);
        //var resp = String.Empty;

        //foreach (var emp in emplList)
        //{
        //    resp += emp.name + " \\ ";
        //}

        //context.Response.ContentType = "application/json";
        //context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        //context.Response.Write(jsonSerializer.Serialize(resp));

        //using (DataClassesDataContext db = new DataClassesDataContext())
        //{
        //    // This block will get all data in table 
        //    var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        //    context.Response.ContentType = "text/json";
        //    Dictionary<string, object> result = new Dictionary<string, object>();
        //    result["data"] = db.TBCustomers.Select(x => new { x.IDCustomer, x.FirstName }).Take(10);
        //    if (result.ContainsKey("data"))
        //        context.Response.Write(serializer.Serialize(result));
        //}


    }

    public class ResultData
    {
        public bool success { get; set; }
        public String message { get; set; }
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
            this.message = message;
            this.data = data;
            this.success = true;
        }

    }

    public class Employee
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}