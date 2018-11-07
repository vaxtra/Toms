using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

/// <summary>
/// Summary description for Configuration
/// </summary>
public class Class_Configuration
{

    public dynamic Dynamic_Get_EmailConfiguration()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBConfigurations.Where(x => x.Name.Contains("email_")).Select(x => new
            {
                x.Name,
                Value = (x.Name.Contains("password")) ? OurClass.DecryptPassword(x.Value) : x.Value
            });
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_Get_ShopInformation()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBConfigurations.Where(x => x.Name.Contains("shop_")).Select(x => new
            {
                x.Name,
                Value = (x.Name.Contains("password")) ? OurClass.DecryptPassword(x.Value) : x.Value
            });
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_Get_EmailLogo()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBConfigurations.Where(x => x.Name.Contains("shop_email_logo")).Select(x => new
            {
                x.Name,
                x.Value
            }).FirstOrDefault().Value;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_Get_ShopEmail()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBConfigurations.Where(x => x.Name.Contains("shop_email")).Select(x => new
            {
                x.Name,
                x.Value
            }).FirstOrDefault().Value;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_Get_ShopTitle()
    {
        try
        {
            return System.Configuration.ConfigurationManager.AppSettings["Title"];
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_Get_AutoCancel()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBConfigurations.Where(x => x.Name.Contains("autocancel_")).Select(x => new
            {
                x.Name,
                x.Value
            });
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_Get_VeritransConfig()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBConfigurations.Where(x => x.Name.Contains("veritrans_")).Select(x => new
            {
                x.Name,
                Value = (x.Name.Contains("password")) ? OurClass.DecryptPassword(x.Value) : x.Value
            });
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public ReturnData AJAX_Update_Configuration(dynamic data)
    {
        try
        {
            //Dictionary<string,dynamic> n = new Dictionary<string,dynamic>();
            //n.leng
            DataClassesDataContext db = new DataClassesDataContext();
            //for (int i = 0; i < data.Count; i++)
            //{
            //    string name = data[0].Key;
            //}
            foreach (var item in data)
            {
                string name = item.Key;
                var row = db.TBConfigurations.Where(x => x.Name == name).FirstOrDefault();
                if (row != null)
                {
                    if (name.Contains("password"))
                        row.Value = OurClass.EncryptPassword(item.Value);
                    else
                        row.Value = item.Value;
                    row.DateLastUpdate = DateTime.Now;
                    db.SubmitChanges();
                }
            }

            return ReturnData.MessageSuccess("OK", null);

        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_Update_ShopInfo(string shop_email, string shop_city, string shop_address, string shop_phone, string shop_email_logo, string baseImage)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                db.TBConfigurations.Where(x => x.Name.Contains("shop_email")).FirstOrDefault().Value = shop_email;
                db.TBConfigurations.Where(x => x.Name.Contains("shop_city")).FirstOrDefault().Value = shop_city;
                db.TBConfigurations.Where(x => x.Name.Contains("shop_address")).FirstOrDefault().Value = shop_address;
                db.TBConfigurations.Where(x => x.Name.Contains("shop_phone")).FirstOrDefault().Value = shop_phone;
                

                if (baseImage != "" && shop_email_logo != "")
                {
                    var logo = db.TBConfigurations.Where(x => x.Name.Contains("shop_email_logo")).FirstOrDefault();
                    FileInfo fi = new FileInfo(HttpContext.Current.Server.MapPath("/assets/images/email_logo/" + logo.Value));
                    if (fi.Exists)
                        fi.Delete();
                    logo.Value = "email_logo" + WITLibrary.ConvertCustom.GetExtention(shop_email_logo);
                    System.Drawing.Image _image = WITLibrary.ConvertCustom.Base64ToImage(baseImage);
                    _image.Save(HttpContext.Current.Server.MapPath("/assets/images/email_logo/" + logo.Value));
                }

                db.SubmitChanges();

                return ReturnData.MessageSuccess("Shop information has been updated successfully.", null);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_Update_AutoCancel(dynamic data)
    {
        try
        {
            //Dictionary<string,dynamic> n = new Dictionary<string,dynamic>();
            //n.leng
            DataClassesDataContext db = new DataClassesDataContext();
            //for (int i = 0; i < data.Count; i++)
            //{
            //    string name = data[0].Key;
            //}
            foreach (var item in data)
            {
                string name = item.Key;
                var row = db.TBConfigurations.Where(x => x.Name == name).FirstOrDefault();
                if (row != null)
                {
                    row.Value = item.Value;
                    row.DateLastUpdate = DateTime.Now;
                    db.SubmitChanges();
                }
            }

            return ReturnData.MessageSuccess("OK", null);

        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_Update_VeritransConfiguration(dynamic data)
    {
        try
        {
            //Dictionary<string,dynamic> n = new Dictionary<string,dynamic>();
            //n.leng
            DataClassesDataContext db = new DataClassesDataContext();
            //for (int i = 0; i < data.Count; i++)
            //{
            //    string name = data[0].Key;
            //}
            foreach (var item in data)
            {
                string name = item.Key;
                var row = db.TBConfigurations.Where(x => x.Name == name).FirstOrDefault();
                if (row != null)
                {
                    if (name.Contains("password"))
                        row.Value = OurClass.EncryptPassword(item.Value);
                    else
                        row.Value = item.Value;
                    row.DateLastUpdate = DateTime.Now;
                    db.SubmitChanges();
                }
            }

            return ReturnData.MessageSuccess("OK", null);

        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_AutoCancel()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            double autocancel = double.Parse(Dynamic_GetValue_AutoCancel());
            var dataOrder = db.TBOrders.Where(x => !x.Deflag && x.TBOrder_Status.Paid == false && x.TBOrder_Status.Logable == false && x.TBOrder_Status.Delivery == false && x.TBOrder_Status.Shipped == false && x.TBOrder_Status.Restock == false && DateTime.Now >= x.DateInsert.AddHours(autocancel));
            Class_Configuration _config = new Class_Configuration();
            var emailLogo = _config.Dynamic_Get_EmailLogo();
            if (dataOrder == null || dataOrder.Count() == 0)
            {
                return ReturnData.MessageFailed("Data not found", null);
            }
            else
            {
                foreach (var item in dataOrder)
                {
                    if (item.IDVoucher != null)
                    {
                        item.TBVoucher.Used -= 1;
                    }

                    var ordDetail = db.TBOrder_Details.Where(x => x.IDOrder == item.IDOrder);
                    foreach (var detail in ordDetail)
                    {
                        detail.TBProduct_Combination.Quantity += detail.Quantity;
                    }
                    item.IDOrderStatus = db.TBOrder_Status.Where(x => !x.Deflag && x.Paid == false && x.Logable == true && x.Delivery == false && x.Shipped == false && x.Restock == true).FirstOrDefault().IDOrderStatus;
                    item.DateLastUpdate = DateTime.Now;

                    //INSERT LOG ORDER
                    Class_Log_Order logOrder = new Class_Log_Order();
                    logOrder.Insert((int?)null, item.IDOrder, item.Reference, item.TBOrder_Status.Name, "Autocancel By System ", item.TBCustomer.FirstName + ' ' + item.TBCustomer.LastName);

                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/template-email-canceled.html")))
                    {
                        string body = "";
                        body = reader.ReadToEnd();
                        body = body.Replace("{name}", item.TBCustomer.FirstName);
                        body = body.Replace("{amount}", item.TotalPaid.ToString());
                        body = body.Replace("{owner}", item.TBPayment_Method.Owner);
                        body = body.Replace("{account}", item.TBPayment_Method.AccountNumber);
                        body = body.Replace("{bank}", item.TBPayment_Method.Bank);
                        if (item.InvoiceNumber == null)
                        {
                            body = body.Replace("{nomorInvoice}", item.Reference);
                        }
                        else
                        {
                            body = body.Replace("{nomorInvoice}", item.InvoiceNumber);
                        }
                        body = body.Replace("{title}", System.Configuration.ConfigurationManager.AppSettings["Title"]);
                        body = body.Replace("{email_logo}", System.Configuration.ConfigurationManager.AppSettings["url"] + "/assets/images/email_logo/" + emailLogo);
                        body = body.Replace("{shop_url}", System.Configuration.ConfigurationManager.AppSettings["url"]);
                        OurClass.SendEmail(item.TBCustomer.Email, System.Web.Configuration.WebConfigurationManager.AppSettings["Title"] + " - Order Cancelled", body, "");
                    }
                }
                db.SubmitChanges();
                return ReturnData.MessageSuccess("Some Order cancelled automatically", null);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public string Dynamic_GetValue_AutoCancel()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var autocancel = db.TBConfigurations.Where(x => x.Name.Contains("autocancel_")).AsEnumerable().Select(x => new
            {
                x.Name,
                x.Value
            }).FirstOrDefault();

            return autocancel.Value;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public string Dynamic_GetValue_SystemStatus()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var system = db.TBConfigurations.Where(x => x.Name.Contains("system_")).AsEnumerable().Select(x => new
            {
                x.Name,
                x.Value
            }).FirstOrDefault();

            return system.Value;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public ReturnData AJAX_LockSystem()
    {
        try
        {
            //Dictionary<string,dynamic> n = new Dictionary<string,dynamic>();
            //n.leng
            DataClassesDataContext db = new DataClassesDataContext();
            //for (int i = 0; i < data.Count; i++)
            //{
            //    string name = data[0].Key;
            //}
            var locksystem = db.TBConfigurations.Where(x => x.Name.ToLower() == "system_lock").FirstOrDefault();

            if (locksystem.Value == "false")
            {
                locksystem.Value = "true";
                db.SubmitChanges();
                return ReturnData.MessageSuccess("THIS WIT. COMMERCE SYSTEM HAS BEEN LOCKED DOWN", null);
            }
            else
            {
                locksystem.Value = "false";
                db.SubmitChanges();
                return ReturnData.MessageSuccess("THIS WIT. COMMERCE SYSTEM HAS BEEN UNLOCKED", null);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
}