using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

[System.Web.Script.Services.ScriptService]
public partial class vt_handling : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Class_Order class_order = new Class_Order();
        //class_order.AJAX_UpdateOrderStatus_vt(27, 2, "");
    }

    [WebMethod]
    public static void HandlerVA(string status_code,string order_id)
    {
        //HttpContext.Current.Response.Write("asdasd");
        //HttpContext.Current.Response.Write(status_code + " " + status_message + " " + transaction_id + " " + masked_card + " " + order_id + " " + gross_amount + " " + payment_type + " " + transaction_time + " " + transaction_status + " " + fraud_status + " " + approval_code + " " + signature_key + " " + bank + " " + eci);
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBOrder order = db.TBOrders.Where(x => x.Reference == order_id).FirstOrDefault();
            if (order != null)
            {
                if (status_code == "200")
                {
                    Class_Order class_order = new Class_Order();
                    class_order.AJAX_UpdateOrderStatus_vt(order.IDOrder, 2, ""); //sukses
                }
                else if (status_code == "202")
                {
                    Class_Order class_order = new Class_Order();
                    class_order.AJAX_UpdateOrderStatus_vt(order.IDOrder, 10, ""); //Transaksi ditolak oleh bank atau Fraud Detection System (FDS).
                }
                else if (status_code == "201")
                {
                    Class_Order class_order = new Class_Order();
                    class_order.AJAX_UpdateOrderStatus_vt(order.IDOrder, 13, ""); //Transaksi ditolak oleh FDS. Anda harus melakukan konfirmasi di Merchant Administration Portal (MAP).
                }
            }

            
        }
        catch (Exception ex)
        {

            HttpContext.Current.Response.Write(ex.Message);
        }
    }

    [WebMethod]
    public static void NotifHandler(string status_code, string status_message, string transaction_id, string masked_card, string order_id, string gross_amount, string payment_type, string transaction_time, string transaction_status, string fraud_status, string signature_key)
    {
        //HttpContext.Current.Response.Write("asdasd");
        //HttpContext.Current.Response.Write(status_code + " " + status_message + " " + transaction_id + " " + masked_card + " " + order_id + " " + gross_amount + " " + payment_type + " " + transaction_time + " " + transaction_status + " " + fraud_status + " " + approval_code + " " + signature_key + " " + bank + " " + eci);
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBOrder order = db.TBOrders.Where(x => x.IDOrder.ToString() == order_id).FirstOrDefault();
            if (order != null)
            {
                HttpContext.Current.Response.Write("ada");
                if (status_code == "200")
                {
                    Class_Order class_order = new Class_Order();
                    ReturnData result =  class_order.AJAX_UpdateOrderStatus_vt(order.IDOrder, 2, "");
                    HttpContext.Current.Response.Write(result.message);
                }
                else
                {
                    Class_Order class_order = new Class_Order();
                    class_order.AJAX_UpdateOrderStatus_vt(order.IDOrder, 2, "");
                    HttpContext.Current.Response.Write("not updated");
                }
            }
        }
        catch (Exception ex)
        {

            HttpContext.Current.Response.Write(ex.Message);
        }
    }
}