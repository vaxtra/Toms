<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

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

    public class Status
    {
        public int code { get; set; }
        public string description { get; set; }
    }

    #region DESTINATIONS
    public class Destination
    {
        public Destinations Sicepat { get; set; }
    }

    public class Destinations
    {
        public Status status { get; set; }
        public List<ListDestination> results { get; set; }
    }
    public class ListDestination
    {
        public string destination_code { get; set; }
        public string subdistrict { get; set; }
        public string city { get; set; }
        public string province { get; set; }
    }
    #endregion

    #region TARIF
    public class Price
    {
        public Prices Sicepat { get; set; }
    }

    public class Prices
    {
        public Status status { get; set; }
        public List<ListPrice> results { get; set; }
    }
    public class ListPrice
    {
        public string service { get; set; }
        public string description { get; set; }
        public string tariff { get; set; }
        public string etd { get; set; }
    }
    #endregion

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
                if (c != "adminorder")
                    result.Error("class not found", null);
                else
                {
                    switch (m)
                    {
                        case "district":
                            {
                                try
                                {
                                    var district = db.TBDistricts.Where(x => !x.Deflag && x.IDCity == int.Parse(data["IDCity"].ToString())).Select(x => new
                                    {
                                        x.IDDistrict,
                                        x.Name,
                                    });
                                    result.Success("OK", district);
                                }
                                catch (Exception ex)
                                {

                                    result.Error("error", ex.Message);
                                }

                            }
                            break;
                        //case "sicepat":
                        //    {
                        //        try
                        //        {
                        //            string path = "http://api.sicepat.com/customer/tariff?api-key=391274a334f7527a6c1f340532aeaddd&origin=" + data["origin"] + "&destination=" + data["destination"] + "&weight=" + data["weight"].ToString().Replace(",", ".");
                        //            dynamic resultData;
                        //            dynamic json;
                        //            using (WebClient wc = new WebClient())
                        //            {
                        //                json = wc.DownloadString(path);
                        //                resultData = JsonConvert.DeserializeObject<Price>(@json);
                        //            }

                        //            if (resultData.Sicepat.status.code == 200)
                        //            {
                        //                result.Success("success", resultData.Sicepat.results);
                        //            }
                        //            else
                        //            {
                        //                result.Error(resultData.Sicepat.status.description, null);
                        //            }

                        //        }
                        //        catch (Exception ex)
                        //        {

                        //            result.Error("error", ex.Message);
                        //        }
                        //    }
                        //    break;
                        case "shipping":
                            {
                                Class_Shipping shipping = new Class_Shipping();
                                var shippingData = db.TBShippings.Where(x => x.IDDistrict == int.Parse(data["IDDistrict"].ToString())).ToList();
                                List<dynamic> resultShipping = new List<dynamic>();
                                foreach (var itemShipping in shippingData)
                                {
                                    //decimal totalWeight = Math.Round((decimal)CartData["TotalWeight"], MidpointRounding.AwayFromZero);
                                    decimal totalWeight = Math.Ceiling((decimal)data["TotalWeight"]); // update shipping witcommerce

                                    decimal totalPrice = 0;
                                    decimal shippingPrice = 0;
									
                                    totalPrice = Class_Currency.GetPriceConversionCurrency(itemShipping.Price) * totalWeight;
                                    shippingPrice = Class_Currency.GetPriceConversionCurrency(itemShipping.Price);
                                    if (totalWeight < 1)
                                        totalPrice = shippingPrice;

                                    Dictionary<string, dynamic> returnShippingData = new Dictionary<string, dynamic>();
                                    returnShippingData.Add("IDCarrier", itemShipping.IDCarrier);
                                    returnShippingData.Add("IDShipping", itemShipping.IDShipping);
                                    returnShippingData.Add("IDDistrict", itemShipping.IDDistrict);
                                    returnShippingData.Add("ImageShipping", itemShipping.TBCarrier.Image);
                                    returnShippingData.Add("Name", itemShipping.TBCarrier.Name);
                                    returnShippingData.Add("Price", shippingPrice);
                                    returnShippingData.Add("Information", itemShipping.TBCarrier.Information);
                                    returnShippingData.Add("TotalPrice", totalPrice);

                                    resultShipping.Add(returnShippingData);
                                }
                                result.Success("ok", resultShipping);
                            }
                            break;
                        case "idshipping":
                            {
                                try
                                {
                                    var listCarrier = db.TBCarriers.Where(x => x.Name.ToLower().Trim() == data["name"].ToString().ToLower().Trim()).FirstOrDefault();
                                    int idCarrier;
                                    int idShipping = 0;
                                    if (listCarrier == null)
                                    {
                                        TBCarrier newCarrier = new TBCarrier();
                                        newCarrier.Name = "Si Cepat " + data["name"];
                                        newCarrier.Image = "sicepat.png";
                                        newCarrier.Information = data["description"] + " " + data["etd"];
                                        newCarrier.Active = true;
                                        newCarrier.Deflag = false;
                                        newCarrier.DateInsert = DateTime.Now;
                                        newCarrier.DateLastUpdate = DateTime.Now;
                                        db.TBCarriers.InsertOnSubmit(newCarrier);
                                        db.SubmitChanges();
                                        idCarrier = newCarrier.IDCarrier;
                                    }
                                    else
                                    {
                                        idCarrier = listCarrier.IDCarrier;
                                        db.SubmitChanges();
                                    }
                                    var listShipping = db.TBShippings.Where(x => x.IDDistrict == (int)data["iddistrict"] && x.IDCarrier == idCarrier).FirstOrDefault();
                                    if (listShipping == null)
                                    {
                                        TBShipping newShipping = new TBShipping();
                                        newShipping.IDCarrier = idCarrier;
                                        newShipping.IDDistrict = (int)data["iddistrict"];
                                        newShipping.Price = (int)data["price"];
                                        newShipping.Deflag = false;
                                        newShipping.DateInsert = DateTime.Now;
                                        newShipping.DateLastUpdate = DateTime.Now;
                                        db.TBShippings.InsertOnSubmit(newShipping);
                                        db.SubmitChanges();
                                        idShipping = newShipping.IDShipping;

                                    }
                                    else
                                    {
                                        listShipping.Price = (int)data["price"];
                                        idShipping = listShipping.IDShipping;
                                        db.SubmitChanges();
                                    }

                                    result.Success("success", idShipping);
                                }
                                catch (Exception ex)
                                {
                                    result.Error("error", ex.Message);
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