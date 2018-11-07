using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adminwitcommerce_shipping_region_import_city : System.Web.UI.Page
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

                    List<CSV_City> record = csvread.GetRecords<CSV_City>().ToList();
                    int totalRead = 0;
                    int totalInsert = 0;
                    foreach (var item in record)
                    {
                        totalRead++;
                        if (db.TBProvinces.Where(X => X.IDProvince == item.IDProvince).FirstOrDefault() != null)
                        {
                            if (db.TBCities.Where(x => x.Name.ToLower() == item.Name.ToLower()).FirstOrDefault() == null)
                            {
                                TBCity newCity = new TBCity { IDProvince = item.IDProvince, Name = item.Name, CityType = item.CityType, Deflag = false, DateInsert = DateTime.Now, DateLastUpdate = DateTime.Now };
                                db.TBCities.InsertOnSubmit(newCity);
                                totalInsert++;
                            }
                            else
                            {
                                lblSuccess.Text += item.Name + "--";
                            }
                        }
                        else
                        {
                            lblSuccess.Text += item.Name + "(" + item.IDProvince + ")--";
                        }
                    }
                    db.SubmitChanges();
                    foreach (TBCity item in db.TBCities)
                    {
                        if (item.CityType == "KO")
                            item.Name = ("KOTA " + item.Name).Trim();
                        else if (item.CityType == "KA")
                            item.Name = ("KAB " + item.Name).Trim();

                        //if (item.CityType == "KO")
                        //    item.Name = item.Name.Substring(1);
                        //else if (item.CityType == "KA")
                        //    item.Name = item.Name.Substring(1);

                        db.SubmitChanges();
                    }
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