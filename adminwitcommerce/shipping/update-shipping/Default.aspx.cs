using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adminwitcommerce_shipping_update_shipping_Default : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    bool existCountry;
    bool existProvince;
    bool existCity;
    bool existDistrict;
    int totalRead = 0;
    int totalInsert = 0;
    int totalUpdate = 0;
    string updateData;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private int IsDistrictExists(string districtName)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            if (db.TBDistricts.Where(x => !x.Deflag && x.Name.ToLower() == districtName.ToLower()).FirstOrDefault() == null)
                return 0;
            else
                return db.TBDistricts.Where(x => !x.Deflag && x.Name.ToLower() == districtName.ToLower()).FirstOrDefault().IDDistrict;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    private int ImportCountry(string name)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var existData = db.TBCountries.Where(x => !x.Deflag && x.Name.ToLower() == name.ToLower()).FirstOrDefault();
            if (existData == null)
            {
                TBCountry newData = new TBCountry();
                newData.Name = name;
                newData.Deflag = false;
                newData.DateInsert = DateTime.Now;
                newData.DateLastUpdate = DateTime.Now;
                db.TBCountries.InsertOnSubmit(newData);
                db.SubmitChanges();
                existProvince = false;
                return newData.IDCountry;
            }
            else
            {
                existProvince = true;
                return existData.IDCountry;
            }

        }
        catch (Exception)
        {

            throw;
        }
    }

    private int ImportProvince(string name, int idCountry)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var existData = db.TBProvinces.Where(x => !x.Deflag && x.Name.ToLower() == name.ToLower()).FirstOrDefault();
            if (existData == null)
            {
                TBProvince newData = new TBProvince();
                newData.IDCountry = idCountry;
                newData.Name = name;
                newData.Deflag = false;
                newData.DateInsert = DateTime.Now;
                newData.DateLastUpdate = DateTime.Now;
                db.TBProvinces.InsertOnSubmit(newData);
                db.SubmitChanges();
                existProvince = false;
                return newData.IDProvince;
            }
            else
            {
                if (existData.IDCountry == idCountry)
                {
                    return existData.IDProvince;
                }
                else
                {
                    TBProvince newData = new TBProvince();
                    newData.IDCountry = idCountry;
                    newData.Name = name;
                    newData.Deflag = false;
                    newData.DateInsert = DateTime.Now;
                    newData.DateLastUpdate = DateTime.Now;
                    db.TBProvinces.InsertOnSubmit(newData);
                    db.SubmitChanges();
                    existProvince = false;
                    return newData.IDProvince;
                }
                
            }
                
        }
        catch (Exception)
        {

            throw;
        }
    }

    private int ImportCity(string name, int idProvince, int idCountry, string cityType)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var existData = db.TBCities.Where(x => !x.Deflag && x.Name.ToLower() == name.ToLower()).FirstOrDefault();
            if (existData == null)
            {
                TBCity newData = new TBCity();
                newData.IDProvince = idProvince;
                newData.Name = name;
                newData.CityType = cityType;
                newData.Deflag = false;
                newData.DateInsert = DateTime.Now;
                newData.DateLastUpdate = DateTime.Now;
                db.TBCities.InsertOnSubmit(newData);
                db.SubmitChanges();
                existCity = false;
                return newData.IDCity;
            }
            else
            {
                if (existData.IDProvince == idProvince)
                {
                    return existData.IDCity;
                }
                else
                {
                    TBCity newData = new TBCity();
                    newData.IDProvince = idProvince;
                    newData.Name = name;
                    newData.CityType = cityType;
                    newData.Deflag = false;
                    newData.DateInsert = DateTime.Now;
                    newData.DateLastUpdate = DateTime.Now;
                    db.TBCities.InsertOnSubmit(newData);
                    db.SubmitChanges();
                    existCity = false;
                    return newData.IDCity;
                }
            }
                
        }
        catch (Exception)
        {

            throw;
        }
    }

    private int ImportDistrict(string name, int idCity, int idProvince, int idCountry)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var existData = db.TBDistricts.Where(x => !x.Deflag && x.Name.ToLower().Trim().Replace(" ", "") == name.ToLower().Trim().Replace(" ", "") && x.IDCity == idCity).FirstOrDefault();
            if (existData == null)
            {
                TBDistrict newData = new TBDistrict();
                newData.IDCity = idCity;
                newData.Name = name;
                newData.Deflag = false;
                newData.DateInsert = DateTime.Now;
                newData.dateLastUpdate = DateTime.Now;
                db.TBDistricts.InsertOnSubmit(newData);
                db.SubmitChanges();
                return newData.IDDistrict;
            }
            else
            {
                if (existData.IDCity == idCity)
                {
                    return existData.IDDistrict;
                }
                else
                {
                    TBDistrict newData = new TBDistrict();
                    newData.IDCity = idCity;
                    newData.Name = name;
                    newData.Deflag = false;
                    newData.DateInsert = DateTime.Now;
                    newData.dateLastUpdate = DateTime.Now;
                    db.TBDistricts.InsertOnSubmit(newData);
                    db.SubmitChanges();
                    return newData.IDDistrict;
                }
            }
                
        }
        catch (Exception)
        {

            throw;
        }
    }

    private int ImportShipping(int idCarrier, int idDistrict, decimal price)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var existData = db.TBShippings.Where(x => !x.Deflag && x.IDDistrict == idDistrict && x.IDCarrier == idCarrier).FirstOrDefault();
            if (existData == null)
            {
                TBShipping newData = new TBShipping();
                newData.IDCarrier = idCarrier;
                newData.IDDistrict = idDistrict;
                newData.Price = price;
                newData.Deflag = false;
                newData.DateInsert = DateTime.Now;
                newData.DateLastUpdate = DateTime.Now;
                db.TBShippings.InsertOnSubmit(newData);
                db.SubmitChanges();
                totalInsert++;
                return newData.IDShipping;

                
            }
            else
            {
                existData.Price = price;
                db.SubmitChanges();
                totalUpdate++;
                updateData += existData.TBDistrict.TBCity.TBProvince.TBCountry.Name + " - " + existData.TBDistrict.TBCity.TBProvince.Name + " - " + existData.TBDistrict.TBCity.Name + " - " + existData.TBDistrict.Name + " - " + existData.Price + "<br/>";
                return existData.IDShipping;
            }

        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (Path.GetExtension(fuFile.FileName).ToLower() != ".csv")
        {
            lblError.Text = "file format must be .csv";
        }
        else
        {
            if (fuFile.HasFile)
            {
                lblSuccess.Text = "";
                lblError.Text = "";
                try
                {
                    DataClassesDataContext db = new DataClassesDataContext();
                    fuFile.SaveAs(Server.MapPath("~/assets/import/shipping/" + fuFile.FileName));
                    StreamReader sr = new StreamReader(Server.MapPath("~/assets/import/shipping/" + fuFile.FileName));
                    try
                    {
                        CsvReader csvread = new CsvReader(sr);
                        csvread.Configuration.Delimiter = ";";
                        csvread.Configuration.HasHeaderRecord = false;
                        char[] delimeter = { ',' };

                        List<CSV_Shipping_New> record = csvread.GetRecords<CSV_Shipping_New>().ToList();

                        foreach (var item in record)
                        {
                            existDistrict = false;
                            existCity = false;
                            existDistrict = false;
                            totalRead++;
                            #region KALO Artikel Master PRODUCT BELUM ADA

                            int idCountry = ImportCountry(item.CountryName);
                            int idProvince = ImportProvince(item.ProvinceName, idCountry);
                            int idCity = ImportCity(item.CityName, idProvince, idCountry, item.CityType);
                            int idDistrict = ImportDistrict(item.DistrictName, idCity, idProvince, idCountry);
                            if (item.Price.Trim() != "-")
                            {
                                ImportShipping(item.IDCarrier, idDistrict, decimal.Parse(item.Price));
                            }
                            

                            
                            #endregion
                            //if (db.TBProvinces.Where(X => X.IDProvince == item.IDProvince).FirstOrDefault() != null)
                            //{
                            //    if (db.TBCities.Where(x => x.Name.ToLower() == item.Name.ToLower()).FirstOrDefault() == null)
                            //    {
                            //        TBCity newCity = new TBCity { IDProvince = item.IDProvince, Name = item.Name, CityType = item.CityType, Deflag = false, DateInsert = DateTime.Now, DateLastUpdate = DateTime.Now };
                            //        db.TBCities.InsertOnSubmit(newCity);
                            //        totalInsert++;
                            //    }
                            //    else
                            //    {
                            //        lblSuccess.Text += item.Name + "--";
                            //    }
                            //}
                            //else
                            //{
                            //    lblSuccess.Text += item.Name + "(" + item.IDProvince + ")--";
                            //}
                        }

                        sr.Close();
                        lblSuccess.Text += totalRead + " row(s) read, " + totalInsert + " row(s) inserted, " + totalUpdate + " row(s) updated";
                        lblReportUpdate.Text += updateData;
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
}