using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.Services;
using JWT;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using WITLibrary;
using System.Web.Configuration;
/// <summary>
/// Summary description for Class_Address
/// </summary>
public class Class_Address
{
    public Class_Address()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //public Datatable AJAX_GetTable_CustomerAddress(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    //{
    //    try
    //    {
    //        IEnumerable<dynamic> data = Dynamic_GetAll_ByEmail();
    //        int count = data.Count();
    //        if (!string.IsNullOrEmpty(search))
    //            data = data.Where(x =>
    //                x.FirstName.ToLower().Contains(search.ToLower()) ||
    //        x.LastName.ToLower().Contains(search.ToLower()) ||
    //                x.Email.ToLower().Contains(search.ToLower())
    //                ).ToArray();
    //        List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
    //        foreach (dynamic currData in data)
    //        {
    //            Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
    //            newData.Add("IDCustomer", currData.IDCustomer);
    //            newData.Add("Name", currData.FirstName + ' ' + currData.LastName);
    //            newData.Add("Email", currData.Email);
    //            newData.Add("Active", currData.Active);
    //            newData.Add("PhoneNumber", currData.PhoneNumber);
    //            resultList.Add(newData);
    //        }
    //        return OurClass.ParseTable(resultList, count, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}

    public ReturnData AJAX_Insert(int idCustomer, int idCountry, int idProvince, int idCity, int idDistrict, string peoplename, string name, string address, string phone, string postalCode, string additionalInformation)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBAddress _newAddress = new TBAddress
            {
                IDCustomer = idCustomer,
                IDCountry = idCountry,
                IDProvince = idProvince,
                IDCity = idCity,
                IDDistrict = idDistrict,
                PeopleName = peoplename,
                Name = name,
                Address = address,
                Phone = phone,
                PostalCode = postalCode,
                AdditionalInformation = additionalInformation,
                Active = true,
                Deflag = false,
                DateInsert = DateTime.Now,
                DateLastUpdate = DateTime.Now
            };
            db.TBAddresses.InsertOnSubmit(_newAddress);
            db.SubmitChanges();
            return ReturnData.MessageSuccess("Address submit successfully", _newAddress.IDAddress);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_Delete(int idAddress, int idCustomer)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBAddress selectedData = db.TBAddresses.Where(x => !x.Deflag && x.IDCustomer == idCustomer && x.IDAddress == idAddress).FirstOrDefault();
            if (selectedData == null)
                return ReturnData.MessageFailed("Data not found", null);

            selectedData.Deflag = true;
            db.SubmitChanges();
            return ReturnData.MessageSuccess("Address has been deleted successfully", null);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_Update(int idAddress, int idCustomer, int idCountry, int idProvince, int idCity, int idDistrict, string peopleName, string name, string address, string phone, string postalCode, string additionalInformation, bool active)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBAddress data = db.TBAddresses.Where(x => !x.Deflag && x.IDCustomer == idCustomer && x.IDAddress == idAddress).FirstOrDefault();
            if (data == null)
                return ReturnData.MessageFailed("Data not found", null);
            data.IDCountry = idCountry;
            data.IDProvince = idProvince;
            data.IDCity = idCity;
            data.IDDistrict = idDistrict;
            data.Name = name;
            data.Address = address;
            data.Phone = phone;
            data.PeopleName = peopleName;
            data.PostalCode = postalCode;
            data.AdditionalInformation = additionalInformation;
            data.Active = active;
            data.DateLastUpdate = DateTime.Now;

            db.SubmitChanges();
            return ReturnData.MessageSuccess("Address has been updated successfully", null);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_GetDetail(int idAddress, int idCustomer)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBAddress selected = db.TBAddresses.Where(x => !x.Deflag && x.IDAddress == idAddress && x.IDCustomer == idCustomer).FirstOrDefault();
            if (selected == null)
                return ReturnData.MessageFailed("Data not found", null);
            return ReturnData.MessageSuccess("OK", Dynamic_GetDetail_ByIDAddress(idAddress));
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public dynamic Dynamic_GetAll_ByIDCustomer(int idCustomer)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBAddresses.Where(x => !x.Deflag && x.IDCustomer == idCustomer && !x.TBCustomer.Deflag).Select(x => new
            {
                x.IDCustomer,
                x.IDAddress,
                x.Address,
                x.IDCity,
                x.IDCountry,
                x.IDDistrict,
                x.IDProvince,
                x.Name,
                x.Phone,
                x.PostalCode,
                x.PeopleName,
                CityName = x.TBCity.Name,
                DistrictName = x.TBDistrict.Name,
                ProvinceName = x.TBProvince.Name,
                CountryName = x.TBCountry.Name
            });
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_GetAll_ByEmail(string email)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBAddresses.Where(x => !x.Deflag && x.TBCustomer.Email == email && !x.TBCustomer.Deflag).Select(x => new
            {
                x.IDCustomer,
                x.IDAddress,
                x.Address,
                x.IDCity,
                x.IDCountry,
                x.IDDistrict,
                x.IDProvince,
                x.Name,
                x.Phone,
                x.PostalCode,
                x.PeopleName,
                x.AdditionalInformation,
                CityName = x.TBCity.Name,
                DistrictName = x.TBDistrict.Name,
                ProvinceName = x.TBProvince.Name,
                CountryName = x.TBCountry.Name
            });
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_GetDetail_ByIDAddress(int idAddress)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBAddresses.Where(x => !x.Deflag && x.IDAddress == idAddress && !x.TBCustomer.Deflag).Select(x => new
            {
                x.IDCustomer,
                x.IDAddress,
                x.Address,
                x.IDCity,
                x.IDCountry,
                x.IDDistrict,
                x.IDProvince,
                x.Name,
                x.Phone,
                x.PostalCode,
                x.PeopleName,
                x.AdditionalInformation,
                CityName = x.TBCity.Name,
                DistrictName = x.TBDistrict.Name,
                ProvinceName = x.TBProvince.Name,
                CountryName = x.TBCountry.Name
            }).FirstOrDefault();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_GetDetail_ByIDAddress_OrderDetail(int idAddress)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBAddresses.Where(x => x.IDAddress == idAddress && !x.TBCustomer.Deflag).Select(x => new
            {
                x.IDCustomer,
                x.IDAddress,
                x.Address,
                x.IDCity,
                x.IDCountry,
                x.IDDistrict,
                x.IDProvince,
                x.Name,
                x.Phone,
                x.PostalCode,
                x.PeopleName,
                x.AdditionalInformation,
                CityName = x.TBCity.Name,
                DistrictName = x.TBDistrict.Name,
                ProvinceName = x.TBProvince.Name,
                CountryName = x.TBCountry.Name
            }).FirstOrDefault();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
}