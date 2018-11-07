using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WITLibrary;

/// <summary>
/// Summary description for Class_Carrier
/// </summary>
public class Class_Carrier
{
    public Class_Carrier()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public bool ValidationName_Insert(DataClassesDataContext db, string name)
    {
        try
        {
            if (db.TBCarriers.AsEnumerable().Where(x => x.Name.ToLower() == name).FirstOrDefault() == null)
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

    #region AJAX
    public Datatable AJAX_GetTable(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            IEnumerable<dynamic> data = db.TBCarriers.Where(X => !X.Deflag).ToList();
            int count = data.Count();
            if (!string.IsNullOrEmpty(search))
                data = data.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToArray();
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (dynamic currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("IDCarrier", currData.IDCarrier);
                newData.Add("Name", currData.Name);
                newData.Add("Image", currData.Image);
                newData.Add("Active", currData.Active);
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
    public ReturnData AJAX_Insert(string baseImage, string information, string fnImage, string name)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                if (!ValidationName_Insert(db, name))
                    return ReturnData.MessageFailed(name + " already exists.", null);

                TBCarrier _newData = new TBCarrier
                {
                    Image = WITLibrary.ConvertCustom.GetExtention(fnImage),
                    Name = name,
                    Information = information,
                    Deflag = false,
                    DateInsert = DateTime.Now,
                    DateLastUpdate = DateTime.Now
                };

                db.TBCarriers.InsertOnSubmit(_newData);
                db.SubmitChanges();

                if (_newData != null)
                {
                    if (baseImage != "" && fnImage != "")
                    {
                        System.Drawing.Image _image = WITLibrary.ConvertCustom.Base64ToImage(baseImage);
                        _image.Save(HttpContext.Current.Server.MapPath("/assets/images/carrier/" + _newData.IDCarrier + _newData.Image));
                        _newData.Image = _newData.IDCarrier + _newData.Image;
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
    public ReturnData AJAX_GetDetail(int idCarrier)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            dynamic _carrier = db.TBCarriers.Where(x => x.IDCarrier == idCarrier && !x.Deflag).Select(x => new { x.IDCarrier, x.Name, x.Information, x.Image }).FirstOrDefault();
            if (_carrier == null)
                return ReturnData.MessageFailed("Data not found", null);
            else
            {
                return ReturnData.MessageSuccess("OK", _carrier);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public ReturnData AJAX_StatusToggle(int idCarrier, bool status)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBCarrier _carrier = db.TBCarriers.Where(x => x.IDCarrier == idCarrier && !x.Deflag).FirstOrDefault();
            if (_carrier == null)
                return ReturnData.MessageFailed("Data not found", null);
            else
            {
                _carrier.Active = status;
                db.SubmitChanges();
                if (status)
                    return ReturnData.MessageSuccess(_carrier.Name + " activated successfully", null);
                else
                    return ReturnData.MessageSuccess(_carrier.Name + " deactivated successfully", null);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public ReturnData AJAX_Update(int idCarrier, string name, string information, string baseImage, string fnImage)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                TBCarrier _carrier = db.TBCarriers.Where(x => x.IDCarrier == idCarrier && !x.Deflag).FirstOrDefault();
                if (_carrier == null)
                    return ReturnData.MessageFailed("Data not found", null);
                if (!ValidationName_Insert(db, name))
                    return ReturnData.MessageFailed(name + " already exists", null);

                _carrier.Name = name;
                _carrier.Information = information;
                _carrier.DateLastUpdate = DateTime.Now;
                if (baseImage != "" && fnImage != "")
                {
                    FileInfo fi = new FileInfo(HttpContext.Current.Server.MapPath("/assets/images/carrier/" + _carrier.Image));
                    if (fi.Exists)
                        fi.Delete();

                    _carrier.Image = _carrier.IDCarrier.ToString() + WITLibrary.ConvertCustom.GetExtention(fnImage);
                    System.Drawing.Image _image = WITLibrary.ConvertCustom.Base64ToImage(baseImage);
                    _image.Save(HttpContext.Current.Server.MapPath("/assets/images/carrier/" + _carrier.Image));
                }

                db.SubmitChanges();
                if (_carrier != null)
                    return ReturnData.MessageSuccess(name + " has been updated successfully.", null);
                return ReturnData.MessageFailed(name + " failed to update.", null);
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
}