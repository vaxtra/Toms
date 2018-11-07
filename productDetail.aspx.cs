using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class productDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataClassesDataContext db = new DataClassesDataContext();
            if (Page.RouteData.Values["ProductName"] != null)
            {
                string name = Page.RouteData.Values["ProductName"].ToString();
                var product = db.TBProducts.Where(x => x.Name.ToLower().Replace("\"", "").Replace(".", "").Replace(" ", "-") == name && !x.Deflag).FirstOrDefault();
                string prod = product.Name.ToString();
                if (product != null)
                {
                    System.Web.UI.HtmlControls.HtmlInputHidden HiddenIDProduct = (System.Web.UI.HtmlControls.HtmlInputHidden)Page.Master.FindControl("HiddenIDProduct");

                    HiddenIDProduct.Value = product.IDProduct.ToString();
                }
            }
        }

    }
}