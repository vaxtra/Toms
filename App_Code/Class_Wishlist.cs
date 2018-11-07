using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using WITLibrary;

/// <summary>
/// Summary description for Class_Wishlist
/// </summary>
public class Class_Wishlist
{
    #region AJAX

    public Datatable AJAX_GetTable(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            IEnumerable<dynamic> data = Dynamic_GetAll(new DataClassesDataContext());
            int count = data.Count();
            if (!string.IsNullOrEmpty(search))
                data = data.Where(x => x.CustomerName.ToLower().Contains(search.ToLower())
                                       || x.ProductName.ToLower().Contains(search.ToLower())
                                       || x.Email.ToLower().Contains(search.ToLower())
                                       ).ToArray();
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (dynamic currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("IDWishlist", currData.IDWishlist);
                newData.Add("CustomerName", currData.CustomerName == null ? "-" : currData.CustomerName);
                newData.Add("Email", currData.Email);
                newData.Add("ProductName", currData.Product);
                newData.Add("DateInsert", currData.DateInsert);
                newData.Add("Member", currData.IDCustomer == null ? false : true);
                resultList.Add(newData);
            }
            return OurClass.ParseTable(resultList, count, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
        }
        catch (Exception)
        {
            return new WITLibrary.Datatable
            {
                sEcho = 0,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0,
                aaData = new List<Dictionary<string, dynamic>>()
            };
        }
    }

    public ReturnData AJAX_Insert(int? idCustomer, int idCombination, string Email)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            if (IsExistsWishlist(Email, idCombination))
            {
                return ReturnData.MessageFailed("You have already added this combination to your wishlist", null);
            }
            TBWishlist _newWishlist = new TBWishlist()
            {
                IDCustomer = idCustomer,
                IDProduct_Combination = idCombination,
                Email = Email,
                DateInsert = DateTime.Now,
                DateLastUpdate = DateTime.Now
            };
            db.TBWishlists.InsertOnSubmit(_newWishlist);
            db.SubmitChanges();
            return ReturnData.MessageSuccess("Wishlist submit successfully", null);
        }
        catch (Exception ex)
        {
            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_Delete(int idWishlist)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var wishlist = db.TBWishlists.Where(x => x.IDWishlist == idWishlist).FirstOrDefault();
            if (wishlist == null)
            {
                return ReturnData.MessageFailed("Data not Found", null);
            }
            else
            {
                db.TBWishlists.DeleteOnSubmit(wishlist);
                db.SubmitChanges();

                return ReturnData.MessageSuccess("Wishlist removed successfully", null);
            }

            
        }
        catch (Exception ex)
        {
            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    #endregion

    #region DYNAMIC

    public dynamic Dynamic_GetAll(DataClassesDataContext db)
    {
        try
        {
            return db.TBWishlists.Select(x => new
            {
                x.IDWishlist,
                x.IDCustomer,
                x.IDProduct_Combination,
                x.Email,
                x.DateInsert,
                x.DateLastUpdate,
                CustomerName = x.TBCustomer.FirstName + " " + x.TBCustomer.LastName,
                Product = x.TBProduct_Combination.TBProduct.Name + " - " + x.TBProduct_Combination.Name,
                x.TBProduct_Combination.IDProduct
            });
        }
        catch (Exception)
        {

            throw;
        }
    }

    public dynamic Dynamic_GetData_ByIDCustomer(int idCustomer)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBWishlists.Where(x => x.IDCustomer == idCustomer).Select(x => new
            {
                x.IDWishlist,
                x.IDProduct_Combination,
                x.IDCustomer,
                x.Email,
                Photo = x.TBProduct_Combination.TBProduct.TBProduct_Photos.FirstOrDefault(y => y.IsCover).Photo,
                Date = x.DateInsert.ToShortDateString(),
                x.DateLastUpdate,
                ProductName = x.TBProduct_Combination.TBProduct.Name,
                CombinationName = x.TBProduct_Combination.Name
            });
        }
        catch (Exception)
        {
            return null;
        }
    }

    public dynamic Dynamic_GetData_ByEmail(string email)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBWishlists.Where(x => x.Email.ToLower() == email.ToLower()).Select(x => new
            {
                x.IDWishlist,
                x.IDProduct_Combination,
                x.IDCustomer,
                x.Email,
                Photo = x.TBProduct_Combination.TBProduct.TBProduct_Photos.FirstOrDefault(y => y.IsCover).Photo,
                Date = x.DateInsert.ToShortDateString(),
                x.DateLastUpdate,
                ProductName = x.TBProduct_Combination.TBProduct.Name,
                CombinationName = x.TBProduct_Combination.Name
            });
        }
        catch (Exception)
        {
            return null;
        }
    }
    #endregion

    #region LINQ
    private bool IsExistsWishlist(string email, int idCombination)
    {
        DataClassesDataContext db = new DataClassesDataContext();
        if (db.TBWishlists.Where(x => x.Email.ToLower() == email.ToLower() && x.IDProduct_Combination == idCombination).FirstOrDefault() != null)
            return true;
        else
            return false;
    }
    #endregion
}