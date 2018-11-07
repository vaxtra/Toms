using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// Summary description for Class_Payment_Confirmation
/// </summary>
public class Class_Payment_Confirmation
{
    public Class_Payment_Confirmation()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public ReturnData AJAX_Submit(int idOrder, string name, string email, string phoneNumber, string bank, decimal amount, string paymentDate, string notes, string baseImage, string fnImage)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();

            DateTime myDate = DateTime.ParseExact(paymentDate, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);

            TBOrder order = db.TBOrders.Where(x => !x.Deflag && x.IDOrder == idOrder).FirstOrDefault();
            if (order == null)
                return ReturnData.MessageFailed("Order not found", null);
            TBPayment_Confirmation data = new TBPayment_Confirmation();
            data.IDOrder = idOrder;
            data.ReferenceNumber = order.Reference;
            data.Name = name;
            data.Email = email;
            data.PhoneNumber = phoneNumber;
            data.Bank = bank;
            data.Amount = amount;
            data.PaymentDate = myDate;
            data.Notes = notes;
            if (baseImage != "" && fnImage != "")
            {
                System.Drawing.Image _image = WITLibrary.ConvertCustom.Base64ToImage(baseImage);
                _image.Save(HttpContext.Current.Server.MapPath("/assets/images/payment_confirmation/" + DateTime.Now.ToString("ddmmyy") + "_" + fnImage));
                data.Image = DateTime.Now.ToString("ddmmyy") + "_" + fnImage;
            }
            db.TBPayment_Confirmations.InsertOnSubmit(data);
            //GANTI STATUS JADI AWAITING PAYMENT VERIFICATION
            order.IDOrderStatus = 13;

            db.SubmitChanges();

            //INSERT LOG ORDER
            Class_Log_Order logOrder = new Class_Log_Order();
            logOrder.Insert((int?)null, order.IDOrder, order.Reference, order.TBOrder_Status.Name, "Customer Payment Confirmation", order.TBCustomer.FirstName + ' ' + order.TBCustomer.LastName);

            //SEND EMAIL TO ADMIN
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/admin-payment-notification.html")))
            {
                string body = "";
                body = reader.ReadToEnd();
                body = body.Replace("{ReferenceCode}", data.ReferenceNumber);
                body = body.Replace("{CustomerName}", data.Name);
                body = body.Replace("{Name}", data.Name);
                body = body.Replace("{Email}", data.Email);
                body = body.Replace("{Date}", data.PaymentDate.ToString("dd/MM/yyyy hh:mm"));
                body = body.Replace("{Amount}", WITLibrary.ConvertString.ToCurrency(data.Amount.ToString()));
                body = body.Replace("{Bank}", data.Bank);
                body = body.Replace("{Notes}", data.Notes);
                string emailAdmin = db.TBConfigurations.Where(x => x.Name == "email_user").FirstOrDefault().Value;
                OurClass.SendEmail(emailAdmin, WebConfigurationManager.AppSettings["Title"] + " Confirm Payment for Order #" + data.ReferenceNumber, body, HttpContext.Current.Server.MapPath("/assets/images/payment_confirmation/" + DateTime.Now.ToString("ddmmyy") + "_" + fnImage));
            }

            //SEND EMAIL KE CUSTOMER
            Class_Configuration _config = new Class_Configuration();
            var emailLogo = _config.Dynamic_Get_EmailLogo();
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/template-email-paymentReceived.html")))
            {
                string body = "";
                body = reader.ReadToEnd();
                body = body.Replace("{name}", name);
                body = body.Replace("{nomorInvoice}", order.Reference);
                body = body.Replace("{amount}", WITLibrary.ConvertString.ToCurrency(order.TotalPaid.ToString()));
                body = body.Replace("{owner}", order.TBPayment_Method.Owner);
                body = body.Replace("{account}", order.TBPayment_Method.AccountNumber);
                body = body.Replace("{bank}", order.TBPayment_Method.Bank);
                body = body.Replace("{title}", System.Configuration.ConfigurationManager.AppSettings["Title"]);
                body = body.Replace("{email_logo}", System.Configuration.ConfigurationManager.AppSettings["url"] + "/assets/images/email_logo/" + emailLogo);
                body = body.Replace("{shop_url}", System.Configuration.ConfigurationManager.AppSettings["url"]);
                OurClass.SendEmail(email, WebConfigurationManager.AppSettings["Title"] + " Order Notification", body, "");
            }


            return ReturnData.MessageSuccess("Order Confirmation submitted successfully", null);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public dynamic Dynamic_GetDetail_ByIDOrder(int idOrder)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBPayment_Confirmations.Where(x => x.IDOrder == idOrder).Select(x => new
            {
                x.IDPaymentConfirmation,
                x.Image,
                x.Name,
                x.Email,
                x.Amount,
                x.Bank,
                x.Notes,
                x.PaymentDate,
                x.PhoneNumber,
                x.ReferenceNumber,
            }).FirstOrDefault();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_GetDetail_ByIDPaymentConfirmation(int idPaymentConfirmation)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBPayment_Confirmations.Where(x => x.IDPaymentConfirmation == idPaymentConfirmation).Select(x => new
            {
                x.IDPaymentConfirmation,
                x.Image,
                x.Name,
                x.Email,
                x.Amount,
                x.Bank,
                x.Notes,
                x.PaymentDate,
                x.PhoneNumber,
                x.ReferenceNumber,
            }).FirstOrDefault();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
}