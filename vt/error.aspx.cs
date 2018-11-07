using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class vt_error : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["order_id"] != null && Request.QueryString["status_code"] != null && Request.QueryString["transaction_status"] != null)
            {
                UpdateOrderStatus();
            }

        }
    }

    protected void UpdateOrderStatus()
    {
        var unfinOrder = db.TBOrders.Where(x => x.Reference == Request.QueryString["order_id"] && !x.Deflag && x.IDOrderStatus == 14);

        if (unfinOrder.Count() == 0)
        {
            Response.Write("Data Not Found");
        }
        else
        {
            foreach (var item in unfinOrder)
            {
                var detail = db.TBOrder_Details.Where(x => x.IDOrder == item.IDOrder);
                foreach (var ordDetail in detail)
                {
                    ordDetail.TBProduct_Combination.Quantity += ordDetail.Quantity;
                }
                item.IDOrderStatus = db.TBOrder_Status.Where(x => !x.Deflag && x.IDOrderStatus == 15 && x.Paid == false && x.Logable == true && x.Delivery == false && x.Shipped == false && x.Restock == true).FirstOrDefault().IDOrderStatus;
                item.DateLastUpdate = DateTime.Now;
            }
            db.SubmitChanges();
        }
    }
}