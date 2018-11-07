using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using WITLibrary;


/// <summary>
/// Summary description for Class_Currency
/// </summary>
public class Class_Currency
{
    public Class_Currency()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Datatable AJAX_GetTable(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                IEnumerable<dynamic> data = Dynamic_GetAll(db);
                int count = data.Count();
                if (!string.IsNullOrEmpty(search))
                    data = data.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToArray();
                List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
                foreach (dynamic currData in data)
                {
                    Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                    newData.Add("IDCurrency", currData.IDCurrency);
                    newData.Add("Name", currData.Name);
                    newData.Add("ConversionRate", currData.ConversionRate);
                    newData.Add("ISOCode", currData.ISOCode);
                    newData.Add("Symbol", currData.Sign);
                    newData.Add("DateInsert", currData.DateInsert);
                    newData.Add("DateLastUpdate", currData.DateLastUpdate);
                    newData.Add("Active", currData.Active);
                    resultList.Add(newData);
                }
                return OurClass.ParseTable(resultList, count, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return new WITLibrary.Datatable
            {
                sEcho = 0,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0,
                aaData = new List<Dictionary<string, dynamic>>()
            };
        }
    }

    public IEnumerable<dynamic> Dynamic_GetAll(DataClassesDataContext db)
    {
        try
        {
            return db.TBCurrencies.Where(x => !x.Deflag).AsEnumerable().Select(x => new
            {
                x.IDCurrency,
                x.Name,
                x.ISOCode,
                x.ISOCodeNumeric,
                x.Sign,
                x.blank,
                x.Format,
                x.Decimal,
                x.ConversionRate,
                x.IsDefault,
                x.Active,
                x.Deflag,
                x.DateInsert,
                x.DateLastUpdate
            }).ToArray();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public ReturnData AJAX_BE_Updates(int idCurrency, string name, string isoCode, string isoCodeNumeric, string sign, decimal conversionRate)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBCurrency currency = db.TBCurrencies.Where(x => !x.Deflag && x.IDCurrency == idCurrency).FirstOrDefault();
            if (currency == null)
                return ReturnData.MessageFailed("Page not found", null);
            currency.Name = name;
            currency.ISOCode = isoCode;
            currency.ISOCodeNumeric = isoCodeNumeric;
            currency.Sign = sign;
            currency.ConversionRate = conversionRate;
            currency.DateLastUpdate = DateTime.Now;
            db.SubmitChanges();

            return ReturnData.MessageSuccess("Data updated successfully", null);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public dynamic Dynamic_GetDetail(int idCurrency)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBCurrencies.Where(x => x.IDCurrency == idCurrency).AsEnumerable().Select(x => new
            {
                x.IDCurrency,
                x.Name,
                x.ISOCode,
                x.ISOCodeNumeric,
                x.Sign,
                x.blank,
                x.Format,
                x.Decimal,
                x.ConversionRate,
                x.IsDefault,
                x.Active,
                x.Deflag,
                x.DateInsert,
                x.DateLastUpdate
            }).FirstOrDefault();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public object GetAdditionalCurrencies(DataClassesDataContext db)
    {
        return db.TBCurrencies.Where(x => !x.Deflag && x.Active == true && x.IsDefault == false).Select(x => new { x.Name, x.IDCurrency, x.ISOCode, x.ISOCodeNumeric, x.ConversionRate });
    }

    public object GetAllCurrencies(DataClassesDataContext db)
    {
        int selectedID = 0;
        if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCurrency"].ToString()] != null)
        {
            string id = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCurrency"].ToString()].Value;
            int.TryParse(id, out selectedID);
        }
        else
            selectedID = 1;
        return db.TBCurrencies.Where(x => !x.Deflag && x.Active == true).Select(x => new
        {
            x.Name,
            x.IDCurrency,
            x.ISOCode,
            x.ISOCodeNumeric,
            x.ConversionRate,
            x.Format,
            Selected = x.IDCurrency == selectedID ? true : false
        });
    }

    public static int GetActiveCurrencyID()
    {
        int activeCurrency = 0;
        if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCurrency"].ToString()] != null)
        {
            string id = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCurrency"].ToString()].Value;
            int.TryParse(id, out activeCurrency);
        }
        else
            activeCurrency = 1;

        return activeCurrency;
    }

    public static decimal GetPriceConversionCurrency(decimal price)
    {
        int activeCurrency = 0;
        if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCurrency"].ToString()] != null)
        {
            string id = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCurrency"].ToString()].Value;
            int.TryParse(id, out activeCurrency);
        }
        else
            activeCurrency = 1;

        DataClassesDataContext db = new DataClassesDataContext();
        var curr = db.TBCurrencies.Where(x => x.IDCurrency == activeCurrency).FirstOrDefault();
        return price / curr.ConversionRate;
    }

    public static decimal GetPriceConversionCurrency(decimal price, int idCurrency)
    {
        int activeCurrency = idCurrency;
        int fromCurrency = 0;
        int currencyCart = CheckCurrencyCart();
        if (currencyCart == 0)
        {
            string id = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCurrency"].ToString()].Value;
            int.TryParse(id, out fromCurrency);
        }
        else
        {
            fromCurrency = currencyCart;
        }
        

        DataClassesDataContext db = new DataClassesDataContext();
        var curr = db.TBCurrencies.Where(x => x.IDCurrency == activeCurrency).FirstOrDefault();
        var fromCurr = db.TBCurrencies.Where(x => x.IDCurrency == fromCurrency).FirstOrDefault();
        if (curr != null)
        {
            if (fromCurrency == 1)
                return price / curr.ConversionRate;
            else
                return price * fromCurr.ConversionRate;
        }
        else
            return 0;
    }

    public static decimal GetPriceDeconversionCurrency(decimal price)
    {
        int activeCurrency = 0;
        if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCurrency"].ToString()] != null)
        {
            string id = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCurrency"].ToString()].Value;
            int.TryParse(id, out activeCurrency);
        }
        else
            activeCurrency = 1;

        DataClassesDataContext db = new DataClassesDataContext();
        var curr = db.TBCurrencies.Where(x => x.IDCurrency == activeCurrency).FirstOrDefault();
        return price * curr.ConversionRate;
    }

    public static int CheckCurrencyCart()
    {
        Class_Order _order = new Class_Order();
        string anonID = HttpContext.Current.Request.AnonymousID;
        TBOrder_temp _cookie = _order.GetEncodedDataOrder(anonID);
        string _token = "";
        if (_cookie == null)
        {
            return 0;
        }
        else
        {
            _token = _cookie.EncodedData;
            dynamic tmp = (OurClass.DecryptToken(_token) as IDictionary<string, dynamic>);
            if ((tmp as Dictionary<string, dynamic>).ContainsKey("IDCurrency"))
            {
                return tmp["IDCurrency"];
            }
            else
            {
                return 0;
            }
        }
    }
}