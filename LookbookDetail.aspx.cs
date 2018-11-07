using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LookbookDetail : System.Web.UI.Page
{
    Class_Post Post = new Class_Post();
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Page.RouteData.Values["PostName"] != null)
            {
                LoadData();
            }
            else
            {
                Response.Redirect("/Blog");
            }
        }
    }

    protected void LoadData()
    {
        var idPost = db.TBPosts.Where(x => x.Post_Title.Trim().Replace(" ", "-").Replace("\"", "").Replace(".", "").ToLower() == Page.RouteData.Values["PostName"].ToString()).FirstOrDefault();
        if (idPost != null)
        {
            var Journal = db.TBPageCategory_Posts.Where(x => x.IDPost.ToString() == idPost.IDPost.ToString() && !x.TBPost.Deflag).Select(x => new
            {
                IDPost = x.IDPost,
                Photo = x.TBPost.TBPostMedias,
                Title = x.TBPost.Post_Title,
                Description = x.TBPost.Post_Content,
                ShortDescription = x.TBPost.Post_ShortContent,
                Date = x.TBPost.DateInsert,
                IDPageCategory = x.IDPageCategory,
                Category = x.TBPageCategory.Name
            }).OrderByDescending(x => x.IDPost).FirstOrDefault();

            lvLookbookPhoto.DataSource = Journal.Photo.Skip(2).Select((x, index) => new {
                x.IDPostMedia,
                x.MediaUrl,
                x.Title,
                x.Description,
                index
            });
            lvLookbookPhoto.DataBind();

            coverlook.Attributes.Add("style", "background: url(/assets/images/post/" + Journal.Photo.Skip(1).FirstOrDefault().MediaUrl + ") no-repeat center center/cover; min-height: 560px; width: 640px; margin-bottom: 40px;");
            lblTitleLookbook.Text = Journal.Title;
            lblShortDescription.Text = Journal.ShortDescription;
            lblDescriptionLookbook.Text = Journal.Description;
            lblCategoryLookbook.Text = Journal.Category;
            //lblShortDesc.Text = Journal.ShortDescription;
        }
    }
}