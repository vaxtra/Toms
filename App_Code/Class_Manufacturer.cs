using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using WITLibrary;

/// <summary>
/// Summary description for Class_Manufacturer
/// </summary>
public class Class_Manufacturer
{
    public Class_Manufacturer()
    { }

    #region AJAX
    public Datatable AJAX_GetTable(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            IEnumerable<dynamic> data = Dynamic_GetAll(new DataClassesDataContext());
            int count = data.Count();
            if (!string.IsNullOrEmpty(search))
                data = data.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToArray();
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (dynamic currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("IDManufacturer", currData.IDManufacturer);
                newData.Add("Name", currData.Name);
                newData.Add("Email", currData.Email);
                newData.Add("Active", currData.Active);
                newData.Add("Phone", currData.Phone);
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
    public WITLibrary.Datatable AJAX_GetTable_BestManufacturer(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            dynamic[] data = Dynamic_Get_BestManufacturer();
            int count = data.Count();
            if (!string.IsNullOrEmpty(search))
                data = data.AsEnumerable().Where(x =>
                    x.Name.ToLower().Contains(search.ToLower()) ||
                    x.ReferenceCode.ToLower().Contains(search.ToLower())
                    ).ToArray();
            Dictionary<string, dynamic>[] resultList = new Dictionary<string, dynamic>[data.Count()];
            for (int i = 0; i < data.Count(); i++)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("IDManufacturer", data[i].IDManufacturer);
                newData.Add("Name", data[i].Name);
                newData.Add("Quantity", data[i].Quantity);
                resultList[i] = newData;
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
    public ReturnData AJAX_GetDetail(int idManufacturer)
    {
        try
        {
            dynamic _data = Dynamic_GetDetail(new DataClassesDataContext(), idManufacturer);
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
    public ReturnData AJAX_Insert(string baseImage, string fnImage, string name, string email, string phone, string address, string description, bool active)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                if (!ValidationInsert_Name(db, name))
                    return ReturnData.MessageFailed(name + " already exists.", null);

                TBManufacturer _newData = new TBManufacturer
                {
                    Logo = WITLibrary.ConvertCustom.GetExtention(fnImage),
                    Name = name,
                    Email = email,
                    Phone = phone,
                    Address = address,
                    ShortDescription = OurClass.ShortDescription(description, 250),
                    Description = description,
                    Active = active,
                    Deflag = false,
                    DateInsert = DateTime.Now,
                    DateLastUpdate = DateTime.Now
                };

                db.TBManufacturers.InsertOnSubmit(_newData);
                db.SubmitChanges();

                if (_newData != null)
                {
                    if (baseImage != "" && fnImage != "")
                    {
                        System.Drawing.Image _image = WITLibrary.ConvertCustom.Base64ToImage(baseImage);
                        _image.Save(HttpContext.Current.Server.MapPath("/assets/images/manufacturer/" + _newData.IDManufacturer + _newData.Logo));
                        _newData.Logo = _newData.IDManufacturer + _newData.Logo;
                        db.SubmitChanges();
                    }
                    if (_newData != null)
                        return ReturnData.MessageSuccess(name + " has been successfully inserted.", null);
                    return ReturnData.MessageFailed(name + " failed to insert.", null);
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
    public ReturnData AJAX_Update(int idManufacturer, string baseImage, string fnImage, string name, string email, string phone, string address, string description, bool active)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                if (!ValidationName_Update(db, idManufacturer, name))
                    return ReturnData.MessageFailed(name + " already exists.", null);

                TBManufacturer _updateData = GetDetail(db, idManufacturer);
                if (_updateData == null)
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);

                string _nameBefore = _updateData.Name;
                _updateData.Logo = _updateData.IDManufacturer.ToString() + WITLibrary.ConvertCustom.GetExtention(fnImage);
                _updateData.Name = name;
                _updateData.Email = email;
                _updateData.Phone = phone;
                _updateData.Address = address;
                _updateData.ShortDescription = OurClass.ShortDescription(description, 250);
                _updateData.Description = description;
                _updateData.Active = active;
                _updateData.DateLastUpdate = DateTime.Now;

                if (baseImage != "" && fnImage != "")
                {
                    System.Drawing.Image _image = WITLibrary.ConvertCustom.Base64ToImage(baseImage);
                    _image.Save(HttpContext.Current.Server.MapPath("/assets/images/manufacturer/" + _updateData.Logo));
                }
                db.SubmitChanges();
                if (_updateData != null)
                    return ReturnData.MessageSuccess(_nameBefore + " has been successfully updated.", null);
                return ReturnData.MessageFailed(_nameBefore + " failed to update.", null);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public ReturnData AJAX_Delete(int idManufacturer)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                TBManufacturer _deleteData = GetDetail(db, idManufacturer);
                if (_deleteData == null)
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);

                string _nameBefore = _deleteData.Name;
                _deleteData.Deflag = true;
                _deleteData.DateLastUpdate = DateTime.Now;
                db.SubmitChanges();
                return ReturnData.MessageSuccess(_deleteData.Name + " has been successfully deleted.", null);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public ReturnData AJAX_StatusToggle(int idManufacturer)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                TBManufacturer _data = GetDetail(db, idManufacturer);
                if (_data == null)
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);

                _data.Active = !_data.Active;
                db.SubmitChanges();
                return ReturnData.MessageSuccess(_data.Name + " has been successfully " + (_data.Active ? "activated" : "deactivated") + ".", null);
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
              ((DataClassesDataContext context) => context.FUNC_Manufacturer_GetAll()
                  .AsEnumerable().ToArray());
            return func;
        }
    }
    private Func<DataClassesDataContext, bool, IEnumerable<dynamic>> Dynamic_Func_GetData_ByActive
    {
        get
        {
            Func<DataClassesDataContext, bool, IEnumerable<dynamic>> func =
              CompiledQuery.Compile<DataClassesDataContext, bool, IEnumerable<dynamic>>
              ((DataClassesDataContext context, bool active) => context.FUNC_Manufacturer_GetData_ByActive(active)
                  .AsEnumerable().ToArray());
            return func;
        }
    }
    private Func<DataClassesDataContext, int, IEnumerable<dynamic>> Dynamic_Func_GetDetail
    {
        get
        {
            Func<DataClassesDataContext, int, IEnumerable<dynamic>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, IEnumerable<dynamic>>
              ((DataClassesDataContext context, int idManufacturer) => context.FUNC_Manufacturer_GetDetail(idManufacturer)
                  .AsEnumerable().ToArray());
            return func;
        }
    }


    // DYNAMIC
    public IEnumerable<dynamic> Dynamic_GetAll(DataClassesDataContext db)
    {
        try
        {
            return Dynamic_Func_GetAll(db).AsEnumerable().Select(x => new
            {
                x.IDManufacturer,
                Logo = OurClass.ImageExists(x.Logo, "manufacturer") ? x.Logo : "noimage.jpg",
                x.Name,
                x.Email,
                x.Phone,
                x.Address,
                x.Description,
                x.Active
            }).ToArray();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public IEnumerable<dynamic> Dynamic_GetData_Active(DataClassesDataContext db)
    {
        try
        {
            return Dynamic_Func_GetData_ByActive(db, true).AsEnumerable().Select(x => new
            {
                x.IDManufacturer,
                Logo = OurClass.ImageExists(x.Logo, "manufacturer") ? x.Logo : "noimage.jpg",
                x.Name,
                x.Email,
                x.Phone,
                x.Address,
                x.Description,
                x.Active
            }).ToArray();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public dynamic Dynamic_GetDetail(DataClassesDataContext db, int idManufacturer)
    {
        try
        {
            return Dynamic_Func_GetDetail(db, idManufacturer).AsEnumerable().AsEnumerable().Select(x => new
            {
                x.IDManufacturer,
                Logo = OurClass.ImageExists(x.Logo, "manufacturer") ? x.Logo : "noimage.jpg",
                x.Name,
                x.Email,
                x.Phone,
                x.Address,
                x.Description,
                x.Active
            }).FirstOrDefault();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public dynamic Dynamic_Get_BestManufacturer()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.SP_Stats_GetBestManufacturer().ToArray();
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
    private Func<DataClassesDataContext, IEnumerable<TBManufacturer>> Func_GetAll
    {
        get
        {
            Func<DataClassesDataContext, IEnumerable<TBManufacturer>> func =
              CompiledQuery.Compile<DataClassesDataContext, IEnumerable<TBManufacturer>>
              ((DataClassesDataContext context) => context.TBManufacturers.AsEnumerable()
                .Where(x => !x.Deflag).ToArray());
            return func;
        }
    }
    private Func<DataClassesDataContext, int, TBManufacturer> Func_GetDetail
    {
        get
        {
            Func<DataClassesDataContext, int, TBManufacturer> func =
              CompiledQuery.Compile<DataClassesDataContext, int, TBManufacturer>
              ((DataClassesDataContext context, int idManufacturer) => context.TBManufacturers.AsEnumerable()
                .Where(x => !x.Deflag && x.IDManufacturer == idManufacturer).FirstOrDefault());
            return func;
        }
    }

    // LINQ
    public IEnumerable<TBManufacturer> GetAll(DataClassesDataContext db)
    {
        try
        {
            return Func_GetAll(db);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public TBManufacturer GetDetail(DataClassesDataContext db, int idManufacturer)
    {
        try
        {
            return Func_GetDetail(db, idManufacturer);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public bool ValidationInsert_Name(DataClassesDataContext db, string name)
    {
        try
        {
            if (Dynamic_GetAll(db).AsEnumerable().Where(x => x.Name.ToLower() == name).FirstOrDefault() == null)
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
    public bool ValidationName_Update(DataClassesDataContext db, int idManufacturer, string name)
    {
        try
        {
            if (Dynamic_GetAll(db).AsEnumerable().Where(x => x.Name.ToLower() == name && x.IDManufacturer != idManufacturer).FirstOrDefault() == null)
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