using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adminwitcommerce_shipping_carrier_Import_shipping : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (fuFile.HasFile)
        {
            try
            {
                DataClassesDataContext db = new DataClassesDataContext();
                fuFile.SaveAs(Server.MapPath("~/assets/import/shipping/" + fuFile.FileName));
                StreamReader sr = new StreamReader(Server.MapPath("~/assets/import/shipping/" + fuFile.FileName));
                try
                {
                    CsvReader csvread = new CsvReader(sr);
                    csvread.Configuration.Delimiter = ";";
                    csvread.Configuration.HasHeaderRecord = true;

                    List<CSV_Shipping> record = csvread.GetRecords<CSV_Shipping>().ToList();
                    int totalRead = 0;
                    int totalInsert = 0;
                    foreach (var item in record)
                    {
                        totalRead++;
                        if (db.TBShippings.Where(x => x.IDCarrier == item.IDCarrier && x.IDDistrict == item.IDDistrict).FirstOrDefault() == null)
                        {
                            TBShipping newShipping = new TBShipping { IDCarrier = item.IDCarrier, IDDistrict = item.IDDistrict, Price = item.Price, Deflag = false, DateInsert = DateTime.Now, DateLastUpdate = DateTime.Now };
                            db.TBShippings.InsertOnSubmit(newShipping);
                            totalInsert++;
                        }
                        else
                        {
                            lblSuccess.Text += "Error duplicate <br/>";
                        }
                    }
                    db.SubmitChanges();
                    sr.Close();
                    lblSuccess.Text += totalRead + " row(s) read, " + totalInsert + " row(s) inserted";
                    alertError.Style.Add(HtmlTextWriterStyle.Display, "none");
                    alertSuccess.Style.Add(HtmlTextWriterStyle.Display, "block");
                }
                catch (Exception ex)
                {
                    sr.Close();
                    lblError.Text = ex.Message;
                    alertError.Style.Add(HtmlTextWriterStyle.Display, "block");
                    alertSuccess.Style.Add(HtmlTextWriterStyle.Display, "none");
                }

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                alertError.Style.Add(HtmlTextWriterStyle.Display, "block");
                alertSuccess.Style.Add(HtmlTextWriterStyle.Display, "none");
            }
        }
    }
}