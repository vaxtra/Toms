using RDN.Veritrans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Web.Configuration;

/// <summary>
/// Summary description for services
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class services : System.Web.Services.WebService
{

    public services()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    public void dtcat()
    {
        Class_Category _category = new Class_Category();
        int idCategoryParent = 0;
        if (Convert.ToInt32(HttpContext.Current.Request["idCategoryParent"]) != 0)
            idCategoryParent = Convert.ToInt32(HttpContext.Current.Request["idCategoryParent"]);
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_category.AJAX_GetTable(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"], idCategoryParent)));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void dtattr()
    {
        Class_Attribute _attribute = new Class_Attribute();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_attribute.AJAX_GetTable(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void dtval()
    {
        Class_Value _value = new Class_Value();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_value.AJAX_GetTable(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"], HttpContext.Current.Request["idattr"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void dtman()
    {
        Class_Manufacturer _manufacturer = new Class_Manufacturer();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_manufacturer.AJAX_GetTable(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void dtcust()
    {
        Class_Customer _customer = new Class_Customer();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_customer.AJAX_GetTable(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void dtCar()
    {
        Class_Carrier _carrier = new Class_Carrier();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_carrier.AJAX_GetTable(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void dtShip()
    {
        Class_Shipping _shipping = new Class_Shipping();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_shipping.AJAX_GetTable(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"], HttpContext.Current.Request["idCarrier"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void dtpro()
    {
        Class_Product _product = new Class_Product();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_product.AJAX_GetTable(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    //[WebMethod]
    //public void dtpro_combi()
    //{
    //    Class_Product _product = new Class_Product();
    //    System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
    //    HttpContext.Current.Response.Clear();
    //    HttpContext.Current.Response.ContentType = ("text/html");
    //    HttpContext.Current.Response.BufferOutput = true;
    //    HttpContext.Current.Response.Write(ser.Serialize(_product.AJAX_GetTable_combi(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
    //    HttpContext.Current.Response.End();
    //}

    [WebMethod]
    public void dtvo()
    {
        Class_Voucher _voucher = new Class_Voucher();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_voucher.AJAX_GetTable(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtOrd()
    {
        Class_Order _order = new Class_Order();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_order.AJAX_GetTable(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtOrd_filterdate()
    {
        DateTime startDate = DateTime.ParseExact(HttpContext.Current.Request["_startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime endDate = DateTime.ParseExact(HttpContext.Current.Request["_endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

        Class_Order _order = new Class_Order();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_order.AJAX_GetTable_FilterDate(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"], startDate, endDate)));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtOrd_filter()
    {
        DateTime startDate = DateTime.ParseExact(HttpContext.Current.Request["_startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime endDate = DateTime.ParseExact(HttpContext.Current.Request["_endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);


        string[] separators = { ",", ".", "!", "?", ";", ":", " " };
        string data = HttpContext.Current.Request["_orderStatus[]"];
        string[] ids = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        List<int> orderStatus = new List<int>();
        foreach (string item in ids)
        {
            int id = 0;
            if (int.TryParse(item, out id))
                orderStatus.Add(id);
        }

        Class_Order _order = new Class_Order();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_order.AJAX_GetTable_Filter(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"], startDate, endDate, orderStatus)));
        HttpContext.Current.Response.End();
    }


    [WebMethod]
    public void admin_dtCountry()
    {
        Class_Region region = new Class_Region();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(region.AJAX_GetTable_Country(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtProvince()
    {
        Class_Region region = new Class_Region();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(region.AJAX_GetTable_Province(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"], HttpContext.Current.Request["idCountry"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtCity()
    {
        Class_Region region = new Class_Region();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(region.AJAX_GetTable_City(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"], HttpContext.Current.Request["idProvince"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtDistrict()
    {
        Class_Region region = new Class_Region();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(region.AJAX_GetTable_District(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"], HttpContext.Current.Request["idCity"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtMonitoring()
    {
        Class_Product_Combination combination = new Class_Product_Combination();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(combination.AJAX_GetTable_Monitoring(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtAvailableQuantity()
    {
        Class_Product_Combination combination = new Class_Product_Combination();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(combination.AJAX_GetTable_AvailableQuantity(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtBestProducts()
    {
        Class_Product product = new Class_Product();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(product.AJAX_GetTable_BestSeller(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtBestCategories()
    {
        Class_Category category = new Class_Category();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(category.AJAX_GetTable_BestCategories(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtBestManufacturer()
    {
        Class_Manufacturer manufacturer = new Class_Manufacturer();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(manufacturer.AJAX_GetTable_BestManufacturer(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtBestCustomer()
    {
        Class_Customer customer = new Class_Customer();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(customer.AJAX_GetTable_BestCustomer(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtPaymentMethod()
    {
        Class_PaymentMethod payment = new Class_PaymentMethod();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(payment.AJAX_GetTable(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtEmployee()
    {
        Class_Employee employee = new Class_Employee();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(employee.AJAX_GetTable(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtPage()
    {
        Class_Page page = new Class_Page();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(page.AJAX_GetTable(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtPageCategory()
    {
        Class_Page_Category pageCategory = new Class_Page_Category();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(pageCategory.AJAX_GetTable(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"], Convert.ToInt32(HttpContext.Current.Request["idPage"]))));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtPost()
    {
        Class_Post post = new Class_Post();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(post.AJAX_GetTable(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtReport()
    {
        Class_Report _report = new Class_Report();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_report.AJAX_GetTable_TotalPerDay(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtReport_FilterDate()
    {
        DateTime startDate = DateTime.ParseExact(HttpContext.Current.Request["_startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime endDate = DateTime.ParseExact(HttpContext.Current.Request["_endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

        Class_Report _report = new Class_Report();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_report.AJAX_GetTable_TotalPerDay_FilterDate(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"], startDate, endDate)));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtPaymentDistribution()
    {
        Class_Report _report = new Class_Report();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_report.AJAX_GetTable_PaymentDistribution(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtPaymentDistribution_FilterDate()
    {
        DateTime startDate = DateTime.ParseExact(HttpContext.Current.Request["_startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime endDate = DateTime.ParseExact(HttpContext.Current.Request["_endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

        Class_Report _report = new Class_Report();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_report.AJAX_GetTable_PaymentDistribution_FilterDate(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"], startDate, endDate)));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtShippingDistribution()
    {
        Class_Report _report = new Class_Report();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_report.AJAX_GetTable_ShippingDistribution(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtShippingDistribution_FilterDate()
    {
        DateTime startDate = DateTime.ParseExact(HttpContext.Current.Request["_startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime endDate = DateTime.ParseExact(HttpContext.Current.Request["_endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

        Class_Report _report = new Class_Report();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_report.AJAX_GetTable_ShippingDistribution_FilterDate(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"], startDate, endDate)));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtDistrictDistribution()
    {
        Class_Report _report = new Class_Report();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_report.AJAX_GetTable_ZoneDistribution(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtDistrictDistribution_FilterDate()
    {
        DateTime startDate = DateTime.ParseExact(HttpContext.Current.Request["_startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime endDate = DateTime.ParseExact(HttpContext.Current.Request["_endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

        Class_Report _report = new Class_Report();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_report.AJAX_GetTable_ZoneDistribution_FilterDate(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"], startDate, endDate)));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtCategoryDistribution()
    {
        Class_Report _report = new Class_Report();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_report.AJAX_GetTable_CategoryDistribution(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtCategoryDistribution_FilterDate()
    {
        DateTime startDate = DateTime.ParseExact(HttpContext.Current.Request["_startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime endDate = DateTime.ParseExact(HttpContext.Current.Request["_endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

        Class_Report _report = new Class_Report();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_report.AJAX_GetTable_CategoryDistribution_FilterDate(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"], startDate, endDate)));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtProvinceDistribution()
    {
        Class_Report _report = new Class_Report();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_report.AJAX_GetTable_ProvinceDistribution(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtProvinceDistribution_FilterDate()
    {
        DateTime startDate = DateTime.ParseExact(HttpContext.Current.Request["_startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime endDate = DateTime.ParseExact(HttpContext.Current.Request["_endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

        Class_Report _report = new Class_Report();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_report.AJAX_GetTable_ProvinceDistribution_FilterDate(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"], startDate, endDate)));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtCityDistribution()
    {
        Class_Report _report = new Class_Report();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_report.AJAX_GetTable_CityDistribution(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtCityDistribution_FilterDate()
    {
        DateTime startDate = DateTime.ParseExact(HttpContext.Current.Request["_startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime endDate = DateTime.ParseExact(HttpContext.Current.Request["_endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

        Class_Report _report = new Class_Report();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_report.AJAX_GetTable_CityDistribution_FilterDate(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"], startDate, endDate)));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtProductDistribution()
    {
        Class_Report _report = new Class_Report();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_report.AJAX_GetTable_ProductDistribution(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtProductDistribution_FilterDate()
    {
        DateTime startDate = DateTime.ParseExact(HttpContext.Current.Request["_startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime endDate = DateTime.ParseExact(HttpContext.Current.Request["_endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

        Class_Report _report = new Class_Report();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_report.AJAX_GetTable_ProductDistribution_FilterDate(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"], startDate, endDate)));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtVoucherDistribution()
    {
        Class_Report _report = new Class_Report();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_report.AJAX_GetTable_VoucherDistribution(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtVoucherDistribution_FilterDate()
    {
        DateTime startDate = DateTime.ParseExact(HttpContext.Current.Request["_startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime endDate = DateTime.ParseExact(HttpContext.Current.Request["_endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

        Class_Report _report = new Class_Report();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_report.AJAX_GetTable_VoucherDistribution_FilterDate(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"], startDate, endDate)));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void dtNewsletter()
    {
        Class_Newsletter _newsletter = new Class_Newsletter();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_newsletter.AJAX_GetTable_Newsletter(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void dtCustomerIsSubscribe()
    {
        Class_Customer _customer = new Class_Customer();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(_customer.AJAX_GetTable_IsSubscribe(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtLog_Stock()
    {
        Class_Log_Stock logStock = new Class_Log_Stock();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(logStock.AJAX_GetTable_Log_Stock(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtLog_Stock_filter()
    {
        DateTime startDate = DateTime.ParseExact(HttpContext.Current.Request["_startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime endDate = DateTime.ParseExact(HttpContext.Current.Request["_endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

        Class_Log_Stock logStock = new Class_Log_Stock();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(logStock.AJAX_GetTable_Log_Stock_filter(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"], startDate, endDate)));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtCustomerAddress()
    {
        Class_Log_Stock logStock = new Class_Log_Stock();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(logStock.AJAX_GetTable_Log_Stock(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtLog_Order()
    {
        Class_Log_Order logOrder = new Class_Log_Order();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(logOrder.AJAX_GetTable_Log_Order(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtLog_Order_filter()
    {
        DateTime startDate = DateTime.ParseExact(HttpContext.Current.Request["_startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime endDate = DateTime.ParseExact(HttpContext.Current.Request["_endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

        Class_Log_Order logOrder = new Class_Log_Order();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(logOrder.AJAX_GetTable_Log_Order_filter(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"], startDate, endDate)));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtRole()
    {
        Class_Role role = new Class_Role();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(role.AJAX_GetTable(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtCurrency()
    {
        Class_Currency currency = new Class_Currency();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(currency.AJAX_GetTable(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public void admin_dtLog_Error()
    {
        Class_Log_Error logError = new Class_Log_Error();
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(logError.AJAX_GetTable_Log_Error(Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]), Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]), Convert.ToInt32(HttpContext.Current.Request["sEcho"]), Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]), Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]), HttpContext.Current.Request["sSortDir_0"], HttpContext.Current.Request["sSearch"])));
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    public dynamic Request(Dictionary<string, dynamic> data)
    {
        try
        {
            if (data.ContainsKey("c") && data.ContainsKey("m"))
            {

                switch ((string)data["c"])
                {
                    #region BACKEND
                    #region category
                    case "becat":
                        {
                            Class_Category _category = new Class_Category();
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "preload":
                                    {
                                        return _category.AJAX_GetDetail(param["id"]);
                                    }
                                case "icat":
                                    {
                                        return _category.AJAX_Insert(param["baseImage"], param["fnImage"], param["idparent"], param["name"], param["description"], param["active"]);
                                    }
                                case "ucat":
                                    {
                                        return _category.AJAX_Update(param["id"], param["baseImage"], param["fnImage"], param["idparent"], param["name"], param["description"], param["active"]);
                                    }
                                case "dcat":
                                    {
                                        return _category.AJAX_Delete(param["id"]);
                                    }
                                case "statustoggle":
                                    {
                                        return _category.AJAX_StatusToggle(param["id"]);
                                    }
                                case "upreload":
                                    {
                                        return _category.AJAX_PreloadUpdate(param["id"]);
                                    }
                                default:
                                    return ReturnData.MessageFailed("endpoint not found", null);
                            }
                        }
                    #endregion
                    #region attribute
                    case "beattr":
                        {
                            Class_Attribute _attribute = new Class_Attribute();
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "iattr":
                                    {
                                        return _attribute.AJAX_Insert(param["Name"], param["IsColor"]);
                                    }
                                case "uattr":
                                    {
                                        return _attribute.AJAX_Update(param["id"], param["Name"], param["IsColor"]);
                                    }
                                case "dattr":
                                    {
                                        return _attribute.AJAX_Delete(param["id"]);
                                    }
                                case "upreload":
                                    {
                                        return _attribute.AJAX_GetDetail(param["id"]);
                                    }
                                default:
                                    return ReturnData.MessageFailed("endpoint not found", null);
                            }
                        }
                    #endregion
                    #region value
                    case "beval":
                        {
                            Class_Value _value = new Class_Value();
                            Class_Attribute _attribute = new Class_Attribute();
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "preload":
                                    {
                                        return _attribute.AJAX_GetDetail(param["idattr"]);
                                    }
                                case "upreload":
                                    {
                                        return _value.AJAX_PreloadUpdate(param["id"]);
                                    }
                                case "ival":
                                    {
                                        return _value.AJAX_Insert(param["idattr"], param["Name"], param["RGBColor"]);
                                    }
                                case "uval":
                                    {
                                        return _value.AJAX_Update(param["id"], param["Name"], param["RGBColor"]);
                                    }
                                case "dval":
                                    {
                                        return _value.AJAX_Delete(param["id"]);
                                    }
                                default:
                                    return ReturnData.MessageFailed("endpoint not found", null);
                            }
                        }
                    #endregion
                    #region manufacturer
                    case "beman":
                        {
                            Class_Manufacturer _manufacturer = new Class_Manufacturer();
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "iman":
                                    {
                                        return _manufacturer.AJAX_Insert(param["baseImage"], param["fnImage"], param["name"], param["email"], param["phone"], param["address"], param["description"], param["active"]);
                                    }
                                case "rman":
                                    {
                                        return _manufacturer.AJAX_GetDetail(param["id"]);
                                    }
                                case "dman":
                                    {
                                        return _manufacturer.AJAX_Delete(param["id"]);
                                    }
                                case "statustoggle":
                                    {
                                        return _manufacturer.AJAX_StatusToggle(param["id"]);
                                    }
                                case "upreload":
                                    {
                                        return _manufacturer.AJAX_GetDetail(param["id"]);
                                    }
                                case "uman":
                                    {
                                        return _manufacturer.AJAX_Update(param["IDManufacturer"], param["baseImage"], param["fnImage"], param["Name"], param["Email"], param["Phone"], param["Address"], param["Description"], param["Active"]);

                                    }
                                default:
                                    return ReturnData.MessageFailed("endpoint not found", null);
                            }
                        }
                    #endregion
                    #region product
                    case "bepro":
                        {
                            Class_Product _product = new Class_Product();
                            Class_Value _value = new Class_Value();
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "preload":
                                    {
                                        return _product.AJAX_Preload_Insert();
                                    }
                                case "rval":
                                    {
                                        return _value.AJAX_GetDataBy_IDAttribute(param["IDAttribute"]);
                                    }
                                case "setcover":
                                    {
                                        return _product.AJAX_ChangeCover_Photo(param["id"]);
                                    }
                                case "uppropos"://up product position
                                    {
                                        return _product.AJAX_UpSequence_Information(param["id"]);
                                    }
                                case "downpropos"://down product position
                                    {
                                        return _product.AJAX_DownSequence_Information(param["id"]);
                                    }
                                case "upreload":
                                    {
                                        return _product.AJAX_Preload_Update(param["id"]);
                                    }
                                case "ipro":
                                    {
                                        List<Dictionary<string, object>> currency = new List<Dictionary<string, object>>();
                                        Dictionary<string, object> item = new Dictionary<string, object>();
                                        item.Add("IDCurrency", 2);//fix dollar
                                        item.Add("PriceBeforeDiscount", param["PriceBeforeDiscount-Dollar"]);//fix dollar
                                        item.Add("Price", param["price-Dollar"]);
                                        currency.Add(item);
                                        return _product.AJAX_Insert_Information(param["IDManufacturer"], param["ReferenceCode"], param["Name"], param["PriceBeforeDiscount"], param["TypeDiscountPercent"], param["Discount"], param["Weight"], param["ShortDescription"], param["Description"], param["Note"], param["Active"], currency);
                                    }
                                case "uproinfo":
                                    {
                                        DataClassesDataContext db = new DataClassesDataContext();
                                        int idProduct = param["IDProduct"];
                                        TBProduct_Currency currency = db.TBProduct_Currencies.Where(x => x.IDCurrency == 2 && x.IDProduct == idProduct).FirstOrDefault();
                                        if (currency != null)
                                        {
                                            currency.Price = (decimal)param["price-Dollar"];
                                            currency.PriceBeforeDiscount = (decimal)param["PriceBeforeDiscount-Dollar"];
                                            db.SubmitChanges();
                                        }
                                        return _product.AJAX_Update_Information(param["IDProduct"], param["ReferenceCode"], param["Name"], param["PriceBeforeDiscount"], param["TypeDiscountPercent"], param["Discount"], param["Weight"], param["ShortDescription"], param["Description"], param["Note"], param["Active"]);
                                    }
                                case "uprocat":
                                    {
                                        List<int> _idCat = new List<int>();
                                        foreach (var item in param["IDCategory"])
                                        {
                                            _idCat.Add(Convert.ToInt32(item));
                                        }
                                        return _product.AJAX_Update_Categories(param["IDProduct"], _idCat.ToArray());
                                    }
                                case "uproseo":
                                    {
                                        return _product.AJAX_Update_Meta(param["IDProduct"], param["Meta"], param["MetaDescription"], param["MetaKeyword"]);
                                    }
                                case "uprodefcat":
                                    {
                                        return _product.AJAX_ChangeDefault_Categories(param["IDProduct_Category"]);
                                    }
                                case "changedefcat":
                                    {
                                        return _product.AJAX_ChangeDefault_Categories(param["idcat"]);
                                    }
                                case "dpro":
                                    {
                                        return _product.AJAX_Delete_Information(param["id"]);
                                    }
                                case "dprophoto":
                                    {
                                        return _product.AJAX_Delete_Photo(param["id"]);
                                    }
                                case "statustoggle":
                                    {
                                        return _product.AJAX_ChangeActive_Information(param["id"]);
                                    }
                                case "iprocom":
                                    {
                                        List<int> _values = new List<int>();
                                        foreach (var item in param["Values"])
                                        {
                                            _values.Add(Convert.ToInt32(item));
                                        }
                                        List<int> _photos = new List<int>();
                                        foreach (var item in param["Photos"])
                                        {
                                            _photos.Add(Convert.ToInt32(item));
                                        }
                                        return _product.AJAX_Insert_Combination(param["IDProduct"], param["ReferenceCode"], param["BasePrice"], param["ImpactPrice"], param["ImpactWeight"], param["Quantity"], _photos.ToArray(), _values.ToArray());
                                    }
                                case "uprocom":
                                    {
                                        List<int> _values = new List<int>();
                                        foreach (var item in param["Values"])
                                        {
                                            _values.Add(Convert.ToInt32(item));
                                        }
                                        List<int> _photos = new List<int>();
                                        foreach (var item in param["Photos"])
                                        {
                                            _photos.Add(Convert.ToInt32(item));
                                        }
                                        return _product.AJAX_Update_Combination(param["IDProduct_Combination"], param["IDProduct"], param["ReferenceCode"], param["BasePrice"], param["ImpactPrice"], param["ImpactWeight"], param["Quantity"], _photos.ToArray(), _values.ToArray());
                                        //return _product.AJAX_Insert_Combination(param["IDProduct"], param["ReferenceCode"], param["BasePrice"], param["ImpactPrice"], param["ImpactWeight"], param["Quantity"], _photos.ToArray(), _values.ToArray());
                                    }
                                case "dprocom":
                                    {
                                        return _product.AJAX_Delete_Combination(param["id"]);
                                    }
                                case "preloaducom":
                                    {
                                        return _product.AJAX_Preload_Update_Combination(param["id"]);
                                    }
                                case "uprocomqty":
                                    {
                                        bool success = true;
                                        bool noerror = true;
                                        List<int> idCom = new List<int>();
                                        foreach (var item in param["IDCombination"])
                                        {
                                            idCom.Add(item);
                                        }

                                        List<int> qty = new List<int>();
                                        foreach (var item in param["qty"])
                                        {
                                            qty.Add(item);
                                        }

                                        for (int i = 0; i < idCom.Count(); i++)
                                        {
                                            noerror = _product.AJAX_Update_Quantity(idCom[i], qty[i]).data;
                                            if (!noerror)
                                            {
                                                success = false;
                                            }
                                        }

                                        if (success)
                                        {
                                            return ReturnData.MessageSuccess("All quantities successfully updated", null);
                                        }
                                        else
                                        {
                                            return ReturnData.MessageFailed("Quantites Failed to update, check input", null);
                                        }
                                    }
                                case "uqty":
                                    {
                                        return _product.AJAX_Update_Quantity(param["id"], param["qty"]);
                                    }
                                default:
                                    return ReturnData.MessageFailed("endpoint not found", null);
                            }
                        }
                    #endregion
                    #region carrier
                    case "becar":
                        {
                            Class_Carrier _carrier = new Class_Carrier();
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "icar":
                                    {
                                        return _carrier.AJAX_Insert(param["baseImage"], param["Information"], param["fnImage"], param["Name"]);
                                    }
                                case "statustoggle":
                                    {
                                        return _carrier.AJAX_StatusToggle(param["IDCarrier"], param["Active"]);
                                    }
                                case "upreload":
                                    {
                                        return _carrier.AJAX_GetDetail(param["IDCarrier"]);
                                    }
                                case "ucar":
                                    {
                                        return _carrier.AJAX_Update(param["IDCarrier"], param["Name"], param["Information"], param["BaseImage"], param["FnImage"]);
                                    }
                                default:
                                    return ReturnData.MessageFailed("endpoint not found", null);
                            }
                        }
                    #endregion
                    #region Shipping
                    case "beship":
                        {
                            Class_Shipping _shipping = new Class_Shipping();
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "rbycar":
                                    {
                                        return _shipping.AJAX_GetByIDCarrier(param["IDCarrier"]);
                                    }
                                case "uprice":
                                    {
                                        return _shipping.AJAX_Update_Price(param["IDShipping"], param["Price"]);
                                    }
                                default:
                                    return ReturnData.MessageFailed("endpoint not found", null);
                            }
                        }
                    #endregion
                    #region Voucher
                    case "bevo":
                        {
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "preload":
                                    {
                                        Class_Customer _cust = new Class_Customer();
                                        Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
                                        result.Add("Customer", _cust.Dynamic_GetAll());
                                        return ReturnData.MessageSuccess("OK", result);
                                    }
                                case "upreload":
                                    {
                                        Class_Customer _cust = new Class_Customer();
                                        Class_Voucher _voucher = new Class_Voucher();
                                        Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
                                        result.Add("Customer", _cust.Dynamic_GetAll());
                                        result.Add("Voucher", _voucher.Dynamic_GetDetail(param["IDVoucher"]));
                                        return ReturnData.MessageSuccess("OK", result);
                                    }
                                case "ivo":
                                    {
                                        Class_Voucher _voucher = new Class_Voucher();
                                        return _voucher.AJAX_Insert(param["IDCustomer"], param["Name"], param["Code"], param["Description"], param["VoucherType"], param["Value"], param["TotalAvailable"], param["MinimumAmount"], param["StartDate"], param["EndDate"], param["Active"]);
                                    }
                                case "dvo":
                                    {
                                        Class_Voucher _voucher = new Class_Voucher();
                                        return _voucher.AJAX_Delete(param["IDVoucher"]);
                                    }
                                case "uvo":
                                    {
                                        Class_Voucher _voucher = new Class_Voucher();
                                        return _voucher.AJAX_Update(param["IDVoucher"], param["IDCustomer"], param["Name"], param["Code"], param["Description"], param["VoucherType"], param["Value"], param["TotalAvailable"], param["MinimumAmount"], param["StartDate"], param["EndDate"], param["Active"]);
                                    }
                                default:
                                    return ReturnData.MessageFailed("Endpoint not found", null);
                            }
                        }
                    #endregion
                    #region MASTER
                    case "bemaster":
                        {
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "preload":
                                    {
                                        Dictionary<string, object> result = new Dictionary<string, object>();
                                        Class_Employee emp = new Class_Employee();
                                        if (emp.DecryptToken(HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()].Value) == null || HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()] == null)
                                            return ReturnData.MessageFailed("Invalid token!", null);
                                        else
                                        {
                                            result.Add("token", HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()].Value);
                                            result.Add("Employee", emp.Dynamic_GetData_By_Token(HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()].Value));
                                        }
                                        foreach (var item in param["RequestData"])
                                        {
                                            if (item == "DetailOrder")
                                            {
                                                Class_Order _order = new Class_Order();
                                                int idOrder = param["IDOrder"];
                                                result.Add("DetailOrder", _order.Dynamic_GetDetail_ByIDOrder(idOrder));
                                            }
                                            if (item == "Invoice")
                                            {
                                                Class_Order order = new Class_Order();
                                                List<int> _idOrd = new List<int>();
                                                foreach (var val in param["IDOrder"])
                                                {
                                                    _idOrd.Add(val);
                                                }
                                                result.Add(item, order.Dynamic_GetDetailInvoice_ByIDOrder(_idOrd));
                                            }
                                            if (item == "EmailConfiguration")
                                            {
                                                Class_Configuration config = new Class_Configuration();
                                                result.Add(item, config.Dynamic_Get_EmailConfiguration());
                                            }
                                            if (item == "MonitoringStock")
                                            {
                                                Class_Product_Combination combination = new Class_Product_Combination();
                                                result.Add(item, combination.Dynamic_GetData_MonitoringStock());
                                            }
                                            if (item == "PaymentMethod")
                                            {
                                                Class_PaymentMethod payment = new Class_PaymentMethod();
                                                result.Add(item, payment.Dynamic_GetDetail_ByIDPayment_Method(param["IDPaymentMethod"]));
                                            }
                                            if (item == "Role")
                                            {
                                                Class_Role role = new Class_Role();
                                                result.Add(item, role.Dynamic_GetAll());
                                            }
                                            if (item == "AutoCancel")
                                            {
                                                Class_Configuration config = new Class_Configuration();
                                                result.Add(item, config.Dynamic_Get_AutoCancel());
                                            }
                                            if (item == "DetailCustomer")
                                            {
                                                Class_Customer customer = new Class_Customer();
                                                int idCustomer = param["IDCustomer"];
                                                result.Add(item, customer.Dynamic_GetData_CustomerDetail(idCustomer));
                                            }
                                            if (item == "ShopInfo")
                                            {
                                                Class_Configuration _config = new Class_Configuration();
                                                result.Add(item, _config.Dynamic_Get_ShopInformation());
                                            }
                                            if (item == "Menu")
                                            {
                                                Class_Menu _menu = new Class_Menu();
                                                result.Add(item, _menu.Dynamic_GetAllMenu_ParentNull());
                                            }
                                            if (item == "RoleMenu")
                                            {
                                                Class_Role _role = new Class_Role();
                                                result.Add(item, _role.Dynamic_GetData_Menu_ByIDRole(param["IDRole"]));
                                            }
                                            if (item == "OrderNotification")
                                            {
                                                Class_Role _role = new Class_Role();
                                                result.Add(item, _role.Dynamic_OrderNotification_ByIDRole(param["IDRole"]));
                                            }
                                            if (item == "RoleOrderStatus")
                                            {
                                                Class_Role _role = new Class_Role();
                                                result.Add(item, _role.Dynamic_GetData_OrderStatus_ByIDRole(param["IDRole"]));
                                            }
                                            if (item == "VeritransConfiguration")
                                            {
                                                Class_Configuration _config = new Class_Configuration();
                                                result.Add(item, _config.Dynamic_Get_VeritransConfig());
                                            }
                                            if (item == "CurrencyDetail")
                                            {
                                                Class_Currency _currency = new Class_Currency();
                                                result.Add(item, _currency.Dynamic_GetDetail(param["IDCurrency"]));
                                            }
                                            if (item == "SystemStatus")
                                            {
                                                Class_Configuration _conf = new Class_Configuration();
                                                result.Add(item, _conf.Dynamic_GetValue_SystemStatus());
                                            }
                                            if (item == "AdminOrderAccount")
                                            {
                                                Class_Customer _cust = new Class_Customer();
                                                result.Add(item, _cust.DYNAMIC_GetData_AdminOrderAccount());
                                            }
                                        }
                                        return ReturnData.MessageSuccess("OK", result);
                                    }
                                default:
                                    return ReturnData.MessageFailed("Endpoint not found", null);
                            }
                        }
                    #endregion
                    #region ORDER
                    case "beord":
                        {
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "rstord"://GET ALL LIST STATUS ORDER
                                    {
                                        Class_Order_Status _status = new Class_Order_Status();
                                        return _status.AJAX_GetAll();
                                    }
                                case "ustord"://UPDATE STATUS ORDER
                                    {
                                        Class_Order _order = new Class_Order();
                                        return _order.AJAX_UpdateOrderStatus(param["IDOrder"], param["IDOrderStatus"], param["ShippingNumber"]);
                                    }
                                case "rconford"://
                                    {
                                        Class_Payment_Confirmation confirm = new Class_Payment_Confirmation();
                                        return ReturnData.MessageSuccess("OK", confirm.Dynamic_GetDetail_ByIDPaymentConfirmation(param["IDPaymentConfirmation"]));
                                    }
                                case "shiplab":
                                    {
                                        Class_Order order = new Class_Order();
                                        List<int> _idOrd = new List<int>();
                                        foreach (var val in param["IDOrder"])
                                        {
                                            _idOrd.Add(val);
                                        }
                                        return order.AJAX_GetShippingLabel(_idOrd);
                                    }
                                case "rstordrole"://GET ALL LIST STATUS ORDER BY ROLE
                                    {
                                        Class_Order_Status _status = new Class_Order_Status();
                                        return _status.AJAX_GetAll_ByIDRole(param["IDRole"]);
                                    }
                                //case "admord": // RIKKI ADMIN ORDER
                                //    {
                                //        Class_Order _order = new Class_Order();
                                //        return _order.AJAX_SubmitOrder_Admin(param);
                                //    }
                                default:
                                    return ReturnData.MessageFailed("Endpoint not found", null);
                            }
                        }
                    #endregion
                    #region CUSTOMER
                    case "becust":
                        {
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "statusToggle":
                                    {
                                        Class_Customer _cust = new Class_Customer();
                                        return _cust.AJAX_BE_StatusToggle(param["IDCustomer"]);
                                    }
                                case "dcust":
                                    {
                                        Class_Customer _cust = new Class_Customer();
                                        return _cust.AJAX_BE_Delete(param["IDCustomer"]);
                                    }
                                default:
                                    return ReturnData.MessageFailed("Endpoint not found", null);
                            }
                        }
                    #endregion
                    #region CONFIGURATION
                    case "beconf":
                        {
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "uconf":
                                    {
                                        Class_Configuration conf = new Class_Configuration();
                                        return conf.AJAX_Update_Configuration(param);
                                    }
                                case "uautocancel":
                                    {
                                        Class_Configuration conf = new Class_Configuration();
                                        return conf.AJAX_Update_AutoCancel(param);
                                    }
                                case "ushopinfo":
                                    {
                                        Class_Configuration conf = new Class_Configuration();
                                        return conf.AJAX_Update_ShopInfo(param["shop_email"], param["shop_city"], param["shop_address"], param["shop_phone"], param["shop_email_logo"], param["BaseImage"]);
                                    }
                                case "lock":
                                    {
                                        Class_Configuration conf = new Class_Configuration();
                                        return conf.AJAX_LockSystem();
                                    }
                                case "regadmord":
                                    {
                                        Class_Customer cust = new Class_Customer();
                                        return cust.AJAX_BE_Register_AdminOrder(param["FirstName"], param["LastName"], param["Gender"], param["Email"], "12345", param["PhoneNumber"], DateTime.Now, false);
                                    }
                                default:
                                    return ReturnData.MessageFailed("endpoint not found", null);
                            }
                        }
                    #endregion
                    #region PAYMENT
                    case "bepay":
                        {
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "ipay":
                                    {
                                        Class_PaymentMethod payment = new Class_PaymentMethod();
                                        return payment.AJAX_Insert(param["Name"], param["Bank"], param["Owner"], param["AccountNumber"], param["Description"], param["Type"], param["BaseImage"], param["FnImage"]);
                                    }
                                case "upay":
                                    {
                                        Class_PaymentMethod payment = new Class_PaymentMethod();
                                        return payment.AJAX_Update(param["IDPaymentMethod"], param["Name"], param["Bank"], param["Owner"], param["AccountNumber"], param["Description"], param["Type"], param["BaseImage"], param["FnImage"]);
                                    }
                                case "dpay":
                                    {
                                        Class_PaymentMethod payment = new Class_PaymentMethod();
                                        return payment.AJAX_Delete(param["IDPaymentMethod"]);
                                    }
                                default:
                                    return ReturnData.MessageFailed("endpoint not found", null);
                            }
                        }
                    #endregion
                    #region DASHBOARD
                    case "admindashboard":
                        {
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "preload":
                                    {
                                        DataClassesDataContext db = new DataClassesDataContext();
                                        Dictionary<string, object> result = new Dictionary<string, object>();
                                        Class_Customer cust = new Class_Customer();
                                        Class_Product _pro = new Class_Product();
                                        Class_Employee emp = new Class_Employee();
                                        if (emp.DecryptToken(HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()].Value) == null || HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()] == null)
                                            return ReturnData.MessageFailed("Invalid token!", null);
                                        else
                                        {
                                            result.Add("token", HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()].Value);
                                            result.Add("Employee", emp.Dynamic_GetData_By_Token(HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()].Value));
                                        }
                                        foreach (var item in param["RequestData"])
                                        {
                                            if (item == "BestSeller")
                                            {
                                                Class_Product product = new Class_Product();
                                                result.Add(item, product.Dynamic_Get_BestSeller(10));
                                            }
                                            if (item == "RecentOrder")
                                            {
                                                Class_Product product = new Class_Product();
                                                result.Add(item, product.Dynamic_Get_RecentOrder(10));
                                            }
                                            if (item == "NewCustomers")
                                            {
                                                Class_Customer Cust = new Class_Customer();
                                                result.Add(item, cust.Dynamic_Get_NewCustomer(10));
                                            }
                                            if (item == "TotalCustomer")
                                            {
                                                Class_Customer Cust = new Class_Customer();
                                                result.Add(item, cust.dynamic_Get_TotalCustomer());
                                            }
                                            if (item == "TotalOrder")
                                            {
                                                Class_Order order = new Class_Order();
                                                result.Add(item, order.Dynamic_Get_TotalOrder());
                                            }
                                            if (item == "AverageOrder")
                                            {
                                                Class_Order order = new Class_Order();
                                                result.Add(item, order.Dynamic_Get_AverageOrder());
                                            }
                                            if (item == "TotalSales")
                                            {
                                                Class_Order order = new Class_Order();
                                                result.Add(item, order.Dynamic_Get_TotalSales());
                                            }
                                            if (item == "ChartRevenue")
                                            {
                                                Class_Order order = new Class_Order();
                                                result.Add(item, order.Dynamic_Get_Chart_Revenue());
                                            }
                                        }
                                        return ReturnData.MessageSuccess("OK", result);
                                    }
                                default:
                                    return ReturnData.MessageFailed("endpoint not found", null);
                            }
                        }
                    #endregion
                    #region LOGIN
                    case "beauth":
                        {
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "login":
                                    {
                                        Class_Employee user = new Class_Employee();
                                        return user.AJAX_BE_Login(param["Email"], param["Password"], param["Remember"]);
                                    }
                                case "logout":
                                    {
                                        Class_Employee emp = new Class_Employee();
                                        return emp.AJAX_FE_Logout();
                                    }
                                case "forgot":
                                    {
                                        Class_Employee emp = new Class_Employee();
                                        return emp.AJAX_Forget_Password(param["Email"]);
                                    }
                                case "reset":
                                    {
                                        Class_Employee emp = new Class_Employee();
                                        return emp.AJAX_Reset_Password(param["Token"], param["Password"]);
                                    }
                                default:
                                    return ReturnData.MessageFailed("Endpoint not found", null);
                            }
                        }
                    #endregion
                    #region EMPLOYEE
                    case "beemp":
                        {
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "iemp":
                                    {
                                        Class_Employee employee = new Class_Employee();
                                        return employee.AJAX_BE_Insert(param["IDRole"], param["Name"], param["Email"], param["Password"]);
                                    }
                                case "uemp":
                                    {
                                        Class_Employee employee = new Class_Employee();
                                        return employee.AJAX_BE_Updates(param["IDEmployee"], param["IDRole"], param["Name"], param["Email"], param["Password"]);
                                    }
                                case "demp":
                                    {
                                        Class_Employee emp = new Class_Employee();
                                        return emp.AJAX_BE_Delete(param["IDEmployee"]);
                                    }
                                case "statusToggle":
                                    {
                                        Class_Employee emp = new Class_Employee();
                                        return emp.AJAX_BE_StatusToggle(param["IDEmployee"]);
                                    }
                                case "detemp":
                                    {
                                        Class_Employee emp = new Class_Employee();
                                        return emp.AJAX_GetDetail(param["IDEmployee"]);
                                    }
                                case "uprofile":
                                    {
                                        Class_Employee emp = new Class_Employee();
                                        var cookie = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()];
                                        if (cookie == null)
                                            return ReturnData.MessageFailed("token is empty", null);
                                        return emp.AJAX_BE_Update_MyProfile_With_Token(cookie.Value, param["Name"], param["Email"], param["Password"]);
                                    }
                                case "detprofile":
                                    {
                                        Class_Employee emp = new Class_Employee();
                                        var cookie = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()];
                                        if (cookie == null)
                                            return ReturnData.MessageFailed("token is empty", null);
                                        return ReturnData.MessageSuccess("OK", emp.Dynamic_GetData_By_Token(cookie.Value));
                                    }
                                case "initadm":
                                    {
                                        Class_Employee emp = new Class_Employee();
                                        return emp.Init_Admin();
                                    }
                                default:
                                    return ReturnData.MessageFailed("Endpoint not found", null);
                            }
                        }
                    #endregion
                    #region PAGES
                    case "bepag":
                        {
                            Class_Page _page = new Class_Page();
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "preload":
                                    {
                                        return _page.AJAX_GetAll();
                                    }
                                case "ipag":
                                    {
                                        return _page.AJAX_Insert(param["pageTitle"], param["pageShortContent"], param["pageContent"], param["active"]);
                                    }
                                case "upaginfo":
                                    {
                                        return _page.AJAX_BE_Updates(param["IDPage"], param["pageTitle"], param["pageShortContent"], param["pageContent"]);
                                    }
                                case "dpag":
                                    {
                                        return _page.AJAX_BE_Delete(param["IDPage"]);
                                    }
                                case "detpag":
                                    {
                                        return _page.AJAX_GetDetail(param["IDPage"]);
                                    }
                                case "statusToggle":
                                    {
                                        return _page.AJAX_BE_StatusToggle(param["IDPage"]);
                                    }
                                case "dpagphoto":
                                    {
                                        return _page.AJAX_Delete_Photo(param["IDPageMedia"]);
                                    }
                                case "setcover":
                                    {
                                        return _page.AJAX_ChangeCover_Photo(param["IDPageMedia"]);
                                    }
                                case "upagphoinfo":
                                    {
                                        Class_Page_Media _pageMedia = new Class_Page_Media();
                                        return _pageMedia.AJAX_Updates(param["IDPageMedia"], param["Title"], param["Description"]);
                                    }
                                case "detpagphoto":
                                    {
                                        return _page.AJAX_GetDetailPhoto(param["IDPageMedia"]);
                                    }
                                default:
                                    return ReturnData.MessageFailed("endpoint not found", null);
                            }
                        }
                    #endregion
                    #region PAGE CATEGORY
                    case "bepagcat":
                        {
                            Class_Page_Category _pageCategory = new Class_Page_Category();
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "ipagcat":
                                    {
                                        return _pageCategory.AJAX_Insert(param["name"], param["description"], param["idPage"]);
                                    }
                                case "upagcat":
                                    {
                                        return _pageCategory.AJAX_Updates(param["idPageCategory"], param["name"], param["description"]);
                                    }
                                case "dpagcat":
                                    {
                                        return _pageCategory.AJAX_Delete(param["idPageCategory"]);
                                    }
                                case "upreload":
                                    {
                                        return _pageCategory.AJAX_GetDetail(param["idPageCategory"]);
                                    }
                                default:
                                    return ReturnData.MessageFailed("endpoint not found", null);
                            }
                        }
                    #endregion
                    #region POST
                    case "bepost":
                        {
                            Class_Post _post = new Class_Post();
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "ipost":
                                    {
                                        return _post.AJAX_Insert(param["postTitle"], param["postShortContent"], param["postContent"], param["idPage"], param["active"]);
                                    }
                                case "upost":
                                    {
                                        return _post.AJAX_Updates(param["IDPost"], param["postTitle"], param["postShortContent"], param["postContent"]);
                                    }
                                case "dpost":
                                    {
                                        return _post.AJAX_Delete(param["IDPost"]);
                                    }
                                case "detpost":
                                    {
                                        return _post.AJAX_GetDetail(param["IDPost"]);
                                    }
                                case "detpagcatpost":
                                    {
                                        return _post.AJAX_GetPageCategory(param["IDPage"], param["IDPost"]);
                                    }
                                case "statusToggle":
                                    {
                                        return _post.AJAX_StatusToggle(param["IDPost"]);
                                    }
                                case "dpostphoto":
                                    {
                                        return _post.AJAX_Delete_Photo(param["IDPostMedia"]);
                                    }
                                case "upostphoinfo":
                                    {
                                        Class_Post_Media _postMedia = new Class_Post_Media();
                                        return _postMedia.AJAX_Updates(param["IDPostMedia"], param["Title"], param["Description"]);
                                    }
                                case "detpostphoto":
                                    {
                                        return _post.AJAX_GetDetailPhoto(param["IDPostMedia"]);
                                    }
                                case "upostpagcat":
                                    {
                                        List<int> _idCat = new List<int>();
                                        foreach (var item in param["IDPageCategory"])
                                        {
                                            _idCat.Add(Convert.ToInt32(item));
                                        }
                                        return _post.AJAX_Update_Categories(param["IDPost"], param["IDPage"], _idCat.ToArray());
                                    }
                                default:
                                    return ReturnData.MessageFailed("endpoint not found", null);
                            }
                        }
                    #endregion
                    #region REPORT
                    case "berep":
                        {
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "preload":
                                    {
                                        DataClassesDataContext db = new DataClassesDataContext();
                                        Dictionary<string, object> result = new Dictionary<string, object>();
                                        Class_Customer cust = new Class_Customer();
                                        Class_Product _pro = new Class_Product();
                                        Class_Employee emp = new Class_Employee();
                                        if (emp.DecryptToken(HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()].Value) == null || HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()] == null)
                                            return ReturnData.MessageFailed("Invalid token!", null);
                                        else
                                        {
                                            result.Add("token", HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()].Value);
                                            result.Add("Employee", emp.Dynamic_GetData_By_Token(HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()].Value));
                                        }
                                        foreach (var item in param["RequestData"])
                                        {
                                            if (item == "TotalCustomer")
                                            {
                                                Class_Customer Cust = new Class_Customer();
                                                if (param["startDate"] != null && param["endDate"] != null)
                                                {
                                                    DateTime startDate = DateTime.ParseExact(param["startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                                    DateTime endDate = DateTime.ParseExact(param["endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                                                    result.Add(item, cust.dynamic_Get_TotalCustomer_FilterDate(startDate, endDate));
                                                }
                                                else
                                                {
                                                    result.Add(item, cust.dynamic_Get_TotalCustomer());
                                                }
                                            }
                                            if (item == "TotalOrder")
                                            {
                                                Class_Order order = new Class_Order();
                                                if (param["startDate"] != null && param["endDate"] != null)
                                                {
                                                    DateTime startDate = DateTime.ParseExact(param["startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                                    DateTime endDate = DateTime.ParseExact(param["endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                                                    result.Add(item, order.Dynamic_Get_TotalOrder_FilterDate(startDate, endDate));
                                                }
                                                else
                                                {
                                                    result.Add(item, order.Dynamic_Get_TotalOrder());
                                                }
                                            }
                                            if (item == "TotalSales")
                                            {
                                                Class_Report report = new Class_Report();
                                                if (param["startDate"] != null && param["endDate"] != null)
                                                {
                                                    DateTime startDate = DateTime.ParseExact(param["startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                                    DateTime endDate = DateTime.ParseExact(param["endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                                                    result.Add(item, report.Dynamic_Get_TotalSales_FilterDate(startDate, endDate));
                                                }
                                                else
                                                {
                                                    result.Add(item, report.Dynamic_Get_TotalSales());
                                                }
                                            }
                                            if (item == "TotalItemsSold")
                                            {
                                                Class_Order order = new Class_Order();
                                                if (param["startDate"] != null && param["endDate"] != null)
                                                {
                                                    DateTime startDate = DateTime.ParseExact(param["startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                                    DateTime endDate = DateTime.ParseExact(param["endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                                                    result.Add(item, order.Dynamic_Get_TotalItemsSold_FilterDate(startDate, endDate));
                                                }
                                                else
                                                {
                                                    result.Add(item, order.Dynamic_Get_TotalItemsSold());
                                                }
                                            }
                                            if (item == "TotalSalesVoucher")
                                            {
                                                Class_Order order = new Class_Order();
                                                if (param["startDate"] != null && param["endDate"] != null)
                                                {
                                                    DateTime startDate = DateTime.ParseExact(param["startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                                    DateTime endDate = DateTime.ParseExact(param["endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                                                    result.Add(item, order.Dynamic_Get_TotalSalesVoucher_FilterDate(startDate, endDate));
                                                }
                                                else
                                                {
                                                    result.Add(item, order.Dynamic_Get_TotalSalesVoucher());
                                                }
                                            }
                                        }
                                        return ReturnData.MessageSuccess("OK", result);
                                    }
                                default:
                                    return ReturnData.MessageFailed("endpoint not found", null);
                            }
                        }
                    #endregion
                    #region NEWSLETTER
                    case "benews":
                        {
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "dnews":
                                    {
                                        Class_Newsletter newsletter = new Class_Newsletter();
                                        return newsletter.AJAX_Delete(param["IDNewsletter"]);
                                    }
                                default:
                                    return ReturnData.MessageFailed("Endpoint not found", null);
                            }
                        }
                    #endregion
                    #region ROLE
                    case "berole":
                        {
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "irole":
                                    {
                                        Class_Role role = new Class_Role();
                                        return role.AJAX_BE_Insert(param["Name"]);
                                    }
                                case "urole":
                                    {
                                        Class_Role role = new Class_Role();
                                        return role.AJAX_BE_Updates(param["IDRole"], param["Name"]);
                                    }
                                case "drole":
                                    {
                                        Class_Role role = new Class_Role();
                                        return role.AJAX_BE_Delete(param["IDRole"]);
                                    }
                                case "detrole":
                                    {
                                        Class_Role role = new Class_Role();
                                        return role.AJAX_GetDetail(param["IDRole"]);
                                    }
                                case "urolemenu":
                                    {
                                        Class_Role role = new Class_Role();
                                        List<int> _idMenu = new List<int>();
                                        foreach (var item in param["IDMenu"])
                                        {
                                            _idMenu.Add(Convert.ToInt32(item));
                                        }
                                        return role.AJAX_Update_Menu(param["IDRole"], _idMenu.ToArray());
                                    }
                                case "uroleordstat":
                                    {
                                        Class_Role role = new Class_Role();
                                        List<int> _idOrderStatus = new List<int>();
                                        foreach (var item in param["IDOrderStatus"])
                                        {
                                            _idOrderStatus.Add(Convert.ToInt32(item));
                                        }
                                        return role.AJAX_Update_OrderStatus(param["IDRole"], _idOrderStatus.ToArray());
                                    }
                                case "unotif":
                                    {
                                        Class_Role role = new Class_Role();
                                        return role.AJAX_Notification_OrderStatus(param["IDRole"], param["IDOrderStatus"]);
                                    }
                                default:
                                    return ReturnData.MessageFailed("Endpoint not found", null);
                            }
                        }
                    #endregion
                    #region CURRENCY
                    case "becurr":
                        {
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "ucurr":
                                    {
                                        Class_Currency currency = new Class_Currency();
                                        return currency.AJAX_BE_Updates(param["IDCurrency"], param["Name"], param["ISOCode"], param["ISOCodeNumeric"], param["Sign"], param["ConversionRate"]);
                                    }
                                default:
                                    return ReturnData.MessageFailed("Endpoint not found", null);
                            }
                        }
                    #endregion
                    #region LOG
                    case "belog":
                        {
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "dlogerror":
                                    {
                                        Class_Log_Error logError = new Class_Log_Error();
                                        return logError.AJAX_Delete(param["ID"]);
                                    }
                                default:
                                    return ReturnData.MessageFailed("Endpoint not found", null);
                            }
                        }
                    #endregion
                    #endregion
                    #region FRONTEND
                    #region MASTER
                    case "femaster":
                        {
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "preload":
                                    {
                                        DataClassesDataContext db = new DataClassesDataContext();
                                        Dictionary<string, object> result = new Dictionary<string, object>();
                                        Class_Customer cust = new Class_Customer();
                                        Class_Product _pro = new Class_Product();

                                        foreach (var item in param["RequestData"])
                                        {
                                            if (item == "OrderSummary")
                                            {
                                                Class_Order _order = new Class_Order();
                                                var cookie = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()];
                                                if (cookie != null)
                                                {
                                                    var _result = cust.DecryptToken(cookie.Value) as IDictionary<string, object>;
                                                    result.Add("OrderSummary", _order.Dynamic_GetLastOrder_ByEmailCustomer(_result["email"].ToString()));
                                                }
                                            }
                                            if (item == "ProductDetail")
                                            {
                                                result.Add(item, _pro.AJAX_FE_GetDetail_Preload(param["_param_IDProduct"]).data);
                                            }
                                            if (item == "Cart")
                                            {
                                                Class_Order _order = new Class_Order();
                                                //if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()] != null)
                                                if (_order.GetEncodedDataOrder(HttpContext.Current.Request.AnonymousID) != null)
                                                {
                                                    result.Add("Cart", _order.DYNAMIC_GetCart());
                                                }
                                            }
                                            if (item == "CartSummary")
                                            {
                                                Class_Order _order = new Class_Order();
                                                //if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()] != null)
                                                if (_order.GetEncodedDataOrder(HttpContext.Current.Request.AnonymousID) != null)
                                                {
                                                    result.Add(item, _order.DYNAMIC_GetCartSummary());
                                                }
                                            }
                                            if (item == "Customer")
                                            {
                                                if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()] == null)
                                                    result.Add("Customer", null);
                                                else
                                                {
                                                    string token = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()].Value;
                                                    var _result = cust.DecryptToken(token) as IDictionary<string, object>;
                                                    result.Add("token", token);
                                                    result.Add("Customer", cust.DYNAMIC_GetData_ByEmailAndPassword(_result["email"].ToString(), _result["password"].ToString()));
                                                }
                                            }
                                            if (item == "ListProduct")
                                            {
                                                Class_Product _product = new Class_Product();
                                                Class_Category _category = new Class_Category();
                                                if (param["IDManufacturer"] != 0)
                                                    result.Add(item, _product.Dynamic_GetData_ByIDManufacturer(param["IDManufacturer"]));
                                                else if (param["IDCategory"] != 0)
                                                {
                                                    result.Add(item, _product.Dynamic_GetData_ByIDCategory(param["IDCategory"], param["_param_take"]));
                                                    result.Add("ProductCategory", _category.Dynamic_FE_GetDetail(param["IDCategory"]));
                                                    result.Add("TotalProduct", _product.AJAX_FE_GetTotalProduct_ByIDCategory(param["IDCategory"]));
                                                }
                                                else if (param["IDValue"] != null && param["PriceRange"] != null)
                                                {
                                                    List<int> _idVal = new List<int>();
                                                    foreach (var val in param["IDValue"])
                                                    {
                                                        _idVal.Add(val);
                                                    }
                                                    List<int> _priceRange = new List<int>();
                                                    foreach (var val in param["PriceRange"])
                                                    {
                                                        _priceRange.Add(val);
                                                    }
                                                    result.Add(item, _product.Dynamic_GetData_ByIDValue_And_PriceRange(_idVal, _priceRange, param["_param_take"]));
                                                }
                                                else
                                                {
                                                    result.Add("ListProduct", _pro.AJAX_FE_GetAll(param["_param_take"]).data);
                                                    result.Add("TotalProduct", _product.AJAX_FE_GetTotalProduct());
                                                }


                                            }
                                            if (item == "ListProductPaging")
                                            {
                                                Class_Product _product = new Class_Product();
                                                Class_Category _category = new Class_Category();
                                                if (param["FilterCategory"])
                                                {
                                                    result.Add("ProductByCategory", _product.Dynamic_GetData_FilterCategory(param["_param_take"], param["IDParent"]));
                                                }
                                                else if (param["IDManufacturer"] != 0)
                                                    result.Add(item, _product.Dynamic_GetData_ByIDManufacturer_Paging(param["IDManufacturer"], param["_page"]));

                                                else if (param["IDCategory"] != 0)
                                                {
                                                    if (param["IDValue"] != null && param["PriceRange"] != null)
                                                    {
                                                        //List<int> _idCat = new List<int>();
                                                        //_idCat.Add(param["IDCategory"]);
                                                        List<int> _idVal = new List<int>();
                                                        foreach (var val in param["IDValue"])
                                                        {
                                                            _idVal.Add(val);
                                                        }
                                                        List<decimal> _priceRange = new List<decimal>();
                                                        foreach (var val in param["PriceRange"])
                                                        {
                                                            _priceRange.Add(val);
                                                        }

                                                        result.Add(item, _product.Dynamic_GetData_ByIDValue_And_IDCategory_And_PriceRange_Paging(_idVal, _priceRange, param["IDCategory"], param["_page"]));
                                                    }
                                                    else
                                                    {
                                                        result.Add(item, _product.Dynamic_GetData_ByIDCategory_Paging(param["IDCategory"], param["_page"]));
                                                        result.Add("ProductCategory", _category.Dynamic_FE_GetDetail(param["IDCategory"]));
                                                        result.Add("TotalProduct", _product.AJAX_FE_GetTotalProduct_ByIDCategory(param["IDCategory"]));
                                                    }
                                                }
                                                else if (param["IDValue"] != null && param["PriceRange"] != null)
                                                {
                                                    List<int> _idVal = new List<int>();
                                                    foreach (var val in param["IDValue"])
                                                    {
                                                        _idVal.Add(val);
                                                    }
                                                    List<decimal> _priceRange = new List<decimal>();
                                                    foreach (var val in param["PriceRange"])
                                                    {
                                                        _priceRange.Add(val);
                                                    }
                                                    result.Add(item, _product.Dynamic_GetData_ByIDValue_And_PriceRange_Paging(_idVal, _priceRange, param["_page"]));
                                                }
                                                else
                                                {
                                                    result.Add(item, _pro.AJAX_FE_GetAll_Paging(param["_page"]).data);
                                                    result.Add("TotalProduct", _product.AJAX_FE_GetTotalProduct());
                                                }
                                            }
                                            if (item == "Country")
                                            {
                                                Class_Region region = new Class_Region();
                                                result.Add("Country", region.DYNAMIC_GetCountry());
                                            }
                                            if (item == "Province")
                                            {
                                                Class_Region region = new Class_Region();
                                                result.Add("Province", region.DYNAMIC_GetProvince_ByIDCountry(1));
                                            }
                                            if (item == "Addresses")
                                            {
                                                Class_Address addr = new Class_Address();
                                                Class_Customer _cust = new Class_Customer();
                                                if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()] == null)
                                                    return ReturnData.MessageFailed("Please login/register", null);
                                                else
                                                {
                                                    string token = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()].Value;
                                                    dynamic _result = OurClass.DecryptToken(token);
                                                    var customer = _cust.DYNAMIC_GetData_ByEmailAndPassword(_result["email"].ToString(), _result["password"].ToString());
                                                    //result.Add("Addresses", addr.Dynamci_GetAll_ByIDCustomer(_result));
                                                    result.Add("Addresses", addr.Dynamic_GetAll_ByIDCustomer(customer.IDCustomer));
                                                }
                                            }
                                            if (item == "Address")
                                            {
                                                Class_Address addr = new Class_Address();
                                                result.Add(item, addr.Dynamic_GetDetail_ByIDAddress(param["_param_IDAddress"]));
                                            }
                                            if (item == "Shipping")
                                            {
                                                Class_Order _order = new Class_Order();
                                                //if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()] != null)
                                                var cookie = _order.GetEncodedDataOrder(HttpContext.Current.Request.AnonymousID);
                                                if (cookie != null)
                                                {
                                                    //string token = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()].Value;
                                                    string token = cookie.EncodedData;
                                                    dynamic CartData = OurClass.DecryptToken(token);
                                                    //dynamic
                                                    dynamic address = CartData["DeliveryAddress"];
                                                    int idDistrict = address["IDDistrict"];
                                                    Class_Shipping shipping = new Class_Shipping();
                                                    dynamic shippingData = shipping.Dynamic_GetShipping_ByIDDistrict(idDistrict);
                                                    List<dynamic> resultShipping = new List<dynamic>();
                                                    foreach (dynamic itemShipping in shippingData)
                                                    {
                                                        //decimal totalWeight = Math.Round((decimal)CartData["TotalWeight"], MidpointRounding.AwayFromZero);
                                                        decimal totalWeight = Math.Ceiling((decimal)CartData["TotalWeight"]); // update shipping witcommerce
                                                        decimal totalPrice = 0;
                                                        decimal shippingPrice = 0;

                                                        totalPrice = Class_Currency.GetPriceConversionCurrency(itemShipping.Price) * totalWeight;
                                                        shippingPrice = Class_Currency.GetPriceConversionCurrency(itemShipping.Price);
                                                        if (CartData["TotalWeight"] < 1)
                                                            totalPrice = shippingPrice;

                                                        Dictionary<string, dynamic> returnShippingData = new Dictionary<string, dynamic>();
                                                        returnShippingData.Add("IDCarrier", itemShipping.IDCarrier);
                                                        returnShippingData.Add("IDShipping", itemShipping.IDShipping);
                                                        returnShippingData.Add("IDDistrict", itemShipping.IDDistrict);
                                                        returnShippingData.Add("ImageShipping", itemShipping.Image);
                                                        returnShippingData.Add("Name", itemShipping.Name);
                                                        returnShippingData.Add("Price", shippingPrice); //FREE SHIPPING SAMPAI AKHIR APRIL
                                                        returnShippingData.Add("Information", itemShipping.Information);
                                                        returnShippingData.Add("TotalPrice", totalPrice); //FREE SHIPPING SAMPAI AKHIR APRIL

                                                        resultShipping.Add(returnShippingData);
                                                    }
                                                    result.Add(item, resultShipping);
                                                }

                                            }
                                            if (item == "Payment")
                                            {
                                                Class_PaymentMethod payment = new Class_PaymentMethod();
                                                result.Add(item, payment.Dynamic_GetAll());
                                            }
                                            if (item == "OrderHistory")
                                            {
                                                Class_Order _order = new Class_Order();
                                                if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()] == null)
                                                    result.Add("OrderHistory", null);
                                                else
                                                {
                                                    string token = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()].Value;
                                                    var _result = cust.DecryptToken(token) as IDictionary<string, object>;
                                                    var customer = cust.DYNAMIC_GetData_ByEmailAndPassword(_result["email"].ToString(), _result["password"].ToString());
                                                    result.Add(item, _order.Dynamic_GetData_ByIDCustomer(customer.IDCustomer));
                                                }

                                            }
                                            if (item == "OrderNumber")
                                            {
                                                Class_Order _order = new Class_Order();
                                                if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()] == null)
                                                    result.Add("OrderHistory", null);
                                                else
                                                {
                                                    string token = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()].Value;
                                                    var _result = cust.DecryptToken(token) as IDictionary<string, object>;
                                                    var customer = cust.DYNAMIC_GetData_ByEmailAndPassword(_result["email"].ToString(), _result["password"].ToString());
                                                    result.Add(item, _order.Dynamic_GetData_OrderNumber_ByIDCustomer(customer.IDCustomer));
                                                }
                                            }
                                            if (item == "SimiliarProduct")
                                            {
                                                Class_Product _product = new Class_Product();
                                                result.Add(item, _product.Dynamic_GetSimiliarProduct(param["_param_IDProduct"], 4));
                                            }
                                            if (item == "FilterProduct")
                                            {
                                                Class_Category cat = new Class_Category();
                                                result.Add(item, cat.Dynamic_GetFilter());
                                            }
                                            if (item == "Voucher")
                                            {
                                                Class_Voucher voucher = new Class_Voucher();
                                                result.Add(item, voucher.Dynamic_GetData_ByIDCustomer(param["IDCustomer"]));
                                            }
                                            if (item == "Category")
                                            {
                                                Class_Category cat = new Class_Category();
                                                result.Add(item, cat.Dynamic_GetAll(db));
                                            }
                                            if (item == "AutoCancel")
                                            {
                                                Class_Configuration config = new Class_Configuration();
                                                result.Add(item, config.AJAX_AutoCancel());
                                            }
                                            if (item == "Post")
                                            {
                                                Class_Post post = new Class_Post();
                                                result.Add(item, post.Dynamic_FE_GetPost_ByIDPageCategory(db, param["IDPageCategory"]));
                                            }
                                            if (item == "CategoryChild")
                                            {
                                                Class_Category cat = new Class_Category();
                                                result.Add(item, cat.Dynamic_GetDataBy_IDCategoryParent(db, param["IDCategoryParent"]));
                                            }
                                            if (item == "Value")
                                            {
                                                Class_Value _value = new Class_Value();
                                                Class_Attribute _att = new Class_Attribute();
                                                List<int> _idVal = new List<int>();
                                                foreach (var id in param["IDAttribute"])
                                                {
                                                    string name = _att.Dynamic_GetDetail(db, id).Name;
                                                    result.Add(name, _value.AJAX_FE_GetDataBy_IDAttribute(id));
                                                }
                                            }
                                            if (item == "Currency")
                                            {
                                                Class_Currency currency = new Class_Currency();
                                                result.Add(item, currency.GetAllCurrencies(db));
                                            }
                                            if (item == "Manufacturer")
                                            {
                                                Class_Manufacturer _manufacturer = new Class_Manufacturer();
                                                result.Add(item, _manufacturer.Dynamic_GetAll(db));
                                            }
                                            if (item == "Wishlist")
                                            {
                                                Class_Wishlist wish = new Class_Wishlist();
                                                if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()] == null)
                                                    result.Add(item, null);
                                                else
                                                {
                                                    string token = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()].Value;
                                                    var _result = cust.DecryptToken(token) as IDictionary<string, object>;
                                                    var customer = cust.DYNAMIC_GetData_ByEmailAndPassword(_result["email"].ToString(), _result["password"].ToString());
                                                    result.Add(item, wish.Dynamic_GetData_ByEmail(_result["email"].ToString()));
                                                }
                                            }
                                            if (item == "CustomerProduct")
                                            {
                                                Class_SAAS saas = new Class_SAAS();
                                                if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()] == null)
                                                    result.Add(item, null);
                                                else
                                                {
                                                    string token = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()].Value;
                                                    var _result = cust.DecryptToken(token) as IDictionary<string, object>;
                                                    var customer = cust.DYNAMIC_GetData_ByEmailAndPassword(_result["email"].ToString(), _result["password"].ToString());
                                                    result.Add(item, saas.GetAllCustomerProduct_ByIDCustomer(customer.IDCustomer));
                                                }
                                            }
                                            if (item == "ExpiredNotification")
                                            {
                                                Class_SAAS saas = new Class_SAAS();
                                                if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()] == null)
                                                    result.Add(item, null);
                                                else
                                                {
                                                    string token = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()].Value;
                                                    var _result = cust.DecryptToken(token) as IDictionary<string, object>;
                                                    var customer = cust.DYNAMIC_GetData_ByEmailAndPassword(_result["email"].ToString(), _result["password"].ToString());
                                                    result.Add(item, saas.GetNotificationExpiredPackage(customer.IDCustomer));
                                                }
                                            }
                                        }
                                        return ReturnData.MessageSuccess("OK", result);
                                    }
                                case "logout":
                                    {
                                        Class_Customer cust = new Class_Customer();
                                        if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()] == null)
                                            return ReturnData.MessageFailed("Cookie not found", null);
                                        else
                                            return cust.AJAX_FE_Logout(HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()].Value.ToString());
                                    }
                                case "inews":
                                    {
                                        Class_Newsletter _newsletter = new Class_Newsletter();
                                        return _newsletter.AJAX_Insert(param["email"]);
                                    }
                                case "contactus":
                                    {
                                        Class_Configuration _config = new Class_Configuration();
                                        string body = "";
                                        using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/admin-contactus.html")))
                                        {

                                            body += reader.ReadToEnd();
                                            body = body.Replace("{CustomerName}", param["Name"]);
                                            body = body.Replace("{CustomerEmail}", param["Email"]);
                                            body = body.Replace("{CustomerPhone}", param["Phone"]);
                                            body = body.Replace("{Message}", param["Message"]);

                                        }
                                        var emailUser = _config.Dynamic_Get_ShopEmail();
                                        return OurClass.SendEmail(emailUser, WebConfigurationManager.AppSettings["Title"] + " Customer Contact", body, "");
                                    }
                                default:
                                    return ReturnData.MessageFailed("Endpoint not found", null);
                            }
                        }
                    #endregion
                    #region PRODUCT
                    case "fepro":
                        {
                            Class_Product _product = new Class_Product();
                            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "prolist":
                                    {
                                        return _product.AJAX_FE_GetAll(param["take"]);
                                    }
                                case "preloaddet":
                                    {
                                        return _product.AJAX_FE_GetDetail_Preload(param["IDProduct"]);
                                    }
                                case "oncolorchg":
                                    {
                                        DataClassesDataContext db = new DataClassesDataContext();
                                        Class_Product_Combination _combination = new Class_Product_Combination();
                                        result.Add("Size", db.SP_GetSizeCombination_ByIDProductAndIDColor(param["IDProduct"], param["IDColor"]));
                                        result.Add("PhotoCombination", _combination.Dynamic_GetData_Photo_ByIDProductCombination(param["IDCombination"]));
                                        return ReturnData.MessageSuccess("OK", result);
                                    }
                                case "rproman":
                                    {
                                        return _product.AJAX_GetData_ByIDManufacturer(param["IDManufacturer"]);
                                    }
                                //get best seller product
                                case "rbest":
                                    {
                                        return _product.AJAX_GetBestSeller(0);
                                    }
                                case "rsale":
                                    {
                                        return _product.AJAX_GetSale();
                                    }
                                case "filter":
                                    {
                                        return _product.AJAX_Filter_Product(param["IDCategoryProduct"], param["IDCategoryGender"], param["IDCategoryPrice"]);
                                    }
                                case "search":
                                    {
                                        return _product.AJAX_Search_Product(param["Keywords"]);
                                    }
                                case "onpagechg":
                                    {
                                        return _product.AJAX_FE_GetAll_Paging(param["CurrentPage"]);
                                    }
                                case "onpagechg_byidman":
                                    {
                                        return _product.Dynamic_GetData_ByIDManufacturer_Paging(param["IDManufacturer"], param["CurrentPage"]);
                                    }
                                case "onpagechg_byidcat":
                                    {
                                        return _product.Dynamic_GetData_ByIDCategory_Paging(param["IDCategory"], param["CurrentPage"]);
                                    }
                                case "onpagechg_byidval_price":
                                    {
                                        List<int> _idVal = new List<int>();
                                        foreach (var val in param["IDValue"])
                                        {
                                            _idVal.Add(val);
                                        }
                                        List<decimal> _priceRange = new List<decimal>();
                                        foreach (var val in param["PriceRange"])
                                        {
                                            _priceRange.Add(val);
                                        }
                                        return _product.Dynamic_GetData_ByIDValue_And_PriceRange_Paging(_idVal, _priceRange, param["CurrentPage"]);
                                    }
                                default:
                                    return ReturnData.MessageFailed("Endpoint not found", null);
                            }
                        }
                    #endregion
                    #region REGISTER
                    case "fereg":
                        {
                            dynamic param = data["data"];
                            Class_Customer cust = new Class_Customer();
                            switch ((string)data["m"])
                            {
                                case "reg":
                                    {
                                        DateTime _birthdate = Convert.ToDateTime(param["Birthdate"]);
                                        string address = param["Address"];
                                        if (param["Address2"] != "")
                                            address += "<br/>" + param["Address2"];
                                        return cust.AJAX_FE_Register(param["FirstName"], param["LastName"], param["Gender"], param["Email"], param["Password"], param["PhoneNumber"], _birthdate, param["Newsletter"], param["AddressName"], address, param["PostalCode"], param["Country"], param["Province"], param["City"], param["District"]);
                                    }
                                case "regpreload":
                                    {
                                        Class_Region _region = new Class_Region();
                                        return _region.AJAX_FE_PreloadRegistration();
                                    }
                                case "rcty":
                                    {
                                        Class_Region _region = new Class_Region();
                                        return _region.AJAX_FE_GetCity_ByIDProvince(param["IDProvince"]);
                                    }
                                case "rdis":
                                    {
                                        Class_Region _region = new Class_Region();
                                        return _region.AJAX_FE_GetDistrict_ByIDCity(param["IDCity"]);
                                    }
                                //Guest register
                                case "greg":
                                    {
                                        DateTime _birthdate = Convert.ToDateTime(param["Birthdate"]);
                                        return cust.AJAX_FE_RegisterGuest(param["FirstName"], param["LastName"], param["Gender"], param["Email"], param["Email"], param["PhoneNumber"], _birthdate, true, "guest address", param["Address"], param["PostalCode"], param["Country"], param["Province"], param["City"], param["District"]);
                                    }
                                default:
                                    return ReturnData.MessageFailed("Endpoint not found", null);
                            }
                        }
                    #endregion
                    #region AUTH
                    case "feauth":
                        {
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "login":
                                    {
                                        Class_Customer _cust = new Class_Customer();
                                        if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()] == null)
                                            return _cust.AJAX_FE_Login(param["e"], param["p"]);
                                        else
                                            return ReturnData.MessageFailed("Already login", null);
                                    }
                                case "forgot":
                                    {
                                        Class_Customer _cust = new Class_Customer();
                                        return _cust.AJAX_Forget_Password(param["Email"]);
                                    }
                                case "reset":
                                    {
                                        Class_Customer cust = new Class_Customer();
                                        return cust.AJAX_Reset_Password(param["Token"], param["Password"]);
                                    }
                                case "EmailCheck":
                                    {
                                        Class_Customer _cust = new Class_Customer();
                                        return _cust.AJAX_FE_Email_Check(param["e"]);
                                    }
                                default:
                                    return ReturnData.MessageFailed("Endpoint not found", null);
                            }
                        }
                    #endregion
                    #region ORDER
                    case "feorder":
                        {
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "addcart":
                                    {
                                        Class_Order _order = new Class_Order();
                                        return _order.DYNAMIC_AddToCart(param["IDProduct"], param["IDCombination"], param["Quantity"], param["Price"], param["CombinationName"], param["ProductName"], param["OrderType"]);
                                    }
                                case "delcart":
                                    {
                                        Class_Order _order = new Class_Order();
                                        return _order.AJAX_DeleteCart(param["IDCombination"]);
                                    }
                                case "getcart":
                                    {
                                        Class_Order _order = new Class_Order();
                                        return _order.AJAX_GetCart();
                                    }
                                case "iorder":
                                    {
                                        Class_Order _order = new Class_Order();
                                        return _order.AJAX_SubmitOrder();
                                    }
                                default:
                                    return ReturnData.MessageFailed("Endpoint not found", null);
                            }
                        }
                    #endregion
                    #region SUMMARY
                    case "fesum":
                        {
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "vo"://submit voucher
                                    {
                                        Class_Voucher vo = new Class_Voucher();
                                        return vo.AJAX_SubmitVoucher(param["Code"], param["Token"], param["TotalAmount"]);
                                    }
                                case "uprc"://update price setelah submit voucher
                                    {
                                        Class_Order order = new Class_Order();
                                        return order.Dynamic_SetTotalPrice(param["Price"]);
                                    }
                                case "dpro":
                                    {
                                        Class_Order order = new Class_Order();
                                        return order.AJAX_DeleteCart(param["IDCombination"]);
                                    }
                                case "uqty":
                                    {
                                        Class_Order order = new Class_Order();
                                        return order.AJAX_UpdateQuantity(param["IDCombination"], param["Quantity"]);
                                    }
                                case "clearcart":
                                    {
                                        Class_Order order = new Class_Order();
                                        return order.AJAX_ClearCart();
                                    }
                                default:
                                    return ReturnData.MessageFailed("Endpoint not found", null);
                            }
                        }
                    #endregion
                    #region ADDRESS
                    case "feaddr":
                        {
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "iaddr":
                                    {
                                        Class_Address addr = new Class_Address();
                                        return addr.AJAX_Insert(param["IDCustomer"], param["Country"], param["Province"], param["City"], param["District"], param["PeopleName"], param["Name"], param["Address"], param["Phone"], param["PostalCode"], param["AdditionalInformation"]);
                                    }
                                case "uaddr":
                                    {
                                        Class_Address addr = new Class_Address();
                                        return addr.AJAX_Update(param["IDAddress"], param["IDCustomer"], param["Country"], param["Province"], param["City"], param["District"], param["PeopleName"], param["Name"], param["Address"], param["Phone"], param["PostalCode"], param["AdditionalInformation"], true);
                                    }
                                case "daddr":
                                    {
                                        Class_Address addr = new Class_Address();
                                        return addr.AJAX_Delete(param["IDAddress"], param["IDCustomer"]);
                                    }
                                case "detaddr":
                                    {
                                        Class_Address addr = new Class_Address();
                                        return addr.AJAX_GetDetail(param["IDAddress"], param["IDCustomer"]);
                                    }
                                case "iaddrtocrt":
                                    {
                                        Class_Order order = new Class_Order();
                                        return order.AJAX_SaveAddressToCart(param["IDBillingAddress"], param["IDDeliveryAddress"], param["Notes"]);
                                    }
                                default:
                                    return ReturnData.MessageFailed("endpoint not found", null);
                            }
                        }
                    #endregion
                    #region SHIPPING
                    case "feshp":
                        {
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "ishptocrt":
                                    {
                                        Class_Shipping shipping = new Class_Shipping();
                                        return shipping.AJAX_SaveShippingToCart(param["IDShipping"]);
                                    }
                                default:
                                    return ReturnData.MessageFailed("endpoint not found", null);
                            }
                        }
                    #endregion
                    #region Payment
                    case "fepay":
                        {
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "ipaytocart":
                                    {
                                        Class_PaymentMethod payment = new Class_PaymentMethod();
                                        return payment.AJAX_Insert_ToCart(param["IDPayment"]);
                                    }
                                default:
                                    return ReturnData.MessageFailed("endpoint not found", null);
                            }
                        }
                    #endregion
                    #region CONFRIM PAYMENT
                    case "fecopay":
                        {
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "icopay":
                                    {
                                        Class_Payment_Confirmation confirm = new Class_Payment_Confirmation();
                                        return confirm.AJAX_Submit(param["IDOrder"], param["Name"], param["Email"], param["PhoneNumber"], param["Bank"], param["Amount"], param["PaymentDate"], param["Notes"], param["baseImage"], param["fnImage"]);
                                    }
                                case "getidorder":
                                    {
                                        Class_Order _order = new Class_Order();
                                        return _order.Dynamic_GetData_IDOrder_ByReference(param["Reference"]);
                                    }
                                default:
                                    return ReturnData.MessageFailed("endpoint not found", null);
                            }
                        }
                    #endregion
                    #region CUSTOMER
                    case "fecust":
                        {
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "uinfo":
                                    {
                                        Class_Customer cust = new Class_Customer();
                                        if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()] == null)
                                            return ReturnData.MessageFailed("Customer not found", null);
                                        else
                                        {
                                            DateTime _birthdate = Convert.ToDateTime(param["Birthdate"]);
                                            return cust.AJAX_Update_PersonalInformation(param["IDCustomer"], param["FirstName"], param["LastName"], param["Gender"], param["Email"], param["PhoneNumber"], _birthdate, param["IsSubscribe"]);
                                        }
                                    }
                                case "upass":
                                    {
                                        Class_Customer cust = new Class_Customer();
                                        if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()] == null)
                                            return ReturnData.MessageFailed("Customer not found", null);
                                        else
                                        {
                                            return cust.AJAX_Update_Password(param["IDCustomer"], param["OldPassword"], param["NewPassword"]);
                                        }
                                    }
                                case "detorder":
                                    {
                                        Class_Order order = new Class_Order();
                                        return order.AJAX_GetDetail_By_IDOrder(param["IDOrder"]);
                                    }
                                default:
                                    return ReturnData.MessageFailed("endpoint not found", null);
                            }
                        }
                    #endregion
                    #region VERITRANS
                    //VT-WEB Veritrans Transaction charge (VT-Web)
                    case "fevt":
                        {
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "vtwebtrx":
                                    {
                                        Class_PaymentMethod pm = new Class_PaymentMethod();

                                        Class_Order order = new Class_Order();
                                        Class_Configuration _config = new Class_Configuration();
                                        DataClassesDataContext db = new DataClassesDataContext();
                                        ReturnData resultPayment = pm.AJAX_Insert_ToCart(param["IDPaymentMethod"]);
                                        //if (!resultPayment.success)
                                        //    return resultPayment;

                                        //GET DYNAMIC CONFIG VERITRANS
                                        string merchant_id = db.TBConfigurations.Where(x => x.Name.Contains("veritrans_id")).FirstOrDefault().Value;
                                        string client_key = db.TBConfigurations.Where(x => x.Name.Contains("veritrans_client")).FirstOrDefault().Value;
                                        string server_key = db.TBConfigurations.Where(x => x.Name.Contains("veritrans_server")).FirstOrDefault().Value;
                                        string api_url = db.TBConfigurations.Where(x => x.Name.Contains("veritrans_api")).FirstOrDefault().Value;

                                        ReturnData resultSaveOrder = order.Object_SubmitOrder_veritrans(param["IDPaymentMethod"]) as ReturnData;
                                        if (!resultSaveOrder.success)
                                            return resultSaveOrder;

                                        VeritransClientConf conf = new VeritransClientConf(merchant_id, client_key, server_key);
                                        TBOrder detailOrder = order.GetDetail_ByIDOrder((resultSaveOrder.data as TBOrder).IDOrder);

                                        TransactionDetails trxDetail = new TransactionDetails(detailOrder.Reference, (int)detailOrder.TotalPaid);
                                        BillingAddress billingAddress = new BillingAddress(detailOrder.TBAddress.PeopleName, "", detailOrder.TBAddress.Address, detailOrder.TBAddress.TBCity.Name, detailOrder.TBAddress.PostalCode, detailOrder.TBAddress.Phone, "IDN");
                                        ShippingAddress shippingAddress = new ShippingAddress(detailOrder.TBAddress1.PeopleName, "", detailOrder.TBAddress1.Address, detailOrder.TBAddress1.TBCity.Name, detailOrder.TBAddress1.PostalCode, detailOrder.TBAddress1.Phone, "IDN");
                                        CustomerDetails customer = new CustomerDetails(detailOrder.TBCustomer.FirstName, detailOrder.TBCustomer.LastName, detailOrder.TBCustomer.Email, detailOrder.TBCustomer.PhoneNumber, billingAddress, shippingAddress);
                                        List<ItemDetail> items = new List<ItemDetail>();

                                        foreach (var item in detailOrder.TBOrder_Details)
                                        {
                                            items.Add(new ItemDetail(item.IDProduct_Combination.ToString(), (int)item.Price, item.Quantity, item.Product_Name));
                                        }

                                        items.Add(new ItemDetail("shipping", (int)detailOrder.TotalShipping, 1, "Shipping"));

                                        //REMOVE COOKIE
                                        //var cookieCart = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()];
                                        var cookieCart = order.GetEncodedDataOrder(HttpContext.Current.Request.AnonymousID);
                                        var cookieVoucher = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()];
                                        if (cookieVoucher != null && detailOrder.TBVoucher != null)
                                        {
                                            items.Add(new ItemDetail("_vcr_", (int)detailOrder.TotalDiscount * -1, 1, "voucher : " + detailOrder.TBVoucher.Name));
                                            HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Expires = DateTime.Now.AddDays(-1);
                                        }
                                        if (cookieCart != null)
                                        {
                                            //HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()].Expires = DateTime.Now.AddDays(-1);
                                            order.DeleteEncodeDataOrder(HttpContext.Current.Request.AnonymousID);
                                        }

                                        VeritransClient VTClient = new VeritransClient("vtweb", new Vtweb(true), trxDetail, items, customer, null, null, null, null);
                                        TransactionChargeResponse result = VTClient.ExecuteTransactionCharge(conf, VTClient, @api_url);

                                        if (result.redirect_url != null)
                                        {
                                            return ReturnData.MessageSuccess(result.status_message, result.redirect_url);
                                        }
                                        return ReturnData.MessageFailed(result.status_message, null);
                                    }
                                default:
                                    return ReturnData.MessageFailed("endpoint not found", null);
                            }
                        }
                    #endregion
                    #region CURRENCY
                    case "fecur":
                        {
                            Dictionary<string, object> param = new Dictionary<string, object>();
                            param = data["data"] as Dictionary<string, object>;
                            switch ((string)data["m"])
                            {
                                case "chgcur":
                                    {
                                        if (!param.ContainsKey("ID"))
                                            return ReturnData.MessageFailed("ID is not specified", null);

                                        if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCurrency"].ToString()] == null)
                                        {

                                            HttpContext.Current.Response.Cookies.Add(new HttpCookie(System.Configuration.ConfigurationManager.AppSettings["cookieCurrency"].ToString(), param["ID"].ToString()));

                                        }

                                        else
                                            HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCurrency"].ToString()].Value = param["ID"].ToString();


                                        Class_Order order = new Class_Order();
                                        order.Dynamic_ChangeCurrencyCart((int)param["ID"]);
                                        return ReturnData.MessageSuccess("success", param["ID"].ToString());
                                    }
                                default:
                                    return ReturnData.MessageFailed("endpoint not found", null);
                            }
                        }
                    #endregion
                    #region WISHLIST
                    case "fewish":
                        {
                            dynamic param = data["data"];
                            switch ((string)data["m"])
                            {
                                case "iwish":
                                    {
                                        Class_Wishlist wish = new Class_Wishlist();
                                        Class_Customer cust = new Class_Customer();
                                        if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()] == null)
                                            return wish.AJAX_Insert(null, param["IDCombination"], param["Email"]);
                                        else
                                        {
                                            var customer = cust.Dynamic_GetData_ByIDCustomer(param["IDCustomer"]);
                                            return wish.AJAX_Insert(customer.IDCustomer, param["IDCombination"], customer.Email);
                                        }
                                    }
                                case "dwish":
                                    {
                                        Class_Wishlist wish = new Class_Wishlist();
                                        return wish.AJAX_Delete(param["IDWishlist"]);
                                    }
                                default:
                                    return ReturnData.MessageFailed("endpoint not found", null);
                            }
                        }
                    #endregion
                    #endregion
                    default:
                        return ReturnData.MessageFailed("endpoint not found", null);
                }
            }
            else
            {
                return "format error";
            }

        }
        catch (Exception ex)
        {
            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
}
