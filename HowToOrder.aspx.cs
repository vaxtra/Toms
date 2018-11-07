using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HowToOrder : System.Web.UI.Page
{
    Class_Post post = new Class_Post();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
    }

    protected void LoadData()
    {
        var cms = post.FE_SinglePostOnly_SingleCategory(1015, 1009);

        ImageHowto1.ImageUrl = "/assets/images/post/" + cms.TBPostMedias.FirstOrDefault().MediaUrl;
        //ImageHowto2.ImageUrl = "/assets/images/post/" + cms.TBPostMedias.Skip(1).FirstOrDefault().MediaUrl;
        lblShortDescription.Text = cms.Post_ShortContent;
        lblDescription.Text = cms.Post_Content;
    }
}