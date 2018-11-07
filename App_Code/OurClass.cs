using JWT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Summary description for OurClass
/// </summary>
public static class OurClass
{
    public static void DeleteFile(string _image, string folderName)
    {
        try
        {
            if (_image != "")
            {
                System.IO.FileInfo _fileInfo = new System.IO.FileInfo(HttpContext.Current.Server.MapPath("~/assets/images/" + folderName + "/" + _image));
                if (_fileInfo.Exists)
                    _fileInfo.Delete();
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            throw;
        }
    }
    public static bool ImageExists(string _image, string folderName)
    {
        try
        {
            if (_image != "")
            {
                System.IO.FileInfo _fileInfo = new System.IO.FileInfo(HttpContext.Current.Server.MapPath("~/assets/images/" + folderName + "/" + _image));
                if (_fileInfo.Exists)
                    return true;
            }
            return false;

        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return false;
        }
    }
    public static string ShortDescription(string isiKonten, int maxLength)
    {
        try
        {

            if (isiKonten == null) return string.Empty;

            int strLength = 0;
            string fixedString = "";

            // Remove HTML tags and newline characters from the text, and decode HTML encoded characters. 
            // This is a basic method. Additional code would be needed to more thoroughly  
            // remove certain elements, such as embedded Javascript. 

            // Remove HTML tags. 
            fixedString = Regex.Replace(isiKonten.ToString(), "<[^>]+>", string.Empty);

            // Remove newline characters.
            fixedString = fixedString.Replace("\r", " ").Replace("\n", " ");

            // Remove encoded HTML characters.
            fixedString = HttpUtility.HtmlDecode(fixedString);

            //remove double space
            fixedString = Regex.Replace(fixedString, @"\s+", " ", RegexOptions.Multiline).Trim();

            strLength = fixedString.ToString().Length;

            // Some feed management tools include an image tag in the Description field of an RSS feed, 
            // so even if the Description field (and thus, the Summary property) is not populated, it could still contain HTML. 
            // Due to this, after we strip tags from the string, we should return null if there is nothing left in the resulting string. 
            if (strLength == 0)
                return string.Empty;

            // Truncate the text if it is too long. 
            else if (strLength >= maxLength)
            {
                fixedString = fixedString.Substring(0, maxLength);

                // Unless we take the next step, the string truncation could occur in the middle of a word.
                // Using LastIndexOf we can find the last space character in the string and truncate there. 
                fixedString = fixedString.Substring(0, fixedString.LastIndexOf(" "));
            }

            fixedString += "...";

            return fixedString;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return isiKonten;
        }
    }
    public static dynamic ParseTable(IEnumerable<Dictionary<string, dynamic>> filteredData, int count, int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir)
    {
        try
        {
            if (iSortingCols == 1)
            {
                if (sSortDir == "asc") { filteredData = filteredData.OrderBy(p => p.Values.ElementAt(iSortCol)); }
                else if (sSortDir == "desc") { filteredData = filteredData.OrderByDescending(p => p.Values.ElementAt(iSortCol)); }
            }

            if (iDisplayLength != -1)
                filteredData = filteredData.Skip(iDisplayStart).Take(iDisplayLength);

            return new WITLibrary.Datatable
            {
                sEcho = sEcho,
                iTotalRecords = count,
                iTotalDisplayRecords = count,
                aaData = filteredData.ToList()
            };
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return new WITLibrary.Datatable
            {
                sEcho = 1,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0,
                aaData = new List<Dictionary<string, dynamic>>()
            };
        }
    }

    public static dynamic EncryptToken(dynamic data)
    {
        var key = System.Configuration.ConfigurationManager.AppSettings["key"].ToString();
        return JWT.JsonWebToken.Encode(data, key, JwtHashAlgorithm.HS256);
    }

    public static dynamic DecryptToken(string token)
    {
        try
        {
            var key = System.Configuration.ConfigurationManager.AppSettings["key"].ToString();
            var result = JWT.JsonWebToken.DecodeToObject(token, key) as IDictionary<string, dynamic>;
            return result;
        }
        catch (JWT.SignatureVerificationException)
        {
            return "Invalid token!";
        }
    }

    public static string EncryptPassword(string clearText)
    {
        string EncryptionKey = System.Configuration.ConfigurationManager.AppSettings["key"].ToString();
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            //encryptor.Padding = PaddingMode.Zeros;
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

    public static string DecryptPassword(string cipherText)
    {
        string EncryptionKey = System.Configuration.ConfigurationManager.AppSettings["key"].ToString();
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            //encryptor.Padding = PaddingMode.Zeros;
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

    public static ReturnData SendEmail(string receiver, string subject, string body, string attachmentPath)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            string sender = db.TBConfigurations.Where(x => x.Name == "email_user").FirstOrDefault().Value;
            dynamic dd = OurClass.DecryptPassword(db.TBConfigurations.Where(x => x.Name == "email_password").FirstOrDefault().Value);
            string passEmail = OurClass.DecryptPassword(db.TBConfigurations.Where(x => x.Name == "email_password").FirstOrDefault().Value);
            string smtpHost = db.TBConfigurations.Where(x => x.Name == "email_smtp_client").FirstOrDefault().Value;
            int smtpPort = int.Parse(db.TBConfigurations.Where(x => x.Name == "email_smtp_port").FirstOrDefault().Value);
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(sender, System.Configuration.ConfigurationManager.AppSettings["Title"]);
            msg.To.Add(new MailAddress(receiver));
            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = true;
            if (attachmentPath != "")
                msg.Attachments.Add(new Attachment(attachmentPath));

            SmtpClient smtp = new SmtpClient(smtpHost, smtpPort);
            smtp.Credentials = new NetworkCredential(sender, passEmail);
            smtp.Send(msg);
            return ReturnData.MessageSuccess("Email sent", null);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message + " - " + attachmentPath, null);
        }
    }

    public static ReturnData SendMultipleEmail(List<string> receiver, string subject, string body, string attachmentPath)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            string sender = db.TBConfigurations.Where(x => x.Name == "email_user").FirstOrDefault().Value;
            dynamic dd = OurClass.DecryptPassword(db.TBConfigurations.Where(x => x.Name == "email_password").FirstOrDefault().Value);
            string passEmail = OurClass.DecryptPassword(db.TBConfigurations.Where(x => x.Name == "email_password").FirstOrDefault().Value);
            string smtpHost = db.TBConfigurations.Where(x => x.Name == "email_smtp_client").FirstOrDefault().Value;
            int smtpPort = int.Parse(db.TBConfigurations.Where(x => x.Name == "email_smtp_port").FirstOrDefault().Value);
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(sender, System.Configuration.ConfigurationManager.AppSettings["Title"]);
            foreach (var emailAddr in receiver)
            {
                msg.To.Add(new MailAddress(emailAddr));
            }
            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = true;
            if (attachmentPath != "")
                msg.Attachments.Add(new Attachment(attachmentPath));

            SmtpClient smtp = new SmtpClient(smtpHost, smtpPort);
            smtp.Credentials = new NetworkCredential(sender, passEmail);
            smtp.Send(msg);
            return ReturnData.MessageSuccess("Email sent", null);
        }
        catch (Exception ex)
        {
            return ReturnData.MessageFailed(ex.Message + " - " + attachmentPath, null);
        }
    }
}