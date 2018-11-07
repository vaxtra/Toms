using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProductList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    DataClassesDataContext db = new DataClassesDataContext();
        //    if (Page.RouteData.Values["category"] != null)
        //    {
        //        string name = Page.RouteData.Values["category"].ToString().ToLower().Replace(" ", "-");
        //        var category = db.TBCategories.Where(x => x.Name.ToLower().Replace(" ", "-") == name && !x.Deflag).FirstOrDefault();
        //        if (category != null)
        //        {
        //            System.Web.UI.HtmlControls.HtmlInputHidden HiddenIDCategory = (System.Web.UI.HtmlControls.HtmlInputHidden)Page.Master.FindControl("HiddenIDCategory");

        //            HiddenIDCategory.Value = category.IDCategory.ToString();
        //        }
        //        Class_Post Post = new Class_Post();
        //        var banner = Post.FE_SinglePost_MultiplePhoto_SingleCategory(2030, 2016);
        //        if (banner.Count() != 0)
        //        {
        //            ProductBanner.Attributes.Add("style", "background: url(/assets/images/post/" + banner.FirstOrDefault().MediaUrl + ") center / 100% auto no-repeat; min-height:540px;");
        //        }
        //    }


        //}
    }
}