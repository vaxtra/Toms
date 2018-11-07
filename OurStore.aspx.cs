using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OurStore : System.Web.UI.Page
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
        var cms = post.FE_SinglePostOnly_SingleCategory(1022, 1013);

        var storelist = post.FE_SinglePost_MultiplePhoto_SingleCategory(1022, 1013);

        lblTitle.Text = cms.Post_Title;

        lvStore.DataSource = storelist;
        lvStore.DataBind();
    }
}