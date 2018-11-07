using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Blog : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    Class_Post Post = new Class_Post();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
    }

    protected void LoadData()
    {
        //var blog = Post.FE_MultiplePost_SinglePhoto_MultipleCategory(1006);

        //lvLatestBlog.DataSource = blog.Take(1);
        //lvLatestBlog.DataBind();

        //lvBlog.DataSource = blog.Skip(1);
        //lvBlog.DataBind();

    }
}