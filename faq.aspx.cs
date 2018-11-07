using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class faq : System.Web.UI.Page
{
    Class_Post Post = new Class_Post();
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
    }

    protected void LoadData()
    {
        var faq = Post.FE_MultiplePost_SinglePhoto_MultipleCategory(1011);

        lvfaq.DataSource = faq;
        lvfaq.DataBind();

        //var cms = post.FE_SinglePostOnly_SingleCategory(1017, 1011);

        //lblTitle.Text = cms.Post_Title;
        //lblShortDescription.Text = cms.Post_ShortContent;
        //lblDescription.Text = cms.Post_Content;
    }
}