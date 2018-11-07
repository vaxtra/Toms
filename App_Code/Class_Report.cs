using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using WITLibrary;

/// <summary>
/// Summary description for Class_Report
/// </summary>
public class Class_Report
{
    public Class_Report()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Datatable AJAX_GetTable_TotalPerDay(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            //DateTime startDate = DateTime.ParseExact(HttpContext.Current.Request["_startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //DateTime endDate = DateTime.ParseExact(HttpContext.Current.Request["_endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            var data = db.TBOrders.Where(X => !X.Deflag && X.DateInsert <= DateTime.Now).OrderBy(x => x.DateInsert).ToList();
            int dayCount = (int)(DateTime.Now - data.FirstOrDefault().DateInsert).TotalDays;
            DateTime loopDate = data.FirstOrDefault().DateInsert.Date;
            int count = 0;
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            for (int i = 1; i <= dayCount; i++)
            {
                var totalOrder = db.TBOrders.Where(x => !x.Deflag && x.DateInsert.Date == loopDate.Date).Count() == 0 ? 0 : db.TBOrders.Where(x => !x.Deflag && x.DateInsert.Date == loopDate.Date).Count();
                var totalCustomer = db.TBCustomers.Where(x => !x.Deflag && x.DateInsert.Date == loopDate.Date).Count() == 0 ? 0 : db.TBCustomers.Where(x => !x.Deflag && x.DateInsert.Date == loopDate.Date).Count();
                var totalItem = db.TBOrder_Details.Where(x => x.TBOrder.DateInsert.Date == loopDate.Date && x.TBOrder.TBOrder_Status.Paid).Count() == 0 ? 0 : db.TBOrder_Details.Where(x => x.TBOrder.DateInsert.Date == loopDate.Date && x.TBOrder.TBOrder_Status.Paid).Sum(x => x.Quantity);
                var totalSales = db.TBOrders.Where(x => !x.Deflag && x.DateInsert.Date == loopDate.Date && x.TBOrder_Status.Paid).Count() == 0 ? 0 : (decimal)db.TBOrders.Where(x => !x.Deflag && x.DateInsert.Date == loopDate.Date && x.TBOrder_Status.Paid).Sum(x => x.TotalPrice);
                var totalSalesVoucher = db.TBOrders.Where(x => !x.Deflag && x.DateInsert.Date == loopDate.Date && x.TBOrder_Status.Paid).Count() == 0 ? 0 : (decimal)db.TBOrders.Where(x => !x.Deflag && x.DateInsert.Date == loopDate.Date && x.TBOrder_Status.Paid).Sum(x => x.TotalPrice - x.TotalDiscount);
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("Number", i);
                newData.Add("Date", loopDate.ToString("dd MMMM yyyy"));
                newData.Add("TotalRegistrant", totalCustomer);
                newData.Add("TotalOrders", totalOrder);
                newData.Add("TotalItems", totalItem);
                newData.Add("TotalSales", totalSales);
                newData.Add("TotalSalesVoucher", totalSalesVoucher);
                loopDate = loopDate.AddDays(1).Date;
                resultList.Add(newData);
            }
            //if (!string.IsNullOrEmpty(search))
            //    data = data.Where(x =>
            //        x.Reference.ToLower().Contains(search.ToLower()) ||
            //        x.TBCustomer.FirstName.Contains(search.ToLower()) ||
            //        x.TBCustomer.LastName.Contains(search.ToLower()) ||
            //        x.TotalPaid.ToString().Contains(search.ToLower()) ||
            //        x.TBPayment_Method.Name.Contains(search.ToLower())
            //        ).ToArray();

            return OurClass.ParseTable(resultList, dayCount, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
            //return dayCount;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            //return 0;
            return new WITLibrary.Datatable
            {
                sEcho = 0,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0,
                aaData = new List<Dictionary<string, dynamic>>()
            };
        }
    }

    public Datatable AJAX_GetTable_TotalPerDay_FilterDate(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search, DateTime startDate, DateTime endDate)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            //DateTime startDate = DateTime.ParseExact(HttpContext.Current.Request["_startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //DateTime endDate = DateTime.ParseExact(HttpContext.Current.Request["_endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            var data = db.TBOrders.Where(X => !X.Deflag && X.DateInsert >= startDate && X.DateInsert <= endDate).OrderBy(x => x.DateInsert).ToList();
            int dayCount = (int)(endDate - startDate).TotalDays + 1;
            DateTime loopDate = startDate.Date;
            int count = 0;
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            for (int i = 1; i <= dayCount; i++)
            {
                var totalOrder = db.TBOrders.Where(x => !x.Deflag && x.DateInsert.Date == loopDate.Date).Count() == 0 ? 0 : db.TBOrders.Where(x => !x.Deflag && x.DateInsert.Date == loopDate.Date).Count();
                var totalCustomer = db.TBCustomers.Where(x => !x.Deflag && x.DateInsert.Date == loopDate.Date).Count() == 0 ? 0 : db.TBCustomers.Where(x => !x.Deflag && x.DateInsert.Date == loopDate.Date).Count();
                var totalItem = db.TBOrder_Details.Where(x => x.TBOrder.DateInsert.Date == loopDate.Date && x.TBOrder.TBOrder_Status.Paid).Count() == 0 ? 0 : db.TBOrder_Details.Where(x => x.TBOrder.DateInsert.Date == loopDate.Date && x.TBOrder.TBOrder_Status.Paid).Sum(x => x.Quantity);
                var totalSales = db.TBOrders.Where(x => !x.Deflag && x.DateInsert.Date == loopDate.Date && x.TBOrder_Status.Paid).Count() == 0 ? 0 : (decimal)db.TBOrders.Where(x => !x.Deflag && x.DateInsert.Date == loopDate.Date && x.TBOrder_Status.Paid).Sum(x => x.TotalPrice);
                var totalSalesVoucher = db.TBOrders.Where(x => !x.Deflag && x.DateInsert.Date == loopDate.Date && x.TBOrder_Status.Paid).Count() == 0 ? 0 : (decimal)db.TBOrders.Where(x => !x.Deflag && x.DateInsert.Date == loopDate.Date && x.TBOrder_Status.Paid).Sum(x => x.TotalPrice - x.TotalDiscount);
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("Number", i);
                newData.Add("Date", loopDate.ToString("dd MMMM yyyy"));
                newData.Add("TotalRegistrant", totalCustomer);
                newData.Add("TotalOrders", totalOrder);
                newData.Add("TotalItems", totalItem);
                newData.Add("TotalSales", totalSales);
                newData.Add("TotalSalesVoucher", totalSalesVoucher);
                loopDate = loopDate.AddDays(1).Date;
                resultList.Add(newData);
            }
            //if (!string.IsNullOrEmpty(search))
            //    data = data.Where(x =>
            //        x.Reference.ToLower().Contains(search.ToLower()) ||
            //        x.TBCustomer.FirstName.Contains(search.ToLower()) ||
            //        x.TBCustomer.LastName.Contains(search.ToLower()) ||
            //        x.TotalPaid.ToString().Contains(search.ToLower()) ||
            //        x.TBPayment_Method.Name.Contains(search.ToLower())
            //        ).ToArray();

            return OurClass.ParseTable(resultList, dayCount, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
            //return dayCount;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            //return 0;
            return new WITLibrary.Datatable
            {
                sEcho = 0,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0,
                aaData = new List<Dictionary<string, dynamic>>()
            };
        }
    }

    public Datatable AJAX_GetTable_PaymentDistribution(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            //DateTime startDate = DateTime.ParseExact(HttpContext.Current.Request["_startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //DateTime endDate = DateTime.ParseExact(HttpContext.Current.Request["_endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            var data = db.TBPayment_Methods.Where(X => !X.Deflag).ToList();
            int count = data.Count();
            decimal averageSales;
            int number = 1;
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (var currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                var totalOrder = db.TBOrders.Where(x => !x.Deflag && x.IDPaymentMethod == currData.IDPaymentMethod).Count() == 0 ? 0 : db.TBOrders.Where(x => !x.Deflag && x.IDPaymentMethod == currData.IDPaymentMethod).Count();
                var totalSales = (decimal)db.TBOrders.Where(x => !x.Deflag && x.IDPaymentMethod == currData.IDPaymentMethod && x.TBOrder_Status.Paid).Count() == 0 ? 0 : db.TBOrders.Where(x => !x.Deflag && x.IDPaymentMethod == currData.IDPaymentMethod && x.TBOrder_Status.Paid).Sum(x => x.TotalPrice);
                if (totalSales > 0)
                {
                    averageSales = (decimal)totalSales / (decimal)totalOrder;
                }
                else
                {
                    averageSales = 0;
                }
                newData.Add("Number", number);
                newData.Add("PaymentMethod", currData.Name);
                newData.Add("TotalOrders", totalOrder);
                newData.Add("TotalSales", totalSales);
                newData.Add("AverageSales", averageSales.ToString("#.##"));
                number += 1;
                resultList.Add(newData);
            }
            //if (!string.IsNullOrEmpty(search))
            //    data = data.Where(x =>
            //        x.Reference.ToLower().Contains(search.ToLower()) ||
            //        x.TBCustomer.FirstName.Contains(search.ToLower()) ||
            //        x.TBCustomer.LastName.Contains(search.ToLower()) ||
            //        x.TotalPaid.ToString().Contains(search.ToLower()) ||
            //        x.TBPayment_Method.Name.Contains(search.ToLower())
            //        ).ToArray();

            return OurClass.ParseTable(resultList, count, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
            //return dayCount;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            //return 0;
            return new WITLibrary.Datatable
            {
                sEcho = 0,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0,
                aaData = new List<Dictionary<string, dynamic>>()
            };
        }
    }

    public Datatable AJAX_GetTable_PaymentDistribution_FilterDate(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search, DateTime startDate, DateTime endDate)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            //DateTime startDate = DateTime.ParseExact(HttpContext.Current.Request["_startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //DateTime endDate = DateTime.ParseExact(HttpContext.Current.Request["_endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            var data = db.TBPayment_Methods.Where(X => !X.Deflag).ToList();
            int count = data.Count();
            decimal averageSales;
            int number = 1;
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (var currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                var totalOrder = db.TBOrders.Where(x => !x.Deflag && x.IDPaymentMethod == currData.IDPaymentMethod && x.DateInsert >= startDate && x.DateInsert <= endDate).Count() == 0 ? 0 : db.TBOrders.Where(x => !x.Deflag && x.IDPaymentMethod == currData.IDPaymentMethod && x.DateInsert >= startDate && x.DateInsert <= endDate).Count();
                var totalSales = (decimal)db.TBOrders.Where(x => !x.Deflag && x.IDPaymentMethod == currData.IDPaymentMethod && x.TBOrder_Status.Paid && x.DateInsert >= startDate && x.DateInsert <= endDate).Count() == 0 ? 0 : db.TBOrders.Where(x => !x.Deflag && x.IDPaymentMethod == currData.IDPaymentMethod && x.TBOrder_Status.Paid && x.DateInsert >= startDate && x.DateInsert <= endDate).Sum(x => x.TotalPrice);
                if (totalSales > 0)
                {
                    averageSales = (decimal)totalSales / (decimal)totalOrder;
                }
                else
                {
                    averageSales = 0;
                }
                newData.Add("Number", number);
                newData.Add("PaymentMethod", currData.Name);
                newData.Add("TotalOrders", totalOrder);
                newData.Add("TotalSales", totalSales);
                newData.Add("AverageSales", averageSales.ToString("#.##"));
                number += 1;
                resultList.Add(newData);
            }
            //if (!string.IsNullOrEmpty(search))
            //    data = data.Where(x =>
            //        x.Reference.ToLower().Contains(search.ToLower()) ||
            //        x.TBCustomer.FirstName.Contains(search.ToLower()) ||
            //        x.TBCustomer.LastName.Contains(search.ToLower()) ||
            //        x.TotalPaid.ToString().Contains(search.ToLower()) ||
            //        x.TBPayment_Method.Name.Contains(search.ToLower())
            //        ).ToArray();

            return OurClass.ParseTable(resultList, count, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
            //return dayCount;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            //return 0;
            return new WITLibrary.Datatable
            {
                sEcho = 0,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0,
                aaData = new List<Dictionary<string, dynamic>>()
            };
        }
    }

    public Datatable AJAX_GetTable_ShippingDistribution(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            //DateTime startDate = DateTime.ParseExact(HttpContext.Current.Request["_startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //DateTime endDate = DateTime.ParseExact(HttpContext.Current.Request["_endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            var data = db.TBCarriers.Where(X => !X.Deflag).ToList();
            int count = data.Count();
            int number = 1;
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (var currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                var totalOrder = db.TBOrders.Where(x => !x.Deflag && x.TBShipping.TBCarrier.IDCarrier == currData.IDCarrier).Count() == 0 ? 0 : db.TBOrders.Where(x => !x.Deflag && x.TBShipping.TBCarrier.IDCarrier == currData.IDCarrier).Count();
                var totalSales = (decimal)db.TBOrders.Where(x => !x.Deflag && x.TBShipping.TBCarrier.IDCarrier == currData.IDCarrier && x.TBOrder_Status.Paid).Count() == 0 ? 0 : db.TBOrders.Where(x => !x.Deflag && x.TBShipping.TBCarrier.IDCarrier == currData.IDCarrier && x.TBOrder_Status.Paid).Sum(x => x.TotalPrice);
                var totalShipCost = (decimal)db.TBOrders.Where(x => !x.Deflag && x.TBShipping.TBCarrier.IDCarrier == currData.IDCarrier && x.TBOrder_Status.Paid).Count() == 0 ? 0 : db.TBOrders.Where(x => !x.Deflag && x.TBShipping.TBCarrier.IDCarrier == currData.IDCarrier && x.TBOrder_Status.Paid).Sum(x => x.TotalShipping);
                newData.Add("Number", number);
                newData.Add("ShippingMethod", currData.Name);
                newData.Add("TotalOrders", totalOrder);
                newData.Add("TotalSales", totalSales);
                newData.Add("TotalShippingCost", totalShipCost);
                number += 1;
                resultList.Add(newData);
            }
            //if (!string.IsNullOrEmpty(search))
            //    data = data.Where(x =>
            //        x.Reference.ToLower().Contains(search.ToLower()) ||
            //        x.TBCustomer.FirstName.Contains(search.ToLower()) ||
            //        x.TBCustomer.LastName.Contains(search.ToLower()) ||
            //        x.TotalPaid.ToString().Contains(search.ToLower()) ||
            //        x.TBPayment_Method.Name.Contains(search.ToLower())
            //        ).ToArray();

            return OurClass.ParseTable(resultList, count, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
            //return dayCount;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            //return 0;
            return new WITLibrary.Datatable
            {
                sEcho = 0,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0,
                aaData = new List<Dictionary<string, dynamic>>()
            };
        }
    }

    public Datatable AJAX_GetTable_ShippingDistribution_FilterDate(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search, DateTime startDate, DateTime endDate)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            //DateTime startDate = DateTime.ParseExact(HttpContext.Current.Request["_startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //DateTime endDate = DateTime.ParseExact(HttpContext.Current.Request["_endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            var data = db.TBCarriers.Where(X => !X.Deflag).ToList();
            int count = data.Count();
            int number = 1;
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (var currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                var totalOrder = db.TBOrders.Where(x => !x.Deflag && x.TBShipping.TBCarrier.IDCarrier == currData.IDCarrier && x.DateInsert >= startDate && x.DateInsert <= endDate).Count() == 0 ? 0 : db.TBOrders.Where(x => !x.Deflag && x.TBShipping.TBCarrier.IDCarrier == currData.IDCarrier && x.DateInsert >= startDate && x.DateInsert <= endDate).Count();
                var totalSales = (decimal)db.TBOrders.Where(x => !x.Deflag && x.TBShipping.TBCarrier.IDCarrier == currData.IDCarrier && x.TBOrder_Status.Paid && x.DateInsert >= startDate && x.DateInsert <= endDate).Count() == 0 ? 0 : db.TBOrders.Where(x => !x.Deflag && x.TBShipping.TBCarrier.IDCarrier == currData.IDCarrier && x.TBOrder_Status.Paid && x.DateInsert >= startDate && x.DateInsert <= endDate).Sum(x => x.TotalPrice);
                var totalShipCost = (decimal)db.TBOrders.Where(x => !x.Deflag && x.TBShipping.TBCarrier.IDCarrier == currData.IDCarrier && x.TBOrder_Status.Paid && x.DateInsert >= startDate && x.DateInsert <= endDate).Count() == 0 ? 0 : db.TBOrders.Where(x => !x.Deflag && x.TBShipping.TBCarrier.IDCarrier == currData.IDCarrier && x.TBOrder_Status.Paid && x.DateInsert >= startDate && x.DateInsert <= endDate).Sum(x => x.TotalShipping);
                newData.Add("Number", number);
                newData.Add("ShippingMethod", currData.Name);
                newData.Add("TotalOrders", totalOrder);
                newData.Add("TotalSales", totalSales);
                newData.Add("TotalShippingCost", totalShipCost);
                number += 1;
                resultList.Add(newData);
            }
            //if (!string.IsNullOrEmpty(search))
            //    data = data.Where(x =>
            //        x.Reference.ToLower().Contains(search.ToLower()) ||
            //        x.TBCustomer.FirstName.Contains(search.ToLower()) ||
            //        x.TBCustomer.LastName.Contains(search.ToLower()) ||
            //        x.TotalPaid.ToString().Contains(search.ToLower()) ||
            //        x.TBPayment_Method.Name.Contains(search.ToLower())
            //        ).ToArray();

            return OurClass.ParseTable(resultList, count, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
            //return dayCount;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            //return 0;
            return new WITLibrary.Datatable
            {
                sEcho = 0,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0,
                aaData = new List<Dictionary<string, dynamic>>()
            };
        }
    }

    public Datatable AJAX_GetTable_ZoneDistribution(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            //DateTime startDate = DateTime.ParseExact(HttpContext.Current.Request["_startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //DateTime endDate = DateTime.ParseExact(HttpContext.Current.Request["_endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            var data = db.TBShippings.Where(X => !X.Deflag).ToList();
            int count = data.Count();
            int number = 1;
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (var currData in data)
            {
                var totalOrder = db.TBOrders.Where(x => !x.Deflag && x.IDShipping == currData.IDShipping).Count() == 0 ? 0 : db.TBOrders.Where(x => !x.Deflag && x.IDShipping == currData.IDShipping).Count();
                var totalSales = (decimal)db.TBOrders.Where(x => !x.Deflag && x.IDShipping == currData.IDShipping && x.TBOrder_Status.Paid).Count() == 0 ? 0 : db.TBOrders.Where(x => !x.Deflag && x.IDShipping == currData.IDShipping && x.TBOrder_Status.Paid).Sum(x => x.TotalPrice);

                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("Number", number);
                newData.Add("Zone", currData.TBDistrict.Name);
                newData.Add("TotalOrders", totalOrder);
                newData.Add("TotalSales", totalSales);
                number += 1;
                resultList.Add(newData);

            }
            //if (!string.IsNullOrEmpty(search))
            //    data = data.Where(x =>
            //        x.Reference.ToLower().Contains(search.ToLower()) ||
            //        x.TBCustomer.FirstName.Contains(search.ToLower()) ||
            //        x.TBCustomer.LastName.Contains(search.ToLower()) ||
            //        x.TotalPaid.ToString().Contains(search.ToLower()) ||
            //        x.TBPayment_Method.Name.Contains(search.ToLower())
            //        ).ToArray();

            return OurClass.ParseTable(resultList, count, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
            //return dayCount;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            //return 0;
            return new WITLibrary.Datatable
            {
                sEcho = 0,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0,
                aaData = new List<Dictionary<string, dynamic>>()
            };
        }
    }

    public Datatable AJAX_GetTable_ZoneDistribution_FilterDate(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search, DateTime startDate, DateTime endDate)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            //DateTime startDate = DateTime.ParseExact(HttpContext.Current.Request["_startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //DateTime endDate = DateTime.ParseExact(HttpContext.Current.Request["_endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            var data = db.TBShippings.Where(X => !X.Deflag).ToList();
            int count = data.Count();
            int number = 1;
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (var currData in data)
            {
                var totalOrder = db.TBOrders.Where(x => !x.Deflag && x.IDShipping == currData.IDShipping && x.DateInsert >= startDate && x.DateInsert <= endDate).Count() == 0 ? 0 : db.TBOrders.Where(x => !x.Deflag && x.IDShipping == currData.IDShipping && x.DateInsert >= startDate && x.DateInsert <= endDate).Count();
                var totalSales = (decimal)db.TBOrders.Where(x => !x.Deflag && x.IDShipping == currData.IDShipping && x.TBOrder_Status.Paid && x.DateInsert >= startDate && x.DateInsert <= endDate).Count() == 0 ? 0 : db.TBOrders.Where(x => !x.Deflag && x.IDShipping == currData.IDShipping && x.TBOrder_Status.Paid && x.DateInsert >= startDate && x.DateInsert <= endDate).Sum(x => x.TotalPrice);

                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("Number", number);
                newData.Add("Zone", currData.TBDistrict.Name);
                newData.Add("TotalOrders", totalOrder);
                newData.Add("TotalSales", totalSales);
                number += 1;
                resultList.Add(newData);
            }
            //if (!string.IsNullOrEmpty(search))
            //    data = data.Where(x =>
            //        x.Reference.ToLower().Contains(search.ToLower()) ||
            //        x.TBCustomer.FirstName.Contains(search.ToLower()) ||
            //        x.TBCustomer.LastName.Contains(search.ToLower()) ||
            //        x.TotalPaid.ToString().Contains(search.ToLower()) ||
            //        x.TBPayment_Method.Name.Contains(search.ToLower())
            //        ).ToArray();

            return OurClass.ParseTable(resultList, count, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
            //return dayCount;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            //return 0;
            return new WITLibrary.Datatable
            {
                sEcho = 0,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0,
                aaData = new List<Dictionary<string, dynamic>>()
            };
        }
    }

    public Datatable AJAX_GetTable_ProvinceDistribution(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            DateTime startDate = db.TBOrders.FirstOrDefault().DateInsert;
            dynamic[] data = db.SP_Report_GetProvinceDistribution(startDate, DateTime.Now).ToArray();
            var province = db.TBProvinces.ToArray();
            int count = province.Count();
            int number = 1;
            decimal TotalOrders = 0;
            decimal TotalSales = 0;
            if (!string.IsNullOrEmpty(search))
                province = province.AsEnumerable().Where(x =>
                    x.Name.ToLower().Contains(search.ToLower())
                    ).ToArray();
            Dictionary<string, dynamic>[] resultList = new Dictionary<string, dynamic>[province.Count()];
            for (int i = 0; i < province.Count(); i++)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                var exist = 0;
                for (int y = 0; y < data.Count(); y++)
                {
                    if (province[i].IDProvince == data[y].IDProvince)
                    {
                        exist = 1;
                        TotalOrders = data[y].TotalOrders;
                        TotalSales = data[y].TotalSales;
                    }
                }

                if (exist == 1)
                {
                    newData.Add("Number", number);
                    newData.Add("Zone", province[i].Name);
                    newData.Add("TotalOrders", TotalOrders);
                    newData.Add("TotalSales", TotalSales);
                    number += 1;
                }
                else
                {
                    newData.Add("Number", number);
                    newData.Add("Zone", province[i].Name);
                    newData.Add("TotalOrders", 0);
                    newData.Add("TotalSales", 0);
                    number += 1;
                }
                resultList[i] = newData;
            }
            return OurClass.ParseTable(resultList, count, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return new Datatable
            {
                sEcho = 0,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0,
                aaData = new List<Dictionary<string, dynamic>>()
            };
        }
    }

    public Datatable AJAX_GetTable_ProvinceDistribution_FilterDate(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search, DateTime startDate, DateTime endDate)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            dynamic[] data = db.SP_Report_GetProvinceDistribution(startDate, endDate).ToArray();
            var province = db.TBProvinces.ToArray();
            int count = province.Count();
            int number = 1;
            decimal TotalOrders = 0;
            decimal TotalSales = 0;
            if (!string.IsNullOrEmpty(search))
                province = province.AsEnumerable().Where(x =>
                    x.Name.ToLower().Contains(search.ToLower())
                    ).ToArray();
            Dictionary<string, dynamic>[] resultList = new Dictionary<string, dynamic>[province.Count()];
            for (int i = 0; i < province.Count(); i++)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                var exist = 0;
                for (int y = 0; y < data.Count(); y++)
                {
                    if (province[i].IDProvince == data[y].IDProvince)
                    {
                        exist = 1;
                        TotalOrders = data[y].TotalOrders;
                        TotalSales = data[y].TotalSales;
                    }
                }

                if (exist == 1)
                {
                    newData.Add("Number", number);
                    newData.Add("Zone", province[i].Name);
                    newData.Add("TotalOrders", TotalOrders);
                    newData.Add("TotalSales", TotalSales);
                    number += 1;
                }
                else
                {
                    newData.Add("Number", number);
                    newData.Add("Zone", province[i].Name);
                    newData.Add("TotalOrders", 0);
                    newData.Add("TotalSales", 0);
                    number += 1;
                }
                resultList[i] = newData;
            }
            return OurClass.ParseTable(resultList, count, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return new Datatable
            {
                sEcho = 0,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0,
                aaData = new List<Dictionary<string, dynamic>>()
            };
        }
    }

    public Datatable AJAX_GetTable_CityDistribution(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            DateTime startDate = db.TBOrders.FirstOrDefault().DateInsert;
            dynamic[] data = db.SP_Report_GetCityDistribution(startDate, DateTime.Now).ToArray();
            var City = db.TBCities.ToArray();
            int count = City.Count();
            int number = 1;
            decimal TotalOrders = 0;
            decimal TotalSales = 0;
            if (!string.IsNullOrEmpty(search))
                City = City.AsEnumerable().Where(x =>
                    x.Name.ToLower().Contains(search.ToLower())
                    ).ToArray();
            Dictionary<string, dynamic>[] resultList = new Dictionary<string, dynamic>[City.Count()];
            for (int i = 0; i < City.Count(); i++)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                var exist = 0;
                for (int y = 0; y < data.Count(); y++)
                {
                    if (City[i].IDCity == data[y].IDCity)
                    {
                        exist = 1;
                        TotalOrders = data[y].TotalOrders;
                        TotalSales = data[y].TotalSales;
                    }
                }

                if (exist == 1)
                {
                    newData.Add("Number", number);
                    newData.Add("Zone", City[i].Name);
                    newData.Add("TotalOrders", TotalOrders);
                    newData.Add("TotalSales", TotalSales);
                    number += 1;
                }
                else
                {
                    newData.Add("Number", number);
                    newData.Add("Zone", City[i].Name);
                    newData.Add("TotalOrders", 0);
                    newData.Add("TotalSales", 0);
                    number += 1;
                }
                resultList[i] = newData;
            }
            return OurClass.ParseTable(resultList, count, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return new Datatable
            {
                sEcho = 0,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0,
                aaData = new List<Dictionary<string, dynamic>>()
            };
        }
    }

    public Datatable AJAX_GetTable_CityDistribution_FilterDate(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search, DateTime startDate, DateTime endDate)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            dynamic[] data = db.SP_Report_GetCityDistribution(startDate, endDate).ToArray();
            var City = db.TBCities.ToArray();
            int count = City.Count();
            int number = 1;
            decimal TotalOrders = 0;
            decimal TotalSales = 0;
            if (!string.IsNullOrEmpty(search))
                City = City.AsEnumerable().Where(x =>
                    x.Name.ToLower().Contains(search.ToLower())
                    ).ToArray();
            Dictionary<string, dynamic>[] resultList = new Dictionary<string, dynamic>[City.Count()];
            for (int i = 0; i < City.Count(); i++)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                var exist = 0;
                for (int y = 0; y < data.Count(); y++)
                {
                    if (City[i].IDCity == data[y].IDCity)
                    {
                        exist = 1;
                        TotalOrders = data[y].TotalOrders;
                        TotalSales = data[y].TotalSales;
                    }
                }

                if (exist == 1)
                {
                    newData.Add("Number", number);
                    newData.Add("Zone", City[i].Name);
                    newData.Add("TotalOrders", TotalOrders);
                    newData.Add("TotalSales", TotalSales);
                    number += 1;
                }
                else
                {
                    newData.Add("Number", number);
                    newData.Add("Zone", City[i].Name);
                    newData.Add("TotalOrders", 0);
                    newData.Add("TotalSales", 0);
                    number += 1;
                }
                resultList[i] = newData;
            }
            return OurClass.ParseTable(resultList, count, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return new Datatable
            {
                sEcho = 0,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0,
                aaData = new List<Dictionary<string, dynamic>>()
            };
        }
    }


    public Datatable AJAX_GetTable_CategoryDistribution(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            DateTime startDate = db.TBOrders.FirstOrDefault().DateInsert;
            dynamic[] data = db.SP_Report_GetCategoryDistribution(startDate, DateTime.Now).ToArray();
            int count = data.Count();
            int number = 1;
            if (!string.IsNullOrEmpty(search))
                data = data.AsEnumerable().Where(x =>
                    x.Name.ToLower().Contains(search.ToLower()) ||
                    x.ReferenceCode.ToLower().Contains(search.ToLower())
                    ).ToArray();
            Dictionary<string, dynamic>[] resultList = new Dictionary<string, dynamic>[data.Count()];
            for (int i = 0; i < data.Count(); i++)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("Number", number);
                newData.Add("Category", data[i].Name);
                newData.Add("TotalOrders", data[i].TotalOrders);
                newData.Add("TotalSales", data[i].TotalSales);
                number += 1;
                resultList[i] = newData;
            }
            return OurClass.ParseTable(resultList, count, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return new Datatable
            {
                sEcho = 0,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0,
                aaData = new List<Dictionary<string, dynamic>>()
            };
        }
    }

    public Datatable AJAX_GetTable_CategoryDistribution_FilterDate(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search, DateTime startDate, DateTime endDate)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            dynamic[] data = db.SP_Report_GetCategoryDistribution(startDate, endDate).ToArray();
            int count = data.Count();
            int number = 1;
            if (!string.IsNullOrEmpty(search))
                data = data.AsEnumerable().Where(x =>
                    x.Name.ToLower().Contains(search.ToLower()) ||
                    x.ReferenceCode.ToLower().Contains(search.ToLower())
                    ).ToArray();
            Dictionary<string, dynamic>[] resultList = new Dictionary<string, dynamic>[data.Count()];
            for (int i = 0; i < data.Count(); i++)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("Number", number);
                newData.Add("Category", data[i].Name);
                newData.Add("TotalOrders", data[i].TotalOrders);
                newData.Add("TotalSales", data[i].TotalSales);
                number += 1;
                resultList[i] = newData;
            }
            return OurClass.ParseTable(resultList, count, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return new Datatable
            {
                sEcho = 0,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0,
                aaData = new List<Dictionary<string, dynamic>>()
            };
        }
    }

    public Datatable AJAX_GetTable_ProductDistribution(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            //DateTime startDate = DateTime.ParseExact(HttpContext.Current.Request["_startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //DateTime endDate = DateTime.ParseExact(HttpContext.Current.Request["_endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            var data = db.TBProducts.Where(X => !X.Deflag).ToList();
            int count = data.Count();
            int number = 1;
            int dayCount = DateTime.Now.DayOfYear - data.FirstOrDefault().DateInsert.DayOfYear;
            string _qtyperday;
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (var currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                Dictionary<string, dynamic> combinationData = new Dictionary<string, dynamic>();
                List<Dictionary<string, dynamic>> combinationList = new List<Dictionary<string, dynamic>>();
                var QtySold = db.TBOrder_Details.Where(x => !x.TBOrder.Deflag && x.TBProduct_Combination.IDProduct == currData.IDProduct && x.TBOrder.TBOrder_Status.Paid).Count() == 0 ? 0 : db.TBOrder_Details.Where(x => !x.TBOrder.Deflag && x.TBProduct_Combination.IDProduct == currData.IDProduct && x.TBOrder.TBOrder_Status.Paid).Sum(x => x.Quantity);
                var totalSales = (decimal)db.TBOrder_Details.Where(x => !x.TBOrder.Deflag && x.TBProduct_Combination.IDProduct == currData.IDProduct && x.TBOrder.TBOrder_Status.Paid).Count() == 0 ? 0 : db.TBOrder_Details.Where(x => !x.TBOrder.Deflag && x.TBProduct_Combination.IDProduct == currData.IDProduct && x.TBOrder.TBOrder_Status.Paid).Sum(x => x.Quantity * x.Price);
                decimal QtyPerDay = QtySold == 0 ? 0 : (decimal)QtySold / (decimal)dayCount;
                if (QtyPerDay > 0)
                {
                    _qtyperday = QtyPerDay.ToString("0.###");
                }
                else
                {
                    _qtyperday = QtyPerDay.ToString();
                }
                newData.Add("Number", number);
                newData.Add("Product", currData.Name);
                foreach (var combination in currData.TBProduct_Combinations)
                {
                    combinationData = new Dictionary<string, dynamic>();
                    combinationData.Add("Name", combination.Name);
                    combinationData.Add("Quantity", combination.TBOrder_Details.Count() == 0 ? 0 : combination.TBOrder_Details.Where(x => x.TBOrder.TBOrder_Status.Paid).Sum(x => x.Quantity));
                    combinationList.Add(combinationData);
                }
                newData.Add("Combination", combinationList);
                newData.Add("TotalOrders", QtySold);
                newData.Add("PricePerUnit", currData.Price);
                newData.Add("QtyPerDay", _qtyperday);
                newData.Add("TotalSales", totalSales);
                number += 1;
                resultList.Add(newData);
            }
            //if (!string.IsNullOrEmpty(search))
            //    data = data.Where(x =>
            //        x.Reference.ToLower().Contains(search.ToLower()) ||
            //        x.TBCustomer.FirstName.Contains(search.ToLower()) ||
            //        x.TBCustomer.LastName.Contains(search.ToLower()) ||
            //        x.TotalPaid.ToString().Contains(search.ToLower()) ||
            //        x.TBPayment_Method.Name.Contains(search.ToLower())
            //        ).ToArray();

            return OurClass.ParseTable(resultList, count, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
            //return dayCount;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            //return 0;
            return new WITLibrary.Datatable
            {
                sEcho = 0,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0,
                aaData = new List<Dictionary<string, dynamic>>()
            };
        }
    }

    public Datatable AJAX_GetTable_ProductDistribution_FilterDate(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search, DateTime startDate, DateTime endDate)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            //DateTime startDate = DateTime.ParseExact(HttpContext.Current.Request["_startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //DateTime endDate = DateTime.ParseExact(HttpContext.Current.Request["_endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            var data = db.TBProducts.Where(X => !X.Deflag).ToList();
            int count = data.Count();
            int number = 1;
            int dayCount = (endDate.DayOfYear - startDate.DayOfYear) + 1;
            string _qtyperday;
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (var currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                Dictionary<string, dynamic> combinationData = new Dictionary<string, dynamic>();
                List<Dictionary<string, dynamic>> combinationList = new List<Dictionary<string, dynamic>>();
                var QtySold = db.TBOrder_Details.Where(x => !x.TBOrder.Deflag && x.TBProduct_Combination.IDProduct == currData.IDProduct && x.TBOrder.TBOrder_Status.Paid && x.TBOrder.DateInsert >= startDate && x.TBOrder.DateInsert <= endDate).Count() == 0 ? 0 : db.TBOrder_Details.Where(x => !x.TBOrder.Deflag && x.TBProduct_Combination.IDProduct == currData.IDProduct && x.TBOrder.TBOrder_Status.Paid && x.TBOrder.DateInsert >= startDate && x.TBOrder.DateInsert <= endDate).Sum(x => x.Quantity);
                var totalSales = (decimal)db.TBOrder_Details.Where(x => !x.TBOrder.Deflag && x.TBProduct_Combination.IDProduct == currData.IDProduct && x.TBOrder.TBOrder_Status.Paid && x.TBOrder.DateInsert >= startDate && x.TBOrder.DateInsert <= endDate).Count() == 0 ? 0 : db.TBOrder_Details.Where(x => !x.TBOrder.Deflag && x.TBProduct_Combination.IDProduct == currData.IDProduct && x.TBOrder.TBOrder_Status.Paid && x.TBOrder.DateInsert >= startDate && x.TBOrder.DateInsert <= endDate).Sum(x => x.Quantity * x.Price);
                decimal QtyPerDay = QtySold == 0 ? 0 : (decimal)QtySold / (decimal)dayCount;
                if (QtyPerDay > 0)
                {
                    _qtyperday = QtyPerDay.ToString("0.###");
                }
                else
                {
                    _qtyperday = QtyPerDay.ToString();
                }
                newData.Add("Number", number);
                newData.Add("Product", currData.Name);
                foreach (var combination in currData.TBProduct_Combinations)
                {
                    combinationData = new Dictionary<string, dynamic>();
                    combinationData.Add("Name", combination.Name);
                    combinationData.Add("Quantity", combination.TBOrder_Details.Count() == 0 ? 0 : combination.TBOrder_Details.Where(x => x.TBOrder.TBOrder_Status.Paid).Sum(x => x.TBOrder.TBOrder_Details.Where(y => y.TBOrder.DateInsert >= startDate && y.TBOrder.DateInsert <= endDate ).Sum(y => y.Quantity)));
                    combinationList.Add(combinationData);
                }
                newData.Add("Combination", combinationList);
                newData.Add("TotalOrders", QtySold);
                newData.Add("PricePerUnit", currData.Price);
                newData.Add("QtyPerDay", _qtyperday);
                newData.Add("TotalSales", totalSales);
                number += 1;
                resultList.Add(newData);
            }
            //if (!string.IsNullOrEmpty(search))
            //    data = data.Where(x =>
            //        x.Reference.ToLower().Contains(search.ToLower()) ||
            //        x.TBCustomer.FirstName.Contains(search.ToLower()) ||
            //        x.TBCustomer.LastName.Contains(search.ToLower()) ||
            //        x.TotalPaid.ToString().Contains(search.ToLower()) ||
            //        x.TBPayment_Method.Name.Contains(search.ToLower())
            //        ).ToArray();

            return OurClass.ParseTable(resultList, count, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
            //return dayCount;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            //return 0;
            return new WITLibrary.Datatable
            {
                sEcho = 0,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0,
                aaData = new List<Dictionary<string, dynamic>>()
            };
        }
    }

    public Datatable AJAX_GetTable_VoucherDistribution(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            //DateTime startDate = DateTime.ParseExact(HttpContext.Current.Request["_startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //DateTime endDate = DateTime.ParseExact(HttpContext.Current.Request["_endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            var data = db.TBVouchers.Where(X => !X.Deflag).ToList();
            int count = data.Count();
            int number = 1;
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (var currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                var QtyUsed = db.TBOrders.Where(x => !x.Deflag && x.IDVoucher == currData.IDVoucher && x.TBOrder_Status.Paid).Count() == 0 ? 0 : db.TBOrders.Where(x => !x.Deflag && x.IDVoucher == currData.IDVoucher && x.TBOrder_Status.Paid).Count();
                var totalDiscount = (decimal)db.TBOrders.Where(x => !x.Deflag && x.IDVoucher == currData.IDVoucher && x.TBOrder_Status.Paid).Count() == 0 ? 0 : db.TBOrders.Where(x => !x.Deflag && x.IDVoucher == currData.IDVoucher && x.TBOrder_Status.Paid).Sum(x => x.TotalDiscount);
                newData.Add("Number", number);
                newData.Add("Voucher", currData.Name);
                newData.Add("Used", QtyUsed);
                newData.Add("TotalDiscount", totalDiscount);
                number += 1;
                resultList.Add(newData);
            }
            //if (!string.IsNullOrEmpty(search))
            //    data = data.Where(x =>
            //        x.Reference.ToLower().Contains(search.ToLower()) ||
            //        x.TBCustomer.FirstName.Contains(search.ToLower()) ||
            //        x.TBCustomer.LastName.Contains(search.ToLower()) ||
            //        x.TotalPaid.ToString().Contains(search.ToLower()) ||
            //        x.TBPayment_Method.Name.Contains(search.ToLower())
            //        ).ToArray();

            return OurClass.ParseTable(resultList, count, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
            //return dayCount;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            //return 0;
            return new WITLibrary.Datatable
            {
                sEcho = 0,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0,
                aaData = new List<Dictionary<string, dynamic>>()
            };
        }
    }

    public Datatable AJAX_GetTable_VoucherDistribution_FilterDate(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search, DateTime startDate, DateTime endDate)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            //DateTime startDate = DateTime.ParseExact(HttpContext.Current.Request["_startDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //DateTime endDate = DateTime.ParseExact(HttpContext.Current.Request["_endDate"] + " 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            var data = db.TBVouchers.Where(X => !X.Deflag).ToList();
            int count = data.Count();
            int number = 1;
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (var currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                var QtyUsed = db.TBOrders.Where(x => !x.Deflag && x.IDVoucher == currData.IDVoucher && x.DateInsert >= startDate && x.DateInsert <= endDate).Count() == 0 ? 0 : db.TBOrders.Where(x => !x.Deflag && x.IDVoucher == currData.IDVoucher && x.DateInsert >= startDate && x.DateInsert <= endDate).Count();
                var totalDiscount = (decimal)db.TBOrders.Where(x => !x.Deflag && x.IDVoucher == currData.IDVoucher && x.TBOrder_Status.Paid && x.DateInsert >= startDate && x.DateInsert <= endDate).Count() == 0 ? 0 : db.TBOrders.Where(x => !x.Deflag && x.IDVoucher == currData.IDVoucher && x.TBOrder_Status.Paid && x.DateInsert >= startDate && x.DateInsert <= endDate).Sum(x => x.TotalDiscount);
                newData.Add("Number", number);
                newData.Add("Voucher", currData.Name);
                newData.Add("Used", QtyUsed);
                newData.Add("TotalDiscount", totalDiscount);
                number += 1;
                resultList.Add(newData);
            }
            //if (!string.IsNullOrEmpty(search))
            //    data = data.Where(x =>
            //        x.Reference.ToLower().Contains(search.ToLower()) ||
            //        x.TBCustomer.FirstName.Contains(search.ToLower()) ||
            //        x.TBCustomer.LastName.Contains(search.ToLower()) ||
            //        x.TotalPaid.ToString().Contains(search.ToLower()) ||
            //        x.TBPayment_Method.Name.Contains(search.ToLower())
            //        ).ToArray();

            return OurClass.ParseTable(resultList, count, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
            //return dayCount;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            //return 0;
            return new WITLibrary.Datatable
            {
                sEcho = 0,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0,
                aaData = new List<Dictionary<string, dynamic>>()
            };
        }
    }

    public dynamic Dynamic_Get_TotalSales()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBOrders.Where(x => !x.Deflag && x.TBOrder_Status.Paid).Sum(x => x.TotalPrice);
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
            return db.TBOrders.Where(x => !x.Deflag && x.TBOrder_Status.Paid && x.DateInsert >= startDate && x.DateInsert <= endDate).Sum(x => x.TotalPrice);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
}
