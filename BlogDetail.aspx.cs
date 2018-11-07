using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BlogDetail : System.Web.UI.Page
{
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
        var idPost = db.TBPosts.Where(x => x.Post_Title.Trim().Replace(" ", "-").Replace("\"", "").Replace(".", "").Replace("!", "").ToLower() == Page.RouteData.Values["PostName"].ToString()).FirstOrDefault();
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

            lvPhotoBlog.DataSource = Journal.Photo.Skip(2);
            lvPhotoBlog.DataBind();

            lblTitlePost.Text = Journal.Title;
            lblShortDescription.Text = Journal.ShortDescription;
            lblDescription.Text = Journal.Description;
			if (Journal.Photo.Count() > 1)
			{
				headermage.Attributes.Add("style", "background:url(/assets/images/post/" + Journal.Photo.Skip(1).Take(1).FirstOrDefault().MediaUrl + ");min-height:600px;");
			}
            
            //lblShortDesc.Text = Journal.ShortDescription;
        }


    }
}