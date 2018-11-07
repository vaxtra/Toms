using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adminwitcommerce_catalog_product_Update : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            Class_Product _product = new Class_Product();
            foreach (string s in HttpContext.Current.Request.Files)
            {
                _product.AJAX_Insert_Photo(WITLibrary.ConvertString.ToInt(Request.QueryString["id"].ToString()), Request.Files[s]);
            }
        }
    }
}