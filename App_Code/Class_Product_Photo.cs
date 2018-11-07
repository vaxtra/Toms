using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class_Product_Photo
/// </summary>
public class Class_Product_Photo
{
    public Class_Product_Photo()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public TBProduct_Photo GetCover(DataClassesDataContext db, int idProduct)
    {
        try
        {
            return db.TBProduct_Photos.Where(x => x.IDProduct == idProduct && x.IsCover).FirstOrDefault();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            throw;
        }
    }

    public dynamic GetPhotosByIDProduct(DataClassesDataContext db, int idProduct)
    {
        try
        {
            return db.TBProduct_Photos.Where(x => x.IDProduct == idProduct).Select(x => new { x.IDProduct, x.IDProduct_Photo, x.Photo, x.IsCover });
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
}