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
            //LoadData();
        }
    }

    protected void LoadData()
    {
        var About = db.TBPageCategory_Posts.Where(x => x.TBPost.TBPage.IDPage == 5 && !x.TBPost.Deflag && x.IDPageCategory == 9).Select(x => new
        {
            IDPost = x.IDPost,
            Photo = x.TBPost.TBPostMedias.FirstOrDefault().MediaUrl,
            Title = x.TBPost.Post_Title,
            PageTitle = x.TBPost.TBPage.Page_Title,
            Description = x.TBPost.Post_Content,
            ShortDescription = x.TBPost.Post_ShortContent,
            Category = x.TBPageCategory.Name,
            Date = x.TBPost.DateInsert,
        }).OrderByDescending(x => x.IDPost).FirstOrDefault();

        imgAbout.ImageUrl = "assets/images/post/"+ About.Photo;
        lblTitle.Text = About.Title;
        lblContent.Text = About.Description;
    }
}