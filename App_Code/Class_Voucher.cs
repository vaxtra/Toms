using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using WITLibrary;

/// <summary>
/// Summary description for Class_Voucher
/// </summary>
public class Class_Voucher
{
    public Class_Voucher()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Datatable AJAX_GetTable(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            IEnumerable<dynamic> data = Dynamic_GetAll(new DataClassesDataContext());
            int count = data.Count();
            if (!string.IsNullOrEmpty(search))
                data = data.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToArray();
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (dynamic currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("IDVoucher", currData.IDVoucher);
                newData.Add("Name", currData.Name);
                newData.Add("Code", currData.Code);
                newData.Add("Description", currData.Description);
                newData.Add("VoucherType", currData.VoucherType);
                newData.Add("Value", currData.Value);
                newData.Add("TotalAvailable", currData.TotalAvailable);
                newData.Add("Used", currData.Used);
                newData.Add("MinimumAmount", currData.MinimumAmount);
                newData.Add("StartDate", currData.StartDate);
                newData.Add("EndDate", currData.EndDate);
                newData.Add("Active", currData.Active);
                resultList.Add(newData);
            }
            return OurClass.ParseTable(resultList, count, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
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
    public ReturnData AJAX_Insert(int? idCustomer, string name, string code, string description, string voucherType, decimal value, int totalAvailable, decimal minimumAmount, string startDate, string endDate, bool active)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            List<TBVoucher> vouchers = db.TBVouchers.Where(x => !x.Deflag).ToList();
            CultureInfo us = new CultureInfo("en-US");
            DateTime _startDate = DateTime.ParseExact(startDate, "yyyy MM dd HH:mm", us);
            DateTime _endDate = DateTime.ParseExact(endDate, "yyyy MM dd HH:mm", us);
            if (vouchers.Where(x => x.Code == code).FirstOrDefault() != null)
                return ReturnData.MessageFailed("Voucher with code '" + code + "' is already exists", null);
            if (_startDate > _endDate)
                return ReturnData.MessageFailed("Start date cannot less than end date", null);
            if (_endDate < DateTime.Now)
                return ReturnData.MessageFailed("Start date cannot past time", null);

            TBVoucher _newVoucher = new TBVoucher
            {
                IDCustomer = idCustomer,
                Name = name.Trim(),
                Code = code.Trim(),
                Description = description,
                VoucherType = voucherType.Trim(),
                Value = value,
                TotalAvailable = totalAvailable,
                Used = 0,
                MinimumAmount = minimumAmount,
                StartDate = _startDate,
                EndDate = _endDate,
                Deflag = false,
                Active = active,
                DateInsert = DateTime.Now,
                DateLastUpdate = DateTime.Now
            };
            db.TBVouchers.InsertOnSubmit(_newVoucher);
            db.SubmitChanges();
            return ReturnData.MessageSuccess("Voucher has been successfully inserted", null);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public ReturnData AJAX_Update(int idVoucher, int? idCustomer, string name, string code, string description, string voucherType, decimal value, int totalAvailable, decimal minimumAmount, string startDate, string endDate, bool active)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            List<TBVoucher> vouchers = db.TBVouchers.Where(x => !x.Deflag).ToList();
            TBVoucher _voucher = vouchers.Where(x => x.IDVoucher == idVoucher).FirstOrDefault();
            CultureInfo us = new CultureInfo("en-US");
            DateTime _startDate = DateTime.ParseExact(startDate, "yyyy MM dd HH:mm", us);
            DateTime _endDate = DateTime.ParseExact(endDate, "yyyy MM dd HH:mm", us);
            if (_voucher == null)
                return ReturnData.MessageFailed("Data not found", null);
            if (vouchers.Where(x => x.IDVoucher != idVoucher && x.Code == code).FirstOrDefault() != null)
                return ReturnData.MessageFailed("Voucher with code '" + code + "' is already exists", null);
            if (_startDate > _endDate)
                return ReturnData.MessageFailed("Start date cannot less than end date", null);
            if (_endDate < DateTime.Now)
                return ReturnData.MessageFailed("Start date cannot past time", null);

            _voucher.IDCustomer = idCustomer;
            _voucher.Name = name.Trim();
            _voucher.Code = code.Trim();
            _voucher.Description = description;
            _voucher.VoucherType = voucherType.Trim();
            _voucher.Value = value;
            _voucher.TotalAvailable = totalAvailable;
            _voucher.MinimumAmount = minimumAmount;
            _voucher.StartDate = _startDate;
            _voucher.EndDate = _endDate;
            _voucher.Active = active;
            _voucher.DateLastUpdate = DateTime.Now;
            db.SubmitChanges();
            return ReturnData.MessageSuccess("Voucher has been successfully updated", null);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public ReturnData AJAX_Delete(int idVoucher)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBVoucher voucher = db.TBVouchers.Where(x => !x.Deflag && x.IDVoucher == idVoucher).FirstOrDefault();
            if (voucher == null)
                return ReturnData.MessageFailed("Data not found", null);

            voucher.Deflag = true;
            db.SubmitChanges();

            return ReturnData.MessageSuccess("Voucher deleted successfully", null);

        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public ReturnData AJAX_SubmitVoucher(string code, string token, int amount)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            Class_Customer _cust = new Class_Customer();

            TBVoucher _voucher = db.TBVouchers.Where(x => x.Active && !x.Deflag && x.Code == code).FirstOrDefault();

            if (_voucher == null)
                return ReturnData.MessageFailed("This voucher is invalid", null);
            if (_voucher.IDCustomer != null)
            {
                if (_voucher.IDCustomer != null && (token == null || token == ""))
                    return ReturnData.MessageFailed("Please login to use this voucher", null);
                else if (token != "")
                {
                    var _temp = _cust.DecryptToken(token) as IDictionary<string, object>;
                    var Customer = _cust.DYNAMIC_GetData_ByEmailAndPassword(_temp["email"].ToString(), _temp["password"].ToString());
                    if (_voucher.IDCustomer != null && _voucher.IDCustomer.Value != Customer.IDCustomer)
                        return ReturnData.MessageFailed("You cannot use this voucher", null);
                }
            }

            if (_voucher.TotalAvailable != 0 && _voucher.TotalAvailable == _voucher.Used)
                return ReturnData.MessageFailed("This voucher only valid for first " + _voucher.TotalAvailable.ToString() + " use", null);
            if (amount < Class_Currency.GetPriceConversionCurrency(_voucher.MinimumAmount))
                return ReturnData.MessageFailed("You have not reached the minimum amount required to use this voucher", null);
            if (_voucher.EndDate < DateTime.Now)
                return ReturnData.MessageFailed("This voucher has expired", null);
            if (_voucher.StartDate > DateTime.Now)
                return ReturnData.MessageFailed("This voucher is not yet valid", null);

            _voucher.DateLastUpdate = DateTime.Now;
            db.SubmitChanges();
            dynamic result = Dynamic_GetDetail(_voucher.IDVoucher);
            string voucherToken = OurClass.EncryptToken((result));
            var cookie = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()];
            if (cookie == null)
                HttpContext.Current.Response.Cookies.Add(new HttpCookie(System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString(), voucherToken));
            else
                HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Value = voucherToken;
            HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Expires = DateTime.Now.AddMinutes(120);
            return ReturnData.MessageSuccess("OK", result);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public dynamic Dynamic_GetData_ByIDCustomer(int IDCustomer)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBVouchers.Where(x => !x.Deflag && x.IDCustomer == IDCustomer).Select(x => new
            {
                x.IDVoucher,
                x.Code,
                x.Description,
                x.EndDate,
                x.MinimumAmount,
                x.Name,
                x.StartDate,
                x.Value,
                x.VoucherType
            });
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public dynamic Dynamic_GetDetail(int idVoucher)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            dynamic result = db.TBVouchers.Where(x => !x.Deflag && x.IDVoucher == idVoucher).Select(x => new
            {
                x.IDCustomer,
                x.IDVoucher,
                x.Name,
                x.Code,
                x.Description,
                x.VoucherType,
                x.Value,
                x.TotalAvailable,
                x.Used,
                x.MinimumAmount,
                x.StartDate,
                x.EndDate,
                x.Active
            }).FirstOrDefault();
            if (result == null)
                return ReturnData.MessageFailed("Data not found", null);
            return result;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_FE_GetDetail(int idVoucher)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            dynamic result = db.TBVouchers.Where(x => !x.Deflag && x.IDVoucher == idVoucher).Select(x => new
            {
                x.IDCustomer,
                x.IDVoucher,
                x.Name,
                x.Code,
                x.Description,
                x.VoucherType,
                Value = Class_Currency.GetPriceConversionCurrency(x.Value),
                x.TotalAvailable,
                x.Used,
                MinimumAmount = Class_Currency.GetPriceConversionCurrency(x.MinimumAmount),
                x.StartDate,
                x.EndDate,
                x.Active
            }).FirstOrDefault();
            if (result == null)
                return ReturnData.MessageFailed("Data not found", null);
            return result;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_GetAll(DataClassesDataContext db)
    {
        try
        {
            return db.TBVouchers.Where(x => !x.Deflag).Select(x => new
            {
                x.IDVoucher,
                x.Name,
                x.Code,
                x.Description,
                x.VoucherType,
                x.Value,
                x.TotalAvailable,
                x.Used,
                x.MinimumAmount,
                x.StartDate,
                x.EndDate,
                x.Active
            });
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            throw;
        }
    }
}