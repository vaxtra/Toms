using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adminwitcommerce_content_post_Update : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            Class_Post _post = new Class_Post();
            foreach (string s in HttpContext.Current.Request.Files)
            {
                _post.AJAX_Insert_Photo(WITLibrary.ConvertString.ToInt(Request.QueryString["id"].ToString()), Request.Files[s]);
            }
        }
    }
}