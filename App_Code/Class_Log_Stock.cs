using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using WITLibrary;

/// <summary>
/// Summary description for Class_Log_Stock
/// </summary>
public class Class_Log_Stock
{
    public Class_Log_Stock()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Datatable AJAX_GetTable_Log_Stock(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            DateTime startDate = db.TBOrders.FirstOrDefault().DateInsert;
            dynamic[] data = db.TBLog_Stocks.OrderByDescending(x => x.ID).ToArray();
            int count = data.Count();
            int number = 1;
            if (!string.IsNullOrEmpty(search))
                data = data.AsEnumerable().Where(x =>
                    x.TBEmployee.Name.ToLower().Contains(search.ToLower()) ||
                    x.ProductName.ToLower().Contains(search.ToLower()) ||
                    x.Description.ToLower().Contains(search.ToLower())
                    ).ToArray();
            Dictionary<string, dynamic>[] resultList = new Dictionary<string, dynamic>[data.Count()];
            for (int i = 0; i < data.Count(); i++)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("Number", number);
                newData.Add("EmployeeName", data[i].TBEmployee == null ? "" : data[i].TBEmployee.Name);
                newData.Add("ProductName", data[i].ProductName);
                newData.Add("Type", data[i].Type);
                newData.Add("Description", data[i].Description);
                newData.Add("Quantity", data[i].Quantity);
                newData.Add("QuantityBefore", data[i].InitialQuantity);
                newData.Add("QuantityAfter", data[i].LastQuantity);
                newData.Add("InsertDate", data[i].InsertDate);
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

    public Datatable AJAX_GetTable_Log_Stock_filter(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search, DateTime startDate, DateTime endDate)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var datamentah = db.TBLog_Stocks.Where(x => x.InsertDate >= startDate && x.InsertDate <= endDate).ToArray();
            //dynamic[] data = db.SP_Report_GetCategoryDistribution(startDate, endDate).ToArray();
            dynamic[] data = datamentah;

            int count = data.Count();
            int number = 1;
            if (!string.IsNullOrEmpty(search))
                data = data.AsEnumerable().Where(x =>
                    x.TBEmployee.Name.ToLower().Contains(search.ToLower()) ||
                    x.ProductName.ToLower().Contains(search.ToLower()) ||
                    x.Description.ToLower().Contains(search.ToLower())
                    ).ToArray();
            Dictionary<string, dynamic>[] resultList = new Dictionary<string, dynamic>[data.Count()];
            for (int i = 0; i < data.Count(); i++)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("Number", number);
                newData.Add("EmployeeName", data[i].TBEmployee == null ? "" : data[i].TBEmployee.Name);
                newData.Add("ProductName", data[i].ProductName);
                newData.Add("Type", data[i].Type);
                newData.Add("Description", data[i].Description);
                newData.Add("Quantity", data[i].Quantity);
                newData.Add("QuantityBefore", data[i].InitialQuantity);
                newData.Add("QuantityAfter", data[i].LastQuantity);
                newData.Add("InsertDate", data[i].InsertDate);
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

    public bool Insert(int? idEmployee, int idCombination, string productName,int initialQty, int lastQty, int quantity, string type, string description)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBLog_Stock log = new TBLog_Stock();
            log.IDEmployee = idEmployee;
            log.IDProduct_Combination = idCombination;
            log.InsertDate = DateTime.Now;
            log.ProductName = productName;
            log.InitialQuantity = initialQty;
            log.LastQuantity = lastQty;
            log.Quantity = quantity;
            log.Type = type;
            log.Description = description;
            db.TBLog_Stocks.InsertOnSubmit(log);
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