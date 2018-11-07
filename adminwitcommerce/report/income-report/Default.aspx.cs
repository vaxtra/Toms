using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adminwitcommerce_report_IncomeReport_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void btnExportIncomeReport_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Income_Report_" + DateTime.Now.ToShortDateString() + ".xls"));
        Response.ContentType = "application/ms-excel";

        DataClassesDataContext db = new DataClassesDataContext();

        var data = db.TBOrders.Where(X => !X.Deflag && X.DateInsert <= DateTime.Now).OrderBy(x => x.DateInsert).ToList();
        int dayCount = (int)(DateTime.Now - data.FirstOrDefault().DateInsert).TotalDays;
        DateTime loopDate = data.FirstOrDefault().DateInsert.Date;
        int count = 0;
        List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();

        DataTable dt = new DataTable();
        dt.Columns.Add("Date", typeof(string));
        dt.Columns.Add("Total Registrant", typeof(Int32));
        dt.Columns.Add("Total Orders", typeof(Int32));
        dt.Columns.Add("Total Items Sold", typeof(Int32));
        dt.Columns.Add("Total Sales", typeof(Int32));
        dt.Columns.Add("Total Sales w/ Discount", typeof(Int32));

        for (int i = 1; i <= dayCount; i++)
        {
            var totalOrder = db.TBOrders.Where(x => !x.Deflag && x.DateInsert.Date == loopDate.Date).Count() == 0 ? 0 : db.TBOrders.Where(x => !x.Deflag && x.DateInsert.Date == loopDate.Date).Count();
            var totalCustomer = db.TBCustomers.Where(x => !x.Deflag && x.DateInsert.Date == loopDate.Date).Count() == 0 ? 0 : db.TBCustomers.Where(x => !x.Deflag && x.DateInsert.Date == loopDate.Date).Count();
            var totalItem = db.TBOrder_Details.Where(x => x.TBOrder.DateInsert.Date == loopDate.Date && x.TBOrder.TBOrder_Status.Paid).Count() == 0 ? 0 : db.TBOrder_Details.Where(x => x.TBOrder.DateInsert.Date == loopDate.Date && x.TBOrder.TBOrder_Status.Paid).Sum(x => x.Quantity);
            var totalSales = db.TBOrders.Where(x => !x.Deflag && x.DateInsert.Date == loopDate.Date && x.TBOrder_Status.Paid).Count() == 0 ? 0 : (decimal)db.TBOrders.Where(x => !x.Deflag && x.DateInsert.Date == loopDate.Date && x.TBOrder_Status.Paid).Sum(x => x.TotalPrice);
            var totalSalesVoucher = db.TBOrders.Where(x => !x.Deflag && x.DateInsert.Date == loopDate.Date && x.TBOrder_Status.Paid).Count() == 0 ? 0 : (decimal)db.TBOrders.Where(x => !x.Deflag && x.DateInsert.Date == loopDate.Date && x.TBOrder_Status.Paid).Sum(x => x.TotalPrice - x.TotalDiscount);

            dt.Rows.Add(loopDate.ToString("dd MMMM yyyy"), totalCustomer, totalOrder, totalItem, totalSales, totalSalesVoucher);

            loopDate = loopDate.AddDays(1).Date;
        }

        string str = string.Empty;

        foreach (DataColumn dtcol in dt.Columns)
        {
            Response.Write(dtcol.ColumnName);
            Response.Write("\t");
        }
        Response.Write("\n");
        foreach (DataRow dr in dt.Rows)
        {
            str = "";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                Response.Write(Convert.ToString(dr[i]));
                Response.Write("\t");
            }
            Response.Write("\n");
        }
        Response.End();
    }
}