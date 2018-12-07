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
            LoadData();
        }
    }

    protected void LoadData()
    {
        var slide = Post.FE_SinglePost_MultiplePhoto_SingleCategory(1, 1);

        lvSlideshow.DataSource = slide.OrderByDescending(x => x.IDPostMedia);
        lvSlideshow.DataBind();

        var homeblock = Post.FE_SinglePostOnly_SingleCategory(11, 1);

        lblHomeBlockTitle.Text = homeblock.Post_Title;
        lblHomeBlockDescription.Text = homeblock.Post_Content;
        HomeBlockLink.Attributes.Add("href", homeblock.Post_ShortContent);
        imgHomeBlock.ImageUrl = "/assets/images/post/" + homeblock.TBPostMedias.FirstOrDefault().MediaUrl;

        var services = Post.FE_SinglePost_MultiplePhoto_SingleCategory(12, 1);

        lvServices.DataSource = services;
        lvServices.DataBind();

        var feature = Post.FE_SinglePost_MultiplePhoto_SingleCategory(13, 1);

        lvFeatureLeft.DataSource = feature.Skip(1).Take(4);
        lvFeatureLeft.DataBind();

        imgFeature.ImageUrl = "/assets/images/post/" + feature.Take(1).FirstOrDefault().MediaUrl;

        lvFeatureRight.DataSource = feature.Skip(5).Take(4);
        lvFeatureRight.DataBind();

        var testimonial = Post.FE_SinglePost_MultiplePhoto_SingleCategory(14, 1);

        lvTestimonial.DataSource = testimonial;
        lvTestimonial.DataBind();

        var tour = Post.FE_SinglePostOnly_SingleCategory(15, 1);
        imgTour.ImageUrl = "/assets/images/post/" + tour.TBPostMedias.FirstOrDefault().MediaUrl;
        lblTourTitle.Text = tour.Post_Title;
        lblTourDescription.Text = tour.Post_Content;
        TourLink.Attributes.Add("href", tour.Post_ShortContent);

        var client = Post.FE_SinglePost_MultiplePhoto_SingleCategory(16, 1);
        lvClient.DataSource = client;
        lvClient.DataBind();

        var support = Post.FE_SinglePostOnly_SingleCategory(17, 1);
        lblSupportTitle.Text = support.Post_Title;
        lblSupportDescription.Text = support.Post_Content;
        lblSupportContactInfo.Text = support.Post_ShortContent;
        imgSupportRight.ImageUrl = "/assets/images/post/" + support.TBPostMedias.FirstOrDefault().MediaUrl;

    }
}