using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adminwitcommerce_customer_customer_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnExportCustomer_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Customer_" + DateTime.Now.ToShortDateString() + ".xls"));
            Response.ContentType = "application/ms-excel";

            Class_Customer _customer = new Class_Customer();
            var data = _customer.GetAll_IsSbuscribe();
            DataTable dt = new DataTable();
            dt.Columns.Add("IDCustomer", typeof(Int32));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Phone Number", typeof(string));
            dt.Columns.Add("Date Registered", typeof(DateTime));
            dt.Columns.Add("Active", typeof(bool));
            foreach (var item in data)
            {
                dt.Rows.Add(item.IDCustomer, item.FirstName + " " + item.LastName, item.Email, item.PhoneNumber, item.DateInsert, item.Active);
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
        catch (Exception)
        {

            throw;
        }
    }
}