using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProductPromo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieBuyGet"].ToString()] != null)
        {
            Response.Redirect("SummaryPromo.aspx");
        }
    }
}