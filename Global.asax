<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        Register_Routes(RouteTable.Routes);
        //RouteTable.Routes.MapPageRoute("Product", "Product/{idCategory}", "~/ProductList.aspx", false);
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    void Register_Routes(RouteCollection routes)
    {
        routes.MapPageRoute("Products", "Products/{category}", "~/ProductList.aspx", true, new RouteValueDictionary { { "category", "[a-z][A-Z]" } });
        routes.MapPageRoute("Product Detail", "ProductDetail/{ProductName}", "~/ProductDetail.aspx", true, new RouteValueDictionary { { "ProductName", "[a-z][A-Z]" } });
        routes.MapPageRoute("Home", "Home", "~/Default.aspx", true);
        routes.MapPageRoute("Categories", "Categories", "~/ProductCategories.aspx", true);
        routes.MapPageRoute("Summary", "Summary", "~/Summary.aspx", true);
        routes.MapPageRoute("Address", "Address", "~/Address.aspx", true);
        routes.MapPageRoute("Authentication", "Authentication", "~/Authentication.aspx", true);
        routes.MapPageRoute("My Account", "MyAccount", "~/MyAccount.aspx", true);
        routes.MapPageRoute("Change Password", "ChangePassword", "~/ChangePassword.aspx", true);
        routes.MapPageRoute("My Address", "MyAddress", "~/MyAddress.aspx", true);
        routes.MapPageRoute("Confirm Payment", "ConfirmPayment", "~/ConfirmPayment.aspx", true);
        routes.MapPageRoute("Order History", "OrderHistory", "~/OrderHistory.aspx", true);
        routes.MapPageRoute("Voucher", "Voucher", "~/Voucher.aspx", true);
        routes.MapPageRoute("Wishlist", "Wishlist", "~/Wishlist.aspx", true);
        routes.MapPageRoute("Blog", "Blog", "~/Blog.aspx", true);
        routes.MapPageRoute("Blog Detail", "Post/{PostName}", "~/BlogDetail.aspx", true, new RouteValueDictionary { { "category", "[a-z][A-Z][0-9]" } });
        routes.MapPageRoute("Lookbook", "Lookbook", "~/Lookbook.aspx", true);
        routes.MapPageRoute("Lookbook Detail", "LookbookDetail/{PostName}", "~/LookbookDetail.aspx", true, new RouteValueDictionary { { "category", "[a-z][A-Z][0-9]" } });
        routes.MapPageRoute("Contact", "Contact", "~/Contact.aspx", true);
        routes.MapPageRoute("Thank You", "ThankYou", "~/thank-you.aspx", true);
        routes.MapPageRoute("How To Order", "How-to-order", "~/HowToOrder.aspx", true);
        routes.MapPageRoute("Syarat & Ketentuan", "syarat-ketentuan", "~/PrivacyandTerms.aspx", true);
        routes.MapPageRoute("faq", "faq", "~/faq.aspx", true);
        routes.MapPageRoute("About", "About", "~/About.aspx", true);
        routes.MapPageRoute("Our Store List", "Store", "~/OurStore.aspx", true);
        routes.MapPageRoute("Career", "Career", "~/Career.aspx", true);
        routes.MapPageRoute("Package", "Package", "~/Package.aspx", true);
    }

</script>
