using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adminwitcommerce_customer_customer_import : System.Web.UI.Page
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
                //if (emp.DecryptToken(HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()].Value) == null || HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()] == null)
                {

                    fuFile.SaveAs(Server.MapPath("~/assets/import/customer/" + fuFile.FileName));
                    StreamReader sr = new StreamReader(Server.MapPath("~/assets/import/customer/" + fuFile.FileName));
                    try
                    {
                        CsvReader csvread = new CsvReader(sr);
                        csvread.Configuration.Delimiter = ";";
                        csvread.Configuration.HasHeaderRecord = false;
                        char[] delimeter = { ',' };

                        List<CSV_Customer> record = csvread.GetRecords<CSV_Customer>().ToList();
                        int totalRead = 0;
                        int totalNotfound = 0;
                        int totalUpdate = 0;

                        List<TBCustomer> listCustomer = new List<TBCustomer>();
                        foreach (var item in record)
                        {
                            totalRead++;
                            TBCustomer cust = db.TBCustomers.Where(x => !x.Deflag && x.IDCustomer_Group == 1 && x.Email == item.Email).FirstOrDefault();
                            //var employee = emp.GetData_By_Token(HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()].Value);
                            if (cust == null)
                            {
                                TBCustomer newCust = new TBCustomer();
                                newCust.FirstName = item.FirstName;
                                newCust.LastName = item.LastName;
                                newCust.Email = item.Email;
                                if (item.Gender == 2)
                                    newCust.Gender = "P";
                                else
                                    newCust.Gender = "L";
                                newCust.DateInsert = DateTime.ParseExact(item.CreateDate, "dd/MM/yyyy H:mm", System.Globalization.CultureInfo.InvariantCulture);
                                newCust.DateLastUpdate = DateTime.ParseExact(item.LastUpdateDate, "dd/MM/yyyy H:mm", System.Globalization.CultureInfo.InvariantCulture);
                                if (item.Birthday != null && item.Birthday != "NULL")
                                    newCust.Birthday = DateTime.ParseExact(item.Birthday, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                newCust.IDCustomer_Group = 1;
                                newCust.Active = true;
                                newCust.Password = "";

                                //Class_Customer classCustomer = new Class_Customer();

                                //string text = item.Email + "-" + item.FirstName.Substring(0, 2);
                                //string pass = classCustomer.EncryptToken(item.Email, DateTime.Now.ToString("MMddHHmmss"));
                                //newCust.Password = pass;

                                listCustomer.Add(newCust);

                                db.SP_Insert_Customer(1, item.FirstName, item.LastName, newCust.Gender, item.Email, "", "", newCust.Birthday, true, newCust.DateInsert, newCust.DateLastUpdate);
                            }
                            else
                            {
                                totalNotfound++;
                            }
                        }

                        //db.TBCustomers.InsertAllOnSubmit(listCustomer);
                        //db.SubmitChanges();

                        foreach (var item in listCustomer)
                        {
                            Class_Customer classCustomer = new Class_Customer();
                            classCustomer.AJAX_Forget_Password(item.Email);
                        }

                        sr.Close();
                        lblSuccess.Text += totalRead + " row(s) read, " + totalUpdate + " row(s) updated, " + totalNotfound + " row(s) customer exists";
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