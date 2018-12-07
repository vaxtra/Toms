﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Scheduling : System.Web.UI.Page
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
        Class_Post Post = new Class_Post();

        var header = Post.FE_SinglePostOnly_SingleCategory(60, 13);

        slider.Attributes.Add("style", "background:url(/assets/images/post/" + header.TBPostMedias.FirstOrDefault().MediaUrl + ") center no-repeat");
        ltrHeaderTitle.Text = header.Post_Title;
        ltrHeaderDescription.Text = header.Post_Content;

        var tools = Post.FE_SinglePostOnly_SingleCategory(61, 13);

        ltrDashboardTitle.Text = tools.Post_Title;
        ltrDashboardShortDescription.Text = tools.Post_ShortContent;
        ltrDashboardDescription.Text = tools.Post_Content;
        imgDashboard.ImageUrl = "/assets/images/post/" + tools.TBPostMedias.FirstOrDefault().MediaUrl;

        var featurePoint = Post.FE_SinglePostOnly_SingleCategory(62, 13);

        ltrFeaturePointTitle.Text = featurePoint.Post_Title;
        ltrFeaturePointDescription.Text = featurePoint.Post_ShortContent;

        lvFeaturePoint.DataSource = featurePoint.TBPostMedias;
        lvFeaturePoint.DataBind();

        var faq = Post.FE_SinglePostOnly_SingleCategory(63, 13);

        ltrFaq.Text = faq.Post_Content;

    }
}