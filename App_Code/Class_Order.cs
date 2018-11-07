using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using WITLibrary;

/// <summary>
/// Summary description for Class_Order
/// </summary>
public class Class_Order
{
    public Class_Order()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public bool SaveEncodeDataOrder(string anonID, string encodedData)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBOrder_temp temp = db.TBOrder_temps.Where(x => x.AnonID == anonID).FirstOrDefault();
            if (temp == null)
            {
                temp = new TBOrder_temp { AnonID = anonID, EncodedData = encodedData };
                db.TBOrder_temps.InsertOnSubmit(temp);
            }
            else
                temp.EncodedData = encodedData;

            db.SubmitChanges();
            return true;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return false;
        }
    }

    public TBOrder_temp GetEncodedDataOrder(string anonID)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBOrder_temps.Where(x => x.AnonID == anonID).FirstOrDefault();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public bool DeleteEncodeDataOrder(string anonID)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBOrder_temp temp = db.TBOrder_temps.Where(x => x.AnonID == anonID).FirstOrDefault();
            if (temp == null)
                return false;
            else
                db.TBOrder_temps.DeleteOnSubmit(temp);
            db.SubmitChanges();
            return true;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return false;
        }
    }

    public bool DeleteEncodeDataOrder(TBOrder_temp temp)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBOrder_temp deleteData = db.TBOrder_temps.Where(x => x.AnonID == temp.AnonID).FirstOrDefault();
            if (deleteData != null)
            {
                db.TBOrder_temps.DeleteOnSubmit(deleteData);
                db.SubmitChanges();
            }
            else
                return false;
            return true;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return false;
        }
    }

    public dynamic Dynamic_Get_Chart_Revenue()
    {
        try
        {
            DateTime tomorrow = DateTime.Now.AddDays(1);
            DataClassesDataContext db = new DataClassesDataContext();
            var results = from o in db.TBOrders
                          where o.DateInsert.Date < tomorrow && !o.Deflag && o.TBOrder_Status.Paid
                          group o by o.DateInsert.Date into data
                          select new
                          {
                              Date = data.Key.Date,
                              Amount = data.Sum(x => x.TotalPaid)
                          };
            //var query = from all in db.TBOrders
            //           join aa in results on all.DateInsert.Date equals aa.Date
            //           select new { Date = aa.Date, Amount = (aa != null ? aa.Amount : 0) };

            return results.OrderByDescending(x => x.Date).Take(10);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public Datatable AJAX_GetTable(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            IEnumerable<dynamic> data = db.TBOrders.OrderByDescending(x => x.IDOrder).Where(X => !X.Deflag).OrderByDescending(x => x.IDOrder).ToList();
            int count = data.Count();
            if (!string.IsNullOrEmpty(search))
                data = data.Where(x =>
                    x.Reference.ToLower().Contains(search.ToLower()) ||
                    x.TBCustomer.FirstName.ToLower().Contains(search.ToLower()) ||
                    x.TBCustomer.LastName.ToLower().Contains(search.ToLower()) ||
                    x.TotalPaid.ToString().Contains(search.ToLower()) ||
                    x.TBPayment_Method.Name.Contains(search.ToLower())
                    ).ToArray();
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (TBOrder currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("IDOrder", currData.IDOrder);
                newData.Add("Reference", currData.Reference);
                newData.Add("Customer", currData.TBCustomer.FirstName + " " + currData.TBCustomer.LastName);
                newData.Add("TotalPaid", currData.TotalPaid);
                newData.Add("PaymentMethod", currData.TBPayment_Method.Name);
                newData.Add("Status", currData.TBOrder_Status.Name);
                newData.Add("Date", currData.DateInsert.ToString("dd-MM-yyyy") + " " + currData.DateInsert.ToLongTimeString());
                newData.Add("Invoice", currData.InvoiceNumber);
                resultList.Add(newData);
            }
            return OurClass.ParseTable(resultList, count, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return new WITLibrary.Datatable
            {
                sEcho = 0,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0,
                aaData = new List<Dictionary<string, dynamic>>()
            };
        }
    }

    public Datatable AJAX_GetTable_FilterDate(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search, DateTime startDate, DateTime endDate)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            IEnumerable<dynamic> data = db.TBOrders.OrderByDescending(x => x.IDOrder).Where(X => X.DateInsert >= startDate && X.DateInsert <= endDate && !X.Deflag).OrderByDescending(x => x.IDOrder).ToList();
            int count = data.Count();
            if (!string.IsNullOrEmpty(search))
                data = data.Where(x =>
                    x.Reference.ToLower().Contains(search.ToLower()) ||
                    x.TBCustomer.FirstName.ToLower().Contains(search.ToLower()) ||
                    x.TBCustomer.LastName.ToLower().Contains(search.ToLower()) ||
                    x.TotalPaid.ToString().Contains(search.ToLower()) ||
                    x.TBPayment_Method.Name.Contains(search.ToLower())
                    ).ToArray();
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (TBOrder currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("IDOrder", currData.IDOrder);
                newData.Add("Reference", currData.Reference);
                newData.Add("Customer", currData.TBCustomer.FirstName + " " + currData.TBCustomer.LastName);
                newData.Add("TotalPaid", currData.TotalPaid);
                newData.Add("PaymentMethod", currData.TBPayment_Method.Name);
                newData.Add("Status", currData.TBOrder_Status.Name);
                newData.Add("Date", currData.DateInsert.ToString("dd-MM-yyyy") + " " + currData.DateInsert.ToLongTimeString());
                newData.Add("Invoice", currData.InvoiceNumber);
                resultList.Add(newData);
            }
            return OurClass.ParseTable(resultList, count, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return new WITLibrary.Datatable
            {
                sEcho = 0,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0,
                aaData = new List<Dictionary<string, dynamic>>()
            };
        }
    }

    public Datatable AJAX_GetTable_Filter(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search, DateTime startDate, DateTime endDate, List<int> statusOrder)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            List<TBOrder> listOrder = new List<TBOrder>();
            foreach (int item in statusOrder)
            {
                TBOrder_Status _status = db.TBOrder_Status.Where(x => !x.Deflag && x.IDOrderStatus == item).FirstOrDefault();
                if (_status != null)
                    listOrder.AddRange(_status.TBOrders);
            }
            IEnumerable<dynamic> data = listOrder.OrderByDescending(x => x.IDOrder).Where(X => X.DateInsert >= startDate && X.DateInsert <= endDate && !X.Deflag).OrderByDescending(x => x.IDOrder).ToList();
            int count = data.Count();
            if (!string.IsNullOrEmpty(search))
                data = data.Where(x =>
                    x.Reference.ToLower().Contains(search.ToLower()) ||
                    x.TBCustomer.FirstName.ToLower().Contains(search.ToLower()) ||
                    x.TBCustomer.LastName.ToLower().Contains(search.ToLower()) ||
                    x.TotalPaid.ToString().Contains(search.ToLower()) ||
                    x.TBPayment_Method.Name.Contains(search.ToLower())
                    ).ToArray();
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (TBOrder currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("IDOrder", currData.IDOrder);
                newData.Add("Reference", currData.Reference);
                newData.Add("Customer", currData.TBCustomer.FirstName + " " + currData.TBCustomer.LastName);
                newData.Add("TotalPaid", currData.TotalPaid);
                newData.Add("PaymentMethod", currData.TBPayment_Method.Name);
                newData.Add("Status", currData.TBOrder_Status.Name);
                newData.Add("Date", currData.DateInsert.ToString("dd-MM-yyyy") + " " + currData.DateInsert.ToLongTimeString());
                newData.Add("Invoice", currData.InvoiceNumber);
                resultList.Add(newData);
            }
            return OurClass.ParseTable(resultList, count, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return new WITLibrary.Datatable
            {
                sEcho = 0,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0,
                aaData = new List<Dictionary<string, dynamic>>()
            };
        }
    }

    public dynamic Dynamic_Get_TotalOrder()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBOrders.Where(x => !x.Deflag).Count();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_Get_TotalOrder_FilterDate(DateTime startDate, DateTime endDate)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBOrders.Where(x => !x.Deflag && x.DateInsert >= startDate && x.DateInsert <= endDate).Count();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_Get_TotalSales()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBOrders.Where(x => !x.Deflag && x.TBOrder_Status.Paid).Sum(x => x.TotalPaid);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_Get_TotalSales_FilterDate(DateTime startDate, DateTime endDate)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBOrders.Where(x => !x.Deflag && x.TBOrder_Status.Paid && x.DateInsert >= startDate && x.DateInsert <= endDate).Sum(x => x.TotalPaid);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_Get_AverageOrder()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            decimal count = db.TBOrders.Where(x => !x.Deflag && x.TBOrder_Status.Paid).Count();
            decimal total = (decimal)db.TBOrders.Where(x => !x.Deflag && x.TBOrder_Status.Paid).Sum(x => x.TotalPaid);
            return (total / count).ToString("#.##");
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_Get_TotalItemsSold()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBOrder_Details.Where(x => x.TBOrder.TBOrder_Status.Paid).Sum(x => x.Quantity);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_Get_TotalItemsSold_FilterDate(DateTime startDate, DateTime endDate)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBOrder_Details.Where(x => x.TBOrder.TBOrder_Status.Paid && x.TBOrder.DateInsert >= startDate && x.TBOrder.DateInsert <= endDate).Sum(x => x.Quantity);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_Get_TotalSalesVoucher()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBOrders.Where(x => !x.Deflag && x.TBOrder_Status.Paid).Sum(x => x.TotalPrice - x.TotalDiscount);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_Get_TotalSalesVoucher_FilterDate(DateTime startDate, DateTime endDate)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBOrders.Where(x => !x.Deflag && x.TBOrder_Status.Paid && x.DateInsert >= startDate && x.DateInsert <= endDate).Sum(x => x.TotalPrice - x.TotalDiscount);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public ReturnData AJAX_UpdateOrderStatus(int idOrder, int idOrderStatus, string shippingNumber)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            int firstQty = 0;
            var dataOrder = db.TBOrders.Where(x => !x.Deflag && x.IDOrder == idOrder).FirstOrDefault();
            if (dataOrder == null)
                return ReturnData.MessageFailed("Data order not found", null);
            var orderStatus = db.TBOrder_Status.Where(x => x.IDOrderStatus == idOrderStatus && !x.Deflag).FirstOrDefault();
            if (orderStatus == null)
                return ReturnData.MessageFailed("Data status order not found", null);

            Class_Employee emp = new Class_Employee();
            var employee = emp.GetData_By_Token(HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()].Value);

            //KALO STATUS RESTOCK
            if (orderStatus.Restock)
            {
                if (dataOrder.IDVoucher != null)
                {
                    dataOrder.TBVoucher.Used -= 1;
                }

                foreach (var item in dataOrder.TBOrder_Details)
                {
                    firstQty = item.TBProduct_Combination.Quantity;
                    item.TBProduct_Combination.Quantity += item.Quantity;
                    //INSERT LOG
                    Class_Log_Stock log = new Class_Log_Stock();
                    log.Insert(employee.IDEmployee, item.TBProduct_Combination.IDProduct_Combination, item.TBProduct.Name, firstQty, item.TBProduct_Combination.Quantity, item.Quantity, "increase", "RESTOCK (Cancel Order) #" + idOrder.ToString() + " by " + employee.Name + "( " + employee.Email + " )");
                    db.SubmitChanges();
                }
            }

            dataOrder.IDOrderStatus = idOrderStatus;
            dataOrder.DateLastUpdate = DateTime.Now;
            if (shippingNumber != "")
                dataOrder.ShippingNumber = shippingNumber;
            if (dataOrder.TBOrder_Status.Paid && dataOrder.InvoiceNumber == null)
                dataOrder.InvoiceNumber = GenerateInvoiceCode();
            else
                dataOrder.InvoiceNumber = null;

            // SAAS PRODUCT PACKAGE
            if (dataOrder.ProcessStatus == "pending" && orderStatus.Paid)
            {
                if (dataOrder.OrderType == "new")
                {
                    foreach (var item in dataOrder.TBOrder_Details)
                    {
                        var customerProduct = db.TBCustomer_Products.Where(x => x.IDCustomer == dataOrder.IDCustomer && x.IDProduct == item.IDProduct).FirstOrDefault();
                        if (customerProduct == null)
                        {
                            TBCustomer_Product custProd = new TBCustomer_Product();
                            custProd.IDCustomer = dataOrder.IDCustomer;
                            custProd.IDProduct = item.IDProduct;
                            custProd.StartDate = DateTime.Now;
                            custProd.UpdateDate = DateTime.Now;
                            custProd.EndDate = DateTime.Now.AddDays((int)item.Weight);
                            db.TBCustomer_Products.InsertOnSubmit(custProd);
                        }
                    }
                }
                else if (dataOrder.OrderType == "renew")
                {

                    if (DateTime.Now <= dataOrder.TBCustomer_Product.EndDate)
                    {
                        dataOrder.TBCustomer_Product.UpdateDate = dataOrder.TBCustomer_Product.EndDate;
                        dataOrder.TBCustomer_Product.EndDate = dataOrder.TBCustomer_Product.StartDate.Value.AddDays((int)dataOrder.TBOrder_Details.FirstOrDefault().Weight);
                    }
                    else
                    {
                        dataOrder.TBCustomer_Product.UpdateDate = DateTime.Now;
                        dataOrder.TBCustomer_Product.EndDate = DateTime.Now.AddDays((int)dataOrder.TBOrder_Details.FirstOrDefault().Weight);
                    }
                }
                else if (dataOrder.OrderType == "upgrade")
                {
                    if (DateTime.Now.Month >= dataOrder.TBCustomer_Product.EndDate.Value.Month)
                    {
                        dataOrder.TBCustomer_Product.EndDate.Value.AddDays((int)dataOrder.TBOrder_Details.FirstOrDefault().Weight);
                    }
                    dataOrder.TBCustomer_Product.IDProduct = dataOrder.TBOrder_Details.FirstOrDefault().IDProduct;
                }
                dataOrder.ProcessStatus = "complete";
            }

            db.SubmitChanges();

            //INSERT LOG ORDER
            Class_Log_Order logOrder = new Class_Log_Order();
            logOrder.Insert(employee.IDEmployee, dataOrder.IDOrder, dataOrder.Reference, dataOrder.TBOrder_Status.Name, "Update Order Status by " + employee.Name, dataOrder.TBCustomer.FirstName + ' ' + dataOrder.TBCustomer.LastName);

            //KALO KIRIM EMAIL
            if (orderStatus.SendEmail && orderStatus.EmailTemplate != null)
            {
                Class_Configuration _config = new Class_Configuration();
                var emailLogo = _config.Dynamic_Get_EmailLogo();
                switch (orderStatus.Name)
                {
                    case "Payment Accepted":
                        {
                            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/" + orderStatus.EmailTemplate)))
                            {
                                string body = "";
                                body = reader.ReadToEnd();
                                body = body.Replace("{name}", dataOrder.TBCustomer.FirstName);
                                body = body.Replace("{title}", System.Configuration.ConfigurationManager.AppSettings["Title"]);
                                body = body.Replace("{email_logo}", System.Configuration.ConfigurationManager.AppSettings["url"] + "/assets/images/email_logo/" + emailLogo);
                                body = body.Replace("{shop_url}", System.Configuration.ConfigurationManager.AppSettings["url"]);
                                OurClass.SendEmail(dataOrder.TBCustomer.Email, System.Web.Configuration.WebConfigurationManager.AppSettings["Title"] + " - Order " + orderStatus.Name, body, "");
                            }
                        }
                        break;
                    case "Shipped":
                        {
                            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/" + orderStatus.EmailTemplate)))
                            {
                                string body = "";
                                body = reader.ReadToEnd();
                                body = body.Replace("{name}", dataOrder.TBCustomer.FirstName);
                                body = body.Replace("{Carrier}", dataOrder.TBShipping.TBCarrier.Name);
                                body = body.Replace("{ShippingNumber}", dataOrder.ShippingNumber);
                                body = body.Replace("{nomorInvoice}", dataOrder.InvoiceNumber);
                                body = body.Replace("{title}", System.Configuration.ConfigurationManager.AppSettings["Title"]);
                                body = body.Replace("{email_logo}", System.Configuration.ConfigurationManager.AppSettings["url"] + "/assets/images/email_logo/" + emailLogo);
                                body = body.Replace("{shop_url}", System.Configuration.ConfigurationManager.AppSettings["url"]);
                                OurClass.SendEmail(dataOrder.TBCustomer.Email, System.Web.Configuration.WebConfigurationManager.AppSettings["Title"] + " - Order " + orderStatus.Name, body, "");
                            }
                        }
                        break;
                    case "Packed":
                        {
                            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/" + orderStatus.EmailTemplate)))
                            {
                                string body = "";
                                body = reader.ReadToEnd();
                                body = body.Replace("{name}", dataOrder.TBCustomer.FirstName);
                                body = body.Replace("{amount}", dataOrder.TotalPaid.ToString());
                                body = body.Replace("{owner}", dataOrder.TBPayment_Method.Owner);
                                body = body.Replace("{account}", dataOrder.TBPayment_Method.AccountNumber);
                                body = body.Replace("{bank}", dataOrder.TBPayment_Method.Bank);
                                if (dataOrder.InvoiceNumber == null)
                                {
                                    body = body.Replace("{nomorInvoice}", dataOrder.Reference);
                                }
                                else
                                {
                                    body = body.Replace("{nomorInvoice}", dataOrder.InvoiceNumber);
                                }
                                body = body.Replace("{title}", System.Configuration.ConfigurationManager.AppSettings["Title"]);
                                body = body.Replace("{email_logo}", System.Configuration.ConfigurationManager.AppSettings["url"] + "/assets/images/email_logo/" + emailLogo);
                                body = body.Replace("{shop_url}", System.Configuration.ConfigurationManager.AppSettings["url"]);
                                OurClass.SendEmail(dataOrder.TBCustomer.Email, System.Web.Configuration.WebConfigurationManager.AppSettings["Title"] + " - Order " + orderStatus.Name, body, "");
                            }
                        }
                        break;
                    case "Delivered":
                        {
                            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/" + orderStatus.EmailTemplate)))
                            {
                                string body = "";
                                body = reader.ReadToEnd();
                                body = body.Replace("{name}", dataOrder.TBCustomer.FirstName);
                                body = body.Replace("{Carrier}", dataOrder.TBShipping.TBCarrier.Name);
                                body = body.Replace("{ShippingNumber}", dataOrder.ShippingNumber);
                                body = body.Replace("{nomorInvoice}", dataOrder.InvoiceNumber);
                                body = body.Replace("{title}", System.Configuration.ConfigurationManager.AppSettings["Title"]);
                                body = body.Replace("{email_logo}", System.Configuration.ConfigurationManager.AppSettings["url"] + "/assets/images/email_logo/" + emailLogo);
                                body = body.Replace("{shop_url}", System.Configuration.ConfigurationManager.AppSettings["url"]);
                                OurClass.SendEmail(dataOrder.TBCustomer.Email, System.Web.Configuration.WebConfigurationManager.AppSettings["Title"] + " - Order " + orderStatus.Name, body, "");
                            }
                        }
                        break;
                    case "Cancel":
                        {
                            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/" + orderStatus.EmailTemplate)))
                            {
                                string body = "";
                                body = reader.ReadToEnd();
                                body = body.Replace("{name}", dataOrder.TBCustomer.FirstName);
                                body = body.Replace("{amount}", dataOrder.TotalPaid.ToString());
                                body = body.Replace("{owner}", dataOrder.TBPayment_Method.Owner);
                                body = body.Replace("{account}", dataOrder.TBPayment_Method.AccountNumber);
                                body = body.Replace("{bank}", dataOrder.TBPayment_Method.Bank);
                                if (dataOrder.InvoiceNumber == null)
                                {
                                    body = body.Replace("{nomorInvoice}", dataOrder.Reference);
                                }
                                else
                                {
                                    body = body.Replace("{nomorInvoice}", dataOrder.InvoiceNumber);
                                }
                                body = body.Replace("{title}", System.Configuration.ConfigurationManager.AppSettings["Title"]);
                                body = body.Replace("{email_logo}", System.Configuration.ConfigurationManager.AppSettings["url"] + "/assets/images/email_logo/" + emailLogo);
                                body = body.Replace("{shop_url}", System.Configuration.ConfigurationManager.AppSettings["url"]);
                                OurClass.SendEmail(dataOrder.TBCustomer.Email, System.Web.Configuration.WebConfigurationManager.AppSettings["Title"] + " - Order " + orderStatus.Name, body, "");
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            return ReturnData.MessageSuccess("Status order updated successfully", Dynamic_GetDetail_ByIDOrder(idOrder));
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_UpdateOrderStatus_vt(int idOrder, int idOrderStatus, string shippingNumber)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            int firstQty = 0;
            var dataOrder = db.TBOrders.Where(x => !x.Deflag && x.IDOrder == idOrder).FirstOrDefault();
            if (dataOrder == null)
                return ReturnData.MessageFailed("Data order not found", null);
            var orderStatus = db.TBOrder_Status.Where(x => x.IDOrderStatus == idOrderStatus && !x.Deflag).FirstOrDefault();
            if (orderStatus == null)
                return ReturnData.MessageFailed("Data status order not found", null);

            //Class_Employee emp = new Class_Employee();
            //var employee = emp.GetData_By_Token(HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()].Value);

            //KALO STATUS RESTOCK
            if (orderStatus.Restock)
            {
                foreach (var item in dataOrder.TBOrder_Details)
                {
                    firstQty = item.TBProduct_Combination.Quantity;
                    item.TBProduct_Combination.Quantity += item.Quantity;
                    //INSERT LOG
                    Class_Log_Stock log = new Class_Log_Stock();
                    log.Insert(0, item.TBProduct_Combination.IDProduct_Combination, item.TBProduct.Name, firstQty, item.TBProduct_Combination.Quantity, item.Quantity, "increase", "RESTOCK (Cancel Order) #" + idOrder.ToString() + " by veritrans( vt-direct )");
                    db.SubmitChanges();
                }
            }

            dataOrder.IDOrderStatus = idOrderStatus;
            dataOrder.DateLastUpdate = DateTime.Now;
            if (shippingNumber != "")
                dataOrder.ShippingNumber = shippingNumber;
            if (dataOrder.TBOrder_Status.Paid)
                dataOrder.InvoiceNumber = GenerateInvoiceCode();
            else
                dataOrder.InvoiceNumber = null;
            db.SubmitChanges();

            //INSERT LOG ORDER
            Class_Log_Order logOrder = new Class_Log_Order();
            logOrder.Insert(0, dataOrder.IDOrder, dataOrder.Reference, dataOrder.TBOrder_Status.Name, "Update Order Status by veritrans (vt web)", dataOrder.TBCustomer.FirstName + ' ' + dataOrder.TBCustomer.LastName);

            //KALO KIRIM EMAIL
            if (orderStatus.SendEmail && orderStatus.EmailTemplate != null)
            {
                Class_Configuration _config = new Class_Configuration();
                var emailLogo = _config.Dynamic_Get_EmailLogo();
                switch (orderStatus.Name)
                {
                    case "Payment Accepted":
                        {
                            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/" + orderStatus.EmailTemplate)))
                            {
                                string body = "";
                                body = reader.ReadToEnd();
                                body = body.Replace("{name}", dataOrder.TBCustomer.FirstName);
                                body = body.Replace("{title}", System.Configuration.ConfigurationManager.AppSettings["Title"]);
                                body = body.Replace("{email_logo}", System.Configuration.ConfigurationManager.AppSettings["url"] + "/assets/images/email_logo/" + emailLogo);
                                body = body.Replace("{shop_url}", System.Configuration.ConfigurationManager.AppSettings["url"]);
                                OurClass.SendEmail(dataOrder.TBCustomer.Email, System.Web.Configuration.WebConfigurationManager.AppSettings["Title"] + " - Order " + orderStatus.Name, body, "");
                            }
                        }
                        break;
                    case "Shipped":
                        {
                            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/" + orderStatus.EmailTemplate)))
                            {
                                string body = "";
                                body = reader.ReadToEnd();
                                body = body.Replace("{name}", dataOrder.TBCustomer.FirstName);
                                body = body.Replace("{Carrier}", dataOrder.TBShipping.TBCarrier.Name);
                                body = body.Replace("{ShippingNumber}", dataOrder.ShippingNumber);
                                body = body.Replace("{nomorInvoice}", dataOrder.InvoiceNumber);
                                body = body.Replace("{title}", System.Configuration.ConfigurationManager.AppSettings["Title"]);
                                body = body.Replace("{email_logo}", System.Configuration.ConfigurationManager.AppSettings["url"] + "/assets/images/email_logo/" + emailLogo);
                                body = body.Replace("{shop_url}", System.Configuration.ConfigurationManager.AppSettings["url"]);
                                OurClass.SendEmail(dataOrder.TBCustomer.Email, System.Web.Configuration.WebConfigurationManager.AppSettings["Title"] + " - Order " + orderStatus.Name, body, "");
                            }
                        }
                        break;
                    case "Packed":
                        {
                            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/" + orderStatus.EmailTemplate)))
                            {
                                string body = "";
                                body = reader.ReadToEnd();
                                body = body.Replace("{name}", dataOrder.TBCustomer.FirstName);
                                body = body.Replace("{amount}", dataOrder.TotalPaid.ToString());
                                body = body.Replace("{owner}", dataOrder.TBPayment_Method.Owner);
                                body = body.Replace("{account}", dataOrder.TBPayment_Method.AccountNumber);
                                body = body.Replace("{bank}", dataOrder.TBPayment_Method.Bank);
                                if (dataOrder.InvoiceNumber == null)
                                {
                                    body = body.Replace("{nomorInvoice}", dataOrder.Reference);
                                }
                                else
                                {
                                    body = body.Replace("{nomorInvoice}", dataOrder.InvoiceNumber);
                                }
                                body = body.Replace("{title}", System.Configuration.ConfigurationManager.AppSettings["Title"]);
                                body = body.Replace("{email_logo}", System.Configuration.ConfigurationManager.AppSettings["url"] + "/assets/images/email_logo/" + emailLogo);
                                body = body.Replace("{shop_url}", System.Configuration.ConfigurationManager.AppSettings["url"]);
                                OurClass.SendEmail(dataOrder.TBCustomer.Email, System.Web.Configuration.WebConfigurationManager.AppSettings["Title"] + " - Order " + orderStatus.Name, body, "");
                            }
                        }
                        break;
                    case "Delivered":
                        {
                            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/" + orderStatus.EmailTemplate)))
                            {
                                string body = "";
                                body = reader.ReadToEnd();
                                body = body.Replace("{name}", dataOrder.TBCustomer.FirstName);
                                body = body.Replace("{Carrier}", dataOrder.TBShipping.TBCarrier.Name);
                                body = body.Replace("{ShippingNumber}", dataOrder.ShippingNumber);
                                body = body.Replace("{nomorInvoice}", dataOrder.InvoiceNumber);
                                body = body.Replace("{title}", System.Configuration.ConfigurationManager.AppSettings["Title"]);
                                body = body.Replace("{email_logo}", System.Configuration.ConfigurationManager.AppSettings["url"] + "/assets/images/email_logo/" + emailLogo);
                                body = body.Replace("{shop_url}", System.Configuration.ConfigurationManager.AppSettings["url"]);
                                OurClass.SendEmail(dataOrder.TBCustomer.Email, System.Web.Configuration.WebConfigurationManager.AppSettings["Title"] + " - Order " + orderStatus.Name, body, "");
                            }
                        }
                        break;
                    case "Cancel":
                        {
                            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/" + orderStatus.EmailTemplate)))
                            {
                                string body = "";
                                body = reader.ReadToEnd();
                                body = body.Replace("{name}", dataOrder.TBCustomer.FirstName);
                                body = body.Replace("{amount}", dataOrder.TotalPaid.ToString());
                                body = body.Replace("{owner}", dataOrder.TBPayment_Method.Owner);
                                body = body.Replace("{account}", dataOrder.TBPayment_Method.AccountNumber);
                                body = body.Replace("{bank}", dataOrder.TBPayment_Method.Bank);
                                if (dataOrder.InvoiceNumber == null)
                                {
                                    body = body.Replace("{nomorInvoice}", dataOrder.Reference);
                                }
                                else
                                {
                                    body = body.Replace("{nomorInvoice}", dataOrder.InvoiceNumber);
                                }
                                body = body.Replace("{title}", System.Configuration.ConfigurationManager.AppSettings["Title"]);
                                body = body.Replace("{email_logo}", System.Configuration.ConfigurationManager.AppSettings["url"] + "/assets/images/email_logo/" + emailLogo);
                                body = body.Replace("{shop_url}", System.Configuration.ConfigurationManager.AppSettings["url"]);
                                OurClass.SendEmail(dataOrder.TBCustomer.Email, System.Web.Configuration.WebConfigurationManager.AppSettings["Title"] + " - Order " + orderStatus.Name, body, "");
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            return ReturnData.MessageSuccess("Status order updated successfully", Dynamic_GetDetail_ByIDOrder(idOrder));
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_GetDetail_By_IDOrder(int idOrder)
    {
        try
        {
            return ReturnData.MessageSuccess("OK", Dynamic_GetDetail_ByIDOrder(idOrder));
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public dynamic Dynamic_ChangeCurrencyCart(int idCurrency)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            //HttpCookie _cookie = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()];
            string anonID = HttpContext.Current.Request.AnonymousID;
            TBOrder_temp _cookie = this.GetEncodedDataOrder(anonID);

            string _token = "";
            Dictionary<string, dynamic> _product = new Dictionary<string, dynamic>();
            List<Dictionary<string, dynamic>> _listProduct = new List<Dictionary<string, dynamic>>();
            Dictionary<string, dynamic> _tokenData = new Dictionary<string, dynamic>();

            if (_cookie == null)
            {
                return null;
            }
            else
            {
                _token = _cookie.EncodedData;
                dynamic tmp = (OurClass.DecryptToken(_token) as IDictionary<string, dynamic>);

                var _tempTokenData = (ArrayList)tmp["Product"];

                List<int> _listIDCom = new List<int>();// list untuk simpan list idcombination untuk pengecekan
                decimal totalPrice = 0;
                int totalQuantity = 0;
                decimal totalWeight = 0;
                foreach (dynamic item in _tempTokenData)
                {
                    _listIDCom.Add(item["IDCombination"]);
                    _product = new Dictionary<string, dynamic>();
                    _product.Add("IDProduct", item["IDProduct"]);
                    _product.Add("IDCombination", item["IDCombination"]);
                    _product.Add("Quantity", item["Quantity"]);
                    _product.Add("Price", Class_Currency.GetPriceConversionCurrency(item["PricePerUnit"], idCurrency) * item["Quantity"]);
                    _product.Add("CombinationName", item["CombinationName"]);
                    _product.Add("ProductName", item["ProductName"]);
                    _product.Add("PricePerUnit", Class_Currency.GetPriceConversionCurrency(item["PricePerUnit"], idCurrency));
                    _product.Add("WeightPerUnit", item["WeightPerUnit"]);
                    _product.Add("Weight", item["Weight"]);
                    _listProduct.Add(_product);

                    totalPrice += _product["Price"];
                    totalQuantity += _product["Quantity"];
                    totalWeight += _product["Weight"];
                }
                _tokenData.Add("Product", _listProduct);
                _tokenData.Add("TotalPrice", totalPrice);
                _tokenData.Add("TotalQuantity", totalQuantity);
                _tokenData.Add("TotalWeight", totalWeight);
                _tokenData.Add("IDCurrency", idCurrency);

                if ((tmp as Dictionary<string, dynamic>).ContainsKey("BillingAddress"))
                {
                    dynamic billingAddress = tmp["BillingAddress"];
                    _tokenData.Add("BillingAddress", billingAddress);
                }

                if ((tmp as Dictionary<string, dynamic>).ContainsKey("DeliveryAddress"))
                {
                    dynamic deliveryAddress = tmp["DeliveryAddress"];
                    _tokenData.Add("DeliveryAddress", deliveryAddress);
                }

                if ((tmp as Dictionary<string, dynamic>).ContainsKey("PaymentMethod"))
                {
                    dynamic payment = tmp["PaymentMethod"];
                    _tokenData.Add("PaymentMethod", payment);
                }

                if ((tmp as Dictionary<string, dynamic>).ContainsKey("Notes"))
                {
                    dynamic notes = tmp["Notes"];
                    _tokenData.Add("Notes", notes);
                }

                //UPDATE VOUCHER VALUE
                if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()] != null && HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Value != "")
                {
                    string token = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Value;
                    dynamic voucher = OurClass.DecryptToken(token);
                    _tokenData["Voucher"] = voucher;
                    if (voucher["VoucherType"] == "percent")
                        voucher["Discount"] = _tokenData["TotalPrice"] * voucher["Value"] / 100;
                    if (voucher["VoucherType"] == "amount")
                        voucher["Discount"] = voucher["Value"];
                    _tokenData["Subtotal"] = Class_Currency.GetPriceConversionCurrency(_tokenData["TotalPrice"], idCurrency) - Class_Currency.GetPriceConversionCurrency(voucher["Discount"], idCurrency);
                    _token = OurClass.EncryptToken(voucher);
                    HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Value = _token;
                }
                else
                    _tokenData.Add("Subtotal", totalPrice);

                if ((tmp as Dictionary<string, dynamic>).ContainsKey("Shipping"))
                {
                    dynamic shipping = tmp["Shipping"];
                    shipping["Price"] = Class_Currency.GetPriceConversionCurrency(shipping["Price"], idCurrency);
                    shipping["TotalPrice"] = Math.Ceiling((decimal)tmp["TotalWeight"]) * shipping["Price"];
                    if (Math.Ceiling((decimal)tmp["TotalWeight"]) < 1)
                    {
                        shipping["TotalPrice"] = shipping["Price"];
                    }
                    _tokenData.Add("Shipping", shipping);
                    //decimal subtotal = _tokenData["Subtotal"] + shipping["TotalPrice"];
                    //_tokenData["Subtotal"] = subtotal;
                }

                _token = OurClass.EncryptToken(_tokenData);
                //HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()].Value = _token;
                if (this.SaveEncodeDataOrder(_cookie.AnonID, _token))
                {
                    return ReturnData.MessageSuccess("Product add to cart successfully", _tokenData);
                }
                else
                    return ReturnData.MessageSuccess("Could not save to database.", _token);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageSuccess(ex.Message, null);
        }

    }

    public ReturnData AJAX_SubmitOrder()
    {
        try
        {
            var cookieUser = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()];
            //var cookieCurrency = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCurrency"].ToString()].Value;
            if (cookieUser == null)
                return ReturnData.MessageSuccess("Invalid token", null);
            //var cookieCart = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()];
            var cookieCart = this.GetEncodedDataOrder(HttpContext.Current.Request.AnonymousID);
            var cookieVoucher = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()];
            if (cookieCart == null)
            {
                if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()] != null && HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Value != "")
                    HttpContext.Current.Request.Cookies.Remove(System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString());
                return ReturnData.MessageSuccess("Invalid token", null);
            }

            DataClassesDataContext db = new DataClassesDataContext();
            Class_Customer cust = new Class_Customer();
            Class_Currency curr = new Class_Currency();

            dynamic _user = OurClass.DecryptToken(cookieUser.Value);
            dynamic _order = DYNAMIC_GetCartSummary();
            dynamic user = cust.Dynamic_GetData_ByEmail(_user["email"]);

            if (user == null)
                return ReturnData.MessageFailed("Customer not found", null);

            if (_order == null)
                return ReturnData.MessageFailed("Order not found", null);

            dynamic BillingAddress = _order["BillingAddress"];
            dynamic DeliveryAddress = _order["DeliveryAddress"];
            dynamic PaymentMethod = _order["PaymentMethod"];
            dynamic Product = _order["Product"];
            dynamic Shipping = _order["Shipping"];
            dynamic Voucher = (_order as Dictionary<string, dynamic>).ContainsKey("Voucher") ? _order["Voucher"] : null;
            int idVoucher = (_order as Dictionary<string, dynamic>).ContainsKey("Voucher") ? Voucher["IDVoucher"] : 0;
            TBVoucher SelectedVoucher = db.TBVouchers.Where(X => !X.Deflag && X.Active && X.IDVoucher == idVoucher).FirstOrDefault();
            if ((_order as Dictionary<string, dynamic>).ContainsKey("Voucher"))
            {
                if (SelectedVoucher == null)
                    return ReturnData.MessageFailed("Voucher is not found", null);
                if (SelectedVoucher.TotalAvailable == SelectedVoucher.Used && SelectedVoucher.TotalAvailable != 0)
                    return ReturnData.MessageFailed("Voucher is running out", null);
            }

            //if (Product.Count > 0 && Product != null)
            //{
            //    List<TBProduct_Combination> CombinationProducts = new List<TBProduct_Combination>();
            //    foreach (dynamic item in Product)
            //    {
            //        int idCombination = item["IDCombination"];
            //        TBProduct_Combination CheckCom = db.TBProduct_Combinations.Where(x => !x.Deflag && x.IDProduct_Combination == idCombination).FirstOrDefault();

            //        if (item["Quantity"] > CheckCom.Quantity || CheckCom.Quantity < 1)
            //        {
            //            AJAX_DeleteCart(idCombination);
            //            return ReturnData.MessageFailed("We're Sorry, one product in your shopping cart, " + CheckCom.TBProduct.Name + " (" + CheckCom.Name + ") is out of stock", null);
            //        }

            //        //CombinationProducts.Add(db.TBProduct_Combinations.Where(x => !x.Deflag && x.IDProduct == idCombination).FirstOrDefault());
            //    }
            //}

            TBOrder order = new TBOrder();

            order.DateInsert = DateTime.Now;
            order.DateLastUpdate = DateTime.Now;
            order.IDCustomer = user.IDCustomer;
            order.IDPaymentMethod = PaymentMethod["IDPaymentMethod"];
            order.IDOrderStatus = 1;
            order.IDShipping = Shipping["IDShipping"];
            order.IDDeliveryAddress = DeliveryAddress["IDAddress"];
            order.IDBillingAddress = BillingAddress["IDAddress"];
            if (idVoucher != 0)
                order.IDVoucher = idVoucher;

            order.Payment = PaymentMethod["Name"];
            order.TotalWeight = _order["TotalWeight"];
            order.TotalShipping = Class_Currency.GetPriceDeconversionCurrency(Shipping["TotalPrice"]);
            order.TotalPrice = Class_Currency.GetPriceDeconversionCurrency(_order["TotalPrice"]);
            order.TotalPaid = Class_Currency.GetPriceDeconversionCurrency(_order["Subtotal"]);
            order.Notes = _order["Notes"];
            order.Deflag = false;
            order.IDCurrency = Class_Currency.GetActiveCurrencyID();
            order.ConversionRate = db.TBCurrencies.Where(x => x.IDCurrency == Class_Currency.GetActiveCurrencyID() && !x.Deflag).FirstOrDefault().ConversionRate;
            order.OrderType = _order["OrderType"];
            order.IDCustomer_Product = _order["IDCustomerProduct"];
            order.ProcessStatus = "pending";

            db.TBOrders.InsertOnSubmit(order);
            //db.SubmitChanges();

            int discountProduct = 0;
            string _stringProduct_email = "";
            _stringProduct_email += "<tr>";
            _stringProduct_email += "<td>Product</td>";
            _stringProduct_email += "<td>Combination</td>";
            _stringProduct_email += "<td>Price</td>";
            _stringProduct_email += "<td>Qty</td>";
            _stringProduct_email += "<td>Total Price</td>";
            _stringProduct_email += "</tr>";

            foreach (dynamic item in Product)
            {
                decimal idCombination = item["IDCombination"];
                TBProduct_Combination com = db.TBProduct_Combinations.Where(x => !x.Deflag && x.IDProduct_Combination == idCombination).FirstOrDefault();
                int firstQty = com.Quantity;
                if (com != null)
                {
                    TBOrder_Detail detail = new TBOrder_Detail();

                    detail.Discount = com.TBProduct.TotalDiscount;
                    //detail.IDOrder = order.IDOrder;
                    detail.IDCurrency = Class_Currency.GetActiveCurrencyID();
                    detail.TBOrder = order;
                    detail.IDProduct = item["IDProduct"];
                    detail.IDProduct_Combination = item["IDCombination"];
                    detail.Product_Name = com.Name;
                    detail.Weight = item["Weight"];
                    detail.Price = com.Price;
                    detail.OriginalPrice = com.TBProduct.PriceBeforeDiscount;
                    detail.Quantity = item["Quantity"];
                    db.TBOrder_Details.InsertOnSubmit(detail);
                    com.Quantity -= detail.Quantity;


                    //INSERT LOG STOCK
                    Class_Log_Stock log = new Class_Log_Stock();
                    log.Insert(null, detail.IDProduct_Combination, com.TBProduct.Name + " - " + detail.Product_Name, firstQty, com.Quantity, detail.Quantity, "decrease", "Transaction # " + order.IDOrder + " by " + user.Email);

                    //discountProduct += com.TotalDiscount * item["Quantity"];

                    //string product untuk email
                    _stringProduct_email += "<tr><td>" + com.TBProduct.Name + "</td>";
                    _stringProduct_email += "<td>" + com.Name + "</td>";
                    _stringProduct_email += "<td>" + WITLibrary.ConvertString.ToCurrency(detail.Price.ToString()) + "</td>";
                    _stringProduct_email += "<td>" + detail.Quantity.ToString() + "</td>";
                    _stringProduct_email += "<td>" + WITLibrary.ConvertString.ToCurrency((detail.Quantity * detail.Price).ToString()) + "</td>";
                    _stringProduct_email += "</tr>";
                }
            }
            order.TotalDiscount = discountProduct;

            if (SelectedVoucher != null)
            {
                SelectedVoucher.DateLastUpdate = DateTime.Now;
                SelectedVoucher.Used++;
                order.TotalDiscount += Class_Currency.GetPriceDeconversionCurrency(Voucher["Discount"]);
            }
            order.Reference = GenerateReferenceCode();

            db.SubmitChanges();

            //INSERT LOG ORDER
            Class_Log_Order logOrder = new Class_Log_Order();
            logOrder.Insert((int?)null, order.IDOrder, order.Reference, order.TBOrder_Status.Name, "Customer Place Order", order.TBCustomer.FirstName + ' ' + order.TBCustomer.LastName);

            if (cookieVoucher != null)
                HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Expires = DateTime.Now.AddDays(-1);
            if (cookieCart != null)
            {
                //HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()].Expires = DateTime.Now.AddDays(-1);
                this.DeleteEncodeDataOrder(cookieCart);
            }

            //SEND EMAIL KE CUSTOMER
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/template-email-waitingPayment.html")))
            {
                Class_Configuration _config = new Class_Configuration();
                var emailLogo = _config.Dynamic_Get_EmailLogo();
                var signCurrency = db.TBCurrencies.Where(x => x.IDCurrency == Class_Currency.GetActiveCurrencyID() && !x.Deflag).FirstOrDefault();
                string body = "";
                body = reader.ReadToEnd();
                body = body.Replace("{name}", user.FirstName);
                body = body.Replace("{nomorInvoice}", order.Reference);
                body = body.Replace("{amount}", signCurrency.Sign + " " + WITLibrary.ConvertString.ToCurrency(Class_Currency.GetPriceConversionCurrency((decimal)order.TotalPaid).ToString()));
                body = body.Replace("{owner}", order.TBPayment_Method.Owner);
                body = body.Replace("{account}", order.TBPayment_Method.AccountNumber);
                body = body.Replace("{bank}", order.TBPayment_Method.Bank);
                body = body.Replace("{title}", System.Configuration.ConfigurationManager.AppSettings["Title"]);
                body = body.Replace("{email_logo}", System.Configuration.ConfigurationManager.AppSettings["url"] + "/assets/images/email_logo/" + emailLogo);
                body = body.Replace("{shop_url}", System.Configuration.ConfigurationManager.AppSettings["url"]);
                OurClass.SendEmail(user.Email, WebConfigurationManager.AppSettings["Title"] + " Order Notification", body, "");
            }

            //SEND EMAIL KE ADMIN
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/admin-order-notification.html")))
            {
                string body = "";
                string _deliveryAddress = "";
                string _billingAddress = "";

                _deliveryAddress += order.TBAddress1.PeopleName + "<br/>";
                _deliveryAddress += order.TBAddress1.Address + "<br/>";
                _deliveryAddress += order.TBAddress1.TBDistrict.Name + "<br/>";
                _deliveryAddress += order.TBAddress1.TBProvince.Name + "<br/>";
                _deliveryAddress += order.TBAddress1.TBCountry.Name + "<br/>";
                _deliveryAddress += order.TBAddress1.PostalCode + "<br/>";
                _deliveryAddress += order.TBAddress1.Phone + "<br/>";

                _billingAddress += order.TBAddress.PeopleName + "<br/>";
                _billingAddress += order.TBAddress.Address + "<br/>";
                _billingAddress += order.TBAddress.TBDistrict.Name + "<br/>";
                _billingAddress += order.TBAddress.TBProvince.Name + "<br/>";
                _billingAddress += order.TBAddress.TBCountry.Name + "<br/>";
                _billingAddress += order.TBAddress.PostalCode + "<br/>";
                _billingAddress += order.TBAddress.Phone + "<br/>";

                body = reader.ReadToEnd();
                body = body.Replace("{CustomerName}", user.FirstName);
                body = body.Replace("{ReferenceCode}", order.Reference);
                body = body.Replace("{Date}", order.DateInsert.ToString("dd/MM/yyyy hh:mm"));
                body = body.Replace("{PaymentMethod}", order.TBPayment_Method.Name);
                body = body.Replace("{Shipping}", order.TBShipping.TBCarrier.Name);
                body = body.Replace("{bank}", order.TBPayment_Method.Bank);
                body = body.Replace("{Products}", _stringProduct_email);
                body = body.Replace("{SubTotal}", WITLibrary.ConvertString.ToCurrency(Class_Currency.GetPriceDeconversionCurrency((decimal)order.TotalPrice).ToString()));
                body = body.Replace("{TotalShipping}", WITLibrary.ConvertString.ToCurrency(Class_Currency.GetPriceDeconversionCurrency((decimal)order.TotalShipping).ToString()));
                body = body.Replace("{Discount}", WITLibrary.ConvertString.ToCurrency(Class_Currency.GetPriceDeconversionCurrency((decimal)order.TotalDiscount).ToString()));
                body = body.Replace("{GrandTotal}", WITLibrary.ConvertString.ToCurrency(Class_Currency.GetPriceDeconversionCurrency((decimal)order.TotalPaid).ToString()));
                body = body.Replace("{DeliveryAddress}", _deliveryAddress);
                body = body.Replace("{BillingAddress}", _billingAddress);
                string emailAdmin = db.TBConfigurations.Where(x => x.Name == "email_user").FirstOrDefault().Value;
                OurClass.SendEmail(emailAdmin, WebConfigurationManager.AppSettings["Title"] + " Order Notification", body, "");
            }

            return ReturnData.MessageSuccess("Order Submit successfully", order.IDOrder);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_ClearCart()
    {
        try
        {
            Class_Order order = new Class_Order();
            var cookieCart = order.GetEncodedDataOrder(HttpContext.Current.Request.AnonymousID);
            if (cookieCart != null)
            {
                //HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()].Expires = DateTime.Now.AddDays(-1);
                order.DeleteEncodeDataOrder(cookieCart);
            }

            return ReturnData.MessageSuccess("Cart Deleted successfully", null);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }

    }

    //public ReturnData AJAX_SubmitOrder_Admin(dynamic _order)
    //{
    //    try
    //    {
    //        //var cookieUser = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()];
    //        ////var cookieCurrency = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCurrency"].ToString()].Value;
    //        //if (cookieUser == null)
    //        //    return ReturnData.MessageSuccess("Invalid token", null);
    //        ////var cookieCart = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()];
    //        //var cookieCart = this.GetEncodedDataOrder(HttpContext.Current.Request.AnonymousID);
    //        //var cookieVoucher = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()];
    //        //if (cookieCart == null)
    //        //{
    //        //    if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()] != null && HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Value != "")
    //        //        HttpContext.Current.Request.Cookies.Remove(System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString());
    //        //    return ReturnData.MessageSuccess("Invalid token", null);
    //        //}

    //        DataClassesDataContext db = new DataClassesDataContext();
    //        //Class_Customer cust = new Class_Customer();
    //        //Class_Currency curr = new Class_Currency();

    //        //dynamic _user = OurClass.DecryptToken(cookieUser.Value);
    //        //dynamic _order = DYNAMIC_GetCartSummary();
    //        //dynamic user = cust.Dynamic_GetData_ByEmail(_user["email"]);

    //        //if (user == null)
    //        //    return ReturnData.MessageFailed("Customer not found", null);

    //        if (_order == null)
    //            return ReturnData.MessageFailed("Order not found", null);

    //        Class_Customer classCustomer = new Class_Customer();
    //        dynamic cust = _order["Customer"];
    //        dynamic BillingAddress = _order["BillingAddress"];
    //        dynamic DeliveryAddress = _order["DeliveryAddress"];
    //        dynamic PaymentMethod = _order["IDPaymentMethod"];
    //        dynamic Product = _order["Products"];
    //        dynamic Shipping = _order["IDShipping"];

    //        //check qty availability
    //        if (Product.Length > 0 && Product != null)
    //        {
    //            List<TBProduct_Combination> CombinationProducts = new List<TBProduct_Combination>();
    //            foreach (dynamic item in Product)
    //            {
    //                int idCombination = item["idcom"];
    //                TBProduct_Combination CheckCom = db.TBProduct_Combinations.Where(x => !x.Deflag && x.IDProduct_Combination == idCombination).FirstOrDefault();

    //                if (item["qty"] > CheckCom.Quantity || CheckCom.Quantity < 1)
    //                {
    //                    return ReturnData.MessageFailed("We're Sorry, one product in your shopping cart, " + CheckCom.TBProduct.Name + " (" + CheckCom.Name + ") is out of stock", null);
    //                }

    //                //CombinationProducts.Add(db.TBProduct_Combinations.Where(x => !x.Deflag && x.IDProduct == idCombination).FirstOrDefault());
    //            }
    //        }
    //        else
    //            return ReturnData.MessageFailed("No product selected", null);

    //        ReturnData hasilCustomer = classCustomer.AJAX_FE_RegisterCustomGroup(cust["FirstName"], cust["LastName"], cust["Gender"], cust["Email"], "12345", cust["Phone"], DateTime.Now, true, "Shipping Address", DeliveryAddress["Address"], DeliveryAddress["PostalCode"], DeliveryAddress["IDCountry"], DeliveryAddress["IDProvince"], DeliveryAddress["IDCity"], DeliveryAddress["IDDistrict"], cust["IDCustomerGroup"], DeliveryAddress["Name"]);
    //        if (!hasilCustomer.success)
    //            return hasilCustomer;
    //        //BARU SAMPE SINI TINGGAL LANJUTIN AMBIL DATA CUSTOMER DLL
    //        Dictionary<string, dynamic> dataCustomer = (Dictionary<string, dynamic>)hasilCustomer.data;
    //        int idCustomer = int.Parse(dataCustomer["IDCustomer"].ToString());
    //        int idDeliveryAddress = int.Parse(dataCustomer["IDAddress"].ToString());

    //        Class_Address class_address = new Class_Address();
    //        ReturnData billingAddrResult = class_address.AJAX_Insert(idCustomer, int.Parse(BillingAddress["IDCountry"].ToString()), int.Parse(BillingAddress["IDProvince"].ToString()), int.Parse(BillingAddress["IDCity"].ToString()), int.Parse(BillingAddress["IDDistrict"].ToString()), BillingAddress["Name"], "Billing Address", BillingAddress["Address"], BillingAddress["Phone"], BillingAddress["PostalCode"], "");
    //        int idBillingAddress = int.Parse(billingAddrResult.data.ToString());
    //        //dynamic Voucher = (_order as Dictionary<string, dynamic>).ContainsKey("Voucher") ? _order["Voucher"] : null;
    //        int idVoucher = 0;
    //        //TBVoucher SelectedVoucher = db.TBVouchers.Where(X => !X.Deflag && X.Active && X.IDVoucher == idVoucher).FirstOrDefault();
    //        //if ((_order as Dictionary<string, dynamic>).ContainsKey("Voucher"))
    //        //{
    //        //    if (SelectedVoucher == null)
    //        //        return ReturnData.MessageFailed("Voucher is not found", null);
    //        //    if (SelectedVoucher.TotalAvailable == SelectedVoucher.Used && SelectedVoucher.TotalAvailable != 0)
    //        //        return ReturnData.MessageFailed("Voucher is running out", null);
    //        //}

    //        TBOrder order = new TBOrder();

    //        order.IsAdminOrder = true;
    //        order.DateInsert = DateTime.Now;
    //        order.DateLastUpdate = DateTime.Now;
    //        order.IDCustomer = idCustomer;
    //        order.IDPaymentMethod = _order["IDPaymentMethod"];
    //        order.IDOrderStatus = 1;
    //        order.IDShipping = _order["IDShipping"];
    //        order.IDDeliveryAddress = idDeliveryAddress;
    //        order.IDBillingAddress = idBillingAddress;
    //        if (idVoucher != 0)
    //            order.IDVoucher = idVoucher;

    //        order.Payment = _order["PaymentMethod"];
    //        order.TotalWeight = _order["TotalWeight"];
    //        order.TotalShipping = Class_Currency.GetPriceDeconversionCurrency(decimal.Parse(_order["TotalShipping"].ToString()));
    //        order.TotalPrice = Class_Currency.GetPriceDeconversionCurrency(decimal.Parse(_order["Subtotal"].ToString()));
    //        order.TotalPaid = Class_Currency.GetPriceDeconversionCurrency(decimal.Parse(_order["TotalPaid"].ToString()));
    //        order.Notes = ((Dictionary<string, dynamic>)_order).ContainsKey("Notes") ? _order["Notes"] : "";
    //        order.Deflag = false;
    //        order.IDCurrency = Class_Currency.GetActiveCurrencyID();
    //        order.ConversionRate = db.TBCurrencies.Where(x => x.IDCurrency == Class_Currency.GetActiveCurrencyID() && !x.Deflag).FirstOrDefault().ConversionRate;


    //        db.TBOrders.InsertOnSubmit(order);
    //        //db.SubmitChanges();

    //        int discountProduct = 0;
    //        string _stringProduct_email = "";
    //        _stringProduct_email += "<tr>";
    //        _stringProduct_email += "<td>Product</td>";
    //        _stringProduct_email += "<td>Combination</td>";
    //        _stringProduct_email += "<td>Price</td>";
    //        _stringProduct_email += "<td>Qty</td>";
    //        _stringProduct_email += "<td>Total Price</td>";
    //        _stringProduct_email += "</tr>";

    //        foreach (dynamic item in Product)
    //        {
    //            decimal idCombination = item["idcom"];
    //            TBProduct_Combination com = db.TBProduct_Combinations.Where(x => !x.Deflag && x.IDProduct_Combination == idCombination).FirstOrDefault();
    //            int firstQty = com.Quantity;
    //            if (com != null)
    //            {
    //                TBOrder_Detail detail = new TBOrder_Detail();

    //                detail.Discount = com.TBProduct.TotalDiscount;
    //                //detail.IDOrder = order.IDOrder;
    //                detail.IDCurrency = Class_Currency.GetActiveCurrencyID();
    //                detail.TBOrder = order;
    //                detail.IDProduct = item["idprod"];
    //                detail.IDProduct_Combination = item["idcom"];
    //                detail.Product_Name = com.Name;
    //                detail.Weight = item["weight"];
    //                detail.Price = com.Price;
    //                detail.OriginalPrice = com.TBProduct.PriceBeforeDiscount;
    //                detail.Quantity = item["qty"];
    //                db.TBOrder_Details.InsertOnSubmit(detail);
    //                com.Quantity -= detail.Quantity;


    //                //INSERT LOG STOCK
    //                Class_Log_Stock log = new Class_Log_Stock();
    //                //log.Insert(null, detail.IDProduct_Combination, com.TBProduct.Name + " - " + detail.Product_Name, firstQty, com.Quantity, detail.Quantity, "decrease", "Transaction # " + order.IDOrder + " by " + user.Email);

    //                //discountProduct += com.TotalDiscount * item["Quantity"];

    //                //string product untuk email
    //                _stringProduct_email += "<tr><td>" + com.TBProduct.Name + "</td>";
    //                _stringProduct_email += "<td>" + com.Name + "</td>";
    //                _stringProduct_email += "<td>" + WITLibrary.ConvertString.ToCurrency(detail.Price.ToString()) + "</td>";
    //                _stringProduct_email += "<td>" + detail.Quantity.ToString() + "</td>";
    //                _stringProduct_email += "<td>" + WITLibrary.ConvertString.ToCurrency((detail.Quantity * detail.Price).ToString()) + "</td>";
    //                _stringProduct_email += "</tr>";
    //            }
    //        }
    //        order.TotalDiscount = discountProduct;

    //        //if (SelectedVoucher != null)
    //        //{
    //        //    SelectedVoucher.DateLastUpdate = DateTime.Now;
    //        //    SelectedVoucher.Used++;
    //        //    order.TotalDiscount += Class_Currency.GetPriceDeconversionCurrency(Voucher["Discount"]);
    //        //}
    //        order.Reference = GenerateReferenceCode();

    //        db.SubmitChanges();

    //        //INSERT LOG ORDER
    //        Class_Log_Order logOrder = new Class_Log_Order();
    //        logOrder.Insert((int?)null, order.IDOrder, order.Reference, order.TBOrder_Status.Name, "Admin Place Order", order.TBCustomer.FirstName + ' ' + order.TBCustomer.LastName);

    //        //if (cookieVoucher != null)
    //        //    HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Expires = DateTime.Now.AddDays(-1);
    //        //if (cookieCart != null)
    //        //{
    //        //    //HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()].Expires = DateTime.Now.AddDays(-1);
    //        //    this.DeleteEncodeDataOrder(cookieCart);
    //        //}

    //        //SEND EMAIL KE CUSTOMER
    //        //using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/template-email-waitingPayment.html")))
    //        //{
    //        //    Class_Configuration _config = new Class_Configuration();
    //        //    var emailLogo = _config.Dynamic_Get_EmailLogo();
    //        //    var signCurrency = db.TBCurrencies.Where(x => x.IDCurrency == Class_Currency.GetActiveCurrencyID() && !x.Deflag).FirstOrDefault();
    //        //    string body = "";
    //        //    body = reader.ReadToEnd();
    //        //    body = body.Replace("{name}", user.FirstName);
    //        //    body = body.Replace("{nomorInvoice}", order.Reference);
    //        //    body = body.Replace("{amount}", signCurrency.Sign + " " + WITLibrary.ConvertString.ToCurrency(Class_Currency.GetPriceConversionCurrency((decimal)order.TotalPaid).ToString()));
    //        //    body = body.Replace("{owner}", order.TBPayment_Method.Owner);
    //        //    body = body.Replace("{account}", order.TBPayment_Method.AccountNumber);
    //        //    body = body.Replace("{bank}", order.TBPayment_Method.Bank);
    //        //    body = body.Replace("{title}", System.Configuration.ConfigurationManager.AppSettings["Title"]);
    //        //    body = body.Replace("{email_logo}", System.Configuration.ConfigurationManager.AppSettings["url"] + "/assets/images/email_logo/" + emailLogo);
    //        //    body = body.Replace("{shop_url}", System.Configuration.ConfigurationManager.AppSettings["url"]);
    //        //    OurClass.SendEmail(user.Email, WebConfigurationManager.AppSettings["Title"] + " Order Notification", body, "");
    //        //}

    //        //SEND EMAIL KE ADMIN
    //        //using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/admin-order-notification.html")))
    //        //{
    //        //    string body = "";
    //        //    string _deliveryAddress = "";
    //        //    string _billingAddress = "";

    //        //    _deliveryAddress += order.TBAddress1.PeopleName + "<br/>";
    //        //    _deliveryAddress += order.TBAddress1.Address + "<br/>";
    //        //    _deliveryAddress += order.TBAddress1.TBDistrict.Name + "<br/>";
    //        //    _deliveryAddress += order.TBAddress1.TBProvince.Name + "<br/>";
    //        //    _deliveryAddress += order.TBAddress1.TBCountry.Name + "<br/>";
    //        //    _deliveryAddress += order.TBAddress1.PostalCode + "<br/>";
    //        //    _deliveryAddress += order.TBAddress1.Phone + "<br/>";

    //        //    _billingAddress += order.TBAddress.PeopleName + "<br/>";
    //        //    _billingAddress += order.TBAddress.Address + "<br/>";
    //        //    _billingAddress += order.TBAddress.TBDistrict.Name + "<br/>";
    //        //    _billingAddress += order.TBAddress.TBProvince.Name + "<br/>";
    //        //    _billingAddress += order.TBAddress.TBCountry.Name + "<br/>";
    //        //    _billingAddress += order.TBAddress.PostalCode + "<br/>";
    //        //    _billingAddress += order.TBAddress.Phone + "<br/>";

    //        //    body = reader.ReadToEnd();
    //        //    body = body.Replace("{CustomerName}", user.FirstName);
    //        //    body = body.Replace("{ReferenceCode}", order.Reference);
    //        //    body = body.Replace("{Date}", order.DateInsert.ToString("dd/MM/yyyy hh:mm"));
    //        //    body = body.Replace("{PaymentMethod}", order.TBPayment_Method.Name);
    //        //    body = body.Replace("{Shipping}", order.TBShipping.TBCarrier.Name);
    //        //    body = body.Replace("{bank}", order.TBPayment_Method.Bank);
    //        //    body = body.Replace("{Products}", _stringProduct_email);
    //        //    body = body.Replace("{SubTotal}", WITLibrary.ConvertString.ToCurrency(Class_Currency.GetPriceDeconversionCurrency((decimal)order.TotalPrice).ToString()));
    //        //    body = body.Replace("{TotalShipping}", WITLibrary.ConvertString.ToCurrency(Class_Currency.GetPriceDeconversionCurrency((decimal)order.TotalShipping).ToString()));
    //        //    body = body.Replace("{Discount}", WITLibrary.ConvertString.ToCurrency(Class_Currency.GetPriceDeconversionCurrency((decimal)order.TotalDiscount).ToString()));
    //        //    body = body.Replace("{GrandTotal}", WITLibrary.ConvertString.ToCurrency(Class_Currency.GetPriceDeconversionCurrency((decimal)order.TotalPaid).ToString()));
    //        //    body = body.Replace("{DeliveryAddress}", _deliveryAddress);
    //        //    body = body.Replace("{BillingAddress}", _billingAddress);
    //        //    string emailAdmin = db.TBConfigurations.Where(x => x.Name == "email_user").FirstOrDefault().Value;
    //        //    OurClass.SendEmail(emailAdmin, WebConfigurationManager.AppSettings["Title"] + " Order Notification", body, "");
    //        //}

    //        return ReturnData.MessageSuccess("Order Submit successfully", null);
    //        //return null;
    //    }
    //    catch (Exception ex)
    //    {
    //        Class_Log_Error log = new Class_Log_Error();
    //        log.Insert(ex.Message, ex.StackTrace);

    //        return ReturnData.MessageFailed(ex.Message, null);
    //    }
    //}

    public ReturnData Dynamic_SetTotalPrice(int price)
    {
        try
        {
            //var cookie = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()];
            var cookie = this.GetEncodedDataOrder(HttpContext.Current.Request.AnonymousID);
            if (cookie == null)
                return ReturnData.MessageFailed("Data not found", null);
            var tmp = OurClass.DecryptToken(cookie.EncodedData);
            tmp["Subtotal"] = price;
            string _newToken = OurClass.EncryptToken(tmp);
            //HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()].Value = _newToken;
            this.SaveEncodeDataOrder(HttpContext.Current.Request.AnonymousID, _newToken);
            return ReturnData.MessageSuccess("OK", tmp);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_SaveAddressToCart(int idBillingAddress, int idDeliveryAddress, string notes)
    {
        try
        {
            //var cartCookies = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()];
            var cartCookies = this.GetEncodedDataOrder(HttpContext.Current.Request.AnonymousID);
            decimal _voucher = 0;
            if (cartCookies == null)
                return ReturnData.MessageFailed("Cart is empty", null);
            else
            {
                DataClassesDataContext db = new DataClassesDataContext();
                Class_Address addr = new Class_Address();
                var token = cartCookies.EncodedData;
                dynamic deliveryAddress = addr.Dynamic_GetDetail_ByIDAddress(idDeliveryAddress);
                dynamic billingAddress = addr.Dynamic_GetDetail_ByIDAddress(idBillingAddress);
                if (deliveryAddress == null || billingAddress == null)
                    return ReturnData.MessageFailed("Data not found", null);
                dynamic cartData = OurClass.DecryptToken(token);
                cartData["DeliveryAddress"] = deliveryAddress;
                cartData["BillingAddress"] = billingAddress;
                cartData["Notes"] = notes;

                TBAddress delivAddress = db.TBAddresses.Where(x => x.IDAddress == idDeliveryAddress && !x.Deflag).FirstOrDefault();
                int idShipping = db.TBShippings.Where(x => x.IDDistrict == delivAddress.IDDistrict && !x.Deflag).FirstOrDefault().IDShipping;

                if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()] != null && HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Value != "")
                {
                    string tokenVoucher = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Value;
                    dynamic voucher = OurClass.DecryptToken(tokenVoucher);
                    cartData["Voucher"] = voucher;
                    if (voucher["VoucherType"] == "percent")
                        voucher["Discount"] = (int)(cartData["TotalPrice"] * voucher["Value"] / 100);
                    if (voucher["VoucherType"] == "amount")
                        voucher["Discount"] = voucher["Value"];
                    cartData["Subtotal"] = cartData["TotalPrice"] - voucher["Discount"];
                    _voucher = voucher["Discount"];
                }

                /*SHIPPING*/
                Class_Shipping _shipping = new Class_Shipping();
                dynamic shippingData = _shipping.Dynamic_GetShipping_ByIDShipping(idShipping);
                dynamic resultShipping = new Dictionary<string, dynamic>();
                int idProvince = deliveryAddress.IDProvince;
                int idCity = deliveryAddress.IDCity;

                decimal totalPrice = 0;
                decimal price = 0;

                price = shippingData.Price;
                totalPrice = shippingData.Price * Math.Ceiling((decimal)cartData["TotalWeight"]);

                if (cartData["TotalWeight"] < 1)
                    totalPrice = shippingData.Price;

                resultShipping["IDCarrier"] = shippingData.IDCarrier;
                resultShipping["IDShipping"] = shippingData.IDShipping;
                resultShipping["IDDistrict"] = shippingData.IDDistrict;
                resultShipping["Image"] = shippingData.Image;
                resultShipping["Name"] = shippingData.Name;
                resultShipping["Price"] = price;
                resultShipping["Information"] = shippingData.Information;
                resultShipping["TotalPrice"] = totalPrice;

                cartData["Shipping"] = resultShipping;
                /*END SHIPPING*/


                decimal subtotal = cartData["TotalPrice"] - _voucher + resultShipping["TotalPrice"];
                cartData["Subtotal"] = subtotal;

                token = OurClass.EncryptToken(cartData);
                //HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()].Value = token;
                this.SaveEncodeDataOrder(HttpContext.Current.Request.AnonymousID, token);
                return ReturnData.MessageSuccess("OK", cartData);
            }
        }
        catch (Exception ex)
        {
            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    private string GetProductName_By_IDOrder(DataClassesDataContext db, int idOrder)
    {
        try
        {
            string result = "";
            foreach (var item in db.TBOrder_Details.Where(x => x.IDOrder == idOrder))
            {
                result += item.TBProduct.Name + " (" + item.Quantity + ");<br/>";
            }
            return result;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public ReturnData AJAX_GetShippingLabel(List<int> idOrder)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            List<TBOrder> listShippingLabel = new List<TBOrder>();
            foreach (int item in idOrder)
            {
                var order = db.TBOrders.Where(x => x.IDOrder == item && x.TBOrder_Status.Paid).ToList();
                //.Select(x => new
                //{
                //    Name = x.TBAddress1.PeopleName,
                //    Address = x.TBAddress1.Address,
                //    Phone = x.TBAddress1.Phone,
                //    Product = GetProductName_By_IDOrder(db, item),
                //    City = x.TBAddress1.TBCity.Name,
                //    District = x.TBAddress1.TBDistrict.Name
                //});
                if (order == null)
                    return ReturnData.MessageFailed("Data order not found", null);
                foreach (var label in order)
                {
                    listShippingLabel.Add(label);
                }
            }

            var shippingLabel = listShippingLabel.AsEnumerable().OrderByDescending(x => x.IDOrder).Select(x => new
            {
                Name = x.TBAddress1.PeopleName,
                Address = x.TBAddress1.Address,
                Phone = x.TBAddress1.Phone,
                Product = GetProductName_By_IDOrder(db, x.IDOrder),
                City = x.TBAddress1.TBCity.Name,
                District = x.TBAddress1.TBDistrict.Name,
                Province = x.TBAddress1.TBProvince.Name,
                Country = x.TBAddress1.TBCountry.Name,
                PostalCode = x.TBAddress1.PostalCode,
                ShippingMethod = x.TBShipping.TBCarrier.Name,
                Reference = x.Reference,
                Notes = x.Notes,
                OrderDate = x.DateInsert.ToShortDateString(),
                ShopTitle = System.Configuration.ConfigurationManager.AppSettings["Title"],
                ShopEmail = db.TBConfigurations.Where(y => y.Name.Contains("shop_email")).FirstOrDefault().Value == null ? "-" : db.TBConfigurations.Where(y => y.Name.Contains("shop_email")).FirstOrDefault().Value,
                ShopCity = db.TBConfigurations.Where(y => y.Name.Contains("shop_city")).FirstOrDefault().Value == null ? "-" : db.TBConfigurations.Where(y => y.Name.Contains("shop_city")).FirstOrDefault().Value,
                ShopAddress = db.TBConfigurations.Where(y => y.Name.Contains("shop_address")).FirstOrDefault().Value == null ? "-" : db.TBConfigurations.Where(y => y.Name.Contains("shop_address")).FirstOrDefault().Value,
                ShopPhone = db.TBConfigurations.Where(y => y.Name.Contains("shop_phone")).FirstOrDefault().Value == null ? "-" : db.TBConfigurations.Where(y => y.Name.Contains("shop_phone")).FirstOrDefault().Value,
                EmailLogo = db.TBConfigurations.Where(y => y.Name.Contains("shop_email_logo")).FirstOrDefault().Value == null ? "-" : db.TBConfigurations.Where(y => y.Name.Contains("shop_email_logo")).FirstOrDefault().Value,
            });

            return ReturnData.MessageSuccess("OK", shippingLabel);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public string GenerateInvoiceCode()
    {
        DataClassesDataContext db = new DataClassesDataContext();
        // Generate Nomor Invoice
        string result = "";
        bool cek = true;
        int nomorUrut = db.TBOrders.Where(x => !x.Deflag && x.TBOrder_Status.Paid).Count();
        while (cek)
        {
            DateTime _waktu = DateTime.Now;
            //int nomorUrut = db.TBTransaksis.Where(x => x.Tanggal.Value.Date == _waktu.Date && x.IDStatusTransaksi != StatusTransaksi.UnVerified && x.IDTempat == Custom.StringToInt(Tempat.ECommerce)).Count() + 1;
            nomorUrut++;
            string invoiceWord = System.Configuration.ConfigurationManager.AppSettings["InvoiceWord"];
            result = invoiceWord + _waktu.ToString("MMyy") + "-" + (("000000" + nomorUrut.ToString()).Substring(nomorUrut.ToString().Length - 1, 7));
            if (db.TBOrders.Where(x => x.InvoiceNumber == result).FirstOrDefault() == null)
                cek = false;
        }
        return result;
    }

    public string GenerateReferenceCode()
    {
        DataClassesDataContext db = new DataClassesDataContext();
        // Generate Nomor Invoice
        bool exists = true;
        string result = "";
        while (exists)
        {
            DateTime _waktu = DateTime.Now;
            //int nomorUrut = db.TBTransaksis.Where(x => x.Tanggal.Value.Date == _waktu.Date && x.IDStatusTransaksi != StatusTransaksi.UnVerified && x.IDTempat == Custom.StringToInt(Tempat.ECommerce)).Count() + 1;
            int nomorUrut = db.TBOrders.Count() + 1;
            result = _waktu.ToString("MMyy") + "-" + (("000000" + nomorUrut.ToString()).Substring(nomorUrut.ToString().Length - 1, 7));
            if (db.TBOrders.Where(x => x.Reference == result).FirstOrDefault() == null)
                exists = false;
        }
        return result;
    }

    public dynamic AJAX_GetCart()
    {
        try
        {
            Dictionary<string, dynamic> _product = new Dictionary<string, dynamic>();
            List<Dictionary<string, dynamic>> _listProduct = new List<Dictionary<string, dynamic>>();
            Dictionary<string, dynamic> _tokenData = new Dictionary<string, dynamic>();
            //HttpCookie _cookie = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()];
            var _cookie = this.GetEncodedDataOrder(HttpContext.Current.Request.AnonymousID);
            if (_cookie == null)
                return ReturnData.MessageFailed("Data not found", null);
            else
            {
                string _token = _cookie.EncodedData;

                var _tempTokenData = (ArrayList)(OurClass.DecryptToken(_token) as IDictionary<string, dynamic>)["Product"];

                List<int> _listIDCom = new List<int>();// list untuk simpan list idcombination untuk pengecekan
                int totalPrice = 0;
                int totalQuantity = 0;
                foreach (dynamic item in _tempTokenData)
                {
                    _listIDCom.Add(item["IDCombination"]);
                    _product = new Dictionary<string, dynamic>();
                    _product.Add("IDProduct", item["IDProduct"]);
                    _product.Add("IDCombination", item["IDCombination"]);
                    _product.Add("Quantity", item["Quantity"]);
                    _product.Add("Price", item["Price"]);
                    _product.Add("CombinationName", item["CombinationName"]);
                    _product.Add("ProductName", item["ProductName"]);
                    _listProduct.Add(_product);

                    totalPrice += _product["Price"];
                    totalQuantity += _product["Quantity"];

                }
                _tokenData.Add("Product", _listProduct);
                _tokenData.Add("TotalPrice", totalPrice);
                _tokenData.Add("TotalQuantity", totalQuantity);
                return ReturnData.MessageSuccess("OK", _tokenData);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public dynamic DYNAMIC_GetCart()
    {
        try
        {
            Dictionary<string, dynamic> _product = new Dictionary<string, dynamic>();
            List<Dictionary<string, dynamic>> _listProduct = new List<Dictionary<string, dynamic>>();
            Dictionary<string, dynamic> _tokenData = new Dictionary<string, dynamic>();
            //HttpCookie _cookie = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()];
            var _cookie = this.GetEncodedDataOrder(HttpContext.Current.Request.AnonymousID);

            if (_cookie == null)
                return ReturnData.MessageFailed("Data not found", null);
            else
            {
                string _token = _cookie.EncodedData;

                var _tempTokenData = (ArrayList)(OurClass.DecryptToken(_token) as IDictionary<string, dynamic>)["Product"];

                List<int> _listIDCom = new List<int>();// list untuk simpan list idcombination untuk pengecekan
                int totalPrice = 0;
                int totalQuantity = 0;
                foreach (dynamic item in _tempTokenData)
                {
                    _listIDCom.Add(item["IDCombination"]);
                    _product = new Dictionary<string, dynamic>();
                    _product.Add("IDProduct", item["IDProduct"]);
                    _product.Add("IDCombination", item["IDCombination"]);
                    _product.Add("Quantity", item["Quantity"]);
                    _product.Add("Price", item["Price"]);
                    _product.Add("CombinationName", item["CombinationName"]);
                    _product.Add("ProductName", item["ProductName"]);
                    _listProduct.Add(_product);

                    totalPrice += _product["Price"];
                    totalQuantity += _product["Quantity"];

                }
                _tokenData.Add("Product", _listProduct);
                _tokenData.Add("TotalPrice", totalPrice);
                _tokenData.Add("TotalQuantity", totalQuantity);
                return _tokenData;
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }

    }

    public dynamic DYNAMIC_GetCartSummary()
    {
        try
        {
            Class_Product _pro = new Class_Product();

            List<Dictionary<string, dynamic>> _listProduct = new List<Dictionary<string, dynamic>>();
            Dictionary<string, dynamic> _product = new Dictionary<string, dynamic>();
            Dictionary<string, dynamic> _tokenData = new Dictionary<string, dynamic>();
            decimal _voucher = 0;
            //HttpCookie _cookie = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()];
            var _cookie = this.GetEncodedDataOrder(HttpContext.Current.Request.AnonymousID);
            if (_cookie == null)
                return ReturnData.MessageFailed("Data not found", null);
            else
            {
                string _token = _cookie.EncodedData;

                //var _tempTokenData = (ArrayList)(OurClass.DecryptToken(_token) as IDictionary<string, dynamic>)["Product"];

                //List<int> _listIDCom = new List<int>();// list untuk simpan list idcombination untuk pengecekan
                //int totalPrice = 0;
                //int totalQuantity = 0;
                DataClassesDataContext db = new DataClassesDataContext();
                //List<TBProduct_Combination> ListProduct = new List<TBProduct_Combination>(); //list dari database
                //foreach (dynamic item in _tempTokenData)
                //{
                //    int idcombination = (int)item["IDCombination"];
                //    TBProduct_Combination selectedProduct = db.TBProduct_Combinations.Where(x => x.IDProduct_Combination == idcombination && !x.Deflag).FirstOrDefault();
                //    if (selectedProduct != null)
                //        ListProduct.Add(selectedProduct);

                //    _listIDCom.Add(item["IDCombination"]);
                //    _product = new Dictionary<string, dynamic>();
                //    _product.Add("IDProduct", item["IDProduct"]);
                //    _product.Add("IDCombination", item["IDCombination"]);
                //    _product.Add("Quantity", item["Quantity"]);
                //    _product.Add("Price", item["Price"]);
                //    _product.Add("CombinationName", item["CombinationName"]);
                //    _product.Add("ProductName", item["ProductName"]);
                //    _product.Add("Photo", (new Class_Product_Photo()).GetCover(db, item["IDProduct"]).Photo);
                //    _product.Add("ReferenceCode", selectedProduct.ReferenceCode);
                //    _product.Add("PricePerUnit", selectedProduct.Price);
                //    _product.Add("MaxQuantity", selectedProduct.Quantity);
                //    _listProduct.Add(_product);

                //    totalPrice += _product["Price"];
                //    totalQuantity += _product["Quantity"];

                //}
                //_tokenData.Add("Product", _listProduct);
                //_tokenData.Add("TotalPrice", totalPrice);
                //_tokenData.Add("TotalQuantity", totalQuantity);
                //if ((tmp as Dictionary<string, dynamic>).ContainsKey("Subtotal"))
                //    _tokenData.Add("Subtotal", tmp["Subtotal"]);
                //return _tokenData;

                dynamic tmp = OurClass.DecryptToken(_token);
                dynamic product = tmp["Product"];
                //dynamic idProduct = product["IDProduct"];
                //dynamic photo = 
                foreach (dynamic item in product)
                {
                    int idcombination = (int)item["IDCombination"];
                    TBProduct_Combination selectedProduct = db.TBProduct_Combinations.Where(x => x.IDProduct_Combination == idcombination && !x.Deflag).FirstOrDefault();
                    if (selectedProduct != null)
                    {
                        item["Photo"] = (new Class_Product_Photo()).GetCover(db, selectedProduct.IDProduct).Photo;
                        item["ReferenceCode"] = selectedProduct.ReferenceCode;
                        item["PricePerUnit"] = Class_Currency.GetPriceConversionCurrency(selectedProduct.Price);
                        item["MaxQuantity"] = selectedProduct.Quantity;
                        item["DefaultCategory"] = selectedProduct.TBProduct.TBProduct_Categories.FirstOrDefault(x => x.IsDefault).TBCategory.Name;
                    }
                    int idproduct = (int)item["IDProduct"];
                    TBProduct_Category categoryProduct = db.TBProduct_Categories.Where(x => x.IDProduct == idproduct && x.IsDefault == true).FirstOrDefault();
                    if (categoryProduct != null)
                    {
                        item["IDCategory"] = categoryProduct.IDCategory;
                    }
                }
                tmp["Product"] = product;
                if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()] != null && HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Value != "")
                {
                    string token = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Value;
                    dynamic voucher = OurClass.DecryptToken(token);
                    tmp["Voucher"] = voucher;
                    if (voucher["VoucherType"] == "percent")
                        voucher["Discount"] = tmp["TotalPrice"] * voucher["Value"] / 100;
                    if (voucher["VoucherType"] == "amount")
                        voucher["Discount"] = Class_Currency.GetPriceConversionCurrency(voucher["Value"]);
                    tmp["Subtotal"] = tmp["TotalPrice"] - voucher["Discount"];
                    _voucher = voucher["Discount"];
                }

                if ((tmp as Dictionary<string, dynamic>).ContainsKey("Shipping"))
                {
                    dynamic shipping = tmp["Shipping"];

                    shipping["TotalPrice"] = Math.Ceiling((decimal)tmp["TotalWeight"]) * shipping["Price"];
                    if (Math.Ceiling((decimal)tmp["TotalWeight"]) < 1)
                    {
                        shipping["TotalPrice"] = shipping["Price"];
                    }
                    decimal subtotal = tmp["TotalPrice"] - _voucher + shipping["TotalPrice"];
                    tmp["Subtotal"] = subtotal;
                }
                return tmp;
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public dynamic AJAX_DeleteCart(int idcombination)
    {
        try
        {
            //HttpCookie _cookie = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()];
            var _cookie = this.GetEncodedDataOrder(HttpContext.Current.Request.AnonymousID);
            string _token = _cookie.EncodedData;
            dynamic AllData = OurClass.DecryptToken(_token);
            dynamic products = AllData["Product"];
            int totalPriceUpdated = 0;
            int selecetdIndex = -1;

            if (_cookie == null)
                return ReturnData.MessageFailed("Data not found", null);
            else
            {
                if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()] != null && HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Value != "")
                {
                    //Perhitungan Minimum Amount dari Voucher 
                    string token = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Value;
                    dynamic voucher = OurClass.DecryptToken(token);
                    foreach (dynamic item in products)
                    {
                        if (item["IDCombination"] == idcombination)
                        {
                            selecetdIndex = products.IndexOf(item);
                        }
                        else
                        {
                            int priceUpdated = item["Quantity"] * item["PricePerUnit"];
                            totalPriceUpdated += priceUpdated;
                        }
                    }

                    // Jika Harga Total 0 atau cart kosong, voucher di restart
                    if (totalPriceUpdated <= 0)
                    {
                        int totalPrice = 0;
                        int totalQty = 0;
                        decimal totalWeight = 0;
                        foreach (dynamic item in products)
                        {
                            if (item["IDCombination"] == idcombination)
                            {
                                selecetdIndex = products.IndexOf(item);
                            }
                            else
                            {
                                totalQty += item["Quantity"];
                                totalPrice += item["Price"];
                                totalWeight += (item["Weight"]);
                            }
                        }
                        if (selecetdIndex == -1)
                            return ReturnData.MessageFailed("Data not found", null);
                        products.RemoveAt(selecetdIndex);
                        AllData["Product"] = products;
                        AllData["TotalPrice"] = totalPrice;
                        AllData["Subtotal"] = totalPrice;
                        AllData["TotalQuantity"] = totalQty;
                        AllData["TotalWeight"] = totalWeight;

                        _token = OurClass.EncryptToken(AllData);
                        this.SaveEncodeDataOrder(HttpContext.Current.Request.AnonymousID, _token);

                        HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Expires = DateTime.Now.AddDays(-1);

                        return ReturnData.MessageSuccess("Your used voucher was expired because your cart is empty. Please enter the voucher code again when you added new product", AllData);
                    }

                    // Jika Harga Total Product lebih Kecil dari Minimum Amount Voucher, Return Data
                    else if (totalPriceUpdated < voucher["MinimumAmount"] && _token != null)
                        return ReturnData.MessageFailed("You have used voucher with the minimal amount rules, you cannot shop below minimal amount voucher rules", AllData);

                    else
                    {
                        int totalPrice = 0;
                        int totalQty = 0;
                        decimal totalWeight = 0;
                        foreach (dynamic item in products)
                        {
                            if (item["IDCombination"] == idcombination)
                            {
                                selecetdIndex = products.IndexOf(item);
                            }
                            else
                            {
                                totalQty += item["Quantity"];
                                totalPrice += item["Price"];
                                totalWeight += (item["Weight"]);
                            }
                        }
                        if (selecetdIndex == -1)
                            return ReturnData.MessageFailed("Data not found", null);
                        products.RemoveAt(selecetdIndex);
                        AllData["Product"] = products;
                        AllData["TotalPrice"] = totalPrice;
                        AllData["Subtotal"] = totalPrice;
                        AllData["TotalQuantity"] = totalQty;
                        AllData["TotalWeight"] = totalWeight;

                        //UPDATE VOUCHER VALUE
                        if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()] != null && HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Value != "")
                        {
                            AllData["Voucher"] = voucher;
                            if (voucher["VoucherType"] == "percent")
                                voucher["Discount"] = AllData["TotalPrice"] * voucher["Value"] / 100;
                            if (voucher["VoucherType"] == "amount")
                                voucher["Discount"] = voucher["Value"];
                            AllData["Subtotal"] = AllData["TotalPrice"] - voucher["Discount"];
                            token = OurClass.EncryptToken(voucher);
                            HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Value = token;
                        }

                        _token = OurClass.EncryptToken(AllData);
                        this.SaveEncodeDataOrder(HttpContext.Current.Request.AnonymousID, _token);


                        return ReturnData.MessageSuccess("OK", AllData);
                    }
                }
                // Jika Voucher tidak ada, maka tidak perlu perhitungan Nilai voucher
                else
                {
                    int totalPrice = 0;
                    int totalQty = 0;
                    decimal totalWeight = 0;
                    foreach (dynamic item in products)
                    {
                        if (item["IDCombination"] == idcombination)
                        {
                            selecetdIndex = products.IndexOf(item);
                        }
                        else
                        {
                            totalQty += item["Quantity"];
                            totalPrice += item["Price"];
                            totalWeight += (item["Weight"]);
                        }
                    }
                    if (selecetdIndex == -1)
                        return ReturnData.MessageFailed("Data not found", null);
                    products.RemoveAt(selecetdIndex);
                    AllData["Product"] = products;
                    AllData["TotalPrice"] = totalPrice;
                    AllData["Subtotal"] = totalPrice;
                    AllData["TotalQuantity"] = totalQty;
                    AllData["TotalWeight"] = totalWeight;

                    //UPDATE VOUCHER VALUE
                    //if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()] != null && HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Value != "")
                    //{
                    //    string token = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Value;
                    //    dynamic voucher = OurClass.DecryptToken(token);
                    //    AllData["Voucher"] = voucher;
                    //    if (voucher["VoucherType"] == "percent")
                    //        voucher["Discount"] = AllData["TotalPrice"] * voucher["Value"] / 100;
                    //    if (voucher["VoucherType"] == "amount")
                    //        voucher["Discount"] = voucher["Value"];
                    //    AllData["Subtotal"] = AllData["TotalPrice"] - voucher["Discount"];
                    //    token = OurClass.EncryptToken(voucher);
                    //    HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Value = token;
                    //}

                    _token = OurClass.EncryptToken(AllData);
                    this.SaveEncodeDataOrder(HttpContext.Current.Request.AnonymousID, _token);

                    return ReturnData.MessageSuccess("OK", AllData);
                }

                #region obselete
                //var _tempTokenData = (ArrayList)(OurClass.DecryptToken(_token) as IDictionary<string, dynamic>)["Product"];

                //List<int> _listIDCom = new List<int>();// list untuk simpan list idcombination untuk pengecekan
                //int totalPrice = 0;
                //int totalQuantity = 0;
                //foreach (dynamic item in _tempTokenData)
                //{
                //    if (item["IDCombination"] != idcombination)
                //    {
                //        _listIDCom.Add(item["IDCombination"]);
                //        _product = new Dictionary<string, dynamic>();
                //        _product.Add("IDCombination", item["IDCombination"]);
                //        _product.Add("Quantity", item["Quantity"]);
                //        _product.Add("Price", item["Price"]);
                //        _product.Add("CombinationName", item["CombinationName"]);
                //        _product.Add("ProductName", item["ProductName"]);
                //        _listProduct.Add(_product);

                //        totalPrice += _product["Price"];
                //        totalQuantity += _product["Quantity"];
                //    }
                //}


                //_tokenData.Add("Product", _listProduct);
                //_tokenData.Add("TotalPrice", totalPrice);
                //_tokenData.Add("TotalQuantity", totalQuantity);
                //_token = OurClass.EncryptToken(_tokenData);
                //_cookie.Value = _token;
                //HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()].Value = _token;
                //return ReturnData.MessageSuccess("Product deleted successfully", _tokenData);
                #endregion
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public dynamic AJAX_UpdateQuantity(int idcombination, int qty)
    {
        try
        {
            //var cookie = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()];
            var cookie = this.GetEncodedDataOrder(HttpContext.Current.Request.AnonymousID);
            string token = cookie.EncodedData;
            dynamic AllData = OurClass.DecryptToken(token);
            dynamic products = AllData["Product"];
            int totalPriceUpdated = 0;
            if (cookie == null)
                return ReturnData.MessageFailed("Data not found", null);
            else
            {
                //Perhitungan Minimum Amount dari Voucher 
                if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()] != null && HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Value != "")
                {
                    string _token = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Value;
                    dynamic voucher = OurClass.DecryptToken(_token);

                    foreach (dynamic item in products)
                    {
                        int priceUpdated = qty * item["PricePerUnit"];
                        totalPriceUpdated += priceUpdated;
                    }
                    // Jika Harga Total Product lebih Kecil dari Minimum Amount Voucher, Return Data
                    if (totalPriceUpdated < voucher["MinimumAmount"] && _token != null)
                        return ReturnData.MessageFailed("You have used voucher with the minimal amount rules, you cannot shop below minimal amount voucher rules", AllData);
                    else
                    {
                        int totalQty = 0;
                        int totalPrice = 0;
                        decimal totalWeight = 0;
                        foreach (dynamic item in products)
                        {

                            if (item["IDCombination"] == idcombination)
                            {
                                item["Quantity"] = qty;
                                item["Price"] = qty * item["PricePerUnit"];
                                item["Weight"] = qty * item["WeightPerUnit"];
                            }
                            totalQty += item["Quantity"];
                            totalPrice += item["Price"];
                            totalWeight += (item["Weight"]);
                        }
                        AllData["TotalQuantity"] = totalQty;
                        AllData["TotalPrice"] = totalPrice;
                        AllData["Subtotal"] = totalPrice;
                        AllData["TotalWeight"] = totalWeight;

                        //UPDATE VOUCHER VALUE
                        if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()] != null && HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Value != "")
                        {
                            AllData["Voucher"] = voucher;
                            if (voucher["VoucherType"] == "percent")
                                voucher["Discount"] = AllData["TotalPrice"] * voucher["Value"] / 100;
                            if (voucher["VoucherType"] == "amount")
                                voucher["Discount"] = voucher["Value"];
                            AllData["Subtotal"] = AllData["TotalPrice"] - voucher["Discount"];
                            _token = OurClass.EncryptToken(voucher);
                            HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Value = _token;
                        }

                        token = OurClass.EncryptToken(AllData);
                        this.SaveEncodeDataOrder(HttpContext.Current.Request.AnonymousID, token);

                        return ReturnData.MessageSuccess("OK", AllData);
                    }
                }
                // Jika Voucher tidak ada, maka tidak perlu perhitungan Nilai voucher
                else
                {
                    int totalQty = 0;
                    int totalPrice = 0;
                    decimal totalWeight = 0;
                    foreach (dynamic item in products)
                    {

                        if (item["IDCombination"] == idcombination)
                        {
                            item["Quantity"] = qty;
                            item["Price"] = qty * item["PricePerUnit"];
                            item["Weight"] = qty * item["WeightPerUnit"];
                        }
                        totalQty += item["Quantity"];
                        totalPrice += item["Price"];
                        totalWeight += (item["Weight"]);
                    }
                    AllData["TotalQuantity"] = totalQty;
                    AllData["TotalPrice"] = totalPrice;
                    AllData["Subtotal"] = totalPrice;
                    AllData["TotalWeight"] = totalWeight;

                    //UPDATE VOUCHER VALUE
                    //if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()] != null && HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Value != "")
                    //{
                    //    AllData["Voucher"] = voucher;
                    //    if (voucher["VoucherType"] == "percent")
                    //        voucher["Discount"] = AllData["TotalPrice"] * voucher["Value"] / 100;
                    //    if (voucher["VoucherType"] == "amount")
                    //        voucher["Discount"] = voucher["Value"];
                    //    AllData["Subtotal"] = AllData["TotalPrice"] - voucher["Discount"];
                    //    _token = OurClass.EncryptToken(voucher);
                    //    HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Value = _token;
                    //}

                    token = OurClass.EncryptToken(AllData);
                    this.SaveEncodeDataOrder(HttpContext.Current.Request.AnonymousID, token);

                    return ReturnData.MessageSuccess("OK", AllData);
                }

            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public dynamic DYNAMIC_AddToCart(int idProduct, int idCombination, int qty, decimal price, string combinationName, string productName, string orderType)
    {
        try
        {
            //PRICE CONVERSION
            price = Class_Currency.GetPriceConversionCurrency(price);

            DataClassesDataContext db = new DataClassesDataContext();
            //HttpCookie _cookie = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()];
            string anonID = HttpContext.Current.Request.AnonymousID;
            TBOrder_temp _cookie = this.GetEncodedDataOrder(anonID);

            string _token = "";
            Dictionary<string, dynamic> _product = new Dictionary<string, dynamic>();
            List<Dictionary<string, dynamic>> _listProduct = new List<Dictionary<string, dynamic>>();
            Dictionary<string, dynamic> _tokenData = new Dictionary<string, dynamic>();
            TBProduct_Combination productCombination = db.TBProduct_Combinations.Where(x => !x.Deflag && x.IDProduct_Combination == idCombination).FirstOrDefault();
            if (productCombination == null)
                return ReturnData.MessageFailed("Data not found", null);
            //if (_cookie == null)
            //{
            _product.Add("IDProduct", idProduct);
            _product.Add("IDCombination", idCombination);
            _product.Add("Price", price * qty);
            _product.Add("CombinationName", combinationName);
            _product.Add("Quantity", qty);
            _product.Add("ProductName", productName);
            _product.Add("PricePerUnit", price);
            _product.Add("Weight", productCombination.Weight * qty);
            _product.Add("WeightPerUnit", productCombination.Weight);

            _listProduct.Add(_product);
            _tokenData.Add("Product", _listProduct);
            _tokenData.Add("TotalPrice", price * qty);
            _tokenData.Add("TotalQuantity", qty);
            _tokenData.Add("TotalWeight", productCombination.Weight * qty);
            _tokenData.Add("Subtotal", price * qty);
            _tokenData.Add("IDCurrency", Class_Currency.GetActiveCurrencyID());
            _tokenData.Add("OrderType", orderType);
            _tokenData.Add("IDCustomerProduct", null);
            _token = OurClass.EncryptToken(_tokenData);
            //HttpContext.Current.Response.Cookies.Add(new HttpCookie(System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString(), _token));
            this.SaveEncodeDataOrder(HttpContext.Current.Request.AnonymousID, _token);
            //HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()].Expires = DateTime.Now.AddMinutes(120);
            return ReturnData.MessageSuccess("Product add to cart successfully", _tokenData);
            //}
            //else
            //{
            //    _token = _cookie.EncodedData;

            //    var _tempTokenData = (ArrayList)(OurClass.DecryptToken(_token) as IDictionary<string, dynamic>)["Product"];

            //    List<int> _listIDCom = new List<int>();// list untuk simpan list idcombination untuk pengecekan
            //    decimal totalPrice = 0;
            //    int totalQuantity = 0;
            //    decimal totalWeight = 0;
            //    foreach (dynamic item in _tempTokenData)
            //    {
            //        _listIDCom.Add(item["IDCombination"]);
            //        _product = new Dictionary<string, dynamic>();
            //        _product.Add("IDProduct", item["IDProduct"]);
            //        _product.Add("IDCombination", item["IDCombination"]);
            //        _product.Add("Quantity", item["Quantity"]);
            //        _product.Add("Price", item["PricePerUnit"] * item["Quantity"]);
            //        _product.Add("CombinationName", item["CombinationName"]);
            //        _product.Add("ProductName", item["ProductName"]);
            //        _product.Add("PricePerUnit", item["PricePerUnit"]);
            //        _product.Add("WeightPerUnit", item["WeightPerUnit"]);
            //        _product.Add("Weight", item["Weight"]);
            //        _listProduct.Add(_product);

            //        totalPrice += _product["Price"];
            //        totalQuantity += _product["Quantity"];
            //        totalWeight += _product["Weight"];
            //    }
            //    if (_listIDCom.Contains(idCombination))
            //    {
            //        foreach (var item in _listProduct)
            //        {
            //            var prodQty = db.TBProduct_Combinations.Where(x => x.IDProduct_Combination == idCombination && !x.Deflag).FirstOrDefault();
            //            if (prodQty.Quantity < item["Quantity"] + qty)
            //            {
            //                return ReturnData.MessageFailed("You added maximum quantity of this combination", null);
            //            }
            //            else
            //            {
            //                if (item["IDCombination"] == idCombination)
            //                {
            //                    totalPrice -= item["Price"];
            //                    item["Quantity"] += qty;
            //                    item["Price"] = (item["Quantity"] * price);
            //                    item["Weight"] = item["Quantity"] * item["WeightPerUnit"]; // update shipping witcommerce
            //                    totalPrice += item["Price"];
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        _product = new Dictionary<string, dynamic>();
            //        _product.Add("IDProduct", idProduct);
            //        _product.Add("IDCombination", idCombination);
            //        _product.Add("Price", price * qty);
            //        _product.Add("CombinationName", combinationName);
            //        _product.Add("Quantity", qty);
            //        _product.Add("ProductName", productName);
            //        _product.Add("PricePerUnit", price);
            //        _product.Add("WeightPerUnit", productCombination.Weight);
            //        _product.Add("Weight", productCombination.Weight * qty);
            //        _listProduct.Add(_product);

            //        totalPrice += _product["Price"];
            //        totalWeight += _product["Weight"];
            //    }
            //    totalQuantity += qty;
            //    _tokenData.Add("Product", _listProduct);
            //    _tokenData.Add("TotalPrice", totalPrice);
            //    _tokenData.Add("TotalQuantity", totalQuantity);
            //    _tokenData.Add("TotalWeight", totalWeight);
            //    _tokenData.Add("IDCurrency", Class_Currency.GetActiveCurrencyID());
            //    _tokenData.Add("OrderType", orderType);
            //    _tokenData.Add("IDOrderReferral", null);
            //    //UPDATE VOUCHER VALUE
            //    if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()] != null && HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Value != "")
            //    {
            //        string token = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Value;
            //        dynamic voucher = OurClass.DecryptToken(token);
            //        _tokenData["Voucher"] = voucher;
            //        if (voucher["VoucherType"] == "percent")
            //            voucher["Discount"] = _tokenData["TotalPrice"] * voucher["Value"] / 100;
            //        if (voucher["VoucherType"] == "amount")
            //            voucher["Discount"] = voucher["Value"];
            //        _tokenData["Subtotal"] = Class_Currency.GetPriceConversionCurrency(_tokenData["TotalPrice"]) - Class_Currency.GetPriceConversionCurrency(voucher["Discount"]);
            //        _token = OurClass.EncryptToken(voucher);
            //        HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Value = _token;
            //    }
            //    else
            //        _tokenData.Add("Subtotal", totalPrice);

            //    _token = OurClass.EncryptToken(_tokenData);
            //    HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()].Value = _token;
            //    if (this.SaveEncodeDataOrder(_cookie.AnonID, _token))
            //    {
            //        return ReturnData.MessageSuccess("Product add to cart successfully", _tokenData);
            //    }
            //    else
            //        return ReturnData.MessageSuccess("Could not save to database.", _token);
            //}
            //cek cookies nya ada ngga, kalo ada ganti token
            //kalo ga ada tambahin dulu _witcomcrt_
            //cek kalo token nya kosong
            // kalo ngga kosong, decrypt dulu, ambil object nya.
            //trus tambahin sama cart yang baru
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public dynamic Dynamic_GetData_ByIDCustomer(int IDCustomer)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBOrders.Where(x => !x.Deflag && x.IDCustomer == IDCustomer).Select(x => new
            {
                x.IDBillingAddress,
                x.IDCustomer,
                x.IDDeliveryAddress,
                x.IDOrder,
                x.IDOrderStatus,
                x.IDPaymentMethod,
                x.IDShipping,
                x.IDVoucher,
                x.InvoiceNumber,
                x.Notes,
                x.Payment,
                x.Reference,
                x.ShippingNumber,
                TotalDiscount = Class_Currency.GetPriceConversionCurrency((decimal)x.TotalDiscount),
                TotalPaid = Class_Currency.GetPriceConversionCurrency((decimal)x.TotalPaid),
                TotalPrice = Class_Currency.GetPriceConversionCurrency((decimal)x.TotalPrice),
                TotalShipping = Class_Currency.GetPriceConversionCurrency((decimal)x.TotalShipping),
                x.TotalWeight,
                Date = x.DateInsert,
                PaymentMethod = x.TBPayment_Method.Name,
                OrderStatus = x.TBOrder_Status.Name,
                Shipping = x.TBShipping.TBCarrier.Name,
                TotalProduct = x.TBOrder_Details.Count()
            });
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_GetData_ProductBought_ByIDCustomer(int idCustomer)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBOrder_Details.Where(x => !x.TBOrder.Deflag && x.TBOrder.IDCustomer == idCustomer).Select(x => new
            {
                IDOrder = x.IDOrder,
                ProductName = x.TBProduct.Name,
                CombinationName = x.Product_Name,
                Quantity = x.Quantity,
                Price = x.Price,
                Date = x.TBOrder.DateInsert.ToShortDateString()
            });
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_GetData_OrderNumber_ByIDCustomer(int IDCustomer)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBOrders.Where(x => !x.Deflag && x.IDCustomer == IDCustomer && !x.TBOrder_Status.Paid && !x.TBOrder_Status.Logable).Select(x => new
            {
                x.IDOrder,
                x.Reference
            });
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_GetData_IDOrder_ByReference(string reference)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBOrders.Where(x => !x.Deflag && x.Reference.Trim() == reference.Trim() && !x.TBOrder_Status.Paid && !x.TBOrder_Status.Logable).Select(x => new
            {
                x.IDOrder,
                x.Reference
            }).FirstOrDefault().IDOrder;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public dynamic Dynamic_GetDetail_ByIDOrder(int IDOrder)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            Class_Address address = new Class_Address();
            Class_Product_Combination combination = new Class_Product_Combination();
            Class_Payment_Confirmation ConfirmPayment = new Class_Payment_Confirmation();
            return db.TBOrders.Where(x => !x.Deflag && x.IDOrder == IDOrder).OrderByDescending(x => x.IDOrder).Select(x => new
            {
                x.IDBillingAddress,
                x.IDCustomer,
                x.IDDeliveryAddress,
                x.IDOrder,
                x.IDOrderStatus,
                x.IDPaymentMethod,
                x.IDShipping,
                x.IDVoucher,
                x.InvoiceNumber,
                x.Notes,
                x.Payment,
                x.Reference,
                x.ShippingNumber,
                x.TotalDiscount,
                x.TotalPaid,
                TotalPriceProduct = x.TotalPrice,
                x.TotalShipping,
                x.TotalWeight,
                x.ConversionRate,
                IDPaymentConfirm = db.TBPayment_Confirmations.Where(p => p.IDOrder == x.IDOrder).FirstOrDefault() == null ? 0 : db.TBPayment_Confirmations.Where(p => p.IDOrder == x.IDOrder).FirstOrDefault().IDPaymentConfirmation,
                Date = x.DateInsert,
                DateUpdate = x.DateLastUpdate,
                OrderStatus = x.TBOrder_Status.Name,
                PaymentMethod = x.TBPayment_Method.Name,
                CustomerName = x.TBCustomer.FirstName + " " + x.TBCustomer.LastName,
                CustomerEmail = x.TBCustomer.Email,
                BillingAddress = address.Dynamic_GetDetail_ByIDAddress_OrderDetail(x.TBAddress.IDAddress),
                DeliveryAddress = address.Dynamic_GetDetail_ByIDAddress_OrderDetail(x.TBAddress1.IDAddress),
                Product = combination.Dynamic_GetData_ByIDProductCombination(x.TBOrder_Details.Select(p => p.IDOrder_Detail).ToArray()),
                ConfirmPayment = ConfirmPayment.Dynamic_GetDetail_ByIDOrder(x.IDOrder),
                ShippingName = x.TBShipping.TBCarrier.Name,
                ShippingImage = x.TBShipping.TBCarrier.Image,
                PaymentImage = x.TBPayment_Method.Image,
                PaymentOwner = x.TBPayment_Method.Owner,
                PaymentAccountNumber = x.TBPayment_Method.AccountNumber
            }).FirstOrDefault();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_GetDetailInvoice_ByIDOrder(List<int> IDOrder)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            Class_Address address = new Class_Address();
            Class_Product_Combination combination = new Class_Product_Combination();
            Class_Payment_Confirmation ConfirmPayment = new Class_Payment_Confirmation();
            List<TBOrder> _listOrder = new List<TBOrder>();
            foreach (var id in IDOrder)
            {
                var order = db.TBOrders.Where(x => x.IDOrder == id && !x.Deflag && x.TBOrder_Status.Paid).ToList();
                foreach (var item in order)
                {
                    _listOrder.Add(item);
                }
            }
            return _listOrder.AsEnumerable().OrderByDescending(x => x.IDOrder).Select(x => new
            {
                x.IDBillingAddress,
                x.IDCustomer,
                x.IDDeliveryAddress,
                x.IDOrder,
                x.IDOrderStatus,
                x.IDPaymentMethod,
                x.IDShipping,
                x.IDVoucher,
                x.InvoiceNumber,
                x.Notes,
                x.Payment,
                x.Reference,
                x.ShippingNumber,
                x.TotalDiscount,
                x.TotalPaid,
                x.TotalPrice,
                x.TotalShipping,
                x.TotalWeight,
                IDPaymentConfirm = db.TBPayment_Confirmations.Where(p => p.IDOrder == x.IDOrder).FirstOrDefault() == null ? 0 : db.TBPayment_Confirmations.Where(p => p.IDOrder == x.IDOrder).FirstOrDefault().IDPaymentConfirmation,
                Date = x.DateInsert,
                DateUpdate = x.DateLastUpdate,
                OrderStatus = x.TBOrder_Status.Name,
                PaymentMethod = x.TBPayment_Method.Name,
                CustomerName = x.TBCustomer.FirstName + " " + x.TBCustomer.LastName,
                CustomerEmail = x.TBCustomer.Email,
                BillingAddress = address.Dynamic_GetDetail_ByIDAddress_OrderDetail(x.TBAddress.IDAddress),
                DeliveryAddress = address.Dynamic_GetDetail_ByIDAddress_OrderDetail(x.TBAddress1.IDAddress),
                Product = combination.Dynamic_GetData_ByIDProductCombination(x.TBOrder_Details.Select(p => p.IDOrder_Detail).ToArray()),
                ConfirmPayment = ConfirmPayment.Dynamic_GetDetail_ByIDOrder(x.IDOrder),
                ShippingName = x.TBShipping.TBCarrier.Name,
                ShippingImage = x.TBShipping.TBCarrier.Image,
                PaymentImage = x.TBPayment_Method.Image,
                PaymentOwner = x.TBPayment_Method.Owner,
                PaymentAccountNumber = x.TBPayment_Method.AccountNumber,
                ShopTitle = System.Configuration.ConfigurationManager.AppSettings["Title"],
                ShopEmail = db.TBConfigurations.Where(y => y.Name.Contains("shop_email")).FirstOrDefault().Value == null ? "-" : db.TBConfigurations.Where(y => y.Name.Contains("shop_email")).FirstOrDefault().Value,
                ShopCity = db.TBConfigurations.Where(y => y.Name.Contains("shop_city")).FirstOrDefault().Value == null ? "-" : db.TBConfigurations.Where(y => y.Name.Contains("shop_city")).FirstOrDefault().Value,
                ShopAddress = db.TBConfigurations.Where(y => y.Name.Contains("shop_address")).FirstOrDefault().Value == null ? "-" : db.TBConfigurations.Where(y => y.Name.Contains("shop_address")).FirstOrDefault().Value,
                ShopPhone = db.TBConfigurations.Where(y => y.Name.Contains("shop_phone")).FirstOrDefault().Value == null ? "-" : db.TBConfigurations.Where(y => y.Name.Contains("shop_phone")).FirstOrDefault().Value,
                EmailLogo = db.TBConfigurations.Where(y => y.Name.Contains("shop_email_logo")).FirstOrDefault().Value == null ? "-" : db.TBConfigurations.Where(y => y.Name.Contains("shop_email_logo")).FirstOrDefault().Value,
            });
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_GetLastOrder_ByEmailCustomer(string email)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            Class_Address address = new Class_Address();
            Class_PaymentMethod payment = new Class_PaymentMethod();
            Class_Shipping shipping = new Class_Shipping();
            return db.TBOrders.Where(x => !x.Deflag && x.TBCustomer.Email == email).OrderByDescending(x => x.IDOrder).Select(x => new
            {
                x.IDBillingAddress,
                x.IDCustomer,
                x.IDDeliveryAddress,
                x.IDOrder,
                x.IDOrderStatus,
                x.IDPaymentMethod,
                x.IDShipping,
                x.IDVoucher,
                x.InvoiceNumber,
                x.Notes,
                x.Payment,
                x.Reference,
                x.ShippingNumber,
                TotalDiscount = Class_Currency.GetPriceConversionCurrency((decimal)x.TotalDiscount),
                TotalPaid = Class_Currency.GetPriceConversionCurrency((decimal)x.TotalPaid),
                TotalPrice = Class_Currency.GetPriceConversionCurrency((decimal)x.TotalPrice),
                TotalShipping = Class_Currency.GetPriceConversionCurrency((decimal)x.TotalShipping),
                x.TotalWeight,
                Date = x.DateInsert,
                OrderStatus = x.TBOrder_Status.Name,
                CustomerName = x.TBCustomer.FirstName + " " + x.TBCustomer.LastName,
                CustomerEmail = x.TBCustomer.Email,
                BillingAddress = address.Dynamic_GetDetail_ByIDAddress(x.TBAddress1.IDAddress),
                DeliveryAddress = address.Dynamic_GetDetail_ByIDAddress(x.TBAddress.IDAddress),
                PaymentMethod = payment.Dynamic_GetDetail_ByIDPayment_Method(x.IDPaymentMethod),
                Shipping = shipping.Dynamic_GetShipping_ByIDShipping(x.IDShipping)
            }).FirstOrDefault();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public object Object_SubmitOrder_veritrans()
    {
        try
        {
            Class_PaymentMethod payment = new Class_PaymentMethod();

            dynamic paymentData = payment.Dynamic_GetDetail_ByIDPayment_Method(2);

            var cookieUser = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()];
            //var cookieCurrency = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCurrency"].ToString()].Value;
            if (cookieUser == null)
                return ReturnData.MessageSuccess("Invalid token", null);
            //var cookieCart = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()];
            var cookieCart = this.GetEncodedDataOrder(HttpContext.Current.Request.AnonymousID);
            var cookieVoucher = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()];
            if (cookieCart == null)
            {
                if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()] != null && HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Value != "")
                    HttpContext.Current.Request.Cookies.Remove(System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString());
                return ReturnData.MessageSuccess("Invalid token", null);
            }

            DataClassesDataContext db = new DataClassesDataContext();
            Class_Customer cust = new Class_Customer();
            Class_Currency curr = new Class_Currency();

            dynamic _user = OurClass.DecryptToken(cookieUser.Value);
            dynamic _order = DYNAMIC_GetCartSummary();
            dynamic user = cust.Dynamic_GetData_ByEmail(_user["email"]);

            if (user == null)
                return ReturnData.MessageFailed("Customer not found", null);

            if (_order == null)
                return ReturnData.MessageFailed("Order not found", null);

            dynamic BillingAddress = _order["BillingAddress"];
            dynamic DeliveryAddress = _order["DeliveryAddress"];
            dynamic PaymentMethod = _order["PaymentMethod"];
            dynamic Product = _order["Product"];
            dynamic Shipping = _order["Shipping"];
            dynamic Voucher = (_order as Dictionary<string, dynamic>).ContainsKey("Voucher") ? _order["Voucher"] : null;
            int idVoucher = (_order as Dictionary<string, dynamic>).ContainsKey("Voucher") ? Voucher["IDVoucher"] : 0;
            TBVoucher SelectedVoucher = db.TBVouchers.Where(X => !X.Deflag && X.Active && X.IDVoucher == idVoucher).FirstOrDefault();
            if ((_order as Dictionary<string, dynamic>).ContainsKey("Voucher"))
            {
                if (SelectedVoucher == null)
                    return ReturnData.MessageFailed("Voucher is not found", null);
                if (SelectedVoucher.TotalAvailable == SelectedVoucher.Used && SelectedVoucher.TotalAvailable != 0)
                    return ReturnData.MessageFailed("Voucher is running out", null);
            }

            if (Product.Count > 0 && Product != null)
            {
                List<TBProduct_Combination> CombinationProducts = new List<TBProduct_Combination>();
                foreach (dynamic item in Product)
                {
                    int idCombination = item["IDCombination"];
                    TBProduct_Combination CheckCom = db.TBProduct_Combinations.Where(x => !x.Deflag && x.IDProduct_Combination == idCombination).FirstOrDefault();

                    if (item["Quantity"] > CheckCom.Quantity || CheckCom.Quantity < 1)
                    {
                        AJAX_DeleteCart(idCombination);
                        return ReturnData.MessageFailed("We're Sorry, one product in your shopping cart, " + CheckCom.TBProduct.Name + " (" + CheckCom.Name + ") is out of stock", null);
                    }

                    //CombinationProducts.Add(db.TBProduct_Combinations.Where(x => !x.Deflag && x.IDProduct == idCombination).FirstOrDefault());
                }
            }

            TBOrder order = new TBOrder();

            order.DateInsert = DateTime.Now;
            order.DateLastUpdate = DateTime.Now;
            order.IDCustomer = user.IDCustomer;
            order.IDPaymentMethod = PaymentMethod["IDPaymentMethod"];
            order.IDOrderStatus = 14;
            order.IDShipping = Shipping["IDShipping"];
            order.IDDeliveryAddress = DeliveryAddress["IDAddress"];
            order.IDBillingAddress = BillingAddress["IDAddress"];
            if (idVoucher != 0)
                order.IDVoucher = idVoucher;

            order.Payment = PaymentMethod["Name"];
            order.TotalWeight = _order["TotalWeight"];
            order.TotalShipping = Class_Currency.GetPriceDeconversionCurrency(Shipping["TotalPrice"]);
            order.TotalPrice = Class_Currency.GetPriceDeconversionCurrency(_order["TotalPrice"]);
            order.TotalPaid = Class_Currency.GetPriceDeconversionCurrency(_order["Subtotal"]);
            order.Notes = _order["Notes"];
            order.Deflag = false;
            order.IDCurrency = Class_Currency.GetActiveCurrencyID();
            order.ConversionRate = db.TBCurrencies.Where(x => x.IDCurrency == Class_Currency.GetActiveCurrencyID() && !x.Deflag).FirstOrDefault().ConversionRate;


            db.TBOrders.InsertOnSubmit(order);
            //db.SubmitChanges();

            int discountProduct = 0;
            string _stringProduct_email = "";
            _stringProduct_email += "<tr>";
            _stringProduct_email += "<td>Product</td>";
            _stringProduct_email += "<td>Combination</td>";
            _stringProduct_email += "<td>Price</td>";
            _stringProduct_email += "<td>Qty</td>";
            _stringProduct_email += "<td>Total Price</td>";
            _stringProduct_email += "</tr>";

            int firstQty = 0;

            foreach (dynamic item in Product)
            {
                decimal idCombination = item["IDCombination"];
                TBProduct_Combination com = db.TBProduct_Combinations.Where(x => !x.Deflag && x.IDProduct_Combination == idCombination).FirstOrDefault();
                firstQty = com.Quantity;
                if (com != null)
                {
                    TBOrder_Detail detail = new TBOrder_Detail();
                    detail.Discount = com.TBProduct.TotalDiscount;
                    //detail.IDOrder = order.IDOrder;
                    detail.IDCurrency = Class_Currency.GetActiveCurrencyID();
                    detail.TBOrder = order;
                    detail.IDProduct = item["IDProduct"];
                    detail.IDProduct_Combination = item["IDCombination"];
                    detail.Product_Name = com.Name;
                    detail.Weight = item["Weight"];
                    detail.Price = com.Price;
                    detail.OriginalPrice = com.TBProduct.PriceBeforeDiscount;
                    detail.Quantity = item["Quantity"];
                    db.TBOrder_Details.InsertOnSubmit(detail);
                    com.Quantity -= detail.Quantity;

                    //discountProduct += com.TotalDiscount * item["Quantity"];

                    //string product untuk email
                    _stringProduct_email += "<tr><td>" + com.TBProduct.Name + "</td>";
                    _stringProduct_email += "<td>" + com.Name + "</td>";
                    _stringProduct_email += "<td>" + WITLibrary.ConvertString.ToCurrency(detail.Price.ToString()) + "</td>";
                    _stringProduct_email += "<td>" + detail.Quantity.ToString() + "</td>";
                    _stringProduct_email += "<td>" + WITLibrary.ConvertString.ToCurrency((detail.Quantity * detail.Price).ToString()) + "</td>";
                    _stringProduct_email += "</tr>";
                }
            }
            order.TotalDiscount = discountProduct;

            if (SelectedVoucher != null)
            {
                SelectedVoucher.DateLastUpdate = DateTime.Now;
                SelectedVoucher.Used++;
                order.TotalDiscount += Class_Currency.GetPriceDeconversionCurrency(Voucher["Discount"]);
            }
            order.Reference = GenerateReferenceCode();

            db.SubmitChanges();

            //INSERT LOG STOCK
            foreach (var item in Product)
            {
                decimal idCombination = item["IDCombination"];
                TBProduct_Combination com = db.TBProduct_Combinations.Where(x => !x.Deflag && x.IDProduct_Combination == idCombination).FirstOrDefault();
                Class_Log_Stock log = new Class_Log_Stock();
                log.Insert(null, com.IDProduct_Combination, com.TBProduct.Name + " - " + com.TBProduct.Name, firstQty, com.Quantity, item["Quantity"], "decrease", "Transaction # " + order.IDOrder + " by " + user.Email);
            }

            //INSERT LOG ORDER
            Class_Log_Order logOrder = new Class_Log_Order();
            logOrder.Insert((int?)null, order.IDOrder, order.Reference, order.TBOrder_Status.Name, "Customer Place Order", order.TBCustomer.FirstName + ' ' + order.TBCustomer.LastName);

            if (cookieVoucher != null)
                HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Expires = DateTime.Now.AddDays(-1);
            if (cookieCart != null)
            {
                //HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()].Expires = DateTime.Now.AddDays(-1);
                this.DeleteEncodeDataOrder(cookieCart);
            }

            //SEND EMAIL KE CUSTOMER
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/template-email-waitingPayment.html")))
            {
                Class_Configuration _config = new Class_Configuration();
                var emailLogo = _config.Dynamic_Get_EmailLogo();
                var signCurrency = db.TBCurrencies.Where(x => x.IDCurrency == Class_Currency.GetActiveCurrencyID() && !x.Deflag).FirstOrDefault();
                string body = "";
                body = reader.ReadToEnd();
                body = body.Replace("{name}", user.FirstName);
                body = body.Replace("{nomorInvoice}", order.Reference);
                body = body.Replace("{amount}", signCurrency.Sign + " " + WITLibrary.ConvertString.ToCurrency(Class_Currency.GetPriceConversionCurrency((decimal)order.TotalPaid).ToString()));
                body = body.Replace("{owner}", order.TBPayment_Method.Owner);
                body = body.Replace("{account}", order.TBPayment_Method.AccountNumber);
                body = body.Replace("{bank}", order.TBPayment_Method.Bank);
                body = body.Replace("{title}", System.Configuration.ConfigurationManager.AppSettings["Title"]);
                body = body.Replace("{email_logo}", System.Configuration.ConfigurationManager.AppSettings["url"] + "/assets/images/email_logo/" + emailLogo);
                body = body.Replace("{shop_url}", System.Configuration.ConfigurationManager.AppSettings["url"]);
                OurClass.SendEmail(user.Email, WebConfigurationManager.AppSettings["Title"] + " Order Notification", body, "");
            }

            //SEND EMAIL KE ADMIN
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/admin-order-notification.html")))
            {
                string body = "";
                string _deliveryAddress = "";
                string _billingAddress = "";

                _deliveryAddress += order.TBAddress1.PeopleName + "<br/>";
                _deliveryAddress += order.TBAddress1.Address + "<br/>";
                _deliveryAddress += order.TBAddress1.TBDistrict.Name + "<br/>";
                _deliveryAddress += order.TBAddress1.TBProvince.Name + "<br/>";
                _deliveryAddress += order.TBAddress1.TBCountry.Name + "<br/>";
                _deliveryAddress += order.TBAddress1.PostalCode + "<br/>";
                _deliveryAddress += order.TBAddress1.Phone + "<br/>";

                _billingAddress += order.TBAddress.PeopleName + "<br/>";
                _billingAddress += order.TBAddress.Address + "<br/>";
                _billingAddress += order.TBAddress.TBDistrict.Name + "<br/>";
                _billingAddress += order.TBAddress.TBProvince.Name + "<br/>";
                _billingAddress += order.TBAddress.TBCountry.Name + "<br/>";
                _billingAddress += order.TBAddress.PostalCode + "<br/>";
                _billingAddress += order.TBAddress.Phone + "<br/>";

                body = reader.ReadToEnd();
                body = body.Replace("{CustomerName}", user.FirstName);
                body = body.Replace("{ReferenceCode}", order.Reference);
                body = body.Replace("{Date}", order.DateInsert.ToString("dd/MM/yyyy hh:mm"));
                body = body.Replace("{PaymentMethod}", order.TBPayment_Method.Name);
                body = body.Replace("{Shipping}", order.TBShipping.TBCarrier.Name);
                body = body.Replace("{bank}", order.TBPayment_Method.Bank);
                body = body.Replace("{Products}", _stringProduct_email);
                body = body.Replace("{SubTotal}", WITLibrary.ConvertString.ToCurrency(Class_Currency.GetPriceDeconversionCurrency((decimal)order.TotalPrice).ToString()));
                body = body.Replace("{TotalShipping}", WITLibrary.ConvertString.ToCurrency(Class_Currency.GetPriceDeconversionCurrency((decimal)order.TotalShipping).ToString()));
                body = body.Replace("{Discount}", WITLibrary.ConvertString.ToCurrency(Class_Currency.GetPriceDeconversionCurrency((decimal)order.TotalDiscount).ToString()));
                body = body.Replace("{GrandTotal}", WITLibrary.ConvertString.ToCurrency(Class_Currency.GetPriceDeconversionCurrency((decimal)order.TotalPaid).ToString()));
                body = body.Replace("{DeliveryAddress}", _deliveryAddress);
                body = body.Replace("{BillingAddress}", _billingAddress);
                string emailAdmin = db.TBConfigurations.Where(x => x.Name == "email_user").FirstOrDefault().Value;
                OurClass.SendEmail(emailAdmin, WebConfigurationManager.AppSettings["Title"] + " Order Notification", body, "");
            }

            return ReturnData.MessageSuccess("Order Submit successfully", this.GetDetail_ByIDOrder(order.IDOrder));
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public object Object_SubmitOrder_veritrans(int paymentMethodID)
    {
        try
        {
            Class_PaymentMethod payment = new Class_PaymentMethod();

            dynamic paymentData = payment.Dynamic_GetDetail_ByIDPayment_Method(paymentMethodID);

            var cookieUser = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()];
            //var cookieCurrency = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCurrency"].ToString()].Value;
            if (cookieUser == null)
                return ReturnData.MessageSuccess("Invalid token", null);
            //var cookieCart = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()];
            var cookieCart = this.GetEncodedDataOrder(HttpContext.Current.Request.AnonymousID);
            var cookieVoucher = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()];
            if (cookieCart == null)
            {
                if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()] != null && HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Value != "")
                    HttpContext.Current.Request.Cookies.Remove(System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString());
                return ReturnData.MessageSuccess("Invalid token", null);
            }

            DataClassesDataContext db = new DataClassesDataContext();
            Class_Customer cust = new Class_Customer();
            Class_Currency curr = new Class_Currency();

            dynamic _user = OurClass.DecryptToken(cookieUser.Value);
            dynamic _order = DYNAMIC_GetCartSummary();
            dynamic user = cust.Dynamic_GetData_ByEmail(_user["email"]);

            if (user == null)
                return ReturnData.MessageFailed("Customer not found", null);

            if (_order == null)
                return ReturnData.MessageFailed("Order not found", null);

            dynamic BillingAddress = _order["BillingAddress"];
            dynamic DeliveryAddress = _order["DeliveryAddress"];
            dynamic PaymentMethod = _order["PaymentMethod"];
            dynamic Product = _order["Product"];
            dynamic Shipping = _order["Shipping"];
            dynamic Voucher = (_order as Dictionary<string, dynamic>).ContainsKey("Voucher") ? _order["Voucher"] : null;
            int idVoucher = (_order as Dictionary<string, dynamic>).ContainsKey("Voucher") ? Voucher["IDVoucher"] : 0;
            TBVoucher SelectedVoucher = db.TBVouchers.Where(X => !X.Deflag && X.Active && X.IDVoucher == idVoucher).FirstOrDefault();
            if ((_order as Dictionary<string, dynamic>).ContainsKey("Voucher"))
            {
                if (SelectedVoucher == null)
                    return ReturnData.MessageFailed("Voucher is not found", null);
                if (SelectedVoucher.TotalAvailable == SelectedVoucher.Used && SelectedVoucher.TotalAvailable != 0)
                    return ReturnData.MessageFailed("Voucher is running out", null);
            }

            if (Product.Count > 0 && Product != null)
            {
                List<TBProduct_Combination> CombinationProducts = new List<TBProduct_Combination>();
                foreach (dynamic item in Product)
                {
                    int idCombination = item["IDCombination"];
                    TBProduct_Combination CheckCom = db.TBProduct_Combinations.Where(x => !x.Deflag && x.IDProduct_Combination == idCombination).FirstOrDefault();

                    if (item["Quantity"] > CheckCom.Quantity || CheckCom.Quantity < 1)
                    {
                        AJAX_DeleteCart(idCombination);
                        return ReturnData.MessageFailed("We're Sorry, one product in your shopping cart, " + CheckCom.TBProduct.Name + " (" + CheckCom.Name + ") is out of stock", null);
                    }

                    //CombinationProducts.Add(db.TBProduct_Combinations.Where(x => !x.Deflag && x.IDProduct == idCombination).FirstOrDefault());
                }
            }

            TBOrder order = new TBOrder();

            order.DateInsert = DateTime.Now;
            order.DateLastUpdate = DateTime.Now;
            order.IDCustomer = user.IDCustomer;
            order.IDPaymentMethod = PaymentMethod["IDPaymentMethod"];
            order.IDOrderStatus = 14;
            order.IDShipping = Shipping["IDShipping"];
            order.IDDeliveryAddress = DeliveryAddress["IDAddress"];
            order.IDBillingAddress = BillingAddress["IDAddress"];
            if (idVoucher != 0)
                order.IDVoucher = idVoucher;

            order.Payment = PaymentMethod["Name"];
            order.TotalWeight = _order["TotalWeight"];
            order.TotalShipping = Class_Currency.GetPriceDeconversionCurrency(Shipping["TotalPrice"]);
            order.TotalPrice = Class_Currency.GetPriceDeconversionCurrency(_order["TotalPrice"]);
            order.TotalPaid = Class_Currency.GetPriceDeconversionCurrency(_order["Subtotal"]);
            order.Notes = _order["Notes"];
            order.Deflag = false;
            order.IDCurrency = Class_Currency.GetActiveCurrencyID();
            order.ConversionRate = db.TBCurrencies.Where(x => x.IDCurrency == Class_Currency.GetActiveCurrencyID() && !x.Deflag).FirstOrDefault().ConversionRate;


            db.TBOrders.InsertOnSubmit(order);
            //db.SubmitChanges();

            int discountProduct = 0;
            string _stringProduct_email = "";
            _stringProduct_email += "<tr>";
            _stringProduct_email += "<td>Product</td>";
            _stringProduct_email += "<td>Combination</td>";
            _stringProduct_email += "<td>Price</td>";
            _stringProduct_email += "<td>Qty</td>";
            _stringProduct_email += "<td>Total Price</td>";
            _stringProduct_email += "</tr>";

            foreach (dynamic item in Product)
            {
                decimal idCombination = item["IDCombination"];
                TBProduct_Combination com = db.TBProduct_Combinations.Where(x => !x.Deflag && x.IDProduct_Combination == idCombination).FirstOrDefault();
                int firstQty = com.Quantity;
                if (com != null)
                {
                    TBOrder_Detail detail = new TBOrder_Detail();
                    detail.Discount = com.TBProduct.TotalDiscount;
                    //detail.IDOrder = order.IDOrder;
                    detail.IDCurrency = Class_Currency.GetActiveCurrencyID();
                    detail.TBOrder = order;
                    detail.IDProduct = item["IDProduct"];
                    detail.IDProduct_Combination = item["IDCombination"];
                    detail.Product_Name = com.Name;
                    detail.Weight = item["Weight"];
                    detail.Price = com.Price;
                    detail.OriginalPrice = com.TBProduct.PriceBeforeDiscount;
                    detail.Quantity = item["Quantity"];
                    db.TBOrder_Details.InsertOnSubmit(detail);
                    com.Quantity -= detail.Quantity;

                    //INSERT LOG STOCK
                    Class_Log_Stock log = new Class_Log_Stock();
                    log.Insert(null, detail.IDProduct_Combination, com.TBProduct.Name + " - " + detail.Product_Name, firstQty, com.Quantity, detail.Quantity, "decrease", "Transaction # " + order.IDOrder + " by " + user.Email);

                    //discountProduct += com.TotalDiscount * item["Quantity"];

                    //string product untuk email
                    _stringProduct_email += "<tr><td>" + com.TBProduct.Name + "</td>";
                    _stringProduct_email += "<td>" + com.Name + "</td>";
                    _stringProduct_email += "<td>" + WITLibrary.ConvertString.ToCurrency(detail.Price.ToString()) + "</td>";
                    _stringProduct_email += "<td>" + detail.Quantity.ToString() + "</td>";
                    _stringProduct_email += "<td>" + WITLibrary.ConvertString.ToCurrency((detail.Quantity * detail.Price).ToString()) + "</td>";
                    _stringProduct_email += "</tr>";
                }
            }
            order.TotalDiscount = discountProduct;

            if (SelectedVoucher != null)
            {
                SelectedVoucher.DateLastUpdate = DateTime.Now;
                SelectedVoucher.Used++;
                order.TotalDiscount += Class_Currency.GetPriceDeconversionCurrency(Voucher["Discount"]);
            }
            order.Reference = GenerateReferenceCode();

            db.SubmitChanges();

            //INSERT LOG ORDER
            Class_Log_Order logOrder = new Class_Log_Order();
            logOrder.Insert((int?)null, order.IDOrder, order.Reference, order.TBOrder_Status.Name, "Customer Place Order", order.TBCustomer.FirstName + ' ' + order.TBCustomer.LastName);

            if (cookieVoucher != null)
                HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieVoucher"].ToString()].Expires = DateTime.Now.AddDays(-1);
            if (cookieCart != null)
            {
                //HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()].Expires = DateTime.Now.AddDays(-1);
                this.DeleteEncodeDataOrder(cookieCart);
            }

            //SEND EMAIL KE CUSTOMER
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/template-email-waitingPayment.html")))
            {
                Class_Configuration _config = new Class_Configuration();
                var emailLogo = _config.Dynamic_Get_EmailLogo();
                var signCurrency = db.TBCurrencies.Where(x => x.IDCurrency == Class_Currency.GetActiveCurrencyID() && !x.Deflag).FirstOrDefault();
                string body = "";
                body = reader.ReadToEnd();
                body = body.Replace("{name}", user.FirstName);
                body = body.Replace("{nomorInvoice}", order.Reference);
                body = body.Replace("{amount}", signCurrency.Sign + " " + WITLibrary.ConvertString.ToCurrency(Class_Currency.GetPriceConversionCurrency((decimal)order.TotalPaid).ToString()));
                body = body.Replace("{owner}", order.TBPayment_Method.Owner);
                body = body.Replace("{account}", order.TBPayment_Method.AccountNumber);
                body = body.Replace("{bank}", order.TBPayment_Method.Bank);
                body = body.Replace("{title}", System.Configuration.ConfigurationManager.AppSettings["Title"]);
                body = body.Replace("{email_logo}", System.Configuration.ConfigurationManager.AppSettings["url"] + "/assets/images/email_logo/" + emailLogo);
                body = body.Replace("{shop_url}", System.Configuration.ConfigurationManager.AppSettings["url"]);
                OurClass.SendEmail(user.Email, WebConfigurationManager.AppSettings["Title"] + " Order Notification", body, "");
            }

            //SEND EMAIL KE ADMIN
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/admin-order-notification.html")))
            {
                string body = "";
                string _deliveryAddress = "";
                string _billingAddress = "";

                _deliveryAddress += order.TBAddress1.PeopleName + "<br/>";
                _deliveryAddress += order.TBAddress1.Address + "<br/>";
                _deliveryAddress += order.TBAddress1.TBDistrict.Name + "<br/>";
                _deliveryAddress += order.TBAddress1.TBProvince.Name + "<br/>";
                _deliveryAddress += order.TBAddress1.TBCountry.Name + "<br/>";
                _deliveryAddress += order.TBAddress1.PostalCode + "<br/>";
                _deliveryAddress += order.TBAddress1.Phone + "<br/>";

                _billingAddress += order.TBAddress.PeopleName + "<br/>";
                _billingAddress += order.TBAddress.Address + "<br/>";
                _billingAddress += order.TBAddress.TBDistrict.Name + "<br/>";
                _billingAddress += order.TBAddress.TBProvince.Name + "<br/>";
                _billingAddress += order.TBAddress.TBCountry.Name + "<br/>";
                _billingAddress += order.TBAddress.PostalCode + "<br/>";
                _billingAddress += order.TBAddress.Phone + "<br/>";

                body = reader.ReadToEnd();
                body = body.Replace("{CustomerName}", user.FirstName);
                body = body.Replace("{ReferenceCode}", order.Reference);
                body = body.Replace("{Date}", order.DateInsert.ToString("dd/MM/yyyy hh:mm"));
                body = body.Replace("{PaymentMethod}", order.TBPayment_Method.Name);
                body = body.Replace("{Shipping}", order.TBShipping.TBCarrier.Name);
                body = body.Replace("{bank}", order.TBPayment_Method.Bank);
                body = body.Replace("{Products}", _stringProduct_email);
                body = body.Replace("{SubTotal}", WITLibrary.ConvertString.ToCurrency(Class_Currency.GetPriceDeconversionCurrency((decimal)order.TotalPrice).ToString()));
                body = body.Replace("{TotalShipping}", WITLibrary.ConvertString.ToCurrency(Class_Currency.GetPriceDeconversionCurrency((decimal)order.TotalShipping).ToString()));
                body = body.Replace("{Discount}", WITLibrary.ConvertString.ToCurrency(Class_Currency.GetPriceDeconversionCurrency((decimal)order.TotalDiscount).ToString()));
                body = body.Replace("{GrandTotal}", WITLibrary.ConvertString.ToCurrency(Class_Currency.GetPriceDeconversionCurrency((decimal)order.TotalPaid).ToString()));
                body = body.Replace("{DeliveryAddress}", _deliveryAddress);
                body = body.Replace("{BillingAddress}", _billingAddress);
                string emailAdmin = db.TBConfigurations.Where(x => x.Name == "email_user").FirstOrDefault().Value;
                OurClass.SendEmail(emailAdmin, WebConfigurationManager.AppSettings["Title"] + " Order Notification", body, "");
            }

            return ReturnData.MessageSuccess("Order Submit successfully", this.GetDetail_ByIDOrder(order.IDOrder));
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public TBOrder GetDetail_ByIDOrder(int idOrder)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBOrders.Where(x => x.IDOrder == idOrder && !x.Deflag).FirstOrDefault();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
}