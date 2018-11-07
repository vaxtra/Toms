using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CollectionDetail : System.Web.UI.Page
{
    Class_Post Post = new Class_Post();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["idCollection"] != null)
            {
                LoadData();
            }
            else
            {
                Response.Redirect("Collection.aspx");
            }
        }
    }
    protected void LoadData()
    {
        var CollectionDetail = Post.FE_DetailPost(int.Parse(Request.QueryString["idCollection"]));
        lvCollection.DataSource = Post.FE_DetailPost_AllPhoto(int.Parse(Request.QueryString["idCollection"]));
        lvCollection.DataBind();

        LabelTitlePost.Text = CollectionDetail.Post_Title;
        Labelcontent.Text = CollectionDetail.Post_Content;

    }
}