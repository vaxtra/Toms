using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adminwitcommerce_customer_newsletter_Default : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnExportNonMember_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Newsletter_Non_Member_" + DateTime.Now.ToShortDateString() + ".xls"));
            Response.ContentType = "application/ms-excel";

            Class_Newsletter _newsletter = new Class_Newsletter();
            var data = _newsletter.GetAll_Newsletter();
            DataTable dt = new DataTable();
            dt.Columns.Add("IDNewsletter", typeof(Int32));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Date", typeof(DateTime));
            foreach (var item in data)
            {
                dt.Rows.Add(item.IDNewsletter, item.Email, item.DateInsert);
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
    protected void btnNewsletterMember_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Newsletter_Member_" + DateTime.Now.ToShortDateString() + ".xls"));
            Response.ContentType = "application/ms-excel";

            Class_Customer _customer = new Class_Customer();
            var data = _customer.GetAll_IsSbuscribe();
            DataTable dt = new DataTable();
            dt.Columns.Add("IDCustomer", typeof(Int32));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Date Registered", typeof(DateTime));
            foreach (var item in data)
            {
                dt.Rows.Add(item.IDCustomer, item.FirstName + " " + item.LastName, item.Email, item.DateInsert);
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