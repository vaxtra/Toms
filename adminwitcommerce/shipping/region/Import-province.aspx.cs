using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CsvHelper;
using System.IO;

public partial class adminwitcommerce_shipping_region_Import : System.Web.UI.Page
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
                fuFile.SaveAs(Server.MapPath("~/assets/import/region/" + fuFile.FileName));
                StreamReader sr = new StreamReader(Server.MapPath("~/assets/import/region/" + fuFile.FileName));
                try
                {
                    CsvReader csvread = new CsvReader(sr);
                    csvread.Configuration.Delimiter = ";";
                    csvread.Configuration.HasHeaderRecord = true;

                    List<CSV_Province> record = csvread.GetRecords<CSV_Province>().ToList();
                    int totalRead = 0;
                    int totalInsert = 0;
                    foreach (var item in record)
                    {
                        totalRead++;
                        if (db.TBCountries.Where(X => X.IDCountry == item.IDCountry).FirstOrDefault() != null)
                        {
                            if (db.TBProvinces.Where(x => x.Name.ToLower() == item.Name.ToLower()).FirstOrDefault() == null)
                            {
                                TBProvince newProvince = new TBProvince { IDCountry = item.IDCountry, Name = item.Name, Deflag = false, DateInsert = DateTime.Now, DateLastUpdate = DateTime.Now };
                                db.TBProvinces.InsertOnSubmit(newProvince);
                                totalInsert++;
                            }
                        }
                    }
                    db.SubmitChanges();
                    sr.Close();
                    lblSuccess.Text = totalRead + " row(s) read, " + totalInsert + " row(s) inserted";
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