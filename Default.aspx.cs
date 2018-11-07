using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    Class_Post Post = new Class_Post();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //LoadData();
            ////LoadBLog();
            //LoadLookbook();
        }
    }

    protected void LoadData()
    {
        var slide = Post.FE_SinglePost_MultiplePhoto_SingleCategory(1012, 1005);

        lvSlideshow.DataSource = slide.OrderByDescending(x => x.IDPostMedia);
        lvSlideshow.DataBind();

        var homeblock = Post.FE_SinglePostOnly_SingleCategory(1013, 1005);

        lblTitleHomeBlock.Text = homeblock.Post_Title;
        lblShortDescHomeBlock.Text = homeblock.Post_ShortContent;
        lblDescHomeBlock.Text = homeblock.Post_Content;
        imgHomeBlock.ImageUrl = "/assets/images/post/" + homeblock.TBPostMedias.FirstOrDefault().MediaUrl;

        var videoblock = Post.FE_MultiplePost_SinglePhoto_SingleCategory(1014, 1005);

        lvVideoBlock.DataSource = videoblock;
        lvVideoBlock.DataBind();

        lvCategoryVideo.DataSource = videoblock;
        lvCategoryVideo.DataBind();
    }

    //protected void LoadBLog()
    //{
    //    var blog = Post.FE_MultiplePost_SinglePhoto_MultipleCategory(1006);

    //    lvBlog.DataSource = blog.Take(4);
    //    lvBlog.DataBind();
    //}
    protected void LoadLookbook()
    {
        DataClassesDataContext db = new DataClassesDataContext();
        var lookbook = db.TBPageCategory_Posts.Where(x => !x.TBPost.Deflag && x.TBPost.IDPage == 1007).OrderByDescending(x => x.TBPost.IDPost).FirstOrDefault();

        lblLookbook.Text = lookbook.TBPost.Post_Title;
        imgLookbook.ImageUrl = "/assets/images/post/" + lookbook.TBPost.TBPostMedias.FirstOrDefault().MediaUrl;
        linkLookbook.Attributes.Add("href", "/LookbookDetail/" + lookbook.TBPost.Post_Title.ToString().Trim().Replace(" ", "-").Replace("\"", "").Replace(".", "").ToLower());
        linkLookbook2.Attributes.Add("href", "/LookbookDetail/" + lookbook.TBPost.Post_Title.ToString().Trim().Replace(" ", "-").Replace("\"", "").Replace(".", "").ToLower());
    }
}