using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adminwitcommerce_catalog_product_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnExportProduct_Click(object sender, EventArgs e)
    {

        DataClassesDataContext db = new DataClassesDataContext();
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Product_" + DateTime.Now.ToShortDateString() + ".xls"));
        Response.ContentType = "application/ms-excel";

        Class_Product _product = new Class_Product();

        var data = db.TBProducts;
        string category = "";
        DataTable dt = new DataTable();
        dt.Columns.Add("ProductCode", typeof(string));
        dt.Columns.Add("CombinationCode", typeof(string));
        dt.Columns.Add("Manufacturer", typeof(string));
        dt.Columns.Add("Categories", typeof(string));
        dt.Columns.Add("ProductName", typeof(string));
        dt.Columns.Add("Size", typeof(string));
        dt.Columns.Add("Color", typeof(string));
        dt.Columns.Add("Hexacolor", typeof(string));
        dt.Columns.Add("Price", typeof(string));
        dt.Columns.Add("Weight", typeof(string));
        dt.Columns.Add("Quantity", typeof(string));
        dt.Columns.Add("Description", typeof(string));
        dt.Columns.Add("ShortDescription", typeof(string));
        dt.Columns.Add("Active", typeof(string));
        foreach (var item in data)
        {
            foreach (var categories in item.TBProduct_Categories)
            {
                category += categories.TBCategory.Name + ", ";
            }
            foreach (var combination in item.TBProduct_Combinations)
            {
                //string[] comname = combination.Name.Split(new[] { ',' }, 2);
                dt.Rows.Add(item.ReferenceCode, combination.ReferenceCode, item.TBManufacturer.Name, category, item.Name, combination.Name.ToLower().Replace("size : ", ""), "-", "-", item.Price, item.Weight, combination.Quantity, "-", "-", item.Active);
            }
        }
        string str = string.Empty;

        foreach (DataColumn dtcol in dt.Columns)
        {
            Response.Write(dtcol.ColumnName);
            Response.Write("\t");
        }
        Response.Write("\n");
        foreach (DataRow dr in dt.Rows)
        {
            str = "";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                Response.Write(Convert.ToString(dr[i]));
                Response.Write("\t");
            }
            Response.Write("\n");
        }
        Response.End();

    }
}