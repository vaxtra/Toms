using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WITLibrary;

/// <summary>
/// Summary description for Class_Shipping
/// </summary>
public class Class_Shipping
{
    public Class_Shipping()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region AJAX
    public Datatable AJAX_GetTable(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search, string idCarrier)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            IEnumerable<dynamic> data = db.TBShippings.Where(X => !X.Deflag && X.IDCarrier.ToString() == idCarrier).Select(x => new
            {
                x.IDShipping,
                x.IDCarrier,
                x.IDDistrict,
                x.Price,
                DistrictName = x.TBDistrict.Name,
                CityName = x.TBDistrict.TBCity.Name
            }).ToList();
            int count = data.Count();
            if (!string.IsNullOrEmpty(search))
                data = data.Where(x => x.CityName.ToLower().Contains(search.ToLower()) || x.DistrictName.ToLower().Contains(search.ToLower())).ToArray();
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (dynamic currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("CityName", currData.CityName);
                newData.Add("DistrictName", currData.DistrictName);
                newData.Add("Price", currData.Price);
                newData.Add("IDShipping", currData.IDShipping);
                newData.Add("IDCarrier", currData.IDCarrier);
                newData.Add("IDDistrict", currData.IDDistrict);
                resultList.Add(newData);
            }
            return OurClass.ParseTable(resultList, count, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return new Datatable
            {
                sEcho = 0,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0,
                aaData = new List<Dictionary<string, dynamic>>()
            };
        }
    }
    public ReturnData AJAX_GetByIDCarrier(int idCarrier)
    {
        try
        {

            DataClassesDataContext db = new DataClassesDataContext();
            dynamic _shipping = db.TBShippings.Where(x => x.IDCarrier == idCarrier).Select(x => new { x.IDCarrier, x.IDDistrict, x.IDShipping, x.Price }).ToList();
            return ReturnData.MessageSuccess("OK", _shipping);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public ReturnData AJAX_Update_Price(int idShipping, decimal price)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBShipping _shipping = db.TBShippings.Where(x => x.IDShipping == idShipping && !x.Deflag).FirstOrDefault();
            if (_shipping == null)
                return ReturnData.MessageFailed("Data not found", null);
            else
            {
                _shipping.Price = price;
                db.SubmitChanges();
                return ReturnData.MessageSuccess("Shipping price updated", null);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public dynamic Dynamic_GetShipping_ByIDDistrict(int idDistrict)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBShippings.Where(x => !x.Deflag && x.IDDistrict == idDistrict).Select(x => new
            {
                x.TBCarrier.Information,
                Image = x.TBCarrier.Image,
                x.IDCarrier,
                x.IDShipping,
                x.IDDistrict,
                x.TBCarrier.Name,
                x.Price
            });
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public dynamic Dynamic_GetShipping_ByIDShipping(int idShipping)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBShippings.Where(x => !x.Deflag && x.IDShipping == idShipping).Select(x => new
            {
                x.TBCarrier.Information,
                x.IDCarrier,
                x.IDShipping,
                x.IDDistrict,
                x.TBCarrier.Name,
                x.Price,
                Image = x.TBCarrier.Image
            }).FirstOrDefault();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public ReturnData AJAX_SaveShippingToCart(int idShipping)
    {
        try
        {
            //var cartCookies = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()];
            Class_Order order = new Class_Order();
            var cartCookies = order.GetEncodedDataOrder(HttpContext.Current.Request.AnonymousID);
            if (cartCookies == null)
                return ReturnData.MessageFailed("Invalid token", null);

            DataClassesDataContext db = new DataClassesDataContext();
            Class_Shipping shipping = new Class_Shipping();

            var token = cartCookies.EncodedData;
            dynamic CartData = OurClass.DecryptToken(token);
            dynamic address = CartData["DeliveryAddress"];
            int idDistrict = address["IDDistrict"];

            dynamic shippingData = shipping.Dynamic_GetShipping_ByIDShipping(idShipping);
            dynamic resultShipping = new Dictionary<string, dynamic>();

            decimal totalPrice = 0;
            decimal price = 0;

            totalPrice = Class_Currency.GetPriceConversionCurrency(shippingData.Price * Math.Ceiling((decimal)CartData["TotalWeight"]));
            price = Class_Currency.GetPriceConversionCurrency(shippingData.Price);
            if (CartData["TotalWeight"] < 1)
            {
                totalPrice = Class_Currency.GetPriceConversionCurrency(shippingData.Price);
            }

            resultShipping["IDCarrier"] = shippingData.IDCarrier;
            resultShipping["IDShipping"] = shippingData.IDShipping;
            resultShipping["IDDistrict"] = shippingData.IDDistrict;
            resultShipping["Image"] = shippingData.Image;
            resultShipping["Name"] = shippingData.Name;
            resultShipping["Price"] = price;
            resultShipping["Information"] = shippingData.Information;
            resultShipping["TotalPrice"] = totalPrice;

            CartData["Shipping"] = resultShipping;
            token = OurClass.EncryptToken(CartData);
            //HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()].Value = token;
            order.SaveEncodeDataOrder(HttpContext.Current.Request.AnonymousID, token);
            return ReturnData.MessageSuccess("OK", CartData);

        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    #endregion
}