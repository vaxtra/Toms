using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using JWT;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using WITLibrary;
using System.Web.Configuration;

/// <summary>
/// Summary description for Class_User
/// </summary>
public class Class_Employee
{
    public Class_Employee()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Datatable AJAX_GetTable(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            IEnumerable<dynamic> data = Dynamic_GetAll();
            int count = data.Count();
            if (!string.IsNullOrEmpty(search))
                data = data.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToArray();
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (dynamic currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("IDEmployee", currData.IDEmployee);
                newData.Add("IDRole", currData.IDRole);
                newData.Add("RoleName", currData.RoleName);
                newData.Add("Name", currData.Name);
                newData.Add("Email", currData.Email);
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

    public dynamic Dynamic_GetDetail(DataClassesDataContext db, int idEmployee)
    {
        try
        {
            return db.TBEmployees.Where(x => x.IDEmployee == idEmployee && !x.Deflag).Select(x => new
            {
                x.Name,
                x.IDEmployee,
                x.IDRole,
                x.Email
            }).FirstOrDefault();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public ReturnData AJAX_GetDetail(int idEmployee)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            Class_Role role = new Class_Role();
            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
            data.Add("Employee", Dynamic_GetDetail(db, idEmployee));
            data.Add("Role", role.Dynamic_GetAll());
            return ReturnData.MessageSuccess("OK", data);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_BE_Login(string email, string password, bool remember)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                var data = db.TBEmployees.Where(x => !x.Deflag && x.Email == email && x.Password == EncryptPassword(password)).FirstOrDefault();
                if (data == null)
                    return ReturnData.MessageFailed("Username/password is incorrect", null);
                if (!data.Active)
                    return ReturnData.MessageFailed("Login failed, user is not active", null);
                else
                {
                    var token = EncryptToken(email, password);
                    HttpContext.Current.Response.Cookies.Add(new HttpCookie(System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString(), token));
                    HttpContext.Current.Response.Cookies.Set(new HttpCookie(System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString(), token));
                    if (remember)
                        HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()].Expires = DateTime.Now.AddDays(30);
                    else
                        HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()].Expires = DateTime.Now.AddMinutes(120);
                    return ReturnData.MessageSuccess("OK", token);
                }
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_FE_Logout()
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                //if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()] == null || DecryptToken(HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()].Value) == null)
                //    return ReturnData.MessageFailed("Invalid token!", null);
                //else
                {
                    HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()].Expires = DateTime.Now.AddDays(-1);
                    return ReturnData.MessageSuccess("OK", null);
                }
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public bool IsExists(DataClassesDataContext db, string email)
    {
        if (db.TBEmployees.Where(x => !x.Deflag && x.Email == email).FirstOrDefault() == null)
            return false;
        else
            return true;
    }

    public ReturnData AJAX_BE_Insert(int idRole, string name, string email, string password)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            if (IsExists(db, email))
                return ReturnData.MessageFailed("Email is already registered", null);
            TBEmployee emp = new TBEmployee();
            emp.IDRole = idRole;
            emp.Name = name;
            emp.Email = email;
            emp.Active = true;
            emp.Password = EncryptPassword(password);
            emp.DateInsert = DateTime.Now;
            emp.DateLastUpdate = DateTime.Now;
            db.TBEmployees.InsertOnSubmit(emp);
            db.SubmitChanges();

            return ReturnData.MessageSuccess("New employee was added", null);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_BE_Updates(int idEmployee, int idrole, string name, string email, string password)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBEmployee emp = db.TBEmployees.Where(x => !x.Deflag && x.IDEmployee == idEmployee).FirstOrDefault();
            if (emp == null)
                return ReturnData.MessageFailed("Employee not found", null);
            emp.IDRole = idrole;
            emp.Name = name;
            if (emp.Email != email)
            {
                if (IsExists(db, email))
                    return ReturnData.MessageFailed("Email is already registered", null);
            }
            emp.Email = email;
            if (password != "")
                emp.Password = EncryptPassword(password);
            emp.DateLastUpdate = DateTime.Now;
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

    public ReturnData AJAX_BE_Delete(int idEmployee)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBEmployee emp = db.TBEmployees.Where(x => !x.Deflag && x.IDEmployee == idEmployee).FirstOrDefault();
            if (emp == null)
                return ReturnData.MessageFailed("Employee not found", null);
            emp.Deflag = true;
            emp.DateLastUpdate = DateTime.Now;
            db.SubmitChanges();

            return ReturnData.MessageSuccess("Data deleted successfully", null);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_BE_StatusToggle(int idEmployee)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBEmployee emp = db.TBEmployees.Where(x => !x.Deflag && x.IDEmployee == idEmployee).FirstOrDefault();
            if (emp == null)
                return ReturnData.MessageFailed("Employee not found", null);
            emp.Active = !emp.Active;
            emp.DateLastUpdate = DateTime.Now;
            db.SubmitChanges();

            return ReturnData.MessageSuccess("Status changed successfully", null);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_BE_ChangeStatus(int idEmployee, bool status)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBEmployee emp = db.TBEmployees.Where(x => !x.Deflag && x.IDEmployee == idEmployee).FirstOrDefault();
            if (emp == null)
                return ReturnData.MessageFailed("Employee not found", null);
            emp.Active = status;
            emp.DateLastUpdate = DateTime.Now;
            db.SubmitChanges();

            return ReturnData.MessageSuccess("Status changed successfully", null);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public dynamic Dynamic_GetAll()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBEmployees.Where(x => !x.Deflag).Select(x => new
            {
                x.IDRole,
                RoleName = x.TBRole.Name,
                x.IDEmployee,
                x.Name,
                x.Email,
                x.Password,
                x.Active
            });
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    private string EncryptPassword(string clearText)
    {
        string EncryptionKey = System.Configuration.ConfigurationManager.AppSettings["key"].ToString();
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }

    private string DecryptPassword(string cipherText)
    {
        string EncryptionKey = System.Configuration.ConfigurationManager.AppSettings["key"].ToString();
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }

    public dynamic EncryptToken(string email, string password)
    {
        var data = new Dictionary<string, object>() {
            {"email",email},
            {"password",password}
        };
        var key = System.Configuration.ConfigurationManager.AppSettings["key"].ToString();
        return JWT.JsonWebToken.Encode(data, key, JwtHashAlgorithm.HS256);

    }

    public object DecryptToken(string token)
    {
        try
        {
            var key = System.Configuration.ConfigurationManager.AppSettings["key"].ToString();
            var result = JWT.JsonWebToken.DecodeToObject(token, key) as IDictionary<string, object>;
            return result;
        }
        catch (JWT.SignatureVerificationException)
        {
            return null;
        }
    }

    public TBEmployee GetData_By_Token(string token)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var key = System.Configuration.ConfigurationManager.AppSettings["key"].ToString();
            var result = JWT.JsonWebToken.DecodeToObject(token, key) as IDictionary<string, object>;
            return db.TBEmployees.Where(x => !x.Deflag && x.Email == result["email"] && x.Password == EncryptPassword(result["password"].ToString())).FirstOrDefault();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_GetData_By_Token(string token)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var key = System.Configuration.ConfigurationManager.AppSettings["key"].ToString();
            var result = JWT.JsonWebToken.DecodeToObject(token, key) as IDictionary<string, object>;
            var emp = db.TBEmployees.Where(x => !x.Deflag && x.Email == result["email"] && x.Password == EncryptPassword(result["password"].ToString())).Select(x => new
            {
                x.IDRole,
                x.Email,
                x.Name,
                Role = x.TBRole.Name
            }).FirstOrDefault();
            return emp;
        }
        catch (JWT.SignatureVerificationException)
        {
            return null;
        }
    }

    public ReturnData AJAX_BE_Update_MyProfile_With_Token(string token, string name, string email, string password)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();

            var key = System.Configuration.ConfigurationManager.AppSettings["key"].ToString();
            var result = JWT.JsonWebToken.DecodeToObject(token, key) as IDictionary<string, object>;
            TBEmployee emp = db.TBEmployees.Where(x => !x.Deflag && x.Email == result["email"] && x.Password == EncryptPassword(result["password"].ToString())).FirstOrDefault();
            emp.Name = name;
            if (emp.Email != email)
            {
                if (IsExists(db, email))
                    return ReturnData.MessageFailed("Email is already registered", null);
            }
            emp.Email = email;
            if (password != "")
                emp.Password = EncryptPassword(password);
            emp.DateLastUpdate = DateTime.Now;
            db.SubmitChanges();
            var _newToken = EncryptToken(email, password);
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString(), _newToken));
            return ReturnData.MessageSuccess("OK", null);
        }
        catch (JWT.SignatureVerificationException)
        {
            return ReturnData.MessageFailed("token is invalid", null);
        }
    }

    public dynamic Init_Admin()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            if (IsExists(db, "admin@wit.co.id"))
                return ReturnData.MessageFailed("Email is already registered", null);
            TBEmployee emp = new TBEmployee();
            emp.IDRole = 1;
            emp.Name = "Admin WIT";
            emp.Email = "admin@wit.co.id";
            emp.Active = true;
            emp.Password = EncryptPassword("asdasd");
            emp.DateInsert = DateTime.Now;
            emp.DateLastUpdate = DateTime.Now;
            db.TBEmployees.InsertOnSubmit(emp);
            db.SubmitChanges();

            return ReturnData.MessageSuccess("New employee was added", null);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    private bool IsExistsEmail(string email)
    {
        DataClassesDataContext db = new DataClassesDataContext();
        if (db.TBEmployees.Where(x => !x.Deflag && x.Email == email).FirstOrDefault() != null)
            return true;
        else
            return false;
    }

    public ReturnData AJAX_Forget_Password(string email)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            if (IsExistsEmail(email))
            {
                var emp = db.TBEmployees.Where(x => !x.Deflag && x.Email == email).FirstOrDefault();
                if (emp != null)
                {
                    string text = email + "-" + emp.Name.Substring(0, 2);
                    string token = this.EncryptPassword(text);
                    int aa = token.Length;
                    string pass = EncryptToken(email, DateTime.Now.ToString("MMddHHmmss"));
                    aa = pass.Length;

                    emp.Password = pass;
                    db.SubmitChanges();
                    //SEND EMAIL
                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/template-email-forgot-password.html")))
                    {
                        Class_Configuration _config = new Class_Configuration();
                        var emailLogo = _config.Dynamic_Get_EmailLogo();
                        string body = "";
                        body = reader.ReadToEnd();
                        body = body.Replace("{name}", emp.Name);
                        body = body.Replace("{Url}", WebConfigurationManager.AppSettings["url"].ToString() + "/adminwitcommerce/reset-password.aspx?token=" + emp.Password);
                        body = body.Replace("{title}", System.Configuration.ConfigurationManager.AppSettings["Title"]);
                        body = body.Replace("{email_logo}", System.Configuration.ConfigurationManager.AppSettings["url"] + "/assets/images/email_logo/" + emailLogo);
                        body = body.Replace("{shop_url}", System.Configuration.ConfigurationManager.AppSettings["url"]);
                        OurClass.SendEmail(emp.Email, WebConfigurationManager.AppSettings["Title"] + " Reset Password", body, "");
                    }
                    return ReturnData.MessageSuccess("Reset password link has been sent to your email, please check your inbox or spam folder.", null);
                }
                return ReturnData.MessageSuccess("Employee Not Found", null);
            }
            else
                return ReturnData.MessageSuccess("Your email has not registered as Employee", null);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_Reset_Password(string token, string newPassword)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var emp = db.TBEmployees.Where(x => !x.Deflag && x.Password == token).FirstOrDefault();
            if (emp == null)
                return ReturnData.MessageFailed("Employee not found", null);

            emp.Password = EncryptPassword(newPassword);
            db.SubmitChanges();
            return ReturnData.MessageSuccess("Password reset successfully", null);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
}