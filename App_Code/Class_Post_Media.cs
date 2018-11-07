using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class_Post_Media
/// </summary>
public class Class_Post_Media
{
    public Class_Post_Media()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region AJAX

    public ReturnData AJAX_Updates(int idPostMedia, string title, string description)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBPostMedia postMedia = db.TBPostMedias.Where(x => !x.Deflag && x.IDPostMedia == idPostMedia).FirstOrDefault();
            if (postMedia == null)
                return ReturnData.MessageFailed("Page Media not found", null);
            postMedia.Title = title;
            postMedia.Description = description;
            postMedia.DateLastUpdate = DateTime.Now;
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

    #endregion

    #region DYNAMIC

    public IEnumerable<dynamic> Dynamic_GetData_PageMedia(DataClassesDataContext db, int idPost)
    {
        try
        {
            return db.TBPostMedias.Where(x => x.IDPost == idPost && !x.Deflag).AsEnumerable().Select(x => new
            {
                x.IDPostMedia,
                x.IDPost,
                x.Title,
                Preview = OurClass.ImageExists(x.MediaUrl, "post") ? x.MediaUrl : "noimage.jpg",
                x.Active,
                x.Description
            }).ToArray();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public IEnumerable<dynamic> Dynamic_GetDataDetail_PostMedia(DataClassesDataContext db, int idPostMedia)
    {
        try
        {
            return db.TBPostMedias.Where(x => x.IDPostMedia == idPostMedia && !x.Deflag).AsEnumerable().Select(x => new
            {
                x.IDPostMedia,
                x.Title,
                x.Description
            }).ToArray();
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
    #endregion
}