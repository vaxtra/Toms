using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adminwitcommerce_catalog_import_product_import_inventory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (fuFile.HasFile)
        {
            lblSuccess.Text = "";
            lblError.Text = "";
            try
            {
                DataClassesDataContext db = new DataClassesDataContext();

                Class_Employee emp = new Class_Employee();
                var test = emp.DecryptToken(HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()].Value);
                if (emp.DecryptToken(HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()].Value) != null && HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()] != null)
                {

                    fuFile.SaveAs(Server.MapPath("~/assets/import/inventory/" + fuFile.FileName));
                    StreamReader sr = new StreamReader(Server.MapPath("~/assets/import/inventory/" + fuFile.FileName));
                    try
                    {
                        CsvReader csvread = new CsvReader(sr);
                        csvread.Configuration.Delimiter = ";";
                        csvread.Configuration.HasHeaderRecord = false;
                        char[] delimeter = { ',' };

                        List<CSV_Inventory> record = csvread.GetRecords<CSV_Inventory>().ToList();
                        int totalRead = 0;
                        int totalNotfound = 0;
                        int totalUpdate = 0;
                        foreach (var item in record)
                        {
                            totalRead++;
                            TBProduct_Combination data = db.TBProduct_Combinations.Where(x => !x.Deflag && x.TBProduct.ReferenceCode == item.ReferenceCodeProduct && x.ReferenceCode == item.ReferenceCodeCombination).FirstOrDefault();
                            var employee = emp.GetData_By_Token(HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()].Value);
                            if (data != null)
                            {
                                string jenis="";
                                data.Quantity = item.Quantity;
                                if(data.Quantity > item.Quantity)
                                    jenis="decrease";
                                else
                                    jenis="increase";
                                db.SP_Update_Qty(item.ReferenceCodeCombination, item.Quantity);
                                totalUpdate++;

                                TBLog_Stock log = new TBLog_Stock();
                                log.Description = "import inventory";
                                log.IDEmployee = employee.IDEmployee;
                                log.IDProduct_Combination = data.IDProduct_Combination;
                                log.ProductName = data.TBProduct.Name;
                                log.UserName = employee.Name;
                                log.Type = jenis;
                                log.Quantity = item.Quantity;
                                log.InsertDate = DateTime.Now;

                                db.TBLog_Stocks.InsertOnSubmit(log);
                                db.SP_Insert_LogStock("import inventory", employee.IDEmployee, data.IDProduct_Combination, data.TBProduct.Name, employee.Name, jenis, item.Quantity, DateTime.Now);
                            }
                            else
                            {
                                totalNotfound++;
                            }
                        }

                        sr.Close();
                        lblSuccess.Text += totalRead + " row(s) read, " + totalUpdate + " row(s) updated, " + totalNotfound + " row(s) not found data";
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