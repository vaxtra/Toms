using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WITLibrary;

/// <summary>
/// Summary description for Class_PaymentMethod
/// </summary>
public class Class_PaymentMethod
{
    public Class_PaymentMethod()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region BACKEND
    public ReturnData AJAX_Insert(string name, string bank, string owner, string accountNumber, string description, string typePayment, string baseImage, string fnImage)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBPayment_Method paymentMethod = new TBPayment_Method();
            paymentMethod.Name = name;
            paymentMethod.Bank = bank;
            paymentMethod.Owner = owner;
            paymentMethod.AccountNumber = accountNumber;
            paymentMethod.Description = description;
            paymentMethod.Type = typePayment;
            paymentMethod.Image = WITLibrary.ConvertCustom.GetExtention(fnImage);
            paymentMethod.DateInsert = DateTime.Now;
            paymentMethod.DateLastUpdate = DateTime.Now;
            paymentMethod.Deflag = false;
            db.TBPayment_Methods.InsertOnSubmit(paymentMethod);
            db.SubmitChanges();

            if (paymentMethod != null)
            {
                if (baseImage != "" && fnImage != "")
                {
                    System.Drawing.Image _image = WITLibrary.ConvertCustom.Base64ToImage(baseImage);
                    _image.Save(HttpContext.Current.Server.MapPath("/assets/images/Payment_Method/" + paymentMethod.IDPaymentMethod + paymentMethod.Image));
                    paymentMethod.Image = paymentMethod.IDPaymentMethod + paymentMethod.Image;
                    db.SubmitChanges();
                }
                if (paymentMethod != null)
                    return ReturnData.MessageSuccess(name + " has been successfully inserted.", null);
                return ReturnData.MessageFailed(name + " failed to insert.", null);
            }
            return ReturnData.MessageFailed(name + " failed to insert.", null);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public ReturnData AJAX_Update(int idPaymentMethod, string name, string bank, string owner, string accountNumber, string description, string typePayment, string baseImage, string fnImage)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBPayment_Method paymentMethod = db.TBPayment_Methods.Where(x => x.IDPaymentMethod == idPaymentMethod && !x.Deflag).FirstOrDefault();
            if (paymentMethod == null)
                return ReturnData.MessageFailed("Data not found", null);

            paymentMethod.Name = name;
            paymentMethod.Bank = bank;
            paymentMethod.Owner = owner;
            paymentMethod.AccountNumber = accountNumber;
            paymentMethod.Description = description;
            paymentMethod.Type = typePayment;
            if (baseImage != "" && fnImage != "")
                paymentMethod.Image = WITLibrary.ConvertCustom.GetExtention(fnImage);
            paymentMethod.DateLastUpdate = DateTime.Now;

            if (baseImage != "" && fnImage != "")
            {
                System.Drawing.Image _image = WITLibrary.ConvertCustom.Base64ToImage(baseImage);
                _image.Save(HttpContext.Current.Server.MapPath("/assets/images/Payment_Method/" + paymentMethod.IDPaymentMethod + paymentMethod.Image));
                paymentMethod.Image = paymentMethod.IDPaymentMethod + paymentMethod.Image;
                //db.SubmitChanges();
            }

            db.SubmitChanges();

            if (paymentMethod != null)
                return ReturnData.MessageSuccess(name + " has been successfully updated.", null);
            return ReturnData.MessageFailed(name + " failed to insert.", null);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public ReturnData AJAX_Delete(int id)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBPayment_Method payment = db.TBPayment_Methods.Where(x => x.IDPaymentMethod == id && !x.Deflag).FirstOrDefault();
            if (payment == null)
                return ReturnData.MessageFailed("Data not found", null);
            payment.Deflag = true;
            payment.DateLastUpdate = DateTime.Now;
            db.SubmitChanges();

            return ReturnData.MessageSuccess(payment.Name + " updated successfully", null);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    #endregion

    public Datatable AJAX_GetTable(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            IEnumerable<dynamic> data = db.TBPayment_Methods.Where(X => !X.Deflag).ToList();
            int count = data.Count();
            if (!string.IsNullOrEmpty(search))
                data = data.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToArray();
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (dynamic currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("IDPaymentMethod", currData.IDPaymentMethod);
                newData.Add("Name", currData.Name);
                newData.Add("Bank", currData.Bank);
                newData.Add("Owner", currData.Owner);
                newData.Add("AccountNumber", currData.AccountNumber);
                newData.Add("Description", currData.Description);
                newData.Add("Image", currData.Image);
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

    public dynamic Dynamic_GetAll()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBPayment_Methods.Where(x => !x.Deflag).Select(x => new
                {
                    x.IDPaymentMethod,
                    x.Name,
                    x.Bank,
                    x.Owner,
                    x.AccountNumber,
                    x.Description,
                    x.Image,
                    x.Type
                });
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_GetDetail_ByIDPayment_Method(int idPaymentMethod)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBPayment_Methods.Where(x => !x.Deflag && x.IDPaymentMethod == idPaymentMethod).Select(x => new
            {
                x.IDPaymentMethod,
                x.Name,
                x.Bank,
                x.Owner,
                x.AccountNumber,
                x.Description,
                x.Image,
                x.Type
            }).FirstOrDefault();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public ReturnData AJAX_Insert_ToCart(int idPaymentMethod)
    {
        try
        {
            Class_Order order = new Class_Order();
            //var cartCookies = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()];
            var cartCookies = order.GetEncodedDataOrder(HttpContext.Current.Request.AnonymousID);
            if (cartCookies == null)
                return ReturnData.MessageFailed("Invalid token", null);

            DataClassesDataContext db = new DataClassesDataContext();
            Class_Shipping shipping = new Class_Shipping();

            var token = cartCookies.EncodedData;
            dynamic CartData = OurClass.DecryptToken(token);
            dynamic address = CartData["DeliveryAddress"];
            int idDistrict = address["IDDistrict"];

            dynamic paymentData = Dynamic_GetDetail_ByIDPayment_Method(idPaymentMethod);

            CartData["PaymentMethod"] = paymentData;
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
}