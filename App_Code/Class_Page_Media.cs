using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class_Page_Media
/// </summary>
public class Class_Page_Media
{
	public Class_Page_Media()
	{
		//
		// TODO: Add constructor logic here
		//
    }

    #region AJAX
    public ReturnData AJAX_Updates(int idPageMedia, string title, string description)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBPageMedia pageMedia = db.TBPageMedias.Where(x => !x.Deflag && x.IDPageMedia == idPageMedia).FirstOrDefault();
            if (pageMedia == null)
                return ReturnData.MessageFailed("Page Media not found", null);
            pageMedia.Title = title;
            pageMedia.Description = description;
            pageMedia.DateLastUpdate = DateTime.Now;
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

    public IEnumerable<dynamic> Dynamic_GetData_PageMedia(DataClassesDataContext db, int idPage)
    {
        try
        {
            return db.TBPageMedias.Where(x => x.IDPage == idPage && !x.Deflag).AsEnumerable().Select(x => new
            {
                x.IDPageMedia,
                x.IDPage,
                x.Title,
                Preview = OurClass.ImageExists(x.MediaUrl, "page") ? x.MediaUrl : "noimage.jpg",
                x.Cover,
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

    public IEnumerable<dynamic> Dynamic_GetDataDetail_PageMedia(DataClassesDataContext db, int idPageMedia)
    {
        try
        {
            return db.TBPageMedias.Where(x => x.IDPageMedia == idPageMedia && !x.Deflag).AsEnumerable().Select(x => new
            {
                x.IDPageMedia,
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