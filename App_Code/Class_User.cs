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
public class Class_User
{
    public Class_User()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public ReturnData AJAX_BE_Login(string email, string password)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                var data = db.TBCustomers.Where(x => !x.Deflag && x.Email == email && x.Password == EncryptPassword(password)).FirstOrDefault();
                if (data == null)
                    return ReturnData.MessageFailed("Username/password is incorrect", null);
                else
                {
                    var token = EncryptToken(email, password);
                    HttpContext.Current.Response.Cookies.Add(new HttpCookie(System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString(), token));
                    HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()].Expires = DateTime.Now.AddMinutes(120);
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

    public ReturnData AJAX_FE_Logout(string token)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()].Value != token)
                    return ReturnData.MessageFailed("Invalid token!", null);
                else
                {
                    HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()].Expires = DateTime.Now.AddDays(-1);
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
            return "Invalid token!";
        }
    }
}