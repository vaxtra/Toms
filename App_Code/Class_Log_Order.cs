using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using WITLibrary;

/// <summary>
/// Summary description for Class_Log_Order
/// </summary>
public class Class_Log_Order
{
	public Class_Log_Order()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public Datatable AJAX_GetTable_Log_Order(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            DateTime startDate = db.TBOrders.FirstOrDefault().DateInsert;
            dynamic[] data = db.TBLog_Orders.ToArray();
            int count = data.Count();
            int number = 1;
            if (!string.IsNullOrEmpty(search))
                data = data.AsEnumerable().Where(x =>
                    x.TBEmployee.Name.ToLower().Contains(search.ToLower()) ||
                    x.Reference.ToLower().Contains(search.ToLower()) ||
                    x.Description.ToLower().Contains(search.ToLower())
                    ).ToArray();
            Dictionary<string, dynamic>[] resultList = new Dictionary<string, dynamic>[data.Count()];
            for (int i = 0; i < data.Count(); i++)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("Number", number);
                newData.Add("EmployeeName", data[i].TBEmployee == null ? "" : data[i].TBEmployee.Name);
                newData.Add("Reference", data[i].Reference);
                newData.Add("Customer", data[i].CustomerName);
                newData.Add("Status", data[i].OrderStatus);
                newData.Add("Description", data[i].Description);
                newData.Add("InsertDate", data[i].DateInsert);
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

    public Datatable AJAX_GetTable_Log_Order_filter(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search, DateTime startDate, DateTime endDate)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var datamentah = db.TBLog_Orders.Where(x => x.DateInsert >= startDate && x.DateInsert <= endDate).ToArray();
            //dynamic[] data = db.SP_Report_GetCategoryDistribution(startDate, endDate).ToArray();
            dynamic[] data = datamentah;

            int count = data.Count();
            int number = 1;
            if (!string.IsNullOrEmpty(search))
                data = data.AsEnumerable().Where(x =>
                    x.TBEmployee.Name.ToLower().Contains(search.ToLower()) ||
                    x.Reference.ToLower().Contains(search.ToLower()) ||
                    x.Description.ToLower().Contains(search.ToLower())
                    ).ToArray();
            Dictionary<string, dynamic>[] resultList = new Dictionary<string, dynamic>[data.Count()];
            for (int i = 0; i < data.Count(); i++)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("Number", number);
                newData.Add("EmployeeName", data[i].TBEmployee == null ? "" : data[i].TBEmployee.Name);
                newData.Add("Reference", data[i].Reference);
                newData.Add("Customer", data[i].CustomerName);
                newData.Add("Status", data[i].OrderStatus);
                newData.Add("Description", data[i].Description);
                newData.Add("InsertDate", data[i].DateInsert);
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

    public bool Insert(int? idEmployee, int idOrder, string reference, string orderStatus, string description, string customerName)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBLog_Order log = new TBLog_Order();
            log.IDEmployee = idEmployee;
            log.IDOrder = idOrder;
            log.Reference = reference;
            log.OrderStatus = orderStatus;
            log.CustomerName = customerName;
            log.Description = description;
            log.DateInsert = DateTime.Now;
            if (idEmployee != null)
            {
                log.UserName = db.TBEmployees.Where(x => x.IDEmployee == idEmployee).FirstOrDefault().Name;
            }
            db.TBLog_Orders.InsertOnSubmit(log);
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
}