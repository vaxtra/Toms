<%@ WebHandler Language="C#" Class="WITSync" %>

using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Globalization;

public class WITSync : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        if (context.Request.QueryString["action"] != null)
        {
            if (context.Request.QueryString["action"].ToLower() == "stockopname")
            {
                if (context.Request.QueryString["referenceCode"] != null || context.Request.QueryString["quantity"] != null)
                {
                    context.Response.Write(StockOpname(context.Request.QueryString["referenceCode"], int.Parse(context.Request.QueryString["quantity"])));
                }
                else
                {
                    context.Response.Write("Stock opname action need a parameter : [referenceCode] & [quantity]");
                }
            }

            else if (context.Request.QueryString["action"].ToLower() == "checkquantity")
            {
                if (context.Request.QueryString["referenceCode"] != null)
                {
                    context.Response.Write(CheckQuantity(context.Request.QueryString["referenceCode"]));
                }
                else
                {
                    context.Response.Write("Stock opname action need a parameter : [referenceCode]");
                }
            }

            else if (context.Request.QueryString["action"].ToLower() == "stockupdate")
            {
                if (context.Request.QueryString["referenceCode"] != null || context.Request.QueryString["quantity"] != null || context.Request.QueryString["type"] != null)
                {
                    context.Response.Write(StockUpdate(context.Request.QueryString["referenceCode"], int.Parse(context.Request.QueryString["quantity"]), context.Request.QueryString["type"]));
                }
                else
                {
                    context.Response.Write("Stock opname action need a parameter : [referenceCode] & [quantity] & [type]");
                }

            }

            else if (context.Request.QueryString["action"].ToLower() == "getallcombination")
            {
                context.Response.Write(GetAllCombination());
            }
        }
        else
        {
            context.Response.Write("Please Fill your action type");
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

    public string StockOpname(string referenceCode, int quantity)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                Class_Product prod = new Class_Product();
                Class_Employee emp = new Class_Employee();
                var employee = emp.GetData_By_Token(HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()].Value);

                TBProduct_Combination _productCombination = db.TBProduct_Combinations.Where(x => x.ReferenceCode.ToLower() == referenceCode.ToLower()).FirstOrDefault();
                int firstQty = _productCombination.Quantity;
                if (_productCombination == null)
                    return "Data Combination not found";

                //INSERT LOG STOCK
                Class_Log_Stock log = new Class_Log_Stock();
                if (_productCombination.Quantity > quantity)
                    log.Insert(employee.IDEmployee, _productCombination.IDProduct_Combination, _productCombination.Name, firstQty, quantity, _productCombination.Quantity - quantity, "decrease", "Update Combination by WMS SYNC SYSTEM");
                else if (_productCombination.Quantity < quantity)
                    log.Insert(employee.IDEmployee, _productCombination.IDProduct_Combination, _productCombination.Name, firstQty, quantity, quantity - _productCombination.Quantity, "increase", "Update Combination by WMS SYNC SYSTEM");
                _productCombination.Quantity = quantity;
                _productCombination.DateLastUpdate = DateTime.Now;

                db.SubmitChanges();
                return "Combination Quantity succesfully updated";
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string StockUpdate(string referenceCode, int quantity, string type)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                Class_Product prod = new Class_Product();
                Class_Employee emp = new Class_Employee();
                var employee = emp.GetData_By_Token(HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()].Value);

                TBProduct_Combination _productCombination = db.TBProduct_Combinations.Where(x => x.ReferenceCode.ToLower() == referenceCode.ToLower()).FirstOrDefault();
                if (_productCombination == null)
                    return "Data Combination not found";

                //INSERT LOG STOCK
                Class_Log_Stock log = new Class_Log_Stock();

                int oldQty = _productCombination.Quantity;

                if (type.ToLower() == "increase")
                {
                    log.Insert(employee.IDEmployee, _productCombination.IDProduct_Combination, _productCombination.Name, oldQty, _productCombination.Quantity + quantity, quantity, "increase", "Update Combination by WMS SYNC SYSTEM");
                    _productCombination.Quantity += quantity;
                }
                else if (type.ToLower() == "decrease")
                {
                    log.Insert(employee.IDEmployee, _productCombination.IDProduct_Combination, _productCombination.Name, oldQty, _productCombination.Quantity - quantity, quantity, "decrease", "Update Combination by WMS SYNC SYSTEM");
                    _productCombination.Quantity -= quantity;
                }
                else
                {
                    return "unknown type command";
                }
                _productCombination.DateLastUpdate = DateTime.Now;

                db.SubmitChanges();
                return "Combination Quantity succesfully updated [" + _productCombination.ReferenceCode + "] from " + oldQty + " to " + _productCombination.Quantity;
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string CheckQuantity(string referenceCode)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                //Dictionary<string, dynamic> _result = new Dictionary<string, dynamic>();
                TBProduct_Combination data = db.TBProduct_Combinations.Where(x => !x.Deflag && x.ReferenceCode == referenceCode).FirstOrDefault();
                if (data == null)
                    return "Data not found";
                else
                {
                    return data.TBProduct.Name + " | " + data.Name + " <br/>Quantity = " + data.Quantity.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string GetAllCombination()
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                var combination = db.TBProduct_Combinations.ToList();

                string result = "";

                foreach (var item in combination)
                {
                    result += item.IDProduct_Combination + " | " + item.ReferenceCode + " | Quantity = " + item.Quantity + "<br/>";
                }

                return result;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

}