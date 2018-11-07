using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using WITLibrary;

/// <summary>
/// Summary description for Class_Category
/// </summary>
public class Class_Category
{
    public Class_Category()
    {
    }

    #region AJAX
    public WITLibrary.Datatable AJAX_GetTable(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search, int idCategoryParent)
    {
        try
        {
            IEnumerable<dynamic> data = Dynamic_GetDataBy_IDCategoryParent(new DataClassesDataContext(), idCategoryParent);
            int count = data.Count();
            if (!string.IsNullOrEmpty(search))
                data = data.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToArray();

            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (dynamic currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("IDCategory", currData.IDCategory);
                newData.Add("Name", currData.Name);
                newData.Add("Active", currData.Active);
                newData.Add("IDCategoryParent", currData.IDCategoryParent);
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
    public WITLibrary.Datatable AJAX_GetTable_BestCategories(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            dynamic[] data = Dynamic_Get_BestCategories();
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
                newData.Add("IDCategory", data[i].IDCategory);
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
    public ReturnData AJAX_GetDetail(int idCategory)
    {
        try
        {
            dynamic _data = Dynamic_GetDetail(new DataClassesDataContext(), idCategory);
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
    public ReturnData AJAX_Insert(string baseImage, string fnImage, int? idCategoryParent, string name, string description, bool active)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                if (!ValidationName_Insert(db, name))
                    return ReturnData.MessageFailed(name + " already exists.", null);

                if (idCategoryParent.HasValue && idCategoryParent.Value != 0)
                    if (Dynamic_GetDetail(db, idCategoryParent.Value) == null)
                        return ReturnData.MessageSuccess("The requested resource does not exist.", null);

                TBCategory _newData = new TBCategory
                {
                    IDCategoryParent = (idCategoryParent == 0 ? null : idCategoryParent),
                    Image = WITLibrary.ConvertCustom.GetExtention(fnImage),
                    Name = name,
                    Description = description,
                    Active = active,
                    Deflag = false,
                    DateInsert = DateTime.Now,
                    DateLastUpdate = DateTime.Now
                };

                db.TBCategories.InsertOnSubmit(_newData);
                db.SubmitChanges();

                if (_newData != null)
                {
                    if (baseImage != "" && fnImage != "")
                    {
                        System.Drawing.Image _image = WITLibrary.ConvertCustom.Base64ToImage(baseImage);
                        _image.Save(HttpContext.Current.Server.MapPath("/assets/images/category/" + _newData.IDCategory + _newData.Image));
                        _newData.Image = _newData.IDCategory + _newData.Image;
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
    public ReturnData AJAX_Update(int idcategory, string baseImage, string fnImage, int? idCategoryParent, string name, string description, bool active)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                if (!ValidationName_Update(db, idcategory, name))
                    return ReturnData.MessageFailed(name + " already exists.", null);

                TBCategory _updateData = GetDetail(db, idcategory);
                if (_updateData == null)
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);

                string _nameBefore = _updateData.Name;
                _updateData.Name = name;
                _updateData.Description = description;
                _updateData.Active = active;
                _updateData.DateLastUpdate = DateTime.Now;
                if (baseImage != "" && fnImage != "")
                {
                    _updateData.Image = _updateData.IDCategory.ToString() + WITLibrary.ConvertCustom.GetExtention(fnImage);
                    System.Drawing.Image _image = WITLibrary.ConvertCustom.Base64ToImage(baseImage);
                    _image.Save(HttpContext.Current.Server.MapPath("/assets/images/category/" + _updateData.Image));
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
    public ReturnData AJAX_Delete(int idCategory)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                TBCategory _deleteData = GetDetail(db, idCategory);
                if (_deleteData == null)
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);

                string _nameBefore = _deleteData.Name;
                if (_deleteData.TBProduct_Categories.Where(x => !x.TBProduct.Deflag).AsEnumerable().Count() != 0)
                    return ReturnData.MessageFailed(_nameBefore + " failed to deleted, because category already use for some product.", null);

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
    public ReturnData AJAX_PreloadUpdate(int idcategory)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                dynamic _data = Dynamic_GetDetail(db, idcategory);
                if (_data == null)
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);

                Dictionary<string, dynamic> _result = new Dictionary<string, dynamic>();
                _result.Add("IDCategory", _data.IDCategory);
                _result.Add("IDCategoryParent", _data.IDCategoryParent);
                _result.Add("Image", _data.Image);
                _result.Add("Name", _data.Name);
                _result.Add("Description", _data.Description);
                _result.Add("ParentName", _data.ParentName);
                _result.Add("Active", _data.Active);

                return ReturnData.MessageSuccess("OK", _result);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public ReturnData AJAX_StatusToggle(int idcategory)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                TBCategory _data = GetDetail(db, idcategory);
                if (_data == null)
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);

                _data.Active = !_data.Active;
                db.SubmitChanges();
                Dictionary<string, dynamic> resultData = new Dictionary<string, dynamic>();
                resultData.Add("IDCategoryParent", _data.IDCategoryParent);
                return ReturnData.MessageSuccess(_data.Name + " has been successfully " + (_data.Active ? "activated" : "deactivated") + ".", resultData);
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
              ((DataClassesDataContext context) => context.FUNC_Category_GetAll()
                  .AsEnumerable().ToArray());
            return func;
        }
    }
    private Func<DataClassesDataContext, IEnumerable<dynamic>> Dynamic_Func_GetData_ByIDCategoryParentNull
    {
        get
        {
            Func<DataClassesDataContext, IEnumerable<dynamic>> func =
              CompiledQuery.Compile<DataClassesDataContext, IEnumerable<dynamic>>
              ((DataClassesDataContext context) => context.FUNC_Category_GetData_ByIDCategoryParentNull()
                  .AsEnumerable().ToArray());
            return func;
        }
    }
    private Func<DataClassesDataContext, int, IEnumerable<dynamic>> Dynamic_Func_GetData_ByIDCategoryParent
    {
        get
        {
            Func<DataClassesDataContext, int, IEnumerable<dynamic>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, IEnumerable<dynamic>>
              ((DataClassesDataContext context, int idCategoryParent) => context.FUNC_Category_GetData_ByIDCategoryParent(idCategoryParent)
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
              ((DataClassesDataContext context, int idCategory) => context.FUNC_Category_GetDetail(idCategory)
                  .AsEnumerable().ToArray());
            return func;
        }
    }

    // DYNAMIC
    public dynamic Dynamic_GetFilter()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            result.Add("Product", Dynamic_Func_GetData_ByIDCategoryParent(db, db.TBCategories.Where(x => !x.Deflag && x.Name.ToLower() == "product").FirstOrDefault().IDCategory));
            result.Add("Gender", Dynamic_Func_GetData_ByIDCategoryParent(db, db.TBCategories.Where(x => !x.Deflag && x.Name.ToLower() == "gender").FirstOrDefault().IDCategory));
            return result;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            throw;
        }
    }
    public IEnumerable<dynamic> Dynamic_GetAll(DataClassesDataContext db)
    {
        try
        {
            Class_Product _product = new Class_Product();
            return Dynamic_Func_GetAll(db).AsEnumerable().Select(x => new
            {
                x.IDCategory,
                x.IDCategoryParent,
                ParentName = x.ParentName,
                Image = OurClass.ImageExists(x.Image, "category") ? x.Image : "noimage.jpg",
                x.Name,
                x.Description,
                x.Active,
                Child = Dynamic_GetDataBy_IDCategoryParent(db, x.IDCategory),
                TotalProduct = _product.AJAX_FE_GetTotalProduct_ByIDCategory(x.IDCategory)
            }).ToArray();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public IEnumerable<dynamic> Dynamic_GetDataBy_IDCategoryParent(DataClassesDataContext db, int idCategoryParent)
    {
        try
        {
            Class_Product _product = new Class_Product();
            if (idCategoryParent == 0)
                return Dynamic_Func_GetData_ByIDCategoryParentNull(db).AsEnumerable().Select(x => new
                {
                    x.IDCategory,
                    x.IDCategoryParent,
                    ParentName = x.ParentName,
                    Image = OurClass.ImageExists(x.Image, "category") ? x.Image : "noimage.jpg",
                    x.Name,
                    x.Description,
                    x.Active,
                    TotalProduct = _product.AJAX_FE_GetTotalProduct_ByIDCategory(x.IDCategory)
                }).ToArray();
            return Dynamic_Func_GetData_ByIDCategoryParent(db, idCategoryParent).AsEnumerable().Select(x => new
            {
                x.IDCategory,
                x.IDCategoryParent,
                ParentName = x.ParentName,
                Image = OurClass.ImageExists(x.Image, "category") ? x.Image : "noimage.jpg",
                x.Name,
                x.Description,
                x.Active,
                TotalProduct = _product.AJAX_FE_GetTotalProduct_ByIDCategory(x.IDCategory)
            }).ToArray();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public dynamic Dynamic_GetDetail(DataClassesDataContext db, int idCategory)
    {
        try
        {
            return Dynamic_Func_GetDetail(db, idCategory).AsEnumerable().Select(x => new
            {
                x.IDCategory,
                x.IDCategoryParent,
                ParentName = x.ParentName,
                Image = OurClass.ImageExists(x.Image, "category") ? x.Image : "noimage.jpg",
                x.Name,
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

    public dynamic Dynamic_FE_GetDetail(int idCategory)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return Dynamic_Func_GetDetail(db, idCategory).AsEnumerable().Select(x => new
            {
                x.IDCategory,
                x.IDCategoryParent,
                ParentName = x.ParentName,
                Image = OurClass.ImageExists(x.Image, "category") ? x.Image : "noimage.jpg",
                x.Name,
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

    public dynamic Dynamic_Get_BestCategories()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.SP_Stats_GetBestCategories().OrderByDescending(x => x.Quantity).ToArray();
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
    private Func<DataClassesDataContext, IEnumerable<TBCategory>> Func_GetAll
    {
        get
        {
            Func<DataClassesDataContext, IEnumerable<TBCategory>> func =
              CompiledQuery.Compile<DataClassesDataContext, IEnumerable<TBCategory>>
              ((DataClassesDataContext context) => context.TBCategories.AsEnumerable()
                .Where(x => !x.Deflag).ToArray());
            return func;
        }
    }
    private Func<DataClassesDataContext, IEnumerable<TBCategory>> Func_GetData_ByIDCategoryParentNull
    {
        get
        {
            Func<DataClassesDataContext, IEnumerable<TBCategory>> func =
              CompiledQuery.Compile<DataClassesDataContext, IEnumerable<TBCategory>>
              ((DataClassesDataContext context) => context.TBCategories.AsEnumerable()
                .Where(x => !x.Deflag && x.IDCategoryParent == null).ToArray());
            return func;
        }
    }
    private Func<DataClassesDataContext, int, IEnumerable<TBCategory>> Func_GetData_ByIDCategoryParent
    {
        get
        {
            Func<DataClassesDataContext, int, IEnumerable<TBCategory>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, IEnumerable<TBCategory>>
              ((DataClassesDataContext context, int idCategoryParent) => context.TBCategories.AsEnumerable()
                .Where(x => !x.Deflag && x.IDCategoryParent == idCategoryParent).ToArray());
            return func;
        }
    }
    private Func<DataClassesDataContext, int, TBCategory> Func_GetDetail
    {
        get
        {
            Func<DataClassesDataContext, int, TBCategory> func =
              CompiledQuery.Compile<DataClassesDataContext, int, TBCategory>
              ((DataClassesDataContext context, int idCategory) => context.TBCategories.AsEnumerable()
                .Where(x => !x.Deflag && x.IDCategory == idCategory).FirstOrDefault());
            return func;
        }
    }

    // LINQ
    public IEnumerable<TBCategory> GetAll(DataClassesDataContext db)
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
    public IEnumerable<TBCategory> GetDataBy_IDCategoryParent(DataClassesDataContext db, int idCategoryParent)
    {
        try
        {
            if (idCategoryParent == 0)
                return Func_GetData_ByIDCategoryParentNull(db);
            else
                return Func_GetData_ByIDCategoryParent(db, idCategoryParent);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public TBCategory GetDetail(DataClassesDataContext db, int idCategory)
    {
        try
        {
            return Func_GetDetail(db, idCategory);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public bool ValidationName_Insert(DataClassesDataContext db, string name)
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
    public bool ValidationName_Update(DataClassesDataContext db, int idCategory, string name)
    {
        try
        {
            if (Dynamic_GetAll(db).AsEnumerable().Where(x => x.Name.ToLower() == name && x.IDCategory != idCategory).FirstOrDefault() == null)
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