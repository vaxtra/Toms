using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using CsvHelper;
using System.Text;

public partial class adminwitcommerce_orders_orders_JsonPage : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["start"] != null && Request.QueryString["end"] != null)
            {
                GenerateJsonOrder(DateTime.Parse(Request.QueryString["start"]), DateTime.Parse(Request.QueryString["end"]));
            }
            else
            {
                Response.Write("Please enter start date and end date query string <br/> EX : titatoe.com/adminwitcommerce/JsonPage.aspx?start=2015/1/1&end=2015/1/1");
            }
        }
    }

    private void GenerateJsonOrder(DateTime startDate, DateTime endDate)
    {
        var order = db.TBOrders.OrderByDescending(x => x.IDOrder).Where(X => X.DateInsert >= startDate && X.DateInsert <= endDate && !X.Deflag).OrderByDescending(x => x.IDOrder).Select(x => new
        {
            IDOrder = x.IDOrder,
            Reference = x.Reference,
            OrderStatus = x.TBOrder_Status.Name,
            InvoiceNumber = x.InvoiceNumber,
            PaymentMethod = x.Payment,
            ShippingNumber = x.ShippingNumber,
            TotalWeight = x.TotalWeight,
            TotalShipping = x.TotalShipping,
            TotalDiscountVoucher = x.TotalDiscount,
            TotalPrice = x.TotalPrice,
            TotalPaid = x.TotalPaid,
            Note = x.Notes,
            DateInsert = x.DateInsert,
            DateLastUpdate = x.DateLastUpdate,
            OrderDetail = x.TBOrder_Details.Where(y => y.IDOrder == x.IDOrder).Select(y => new
            {
                ProductCode = y.TBProduct.ReferenceCode,
                ProductName = y.TBProduct.Name,
                CombinationName = y.Product_Name,
                Weight = y.Weight,
                DiscountProduct = y.Discount,
                Price = y.Price,
                OriginalPrice = y.Price,
                Quantity = y.Quantity,
            })
        });


        string json = JsonConvert.SerializeObject(order, Formatting.Indented);
        Response.Write(json);
    }
}