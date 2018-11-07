using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using WITLibrary;


/// <summary>
/// Summary description for Class_Page
/// </summary>
public class Class_Page
{
    public Class_Page()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region AJAX

    public Datatable AJAX_GetTable(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                IEnumerable<dynamic> data = Dynamic_GetAll(db).Where(x => !x.Deflag);
                int count = data.Count();
                if (!string.IsNullOrEmpty(search))
                    data = data.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToArray();
                List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
                foreach (dynamic currData in data)
                {
                    Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                    newData.Add("IDPage", currData.IDPage);
                    newData.Add("Page_Title", currData.Page_Title);
                    newData.Add("Page_ShortContent", currData.Page_ShortContent);
                    newData.Add("Page_Content", currData.Page_Content);
                    newData.Add("DateInsert", currData.DateInsert.ToString("dd-MM-yyyy") + " " + currData.DateInsert.ToLongTimeString());
                    newData.Add("DateLastUpdate", currData.DateLastUpdate.ToString("dd-MM-yyyy") + " " + currData.DateLastUpdate.ToLongTimeString());
                    newData.Add("Active", currData.Active);
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

    public ReturnData AJAX_GetDetail(int idPage)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            Class_Page_Media _pageMedia = new Class_Page_Media();
            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
            data.Add("Page", Dynamic_GetDetail(db, idPage));
            data.Add("PageMedia", _pageMedia.Dynamic_GetData_PageMedia(db, idPage));
            return ReturnData.MessageSuccess("OK", data);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_Insert(string pageTitle, string pageShortContent, string pageContent, bool active)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                if (!ValidationName_Insert(db, pageTitle))
                    return ReturnData.MessageFailed(pageTitle + " already exists.", null);

                TBPage _newData = new TBPage
                {
                    Page_Title = pageTitle,
                    Page_ShortContent = pageShortContent,
                    Page_Content = pageContent,
                    Active = active,
                    Deflag = false,
                    DateInsert = DateTime.Now,
                    DateLastUpdate = DateTime.Now
                };

                db.TBPages.InsertOnSubmit(_newData);
                db.SubmitChanges();

                if (_newData != null)
                {
                    return ReturnData.MessageSuccess(pageTitle + " has been successfully inserted.", _newData.IDPage);
                }
                else
                {
                    return ReturnData.MessageFailed(pageTitle + " failed to insert.", null);
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

    public ReturnData AJAX_BE_Updates(int idPage, string pageTitle, string pageShortContent, string PageContent)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBPage page = db.TBPages.Where(x => !x.Deflag && x.IDPage == idPage).FirstOrDefault();
            if (page == null)
                return ReturnData.MessageFailed("Page not found", null);
            page.Page_Title = pageTitle;
            page.Page_ShortContent = pageShortContent;
            page.Page_Content = PageContent;
            page.DateLastUpdate = DateTime.Now;
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

    public ReturnData AJAX_GetAll()
    {
        try
        {
            dynamic _data = Dynamic_GetAll(new DataClassesDataContext());
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

    public ReturnData AJAX_BE_StatusToggle(int idPage)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBPage pag = db.TBPages.Where(x => !x.Deflag && x.IDPage == idPage).FirstOrDefault();
            if (pag == null)
                return ReturnData.MessageFailed("Page not found", null);
            pag.Active = !pag.Active;
            pag.DateLastUpdate = DateTime.Now;
            db.SubmitChanges();

            return ReturnData.MessageSuccess("Status changed successfully", null);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_BE_Delete(int idPage)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBPage page = db.TBPages.Where(x => x.IDPage == idPage).FirstOrDefault();
            if (page == null)
                return ReturnData.MessageFailed("Page not found", null);
            page.Deflag = true;
            page.DateLastUpdate = DateTime.Now;
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

    public ReturnData AJAX_Insert_Photo(int idPage, HttpPostedFile file)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                dynamic _page = Dynamic_GetDetail_Information(db, idPage);
                if (_page == null)
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);

                List<TBPageMedia> _renameFile = GetData_Photo_ByIDPage(db, idPage).ToList();
                for (int i = 0; i < _renameFile.Count; i++)
                {
                    string oldPhoto = _renameFile[i].MediaUrl;
                    _renameFile[i].MediaUrl = ((string)_page.Page_Title).ToLower().Replace(" ", "-") + "-" + (i + 1).ToString() + WITLibrary.ConvertCustom.GetExtention(oldPhoto);
                    if (OurClass.ImageExists(oldPhoto, "page"))
                        System.IO.File.Move(HttpContext.Current.Server.MapPath("~/assets/images/page/" + oldPhoto), HttpContext.Current.Server.MapPath("~/assets/images/page/" + _renameFile[i].MediaUrl));
                }

                int fileSizeInBytes = file.ContentLength;
                string fileName = file.FileName;
                string fileExtension = System.IO.Path.GetExtension(fileName);

                TBPageMedia _newData = new TBPageMedia
                {
                    IDPage = idPage,
                    Cover = (_renameFile.Where(x => x.Cover).Count() == 0) ? true : false,
                    MediaUrl = ((string)_page.Page_Title).ToLower().Replace(" ", "-") + "-" + (_renameFile.Count() + 1).ToString() + fileExtension,
                    Active = true,
                    DateInsert = DateTime.Now,
                    DateLastUpdate = DateTime.Now,
                };

                db.TBPageMedias.InsertOnSubmit(_newData);
                db.SubmitChanges();
                if (_newData != null)
                {
                    file.SaveAs(HttpContext.Current.Server.MapPath("/assets/images/page/" + _newData.MediaUrl));
                    return ReturnData.MessageSuccess(fileName + " for this product has been successfully uploaded.", null);
                }
                return ReturnData.MessageFailed(fileName + " for this product failed to upload.", null);

            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_Delete_Photo(int idPageMedia)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                TBPageMedia _deleteData = GetDetail_Photo(db, idPageMedia);
                if (_deleteData == null)
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);
                string _nameBefore = _deleteData.MediaUrl;

                db.TBPageMedias.DeleteOnSubmit(_deleteData);
                db.SubmitChanges();

                OurClass.DeleteFile(_deleteData.MediaUrl, "page");
                Dictionary<string, dynamic> _result = new Dictionary<string, dynamic>();
                _result.Add("Photos", Dynamic_GetData_Photo_ByIDPage(db, _deleteData.IDPage));
                return ReturnData.MessageSuccess(_nameBefore + " has been successfully deleted.", _result);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_ChangeCover_Photo(int idPageMedia)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                string _nameBefore = "";
                dynamic _changeData = Dynamic_GetDetail_Photo(db, idPageMedia);
                if (_changeData == null)
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);

                _nameBefore = _changeData.MediaUrl;
                if (_changeData.Cover)
                    return ReturnData.MessageFailed(_nameBefore + " already cover for this product.", null);

                foreach (TBPageMedia item in GetData_Photo_ByIDPage(db, _changeData.IDPage))
                {
                    if (item.IDPageMedia != _changeData.IDPageMedia)
                    {
                        if (item.Cover)
                        {
                            item.Cover = false;
                            item.DateLastUpdate = DateTime.Now;
                        }
                    }
                    else
                    {
                        item.Cover = true;
                        item.DateLastUpdate = DateTime.Now;
                    }
                }
                db.SubmitChanges();
                Dictionary<string, dynamic> _result = new Dictionary<string, dynamic>();
                _result.Add("Photos", Dynamic_GetData_Photo_ByIDPage(db, _changeData.IDPage));
                return ReturnData.MessageSuccess(_nameBefore + " has been successfully set cover for this product.", _result);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_GetDetailPhoto(int idPageMedia)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            Class_Page_Media _pageMedia = new Class_Page_Media();
            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
            data.Add("PageMedia", _pageMedia.Dynamic_GetDataDetail_PageMedia(db, idPageMedia));
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

    public IEnumerable<dynamic> Dynamic_GetAll(DataClassesDataContext db)
    {
        try
        {
            return db.TBPages.Where(x => !x.Deflag).AsEnumerable().Select(x => new
            {
                x.IDPage,
                x.Page_Title,
                x.Page_ShortContent,
                x.Page_Content,
                x.Active,
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
    public dynamic Dynamic_GetDetail(DataClassesDataContext db, int idPage)
    {
        try
        {
            return db.TBPages.Where(x => x.IDPage == idPage).AsEnumerable().Select(x => new
            {
                x.IDPage,
                x.Page_Title,
                x.Page_ShortContent,
                x.Page_Content,
                x.Active,
                x.DateInsert,
                x.DateLastUpdate
            }).FirstOrDefault();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_GetDetail_Information(DataClassesDataContext db, int idpage)
    {
        try
        {
            return Dynamic_Func_GetDetail_Information(db, idpage);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public IEnumerable<TBPageMedia> GetData_Photo_ByIDPage(DataClassesDataContext db, int idPage)
    {
        try
        {
            return Func_GetData_ByIDPage_Photo(db, idPage);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public TBPageMedia GetDetail_Photo(DataClassesDataContext db, int idPageMedia)
    {
        try
        {
            return Func_GetDetail_Photo(db, idPageMedia);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public IEnumerable<dynamic> Dynamic_GetData_Photo_ByIDPage(DataClassesDataContext db, int idPage)
    {
        try
        {
            return Dynamic_Func_GetData_ByIDPage_Photo(db, idPage).AsEnumerable().Select(x => new
            {
                x.IDPageMedia,
                x.IDPage,
                x.Title,
                x.Description,
                Preview = OurClass.ImageExists(x.MediaUrl, "page") ? x.MediaUrl : "noimage.jpg",
                x.Cover,
                x.Active,
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

    public dynamic Dynamic_GetDetail_Photo(DataClassesDataContext db, int idPageMedia)
    {
        try
        {
            return Dynamic_Func_GetDetail_Photo(db, idPageMedia).AsEnumerable().Select(x => new
            {
                x.IDPageMedia,
                x.IDPage,
                x.MediaUrl,
                Preview = OurClass.ImageExists(x.MediaUrl, "page") ? x.MediaUrl : "noimage.jpg",
                x.Cover
            }).FirstOrDefault();
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

    private Func<DataClassesDataContext, int, dynamic> Dynamic_Func_GetDetail_Information
    {
        get
        {
            Func<DataClassesDataContext, int, dynamic> func =
              CompiledQuery.Compile<DataClassesDataContext, int, dynamic>
              ((DataClassesDataContext context, int idpage) => context.FUNC_Page_GetDetail_Information(idpage)
                  .AsEnumerable().FirstOrDefault());
            return func;
        }
    }

    private Func<DataClassesDataContext, int, IEnumerable<TBPageMedia>> Func_GetData_ByIDPage_Photo
    {
        get
        {
            Func<DataClassesDataContext, int, IEnumerable<TBPageMedia>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, IEnumerable<TBPageMedia>>
              ((DataClassesDataContext context, int idPage) => context.TBPageMedias.AsEnumerable()
                .Where(x => x.IDPage == idPage).ToArray());
            return func;
        }
    }

    private Func<DataClassesDataContext, int, TBPageMedia> Func_GetDetail_Photo
    {
        get
        {
            Func<DataClassesDataContext, int, TBPageMedia> func =
              CompiledQuery.Compile<DataClassesDataContext, int, TBPageMedia>
              ((DataClassesDataContext context, int idPageMedia) => context.TBPageMedias.AsEnumerable()
                .Where(x => x.IDPageMedia == idPageMedia).FirstOrDefault());
            return func;
        }
    }

    private Func<DataClassesDataContext, int, IEnumerable<dynamic>> Dynamic_Func_GetData_ByIDPage_Photo
    {
        get
        {
            Func<DataClassesDataContext, int, IEnumerable<dynamic>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, IEnumerable<dynamic>>
              ((DataClassesDataContext context, int idPage) => context.FUNC_Page_Photo_GetData_ByIDPage(idPage)
                  .AsEnumerable().OrderByDescending(x => x.Cover).ToArray());
            return func;
        }
    }

    private Func<DataClassesDataContext, int, IEnumerable<dynamic>> Dynamic_Func_GetDetail_Photo
    {
        get
        {
            Func<DataClassesDataContext, int, IEnumerable<dynamic>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, IEnumerable<dynamic>>
              ((DataClassesDataContext context, int idPageMedia) => context.FUNC_Page_Photo_GetDetail(idPageMedia)
                  .AsEnumerable().ToArray());
            return func;
        }
    }

    public bool ValidationName_Insert(DataClassesDataContext db, string name)
    {
        try
        {
            if (Dynamic_GetAll(db).AsEnumerable().Where(x => x.Page_Title.ToLower() == name).FirstOrDefault() == null)
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
    public bool ValidationName_Update(DataClassesDataContext db, int idPage, string name)
    {
        try
        {
            if (Dynamic_GetAll(db).AsEnumerable().Where(x => x.Page_Title.ToLower() == name && x.IDPage != idPage).FirstOrDefault() == null)
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