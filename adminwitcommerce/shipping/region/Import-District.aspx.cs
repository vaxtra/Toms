using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adminwitcommerce_shipping_region_Import_District : System.Web.UI.Page
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

                    List<CSV_District> record = csvread.GetRecords<CSV_District>().ToList();
                    int totalRead = 0;
                    int totalInsert = 0;
                    string wrongCity = "";
                    foreach (var item in record)
                    {


                        string namaKota = "";
                        namaKota = item.CityName.Trim().ToLower();
                        string type = item.CityName.Substring(0, 2).ToUpper();
                        totalRead++;
                        var city = db.TBCities.Where(X => X.Name.Trim().ToLower() == namaKota).FirstOrDefault();
                        if (city != null)
                        {
                            if (db.TBCities.Where(X => X.Name.ToLower() == namaKota).Count() == 1)
                            {
                                totalInsert++;
                                TBDistrict newDist = new TBDistrict { IDCity = city.IDCity, Name = item.Name, Deflag = false, DateInsert = DateTime.Now, dateLastUpdate = DateTime.Now };
                                db.TBDistricts.InsertOnSubmit(newDist);
                            }
                            else
                                wrongCity += "<br />" + namaKota + "(" + db.TBCities.Where(X => X.CityType == type && X.Name.ToLower() == namaKota).Count() + ")";
                            //}
                            //else
                            //{
                            //    lblSuccess.Text += item.Name + "--";
                            //}
                        }
                        else
                        {
                            wrongCity += "<br/>" + namaKota;
                            //lblSuccess.Text += item.Name + "(" + item.IDProvince + ")--";
                        }
                    }
                    db.SubmitChanges();
                    sr.Close();
                    if (wrongCity.Length > 0)
                        lblSuccess.Text += totalRead + " row(s) read, " + totalInsert + " row(s) inserted, ERROR CITY :" + wrongCity;
                    else
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