using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class About : System.Web.UI.Page
{
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
        Class_Post Post = new Class_Post();
        var header = Post.FE_SinglePostOnly_SingleCategory(2, 2);

        //AboutHeaderImage.Attributes.Add("style", "background:url(/assets/images/post/" + header.TBPostMedias.FirstOrDefault().MediaUrl + ") center no-repeat");
        //ltrAboutHeaderTitle.Text = header.Post_Title;
        //ltrAboutHeaderDescription.Text = header.Post_Content;

        var about = Post.FE_SinglePostOnly_SingleCategory(18, 2);

        ltrAboutUsTitle.Text = about.Post_Title;
        ltrAboutUsDescription.Text = about.Post_Content;

        lvAboutSlider.DataSource = about.TBPostMedias;
        lvAboutSlider.DataBind();

        var vision = Post.FE_SinglePostOnly_SingleCategory(19, 2);

        VisionImage.Attributes.Add("style", "background:url(/assets/images/post/" + vision.TBPostMedias.FirstOrDefault().MediaUrl + ") center no-repeat");
        ltrVisionTitle.Text = vision.Post_Title;
        ltrVisionShortDescription.Text = vision.Post_ShortContent;
        ltrVisionDescription.Text = vision.Post_Content;

        var mission = Post.FE_SinglePostOnly_SingleCategory(20, 2);

        MissionImage.Attributes.Add("style", "background:url(/assets/images/post/" + mission.TBPostMedias.FirstOrDefault().MediaUrl + ") center no-repeat");
        ltrMissionTitle.Text = vision.Post_Title;
        ltrMissionShortDescription.Text = vision.Post_ShortContent;
        ltrMissionDescription.Text = vision.Post_Content;

        var gallery = Post.FE_SinglePost_MultiplePhoto_SingleCategory(21, 2);

        lvGallery.DataSource = gallery;
        lvGallery.DataBind();
    }
}