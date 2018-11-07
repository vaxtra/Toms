using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using WITLibrary;

/// <summary>
/// Summary description for Class_Product_Combination
/// </summary>
public class Class_Product_Combination
{
    public Class_Product_Combination()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Datatable AJAX_GetTable_Monitoring(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            IEnumerable<dynamic> data = Dynamic_GetData_MonitoringStock();
            int count = data.Count();
            if (!string.IsNullOrEmpty(search))
                data = data.Where(x => x.CombinationName.ToLower().Contains(search.ToLower()) || 
                x.ProductName.ToLower().Contains(search.ToLower())
                ).ToArray();
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (dynamic currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("IDProductCombination", currData.IDProduct_Combination);
                newData.Add("ProductName", currData.ProductName);
                newData.Add("CombinationName", currData.CombinationName);
                newData.Add("Quantity", currData.Quantity);
                newData.Add("ReferenceCode", currData.ReferenceCode);
                newData.Add("Active", currData.Active);
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

    public Datatable AJAX_GetTable_AvailableQuantity(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            IEnumerable<dynamic> data = Dynamic_GetData_AvailableQuantity();
            int count = data.Count();
            if (!string.IsNullOrEmpty(search))
                data = data.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToArray();
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (dynamic currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("ProductName", currData.ProductName);
                newData.Add("CombinationName", currData.CombinationName);
                newData.Add("Quantity", currData.Quantity);
                newData.Add("ReferenceCode", currData.ReferenceCode);
                newData.Add("Active", currData.Active);
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

    public dynamic Dynamic_GetData_ByIDProductCombination(dynamic ids)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            List<dynamic> result = new List<dynamic>();
            foreach (var item in ids)
            {
                int id = (int)item;
                dynamic data = db.TBOrder_Details.Where(x => x.IDOrder_Detail == id).Select(x => new
                {
                    ProductName = x.TBProduct.Name,
                    CombinationName = x.TBProduct_Combination.Name,
                    Photo = OurClass.ImageExists(x.TBProduct.TBProduct_Photos.FirstOrDefault(y => y.IsCover).Photo, "product") ? x.TBProduct.TBProduct_Photos.FirstOrDefault(y => y.IsCover).Photo : "noimage.jpg",
                    x.Discount,
                    x.Price,
                    x.Quantity,
                    x.Weight,
                    x.OriginalPrice,
                    TotalDiscount = x.Discount * x.Quantity,
                    TotalPrice = x.OriginalPrice * x.Quantity,
                    Subtotal = (x.OriginalPrice * x.Quantity) - (x.Discount * x.Quantity),
                    x.TBProduct_Combination.ReferenceCode,
                }).FirstOrDefault();
                if (data != null)
                    result.Add(data);
            }
            return result;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_GetData_MonitoringStock()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBProduct_Combinations.Where(x => !x.Deflag).Select(x => new
            {
                x.IDProduct_Combination,
                ProductName = x.TBProduct.Name,
                CombinationName = x.Name,
                x.Discount,
                x.Price,
                x.Quantity,
                x.Weight,
                x.ReferenceCode,
                Active = x.TBProduct.Active
            });
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_GetData_AvailableQuantity()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBProduct_Combinations.Where(x => !x.Deflag && x.Quantity > 0).Select(x => new
            {
                x.IDProduct_Combination,
                ProductName = x.TBProduct.Name,
                CombinationName = x.Name,
                x.Discount,
                x.Price,
                x.Quantity,
                x.Weight,
                x.ReferenceCode,
                Active = x.TBProduct.Active
            });
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public IEnumerable<dynamic> Dynamic_GetData_Photo_ByIDProductCombination(int idProduct)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return Dynamic_Func_GetData_ByIDProductCombination_Photo(db, idProduct).AsEnumerable().Select(x => new
            {
                x.IDProduct_Combination_Photo,
                x.IDProduct_Combination,
                x.IDProduct_Photo,
                x.Photo,
                Preview = OurClass.ImageExists(x.Photo, "product") ? x.Photo : "noimage.jpg",
                x.IsCover
            }).ToArray();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    private Func<DataClassesDataContext, int, IEnumerable<dynamic>> Dynamic_Func_GetData_ByIDProductCombination_Photo
    {
        get
        {
            Func<DataClassesDataContext, int, IEnumerable<dynamic>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, IEnumerable<dynamic>>
              ((DataClassesDataContext context, int idProductCombination) => context.FUNC_Product_Combination_Photo_GetData_ByIDProduct_Combination(idProductCombination)
                  .AsEnumerable().OrderByDescending(x => x.IDProduct_Combination_Photo).ToArray());
            return func;
        }
    }
}