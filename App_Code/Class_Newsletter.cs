using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.Services;
using JWT;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using WITLibrary;
using System.Web.Configuration;

/// <summary>
/// Summary description for Class_Newsletter
/// </summary>
public class Class_Newsletter
{
    public Class_Newsletter()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Datatable AJAX_GetTable_Newsletter(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            IEnumerable<dynamic> data = Dynamic_GetAll_Newsletter();
            int count = data.Count();
            if (!string.IsNullOrEmpty(search))
                data = data.Where(x =>
                    x.FirstName.ToLower().Contains(search.ToLower()) ||
            x.LastName.ToLower().Contains(search.ToLower()) ||
                    x.Email.ToLower().Contains(search.ToLower())
                    ).ToArray();
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (dynamic currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("IDNewsletter", currData.IDNewsletter);
                newData.Add("Email", currData.Email);
                newData.Add("DateInsert", currData.DateInsert.ToString("dd-MM-yyyy") + " " + currData.DateInsert.ToLongTimeString());
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

    public ReturnData AJAX_Insert(string email)
    {
        try
        {
            Class_Customer _customer = new Class_Customer();
            DataClassesDataContext db = new DataClassesDataContext();
            Class_Configuration _config = new Class_Configuration();
            var customer = _customer.Dynamic_GetData_ByEmail(email);
            var subscriber = Dynamic_GetData_ByEmail(email);
            if (customer != null)
            {
                if (customer.IsSubscribe == true)
                {
                    return ReturnData.MessageFailed("Your email has been registered and signed up for newsletter", null);
                }
                else
                {
                    var UpdateCustomer = db.TBCustomers.Where(x => x.Email.ToLower() == email.ToLower() && !x.Deflag && x.Active).FirstOrDefault();
                    UpdateCustomer.IsSubscribe = true;
                    db.SubmitChanges();
                    return ReturnData.MessageSuccess("Your email has been registered as member, You're succesfully signed up for newsletter", null);
                }
            }
            else if (subscriber != null)
            {
                return ReturnData.MessageFailed("Your email has successfully subscribed", null);
            }
            else
            {
                TBNewsletter _newNewsletter = new TBNewsletter
                {
                    Email = email,
                    Deflag = false,
                    DateInsert = DateTime.Now,
                    DateLastUpdate = DateTime.Now
                };
                db.TBNewsletters.InsertOnSubmit(_newNewsletter);
                db.SubmitChanges();

                var emailLogo = _config.Dynamic_Get_EmailLogo();

                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/template-email-newsletter.html")))
                {
                    string body = "";
                    body = reader.ReadToEnd();
                    body = body.Replace("{name}", email);
                    body = body.Replace("{voucher}", "newcompaniion");
                    body = body.Replace("{title}", System.Configuration.ConfigurationManager.AppSettings["Title"]);
                    body = body.Replace("{email_logo}", System.Configuration.ConfigurationManager.AppSettings["url"] + "/assets/images/email_logo/" + emailLogo);
                    body = body.Replace("{shop_url}", System.Configuration.ConfigurationManager.AppSettings["url"]);
                    OurClass.SendEmail(email, System.Web.Configuration.WebConfigurationManager.AppSettings["Title"] + " - Subscriber", body, "");
                }

                return ReturnData.MessageSuccess("Thank You For Subcribed on our website, your email address has been registered", null);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_Delete(int idNewsletter)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBNewsletter selectedData = db.TBNewsletters.Where(x => x.IDNewsletter == idNewsletter && x.Deflag == false).FirstOrDefault();
            if (selectedData == null)
                return ReturnData.MessageFailed("Data not found", null);

            selectedData.Deflag = true;
            db.SubmitChanges();
            return ReturnData.MessageSuccess("Email address has been deleted successfully", null);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public dynamic Dynamic_GetData_ByEmail(string email)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBNewsletters.Where(x => x.Deflag == false && x.Email == email).Select(x => new
            {
                x.IDNewsletter,
                x.Email,
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

    public dynamic Dynamic_GetAll_Newsletter()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBNewsletters.Where(x => x.Deflag == false).Select(x => new
            {
                x.IDNewsletter,
                x.Email,
                x.Deflag,
                x.DateInsert,
                x.DateLastUpdate
            });
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public IEnumerable<TBNewsletter> GetAll_Newsletter()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return Func_GetAll_Newsletter(db);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    private Func<DataClassesDataContext, IEnumerable<TBNewsletter>> Func_GetAll_Newsletter
    {
        get
        {
            Func<DataClassesDataContext, IEnumerable<TBNewsletter>> func =
              CompiledQuery.Compile<DataClassesDataContext, IEnumerable<TBNewsletter>>
              ((DataClassesDataContext context) => context.TBNewsletters.AsEnumerable()
                .Where(x => x.Deflag == false).ToArray());
            return func;
        }
    }
}