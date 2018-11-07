using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using WITLibrary;

/// <summary>
/// Summary description for Class_Value
/// </summary>
public class Class_Value
{
    public Class_Value()
    { }

    #region AJAX
    public Datatable AJAX_GetTable(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search, string idattr)
    {
        try
        {
            IEnumerable<dynamic> data = Dynamic_GetDataBy_IDAttribute(new DataClassesDataContext(), WITLibrary.ConvertString.ToInt(idattr));
            int count = data.Count();
            if (!string.IsNullOrEmpty(search))
                data = data.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToArray();
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (dynamic currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("IDAttribute", currData.IDAttribute);
                newData.Add("IDValue", currData.IDValue);
                newData.Add("Name", currData.Name);
                newData.Add("RGBColor", currData.RGBColor);
                resultList.Add(newData);
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
    public ReturnData AJAX_Insert(int idAttribute, string name, string rgbColor)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                if (new Class_Attribute().Dynamic_GetDetail(db, idAttribute) == null)
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);

                if (!ValidationName_Insert(db, name, idAttribute))
                    return ReturnData.MessageFailed(name + " already exists.", null);

                TBValue _newData = new TBValue
                {
                    IDAttribute = idAttribute,
                    Name = name,
                    RGBColor = rgbColor,
                    Deflag = false,
                    DateInsert = DateTime.Now,
                    DateLastUpdate = DateTime.Now
                };
                db.TBValues.InsertOnSubmit(_newData);
                db.SubmitChanges();

                if (_newData != null)
                    return ReturnData.MessageSuccess(name + " has been successfully inserted.", null);
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
    public ReturnData AJAX_Update(int idValue, string name, string rgbColor)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                TBValue _updateData = GetDetail(db, idValue);
                if (_updateData == null)
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);

                if (!ValidationName_Update(db, idValue, name, _updateData.IDAttribute))
                    return ReturnData.MessageFailed(name + " already exists.", null);

                string _nameBefore = _updateData.Name;
                _updateData.Name = name;
                _updateData.RGBColor = rgbColor;
                _updateData.DateLastUpdate = DateTime.Now;
                db.SubmitChanges();
                if (_updateData != null)
                    return ReturnData.MessageSuccess(_nameBefore + " has been successfully updated.", null);
                return ReturnData.MessageSuccess(_nameBefore + " failed to update.", null);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public ReturnData AJAX_Delete(int idValue)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                TBValue _deleteData = GetDetail(db, idValue);
                if (_deleteData == null)
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);

                string _nameBefore = _deleteData.Name;
                _deleteData.Deflag = true;
                _deleteData.DateLastUpdate = DateTime.Now;
                db.SubmitChanges();
                return ReturnData.MessageSuccess(_nameBefore + " has been successfully deleted.", null);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public ReturnData AJAX_PreloadUpdate(int idValue)
    {
        try
        {
            dynamic _data = Dynamic_GetDetail(new DataClassesDataContext(), idValue);
            if (_data != null)
                return ReturnData.MessageSuccess("OK", _data);
            return ReturnData.MessageFailed("The requested resource does not exist.", null);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public ReturnData AJAX_GetDataBy_IDAttribute(int idAttribute)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var data = db.TBValues.Where(x => !x.Deflag && x.IDAttribute == idAttribute).Select(x => new { x.IDValue, x.Name });
            return ReturnData.MessageSuccess("OK", data);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public dynamic AJAX_FE_GetDataBy_IDAttribute(int idAttribute)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var data = db.TBValues.Where(x => !x.Deflag && x.IDAttribute == idAttribute).Select(x => new
            {
                x.IDValue,
                x.Name,
                TotalProduct = db.TBProduct_Combination_Details.Where(y => y.IDValue == x.IDValue && !y.TBProduct_Combination.Deflag).Count(),
                x.RGBColor
            });
            return data;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public dynamic AJAX_FE_GetSizeByIDProduct(int idProduct)
    {
        try
        {
            Class_Attribute _attr = new Class_Attribute();
            DataClassesDataContext db = new DataClassesDataContext();
            dynamic _result = db.TBValues.Where(x => x.IDAttribute == _attr.IDSize && !x.Deflag).Select(x => new { x.Name, x.IDValue });
            if (_result == null)
                return null;
            else
            {
                return _result;
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public dynamic AJAX_FE_GetColorByIDProduct(int idProduct)
    {
        try
        {
            Class_Attribute _attr = new Class_Attribute();
            DataClassesDataContext db = new DataClassesDataContext();
            dynamic _result = db.TBValues.Where(x => x.IDAttribute == _attr.IDColor && !x.Deflag).Select(x => new { x.Name, x.IDValue });
            if (_result == null)
                return null;
            else
            {
                return _result;
            }
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
    // FUNCTION
    private Func<DataClassesDataContext, IEnumerable<dynamic>> Dynamic_Func_GetAll
    {
        get
        {
            Func<DataClassesDataContext, IEnumerable<dynamic>> func =
              CompiledQuery.Compile<DataClassesDataContext, IEnumerable<dynamic>>
              ((DataClassesDataContext context) => context.FUNC_Value_GetAll()
                  .AsEnumerable().ToArray());
            return func;
        }
    }
    private Func<DataClassesDataContext, int, IEnumerable<dynamic>> Dynamic_Func_GetData_ByIDAttribute
    {
        get
        {
            Func<DataClassesDataContext, int, IEnumerable<dynamic>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, IEnumerable<dynamic>>
              ((DataClassesDataContext context, int idAttribute) => context.FUNC_Value_GetData_ByIDAttribute(idAttribute)
                  .AsEnumerable().ToArray());
            return func;
        }
    }
    private Func<DataClassesDataContext, int, dynamic> Dynamic_Func_GetDetail
    {
        get
        {
            Func<DataClassesDataContext, int, dynamic> func =
              CompiledQuery.Compile<DataClassesDataContext, int, dynamic>
              ((DataClassesDataContext context, int idValue) => context.FUNC_Value_GetDetail(idValue)
                  .AsEnumerable().FirstOrDefault());
            return func;
        }
    }
    // DYNAMIC
    public IEnumerable<dynamic> Dynamic_GetDataBy_IDAttribute(DataClassesDataContext db, int idAttribute)
    {
        try
        {
            return Dynamic_Func_GetData_ByIDAttribute(db, idAttribute).ToArray();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public dynamic Dynamic_GetDetail(DataClassesDataContext db, int idValue)
    {
        try
        {
            return Dynamic_Func_GetDetail(db, idValue);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public dynamic Dynamic_GetAll(DataClassesDataContext db)
    {
        try
        {
            return Dynamic_Func_GetAll(db).ToArray();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    #endregion

    #region LINQ
    // FUNCTION
    private Func<DataClassesDataContext, IEnumerable<TBValue>> Func_GetAll
    {
        get
        {
            Func<DataClassesDataContext, IEnumerable<TBValue>> func =
              CompiledQuery.Compile<DataClassesDataContext, IEnumerable<TBValue>>
              ((DataClassesDataContext context) => context.TBValues.AsEnumerable()
                .Where(x => !x.Deflag).ToArray());
            return func;
        }
    }
    private Func<DataClassesDataContext, int, IEnumerable<TBValue>> Func_GetData_ByIDAttribute
    {
        get
        {
            Func<DataClassesDataContext, int, IEnumerable<TBValue>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, IEnumerable<TBValue>>
              ((DataClassesDataContext context, int idAttribute) => context.TBValues.AsEnumerable()
                .Where(x => !x.Deflag && x.IDAttribute == idAttribute).ToArray());
            return func;
        }
    }
    private Func<DataClassesDataContext, int, TBValue> Func_GetDetail
    {
        get
        {
            Func<DataClassesDataContext, int, TBValue> func =
              CompiledQuery.Compile<DataClassesDataContext, int, TBValue>
              ((DataClassesDataContext context, int idValue) => context.TBValues.AsEnumerable()
                .Where(x => !x.Deflag && x.IDValue == idValue).FirstOrDefault());
            return func;
        }
    }

    // LINQ
    public IEnumerable<TBValue> GetAll(DataClassesDataContext db)
    {
        try
        {
            return Func_GetAll(db).ToArray();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public IEnumerable<TBValue> GetDataBy_IDAttribute(DataClassesDataContext db, int idAttribute)
    {
        try
        {
            return Func_GetData_ByIDAttribute(db, idAttribute).ToArray();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public TBValue GetDetail(DataClassesDataContext db, int idValue)
    {
        try
        {
            return Func_GetDetail(db, idValue);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public bool ValidationName_Insert(DataClassesDataContext db, string name, int idAttribute)
    {
        try
        {
            if (Dynamic_GetDataBy_IDAttribute(db, idAttribute).AsEnumerable().Where(x => x.Name.ToLower() == name).FirstOrDefault() == null)
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
    public bool ValidationName_Update(DataClassesDataContext db, int idValue, string name, int idAttribute)
    {
        try
        {
            if (Dynamic_GetDataBy_IDAttribute(db, idAttribute).AsEnumerable().Where(x => x.Name.ToLower() == name && x.IDValue != idValue).FirstOrDefault() == null)
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
    #endregion
}