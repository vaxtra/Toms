using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using WITLibrary;

/// <summary>
/// Summary description for Class_Post
/// </summary>
public class Class_Post
{
    public Class_Post()
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
            DataClassesDataContext db = new DataClassesDataContext();
            Class_Page_Category _pageCategory = new Class_Page_Category();
            IEnumerable<dynamic> data = Dynamic_GetAll(db);
            int count = data.Count();
            if (!string.IsNullOrEmpty(search))
                data = data.Where(x => x.Post_Title.ToLower().Contains(search.ToLower())).ToArray();
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (dynamic currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("IDPost", currData.IDPost);
                newData.Add("Post_Title", currData.Post_Title);
                newData.Add("Page_Title", currData.Page_Title);
                newData.Add("Post_ShortContent", currData.Post_ShortContent);
                newData.Add("Post_Content", currData.Post_Content);
                newData.Add("DateInsert", currData.DateInsert.ToString("dd-MM-yyyy") + " " + currData.DateInsert.ToLongTimeString());
                newData.Add("DateLastUpdate", currData.DateLastUpdate.ToString("dd-MM-yyyy") + " " + currData.DateLastUpdate.ToLongTimeString());
                newData.Add("Active", currData.Active);
                newData.Add("Categories", Dynamic_GetData_PageCategories_ByIDPost(db, currData.IDPost));
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

    public ReturnData AJAX_Insert(string postTitle, string postShortContent, string postContent, int idPage, bool active)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                if (!ValidationName_Insert(db, postTitle))
                    return ReturnData.MessageFailed(postTitle + " already exists.", null);

                TBPost _newData = new TBPost
                {
                    Post_Title = postTitle,
                    IDPage = idPage,
                    Post_ShortContent = postShortContent,
                    Post_Content = postContent,
                    Active = active,
                    Deflag = false,
                    DateInsert = DateTime.Now,
                    DateLastUpdate = DateTime.Now
                };

                db.TBPosts.InsertOnSubmit(_newData);
                db.SubmitChanges();

                if (_newData != null)
                {
                    return ReturnData.MessageSuccess(postTitle + " has been successfully inserted.", _newData.IDPost);
                }
                else
                {
                    return ReturnData.MessageFailed(postTitle + " failed to insert.", null);
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

    public ReturnData AJAX_Updates(int idPost, string postTitle, string postShortContent, string postContent)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBPost post = db.TBPosts.Where(x => !x.Deflag && x.IDPost == idPost).FirstOrDefault();
            if (post == null)
                return ReturnData.MessageFailed("Post not found", null);
            post.Post_Title = postTitle;
            post.Post_ShortContent = postShortContent;
            post.Post_Content = postContent;
            post.DateLastUpdate = DateTime.Now;
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

    public ReturnData AJAX_StatusToggle(int idPost)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBPost post = db.TBPosts.Where(x => !x.Deflag && x.IDPost == idPost).FirstOrDefault();
            if (post == null)
                return ReturnData.MessageFailed("Post not found", null);
            post.Active = !post.Active;
            post.DateLastUpdate = DateTime.Now;
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

    public ReturnData AJAX_Delete(int idPost)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBPost post = db.TBPosts.Where(x => x.IDPost == idPost).FirstOrDefault();
            if (post == null)
                return ReturnData.MessageFailed("Post not found", null);
            post.Deflag = true;
            post.DateLastUpdate = DateTime.Now;
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

    public ReturnData AJAX_GetDetail(int idPost)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            Class_Post_Media _postMedia = new Class_Post_Media();
            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
            data.Add("Post", Dynamic_GetDetail(db, idPost));
            data.Add("PostMedia", _postMedia.Dynamic_GetData_PageMedia(db, idPost));
            return ReturnData.MessageSuccess("OK", data);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_GetPageCategory(int idPage, int idPost)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
            data.Add("PageCategory", GetTree_PageCategory(db, idPage, idPost));
            data.Add("SelectedPageCategory", Dynamic_GetData_PageCategories_ByIDPost(db, idPost));
            return ReturnData.MessageSuccess("OK", data);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_FE_GetPost_ByIDPageCategory(int idPageCategory)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            Class_Post_Media _postMedia = new Class_Post_Media();
            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
            data.Add("Post", Dynamic_FE_GetPost_ByIDPageCategory(db, idPageCategory));
            return ReturnData.MessageSuccess("OK", data);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_Insert_Photo(int idPost, HttpPostedFile file)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                dynamic _post = Dynamic_GetDetail_Information(db, idPost);
                if (_post == null)
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);

                List<TBPostMedia> _renameFile = GetData_Photo_ByIDPost(db, idPost).ToList();
                for (int i = 0; i < _renameFile.Count; i++)
                {
                    string oldPhoto = _renameFile[i].MediaUrl;
                    _renameFile[i].MediaUrl = ((string)_post.Post_Title).ToLower().Replace(" ", "-") + "-" + (i + 1).ToString() + WITLibrary.ConvertCustom.GetExtention(oldPhoto);
                    if (OurClass.ImageExists(oldPhoto, "post"))
                        System.IO.File.Move(HttpContext.Current.Server.MapPath("~/assets/images/post/" + oldPhoto), HttpContext.Current.Server.MapPath("~/assets/images/post/" + _renameFile[i].MediaUrl));
                }

                int fileSizeInBytes = file.ContentLength;
                string fileName = file.FileName;
                string fileExtension = System.IO.Path.GetExtension(fileName);

                TBPostMedia _newData = new TBPostMedia
                {
                    IDPost = idPost,
                    MediaUrl = ((string)_post.Post_Title).ToLower().Replace(" ", "-") + "-" + (_renameFile.Count() + 1).ToString() + fileExtension,
                    Active = true,
                    DateInsert = DateTime.Now,
                    DateLastUpdate = DateTime.Now,
                };

                db.TBPostMedias.InsertOnSubmit(_newData);
                db.SubmitChanges();
                if (_newData != null)
                {
                    file.SaveAs(HttpContext.Current.Server.MapPath("/assets/images/post/" + _newData.MediaUrl));
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

    public ReturnData AJAX_Delete_Photo(int idPostMedia)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                TBPostMedia _deleteData = GetDetail_Photo(db, idPostMedia);
                if (_deleteData == null)
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);
                string _nameBefore = _deleteData.MediaUrl;

                db.TBPostMedias.DeleteOnSubmit(_deleteData);
                db.SubmitChanges();

                OurClass.DeleteFile(_deleteData.MediaUrl, "post");
                Dictionary<string, dynamic> _result = new Dictionary<string, dynamic>();
                _result.Add("Photos", Dynamic_GetData_Photo_ByIDPost(db, _deleteData.IDPost));
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

    public ReturnData AJAX_GetDetailPhoto(int idPostMedia)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            Class_Post_Media _postMedia = new Class_Post_Media();
            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
            data.Add("PostMedia", _postMedia.Dynamic_GetDataDetail_PostMedia(db, idPostMedia));
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
            return db.TBPosts.Where(x => !x.Deflag).AsEnumerable().Select(x => new
            {
                x.IDPost,
                x.IDPage,
                x.Post_Title,
                x.TBPage.Page_Title,
                x.Post_ShortContent,
                x.Post_Content,
                x.Deflag,
                x.Active,
                x.DateInsert,
                x.DateLastUpdate,
            }).ToArray();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_GetDetail(DataClassesDataContext db, int idPost)
    {
        try
        {
            return db.TBPosts.Where(x => x.IDPost == idPost).AsEnumerable().Select(x => new
            {
                x.IDPost,
                x.IDPage,
                x.Post_Title,
                x.Post_ShortContent,
                x.Post_Content,
                x.Deflag,
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

    public dynamic Dynamic_FE_GetPost_ByIDPageCategory(DataClassesDataContext db, int idPageCategory)
    {
        try
        {
            return db.TBPageCategory_Posts.Where(x => x.IDPageCategory == idPageCategory).AsEnumerable().Select(x => new
            {
                x.IDPost,
                x.TBPost.IDPage,
                x.TBPost.Post_Title,
                x.TBPost.Post_ShortContent,
                x.TBPost.Post_Content,
                x.TBPost.Deflag,
                x.TBPost.Active,
                x.TBPost.DateInsert,
                x.TBPost.DateLastUpdate,
                Media = x.TBPost.TBPostMedias.Where(y => y.IDPost == x.IDPost).Select(y => y.MediaUrl)
            }).FirstOrDefault();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public IEnumerable<TBPostMedia> GetData_Photo_ByIDPost(DataClassesDataContext db, int idPost)
    {
        try
        {
            return Func_GetData_ByIDPost_Photo(db, idPost);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public TBPostMedia GetDetail_Photo(DataClassesDataContext db, int idPostMedia)
    {
        try
        {
            return Func_GetDetail_Photo(db, idPostMedia);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public IEnumerable<dynamic> Dynamic_GetData_Photo_ByIDPost(DataClassesDataContext db, int idPost)
    {
        try
        {
            return Dynamic_Func_GetData_ByIDPost_Photo(db, idPost).AsEnumerable().Select(x => new
            {
                x.IDPostMedia,
                x.IDPost,
                x.Title,
                x.Description,
                Preview = OurClass.ImageExists(x.MediaUrl, "post") ? x.MediaUrl : "noimage.jpg",
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

    public dynamic Dynamic_GetDetail_Photo(DataClassesDataContext db, int idPostMedia)
    {
        try
        {
            return Dynamic_Func_GetDetail_Photo(db, idPostMedia).AsEnumerable().Select(x => new
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

    public dynamic Dynamic_GetDetail_Information(DataClassesDataContext db, int idpost)
    {
        try
        {
            return Dynamic_Func_GetDetail_Information(db, idpost);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public IEnumerable<dynamic> Dynamic_GetData_PageCategories_ByIDPost(DataClassesDataContext db, int idPost)
    {
        try
        {
            return Dynamic_Func_GetData_ByIDPost_PageCategory(db, idPost).Select(x => new
            {
                x.IDPageCategory,
                x.IDPost,
                x.TBPageCategory.Name
            }).AsEnumerable().ToArray();
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

    private Func<DataClassesDataContext, int, IEnumerable<TBPageCategory_Post>> Dynamic_Func_GetData_ByIDPost_PageCategory
    {
        get
        {
            Func<DataClassesDataContext, int, IEnumerable<TBPageCategory_Post>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, IEnumerable<TBPageCategory_Post>>
              ((DataClassesDataContext context, int idPost) => context.TBPageCategory_Posts.Where(x => x.IDPost == idPost)
                  .AsEnumerable().ToArray());
            return func;
        }
    }

    private Func<DataClassesDataContext, int, dynamic> Dynamic_Func_GetDetail_Information
    {
        get
        {
            Func<DataClassesDataContext, int, dynamic> func =
              CompiledQuery.Compile<DataClassesDataContext, int, dynamic>
              ((DataClassesDataContext context, int idpost) => context.FUNC_Post_GetDetail_Information(idpost)
                  .AsEnumerable().FirstOrDefault());
            return func;
        }
    }

    private Func<DataClassesDataContext, int, IEnumerable<TBPostMedia>> Func_GetData_ByIDPost_Photo
    {
        get
        {
            Func<DataClassesDataContext, int, IEnumerable<TBPostMedia>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, IEnumerable<TBPostMedia>>
              ((DataClassesDataContext context, int idPost) => context.TBPostMedias.AsEnumerable()
                .Where(x => x.IDPost == idPost).ToArray());
            return func;
        }
    }

    private Func<DataClassesDataContext, int, TBPostMedia> Func_GetDetail_Photo
    {
        get
        {
            Func<DataClassesDataContext, int, TBPostMedia> func =
              CompiledQuery.Compile<DataClassesDataContext, int, TBPostMedia>
              ((DataClassesDataContext context, int idPostMedia) => context.TBPostMedias.AsEnumerable()
                .Where(x => x.IDPostMedia == idPostMedia).FirstOrDefault());
            return func;
        }
    }

    private Func<DataClassesDataContext, int, IEnumerable<dynamic>> Dynamic_Func_GetData_ByIDPost_Photo
    {
        get
        {
            Func<DataClassesDataContext, int, IEnumerable<dynamic>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, IEnumerable<dynamic>>
              ((DataClassesDataContext context, int idPost) => context.FUNC_Post_Photo_GetData_ByIDPost(idPost)
                  .AsEnumerable().OrderByDescending(x => x.IDPostMedia).ToArray());
            return func;
        }
    }

    private Func<DataClassesDataContext, int, IEnumerable<dynamic>> Dynamic_Func_GetDetail_Photo
    {
        get
        {
            Func<DataClassesDataContext, int, IEnumerable<dynamic>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, IEnumerable<dynamic>>
              ((DataClassesDataContext context, int idPostMedia) => context.FUNC_Post_Photo_GetDetail(idPostMedia)
                  .AsEnumerable().ToArray());
            return func;
        }
    }

    private Func<DataClassesDataContext, int, IEnumerable<TBPageCategory_Post>> Func_GetData_ByIDPost_PageCategory
    {
        get
        {
            Func<DataClassesDataContext, int, IEnumerable<TBPageCategory_Post>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, IEnumerable<TBPageCategory_Post>>
              ((DataClassesDataContext context, int idPost) => context.TBPageCategory_Posts.AsEnumerable()
                .Where(x => !x.TBPost.Deflag && x.IDPost == idPost).ToArray());
            return func;
        }
    }

    public bool ValidationName_Insert(DataClassesDataContext db, string name)
    {
        try
        {
            if (Dynamic_GetAll(db).AsEnumerable().Where(x => x.Post_Title.ToLower() == name).FirstOrDefault() == null)
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
    public bool ValidationName_Update(DataClassesDataContext db, int idPost, string name)
    {
        try
        {
            if (Dynamic_GetAll(db).AsEnumerable().Where(x => x.Post_Title.ToLower() == name && x.IDPost != idPost).FirstOrDefault() == null)
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

    public IEnumerable<TBPageCategory_Post> GetData_PageCategory_ByIDPost(DataClassesDataContext db, int idPost)
    {
        try
        {
            return Func_GetData_ByIDPost_PageCategory(db, idPost).ToArray();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public IEnumerable<TBPageCategory> GetDataBy_IDPage(DataClassesDataContext db, int idPage)
    {
        try
        {
            if (idPage != 0)
                return db.TBPageCategories.Where(x => x.IDPage == idPage && !x.Deflag).AsEnumerable();
            else
                return null;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    #endregion

    #region TREE
    public IEnumerable<dynamic> GetTree_PageCategory(DataClassesDataContext db, int idPage, int idPost)
    {
        Class_Page_Category _pageCategory = new Class_Page_Category();
        return GenerateUL(db, _pageCategory, GetDataBy_IDPage(db, idPage), idPost);

    }

    private IEnumerable<WITLibrary.JsTreeModel> GenerateUL(DataClassesDataContext db, Class_Page_Category _pageCategory, IEnumerable<TBPageCategory> menus, int idPost)
    {
        List<WITLibrary.JsTreeModel> _newCategory = new List<WITLibrary.JsTreeModel>();
        foreach (var menu in menus)
        {
            if (menu.TBPageCategory_Posts.AsEnumerable().Any())
            {
                _newCategory.Add(new WITLibrary.JsTreeModel
                {
                    text = menu.Name,
                    icon = menu.Deflag ? "fa fa-tags icon-state-danger" : "fa fa-tags icon-state-success",
                    state = new WITLibrary.JsTreeState { selected = menu.TBPageCategory_Posts.AsEnumerable().FirstOrDefault(x => x.IDPost == idPost) != null },
                    li_attr = new WITLibrary.JsTreeAttr { id = menu.IDPageCategory.ToString() }
                });
            }
            else
            {
                _newCategory.Add(new WITLibrary.JsTreeModel
                {
                    text = menu.Name,
                    icon = menu.Deflag ? "fa fa-tags icon-state-danger" : "fa fa-tags icon-state-success",
                    li_attr = new WITLibrary.JsTreeAttr { id = menu.IDPageCategory.ToString() },
                    state = new WITLibrary.JsTreeState { selected = menu.TBPageCategory_Posts.AsEnumerable().FirstOrDefault(x => x.IDPost == idPost) != null }
                });
            }
        }
        return _newCategory.ToArray();
    }

    public ReturnData AJAX_Update_Categories(int idPost, int idPage, int[] categories)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            if (categories.Count() == 0)
                return ReturnData.MessageFailed("No selected category to insert.", null);

            dynamic _post = Dynamic_GetDetail_Information(db, idPost);
            if (_post == null)
                return ReturnData.MessageFailed("The requested resource does not exist.", null);

            TBPageCategory_Post[] _oldCategories = GetData_PageCategory_ByIDPost(db, _post.IDPost);
            List<TBPageCategory> _pageCategories = new List<TBPageCategory>();
            Class_Page_Category _pageCategory = new Class_Page_Category();
            foreach (int item in categories)
            {
                if (_oldCategories.AsEnumerable().Where(x => x.IDPageCategory == item).FirstOrDefault() == null)
                {
                    TBPageCategory _newData = _pageCategory.GetDetail(db, item);
                    if (_pageCategories == null)
                        return ReturnData.MessageFailed("Some category does not exists.", null);
                    _pageCategories.Add(_newData);
                }
            }
            foreach (TBPageCategory_Post item in _oldCategories)
            {
                dynamic _item = categories.Where(x => x == item.IDPageCategory).FirstOrDefault();
                if (_item == 0)
                {
                    db.TBPageCategory_Posts.DeleteOnSubmit(item);
                }
            }
            db.SubmitChanges();

            foreach (dynamic item in _pageCategories)
            {
                TBPageCategory_Post _newData = GetData_PageCategory_ByIDPost(db, idPost).Where(x => x.IDPageCategory == item.IDPageCategory).FirstOrDefault();
                if (_newData == null)
                {
                    _newData = new TBPageCategory_Post
                    {
                        IDPost = idPost,
                        IDPageCategory = item.IDPageCategory,
                    };

                    db.TBPageCategory_Posts.InsertOnSubmit(_newData);
                }
            }
            db.SubmitChanges();

            Dictionary<string, dynamic> _result = new Dictionary<string, dynamic>();
            _result.Add("SelectedCategories", Dynamic_GetData_PageCategories_ByIDPost(db, idPost));
            _result.Add("TreeCategories", GetTree_PageCategory(db, idPage, idPost));
            return ReturnData.MessageSuccess(categories.Count() + " categories has been successfully updated.", _result);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    #endregion

    #region FRONTEND

    public IEnumerable<TBPostMedia> FE_SinglePost_MultiplePhoto_SingleCategory(int idPageCategory, int idPage)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var Post = db.TBPageCategory_Posts.Where(x => x.IDPageCategory == idPageCategory && x.TBPost.IDPage == idPage && !x.TBPost.Deflag).OrderByDescending(x => x.IDPost).FirstOrDefault();

            var Media = db.TBPostMedias.Where(x => x.IDPost == Post.IDPost).AsEnumerable();

            if (Post != null)
            {
                return Media;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public IEnumerable<dynamic> FE_MultiplePost_SinglePhoto_SingleCategory(int idPageCategory, int idPage)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var Post = db.TBPageCategory_Posts.Where(x => x.IDPageCategory == idPageCategory && x.TBPost.IDPage == idPage && !x.TBPost.Deflag).Select(x => new
            {
                IDPost = x.IDPost,
                Photo = x.TBPost.TBPostMedias.FirstOrDefault().MediaUrl,
                Title = x.TBPost.Post_Title,
                PageTitle = x.TBPost.TBPage.Page_Title,
                Description = x.TBPost.Post_Content,
                ShortDescription = x.TBPost.Post_ShortContent,
                Category = x.TBPageCategory.Name
            }).OrderByDescending(x => x.IDPost);

            if (Post != null)
            {
                return Post;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public IEnumerable<dynamic> FE_MultiplePost_SinglePhoto_MultipleCategory(int idPage)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var Post = db.TBPageCategory_Posts.Where(x => x.TBPost.IDPage == idPage && !x.TBPost.Deflag).Select(x => new
            {
                IDPost = x.IDPost,
                Photo = x.TBPost.TBPostMedias.FirstOrDefault().MediaUrl,
                Title = x.TBPost.Post_Title,
                PageTitle = x.TBPost.TBPage.Page_Title,
                Description = x.TBPost.Post_Content,
                ShortDescription = x.TBPost.Post_ShortContent,
                Category = x.TBPageCategory.Name,
                Date = x.TBPost.DateInsert,
            }).OrderByDescending(x => x.IDPost);

            if (Post != null)
            {
                return Post;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public IEnumerable<TBPostMedia> FE_DetailPost_AllPhoto(int idPost)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var Post = db.TBPageCategory_Posts.Where(x => x.IDPost == idPost && !x.TBPost.Deflag).OrderByDescending(x => x.IDPost).FirstOrDefault();

            var Media = db.TBPostMedias.Where(x => x.IDPost == Post.IDPost).AsEnumerable();

            if (Post != null)
            {
                return Media;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public TBPost FE_DetailPost(int idPost)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var Post = db.TBPosts.Where(x => x.IDPost == idPost && !x.Deflag).OrderByDescending(x => x.IDPost).FirstOrDefault();

            if (Post != null)
            {
                return Post;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public IEnumerable<dynamic> FE_GetAll_PageCategory(int idPage)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var PageCategory = db.TBPageCategories.Where(x => x.IDPage == idPage && !x.Deflag).OrderByDescending(x => x.IDPageCategory).AsEnumerable().Select(x => new
            {
                IDPageCategory = x.IDPageCategory,
                IDPage = x.IDPage,
                Name = x.Name,
                Description = x.Description
            });

            if (PageCategory != null)
            {
                return PageCategory;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public TBPost FE_SinglePostOnly_SingleCategory(int idPageCategory, int idPage)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var Post = db.TBPageCategory_Posts.Where(x => x.TBPost.IDPage == idPage && !x.TBPost.Deflag && x.IDPageCategory == idPageCategory).OrderByDescending(x => x.IDPost).FirstOrDefault().TBPost;

            if (Post != null)
            {
                return Post;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    #endregion
}
