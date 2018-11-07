using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GenerateProductCurrency : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            List<TBProduct_Currency> listItem = new List<TBProduct_Currency>();
            int counter = 0;
            foreach (var product in db.TBProducts)
            {
                foreach (var currency in db.TBCurrencies.Where(x => x.IsDefault == false))
                {
                    TBProduct_Currency item = new TBProduct_Currency();
                    item.IDCurrency = currency.IDCurrency;
                    item.IDProduct = product.IDProduct;
                    listItem.Add(item);
                }
                counter++;
            }

            db.TBProduct_Currencies.InsertAllOnSubmit(listItem);
            db.SubmitChanges();
            string text = "total product :" + counter.ToString() + " , Total inserted data :" + listItem.Count.ToString();
            Response.Write(text);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}