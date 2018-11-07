using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using WITLibrary;

/// <summary>
/// Summary description for Class_Page_Category
/// </summary>
public class Class_Page_Category
{
    public Class_Page_Category()
    {

    }

    #region AJAX

    public Datatable AJAX_GetTable(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search, int idPage)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                IEnumerable<dynamic> data = Dynamic_GetAll(db, idPage).Where(x => !x.Deflag);
                int count = data.Count();
                if (!string.IsNullOrEmpty(search))
                    data = data.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToArray();
                List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
                foreach (dynamic currData in data)
                {
                    Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                    newData.Add("IDPageCategory", currData.IDPageCategory);
                    newData.Add("IDPage", currData.IDPage);
                    newData.Add("Name", currData.Name);
                    newData.Add("Description", currData.Description);
                    newData.Add("DateInsert", currData.DateInsert.ToString("dd-MM-yyyy") + " " + currData.DateInsert.ToLongTimeString());
                    newData.Add("DateLastUpdate", currData.DateLastUpdate.ToString("dd-MM-yyyy") + " " + currData.DateLastUpdate.ToLongTimeString());
                    newData.Add("Deflag", currData.Deflag);
                    resultList.Add(newData);
                }
                return OurClass.ParseTable(resultList, count, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
            }
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

    public ReturnData AJAX_Insert(string name, string description, int idPage)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                if (!ValidationName_Insert(db, name, idPage))
                    return ReturnData.MessageFailed(name + " already exists.", null);

                TBPageCategory _newData = new TBPageCategory
                {
                    IDPage = idPage,
                    Name = name,
                    Description = description,
                    Deflag = false,
                    DateInsert = DateTime.Now,
                    DateLastUpdate = DateTime.Now
                };

                db.TBPageCategories.InsertOnSubmit(_newData);
                db.SubmitChanges();

                if (_newData != null)
                {
                    return ReturnData.MessageSuccess(name + " has been successfully inserted.", null);
                }
                return ReturnData.MessageFailed(name + " failed to insert.", null);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_Updates(int idPageCategory, string name, string description)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBPageCategory pageCategory = db.TBPageCategories.Where(x => !x.Deflag && x.IDPageCategory == idPageCategory).FirstOrDefault();
            if (pageCategory == null)
                return ReturnData.MessageFailed("Page Category not found", null);
            pageCategory.Name = name;
            pageCategory.Description = description;
            pageCategory.DateLastUpdate = DateTime.Now;
            db.SubmitChanges();

            return ReturnData.MessageSuccess("Data updated successfully", null);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_Delete(int idPageCategory)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBPageCategory pageCategory = db.TBPageCategories.Where(x => x.IDPageCategory == idPageCategory).FirstOrDefault();
            if (pageCategory == null)
                return ReturnData.MessageFailed("Page Category not found", null);
            pageCategory.Deflag = true;
            pageCategory.DateLastUpdate = DateTime.Now;
            db.SubmitChanges();

            return ReturnData.MessageSuccess("Data deleted successfully", null);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_GetDetail(int idPageCategory)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
            data.Add("PageCategory", Dynamic_GetDetail(db, idPageCategory));
            return ReturnData.MessageSuccess("OK", data);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    #endregion

    #region DYNAMIC
    public IEnumerable<dynamic> Dynamic_GetAll(DataClassesDataContext db, int idPage)
    {
        try
        {
            return db.TBPageCategories.AsEnumerable().Where(x => x.IDPage == idPage).Select(x => new
            {
                x.IDPageCategory,
                x.IDPage,
                x.Name,
                x.Description,
                x.Deflag,
                x.DateInsert,
                x.DateLastUpdate
            }).ToArray();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_GetDetail(DataClassesDataContext db, int idPageCategory)
    {
        try
        {
            return db.TBPageCategories.Where(x => x.IDPageCategory == idPageCategory && !x.Deflag).Select(x => new
            {
                x.Name,
                x.IDPageCategory,
                x.Description,
                x.DateLastUpdate
            }).FirstOrDefault();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            throw;
        }
    }

    #endregion

    #region LINQ
    public bool ValidationName_Insert(DataClassesDataContext db, string name, int idPage)
    {
        try
        {
            if (Dynamic_GetAll(db, idPage).AsEnumerable().Where(x => x.Name.ToLower() == name).FirstOrDefault() == null)
                return true;
            return false;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return false;
        }
    }

    public TBPageCategory GetDetail(DataClassesDataContext db, int idPageCategory)
    {
        try
        {
            return Func_GetDetail(db, idPageCategory);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    private Func<DataClassesDataContext, int, TBPageCategory> Func_GetDetail
    {
        get
        {
            Func<DataClassesDataContext, int, TBPageCategory> func =
              CompiledQuery.Compile<DataClassesDataContext, int, TBPageCategory>
              ((DataClassesDataContext context, int idPageCategory) => context.TBPageCategories.AsEnumerable()
                .Where(x => !x.Deflag && x.IDPageCategory == idPageCategory).FirstOrDefault());
            return func;
        }
    }

    #endregion

}