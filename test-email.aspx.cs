using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class test_email : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //SendEmail();
        //DataClassesDataContext db = new DataClassesDataContext();
        //string usernameEmail = db.TBConfigurations.Where(x => x.Name == "email_user").FirstOrDefault().Value;
        //string passEmail = db.TBConfigurations.Where(x => x.Name == "email_password").FirstOrDefault().Value;
        //string smtp = db.TBConfigurations.Where(x => x.Name == "email_smtp_client").FirstOrDefault().Value;
        //int smtpPort = int.Parse(db.TBConfigurations.Where(x => x.Name == "email_smtp_port").FirstOrDefault().Value);
        //string body = string.Empty;
        //using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/email_system.html")))
        //{
        //    body = reader.ReadToEnd();
        //    Response.Write(OurClass.SendEmail("rikkinovar@gmail.com", "TEST EMAIL", "ini body", "").message);
        //}

        //SendEmail2();
        //Response.Write(OurClass.DecryptPassword("hoBscIRR9UA4hOoafeaeW+XBqbCggbJAioE2KDOjFBU="));
        SendEmail2();
    }

    private void SendEmail2()
    {
        //MailMessage msg = new MailMessage();
        //msg.From = new MailAddress("info@erigostore.com", System.Configuration.ConfigurationManager.AppSettings["Title"]);
        //msg.To.Add(new MailAddress("rikkinovar@gmail.com"));
        //msg.Subject = "asdasdasd";
        //msg.Body = "testing email";
        //msg.IsBodyHtml = true;
        ////SmtpClient smtp = new SmtpClient("mail.wit.co.id", 25);
        ////smtp.Credentials = new NetworkCredential("rikkinovar@wit.co.id", "Rikki12345");
        //SmtpClient smtp = new SmtpClient("mail.wit.co.id", 25);
        //smtp.Credentials = new NetworkCredential("rikkinovar@wit.co.id", "Rikki12345");
        ////smtp.UseDefaultCredentials = false;
        //smtp.EnableSsl = false;
        //smtp.Send(msg);

        //SEND EMAIL
        Class_Configuration _config = new Class_Configuration();
        var emailLogo = _config.Dynamic_Get_EmailLogo();
        using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/template-email-register.html")))
        {
            string body = "";
            body = reader.ReadToEnd();
            body = body.Replace("{name}", "Rikki Novar");
            body = body.Replace("{email}", "rikkinovar@gmail.com");
            body = body.Replace("{password}", "asdasd");
            body = body.Replace("{title}", System.Configuration.ConfigurationManager.AppSettings["Title"]);
            body = body.Replace("{email_logo}", System.Configuration.ConfigurationManager.AppSettings["url"] + "/assets/images/email_logo/" + emailLogo);
            body = body.Replace("{shop_url}", System.Configuration.ConfigurationManager.AppSettings["url"]);
            Response.Write(OurClass.SendEmail("vaxtramaendhapaskha@gmail.com", WebConfigurationManager.AppSettings["Title"] + " Account Registration", body, "").message);
        }
    }

    private void SendEmail()
    {
        try
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/template-email.html")))
            {
                body = reader.ReadToEnd();
            }

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("rikki@ouvalresearch.com");
            msg.To.Add(new MailAddress("rikkinovar@gmail.com"));
            msg.Subject = "Meeting";
            msg.Body = "Body message";

            SmtpClient smtp = new SmtpClient("mail.mypartitur.com", 25);
            smtp.Credentials = new NetworkCredential("rikki@ouvalresearch.com", "sontoloyo");
            smtp.Send(msg);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}