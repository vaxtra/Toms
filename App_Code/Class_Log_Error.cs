using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using WITLibrary;

/// <summary>
/// Summary description for Class_Log_Error
/// </summary>
public class Class_Log_Error
{

    public Datatable AJAX_GetTable_Log_Error(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            dynamic[] data = db.TBLog_Errors.OrderByDescending(x => x.ID).ToArray();
            int count = data.Count();
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
                newData.Add("ID", data[i].ID);
                newData.Add("Message", data[i].ErrorMessage);
                newData.Add("StackTrace", data[i].StackTrace);
                newData.Add("Date", data[i].DateInsert);
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

    public bool Insert(string ErrorMessage, string StackTrace)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBLog_Error log = new TBLog_Error();
            log.ErrorMessage = ErrorMessage;
            log.StackTrace = StackTrace;
            log.DateInsert = DateTime.Now;
            log.DateLastUpdate = DateTime.Now;
            db.TBLog_Errors.InsertOnSubmit(log);
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

    public ReturnData AJAX_Delete(int id)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                TBLog_Error _deleteData = db.TBLog_Errors.Where(x => x.ID == id).FirstOrDefault();
                if (_deleteData == null)
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);

                db.TBLog_Errors.DeleteOnSubmit(_deleteData);
                db.SubmitChanges();
                return ReturnData.MessageSuccess("Log has been successfully deleted.", null);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
}