using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Contact : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadData();
    }

    protected void LoadData()
    {
        Class_Post Post = new Class_Post();

        var contact = Post.FE_SinglePostOnly_SingleCategory(5, 5);
        ltrContactInformation.Text = contact.Post_ShortContent;
    }
}