using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Lookbook : System.Web.UI.Page
{
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
        var lookbook = Post.FE_MultiplePost_SinglePhoto_MultipleCategory(1007);

        lvLookbook.DataSource = lookbook;
        lvLookbook.DataBind();
    }
}