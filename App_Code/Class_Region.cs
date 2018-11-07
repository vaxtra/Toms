using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WITLibrary;

/// <summary>
/// Summary description for Class_Region
/// </summary>
public class Class_Region
{
    public Class_Region()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region AJAX
    public Datatable AJAX_GetTable_Country(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            IEnumerable<dynamic> data = db.TBCountries.Where(X => !X.Deflag).Select(x => new
            {
                x.IDCountry,
                x.Name
            }).ToList();
            int count = data.Count();
            if (!string.IsNullOrEmpty(search))
                data = data.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToArray();
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (dynamic currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("IDCountry", currData.IDCountry);
                newData.Add("Name", currData.Name);
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

    public Datatable AJAX_GetTable_Province(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search, string idCountry)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            IEnumerable<dynamic> data = db.TBProvinces.Where(X => !X.Deflag && X.IDCountry.ToString() == idCountry).Select(x => new
            {
                x.IDProvince,
                x.Name
            }).ToList();
            int count = data.Count();
            if (!string.IsNullOrEmpty(search))
                data = data.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToArray();
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (dynamic currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("IDProvince", currData.IDProvince);
                newData.Add("Name", currData.Name);
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

    public Datatable AJAX_GetTable_City(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search, string idProvince)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            IEnumerable<dynamic> data = db.TBCities.Where(X => !X.Deflag && X.IDProvince.ToString() == idProvince).Select(x => new
            {
                x.IDCity,
                x.Name
            }).ToList();
            int count = data.Count();
            if (!string.IsNullOrEmpty(search))
                data = data.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToArray();
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (dynamic currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("IDCity", currData.IDCity);
                newData.Add("Name", currData.Name);
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

    public Datatable AJAX_GetTable_District(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search, string idCity)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            IEnumerable<dynamic> data = db.TBDistricts.Where(X => !X.Deflag && X.IDCity.ToString() == idCity).Select(x => new
            {
                x.IDDistrict,
                x.Name
            }).ToList();
            int count = data.Count();
            if (!string.IsNullOrEmpty(search))
                data = data.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToArray();
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (dynamic currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("IDDistrict", currData.IDDistrict);
                newData.Add("Name", currData.Name);
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

    public ReturnData AJAX_FE_PreloadRegistration()
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
                result.Add("Country", DYNAMIC_GetCountry());
                result.Add("Province", DYNAMIC_GetProvince_ByIDCountry(DYNAMIC_GetCountry().FirstOrDefault().IDCountry));
                return ReturnData.MessageSuccess("OK", result);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public ReturnData AJAX_FE_GetCity_ByIDProvince(int idProvince)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            {
                var result = db.TBCities.Where(x => !x.Deflag && x.IDProvince == idProvince).Select(x => new
                {
                    x.IDCity,
                    x.Name
                });
                return ReturnData.MessageSuccess("OK", result);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public ReturnData AJAX_FE_GetDistrict_ByIDCity(int idCity)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            {
                var result = db.TBDistricts.Where(x => !x.Deflag && x.IDCity == idCity).Select(x => new
                {
                    x.IDDistrict,
                    x.Name
                });
                return ReturnData.MessageSuccess("OK", result);
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

    public dynamic[] DYNAMIC_GetCountry()
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                return db.TBCountries.Where(x => !x.Deflag).Select(x => new { x.IDCountry, x.Name }).ToArray();
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic[] DYNAMIC_GetProvince_ByIDCountry(int idCountry)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                return db.TBProvinces.Where(x => !x.Deflag && x.IDCountry == idCountry).Select(x => new
                {
                    x.IDProvince,
                    x.Name
                }).ToArray();
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic[] DYNAMIC_GetCity_ByIDProvince(int idProvince)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                return db.TBCities.Where(x => !x.Deflag && x.IDProvince == idProvince).Select(x => new { x.IDCity, x.Name }).ToArray();
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic[] DYNAMIC_GetDistrict_ByIDCity(int idCity)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                return db.TBDistricts.Where(x => x.IDCity == idCity).Select(x => new { x.IDDistrict, x.Name }).ToArray();
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
}