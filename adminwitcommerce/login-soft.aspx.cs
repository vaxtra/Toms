using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adminwitcommerce_login_soft : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()] != null)
        {
            Response.Redirect("/adminwitcommerce/");
        }
    }
}