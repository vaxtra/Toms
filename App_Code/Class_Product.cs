using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Globalization;
using System.IO;

/// <summary>
/// Summary description for Class_Product
/// </summary>
public class Class_Product
{
    public Class_Product()
    { }

    #region PRODUCT
    #region AJAX
    public dynamic AJAX_GetTable(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            dynamic[] data = Dynamic_GetTable(new DataClassesDataContext()).ToArray();
            int count = data.Count();
            if (!string.IsNullOrEmpty(search))
                data = data.AsEnumerable().Where(x =>
                    x.Name.ToLower().Contains(search.ToLower()) ||
                    x.ReferenceCode.ToLower().Contains(search.ToLower())
                    ).ToArray();
            Dictionary<string, dynamic>[] resultList = new Dictionary<string, dynamic>[data.Count()];
            for (int i = 0; i < data.Count(); i++)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("SequenceNumber", data[i].SequenceNumber);
                newData.Add("Photo", data[i].Photo);
                newData.Add("Name", data[i].Name);
                newData.Add("ReferenceCode", data[i].ReferenceCode);
                newData.Add("PriceBeforeDiscount", WITLibrary.ConvertCustom.FormatMoney(data[i].PriceBeforeDiscount));
                newData.Add("Price", WITLibrary.ConvertCustom.FormatMoney(data[i].Price));
                newData.Add("Quantity", data[i].Quantity);
                newData.Add("Active", data[i].Active);
                newData.Add("IDProduct", data[i].IDProduct);
                resultList[i] = newData;
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

    //public dynamic AJAX_GetTable_combi(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    //{
    //    try
    //    {
    //        DataClassesDataContext db = new DataClassesDataContext();

    //        dynamic[] data = db.FEView_ProductCombinations.GroupBy(x => x.IDProduct).SelectMany(x => x.Take(1)).ToArray();
    //        int count = data.Count();
    //        if (!string.IsNullOrEmpty(search))
    //            data = data.AsEnumerable().Where(x =>
    //                x.Name.ToLower().Contains(search.ToLower())
    //                ).ToArray();
    //        Dictionary<string, dynamic>[] resultList = new Dictionary<string, dynamic>[data.Count()];
    //        for (int i = 0; i < data.Count(); i++)
    //        {
    //            Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
    //            newData.Add("IDProduct_Combination", data[i].IDProduct_Combination);
    //            newData.Add("IDProduct", data[i].IDProduct);
    //            newData.Add("ReferenceCode", data[i].ReferenceCode);
    //            newData.Add("Photo", data[i].Photo);
    //            newData.Add("Name", data[i].Name);
    //            newData.Add("TotalDiscount", WITLibrary.ConvertCustom.FormatMoney(data[i].TotalDiscount));
    //            newData.Add("Price", WITLibrary.ConvertCustom.FormatMoney(data[i].Price));
    //            newData.Add("Quantity", data[i].Quantity);
    //            newData.Add("Weight", data[i].Weight);
    //            resultList[i] = newData;
    //        }
    //        return OurClass.ParseTable(resultList, count, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
    //    }
    //    catch (Exception ex)
    //    {
    //        Class_Log_Error log = new Class_Log_Error();
    //        log.Insert(ex.Message, ex.StackTrace);

    //        return new WITLibrary.Datatable
    //        {
    //            sEcho = 0,
    //            iTotalRecords = 0,
    //            iTotalDisplayRecords = 0,
    //            aaData = new List<Dictionary<string, dynamic>>()
    //        };
    //    }
    //}
    public dynamic AJAX_GetTable_BestSeller(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            dynamic[] data = Dynamic_Get_BestSeller(0);
            int count = data.Count();
            if (!string.IsNullOrEmpty(search))
                data = data.AsEnumerable().Where(x =>
                    x.Name.ToLower().Contains(search.ToLower()) ||
                    x.ReferenceCode.ToLower().Contains(search.ToLower())
                    ).ToArray();
            Dictionary<string, dynamic>[] resultList = new Dictionary<string, dynamic>[data.Count()];
            for (int i = 0; i < data.Count(); i++)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("Photo", data[i].Photo);
                newData.Add("Name", data[i].Name);
                newData.Add("Quantity", data[i].Quantity);
                newData.Add("Total", data[i].Total);
                newData.Add("IDProduct", data[i].IDProduct);
                resultList[i] = newData;
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

    public dynamic Dynamic_Get_BestSeller(int take)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            List<TBProduct> product = new List<TBProduct>();
            dynamic[] data;
            product = db.TBOrder_Details.Where(x => !x.TBOrder.Deflag && x.TBOrder.TBOrder_Status.Paid).GroupBy(x => x.TBProduct).Select(x => x.Key).ToList();
            if (take == 0)
            {
                data = product.Select(x => new
                {
                    x.IDProduct,
                    x.Name,
                    Quantity = x.TBOrder_Details.Sum(p => p.Quantity),
                    Total = x.TBOrder_Details.Sum(p => p.Price),
                    Photo = x.TBProduct_Photos.Where(p => p.IsCover).FirstOrDefault() != null ? (OurClass.ImageExists(x.TBProduct_Photos.Where(p => p.IsCover).FirstOrDefault().Photo, "product") ? x.TBProduct_Photos.Where(p => p.IsCover).FirstOrDefault().Photo : "noimage.jpg") : "noimage.jpg",
                }).OrderByDescending(x => x.Quantity).ToArray();
            }
            else
            {
                data = product.Select(x => new
                {
                    x.IDProduct,
                    x.Name,
                    Quantity = x.TBOrder_Details.Sum(p => p.Quantity),
                    Total = x.TBOrder_Details.Sum(p => p.Price),
                    Photo = x.TBProduct_Photos.Where(p => p.IsCover).FirstOrDefault() != null ? (OurClass.ImageExists(x.TBProduct_Photos.Where(p => p.IsCover).FirstOrDefault().Photo, "product") ? x.TBProduct_Photos.Where(p => p.IsCover).FirstOrDefault().Photo : "noimage.jpg") : "noimage.jpg",
                }).OrderByDescending(x => x.Quantity).Take(take).ToArray();
            }

            return data;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public dynamic Dynamic_Get_RecentOrder(int take)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            List<TBOrder> orders = new List<TBOrder>();
            if (take == 0)
                orders = db.TBOrders.Where(x => !x.Deflag).OrderByDescending(x => x.IDOrder).ToList();
            else
                orders = db.TBOrders.Where(x => !x.Deflag).OrderByDescending(x => x.IDOrder).Take(take).ToList();
            var data = orders.Select(x => new
            {
                x.IDOrder,
                CustomerName = x.TBCustomer.FirstName + " " + x.TBCustomer.LastName,
                Quantity = x.TBOrder_Details.Sum(p => p.Quantity),
                Total = x.TotalPaid
            });
            return data;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public ReturnData AJAX_Preload_Insert()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();

            Class_Manufacturer _manufacturer = new Class_Manufacturer();
            Class_Currency curr = new Class_Currency();
            Dictionary<string, dynamic> _result = new Dictionary<string, dynamic>();
            _result.Add("Manufacturer", _manufacturer.Dynamic_GetData_Active(db));
            _result.Add("Currency", curr.GetAdditionalCurrencies(db));
            return ReturnData.MessageSuccess("OK", _result);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public ReturnData AJAX_Preload_Update(int idProduct)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                Dictionary<string, dynamic> _result = new Dictionary<string, dynamic>();
                dynamic _product = Dynamic_GetDetail_Information(db, idProduct);
                if (_product != null)
                {
                    Class_Attribute _attribute = new Class_Attribute();
                    _result.Add("Product", _product);
                    _result.Add("DefaultCategory", Dynamic_GetDefault_Categories(db, idProduct));
                    _result.Add("SelectedCategories", Dynamic_GetData_Categories_ByIDProduct(db, idProduct));
                    _result.Add("TreeCategories", GetTree_Category(db, idProduct));
                    _result.Add("Photos", Dynamic_GetData_Photo_ByIDProduct(db, idProduct));
                    _result.Add("Attributes", _attribute.Dynamic_GetAll(db));
                    _result.Add("Combinations", GetData_Combination_ByIDProduct(db, idProduct));
                    _result.Add("Currency", Dynamic_GetCurrencyByIDProduct(idProduct));
                    return ReturnData.MessageSuccess("OK", _result);
                }
                return ReturnData.MessageFailed("The requested resource does not exist.", null);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public dynamic Dynamic_GetData_ByIDManufacturer(int idManufacturer)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var data = Dynamic_Func_GetAll(db).AsEnumerable().Where(x => x.IDManufacturer == idManufacturer).OrderByDescending(x => x.SequenceNumber).Select(x => new
            {
                x.IDProduct,
                x.Name,
                x.ReferenceCode,
                Price = GetPrice(db, x.IDProduct)["Price"],
                PriceBeforeDiscount = GetPrice(db, x.IDProduct)["PriceBeforeDiscount"],
                x.Quantity,
                x.Active,
                x.IDManufacturer,
                x.SequenceNumber,
                x.TotalDiscount,
                x.TypeDiscountPercent,
                x.Discount,
                x.Weight,
                x.ShortDescription,
                x.Description,
                x.Note,
                x.Meta,
                x.MetaDescription,
                x.MetaKeyword,
                Photo = OurClass.ImageExists(x.Photo, "product") ? x.Photo : "noimage.jpg",
                x.Manufacturer,
                x.Category
            });

            return data;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_GetData_ByIDCategory(int idCategory, int take)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            if (take == 0)
            {
                var data = Dynamic_Func_GetAll_ByIDCategory(db, idCategory).AsEnumerable().Where(x => x.IDCategory == idCategory).Select(x => new
                {
                    x.IDProduct,
                    x.Name,
                    x.ReferenceCode,
                    Price = GetPrice(db, x.IDProduct)["Price"],
                    PriceBeforeDiscount = GetPrice(db, x.IDProduct)["PriceBeforeDiscount"],
                    x.Quantity,
                    x.Active,
                    x.IDManufacturer,
                    x.SequenceNumber,
                    x.TotalDiscount,
                    x.TypeDiscountPercent,
                    x.Discount,
                    x.Weight,
                    x.ShortDescription,
                    x.Description,
                    x.Note,
                    x.Meta,
                    x.MetaDescription,
                    x.MetaKeyword,
                    Photo = OurClass.ImageExists(x.Photo, "product") ? x.Photo : "noimage.jpg",
                    x.Manufacturer,
                    x.Category
                }).OrderByDescending(x => x.Quantity);

                return data;
            }
            else
            {
                var data = Dynamic_Func_GetAll_ByIDCategory(db, idCategory).AsEnumerable().Where(x => x.IDCategory == idCategory).OrderByDescending(x => x.SequenceNumber).Select(x => new
                {
                    x.IDProduct,
                    x.Name,
                    x.ReferenceCode,
                    Price = GetPrice(db, x.IDProduct)["Price"],
                    PriceBeforeDiscount = GetPrice(db, x.IDProduct)["PriceBeforeDiscount"],
                    x.Quantity,
                    x.Active,
                    x.IDManufacturer,
                    x.SequenceNumber,
                    x.TotalDiscount,
                    x.TypeDiscountPercent,
                    x.Discount,
                    x.Weight,
                    x.ShortDescription,
                    x.Description,
                    x.Note,
                    x.Meta,
                    x.MetaDescription,
                    x.MetaKeyword,
                    Photo = OurClass.ImageExists(x.Photo, "product") ? x.Photo : "noimage.jpg",
                    x.Manufacturer,
                    x.Category
                }).OrderByDescending(x => x.Quantity).Take(take);

                return data;
            }

        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_GetData_FilterCategory(int take, int idCategory)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            if (idCategory == 0)
            {
                if (take == 0)
                {
                    var data = db.TBCategories.Where(x => !x.Deflag && x.IDCategoryParent == null && x.Active == true).Select(x => new
                    {
                        x.Name,
                        x.IDCategory,
                        x.Image,
                        Products = Dynamic_GetData_ByIDCategory(x.IDCategory, 0)
                    });

                    return data;
                }
                else
                {
                    var data = db.TBCategories.Where(x => !x.Deflag && x.IDCategoryParent == null && x.Active == true).Select(x => new
                    {
                        x.Name,
                        x.IDCategory,
                        x.Image,
                        Products = Dynamic_GetData_ByIDCategory(x.IDCategory, take)
                    });

                    return data;
                }
            }
            else
            {
                if (take == 0)
                {
                    var data = db.TBCategories.Where(x => !x.Deflag && x.IDCategoryParent == idCategory && x.Active == true).Select(x => new
                    {
                        x.Name,
                        x.IDCategory,
                        x.Image,
                        Products = Dynamic_GetData_ByIDCategory(x.IDCategory, 0)
                    });

                    return data;
                }
                else
                {
                    var data = db.TBCategories.Where(x => !x.Deflag && x.IDCategoryParent == idCategory && x.Active == true).Select(x => new
                    {
                        x.Name,
                        x.IDCategory,
                        x.Image,
                        Products = Dynamic_GetData_ByIDCategory(x.IDCategory, take)
                    });

                    return data;
                }
            }


        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public dynamic Dynamic_GetData_ByIDValue(dynamic idValue, int take)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            List<dynamic> listProduct = new List<dynamic>();
            foreach (var item in idValue)
            {
                var _combinationDetail = db.FUNC_Product_GetAll_ByIDValue(Convert.ToInt32(item));
                if (_combinationDetail != null)
                    listProduct.AddRange(_combinationDetail);
            }
            IEnumerable<dynamic> Products = listProduct.OrderByDescending(x => x.SequenceNumber).ToList();
            int count = Products.Count();
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            List<int> SameProduct = new List<int>();
            foreach (var currData in Products)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                if (!SameProduct.Contains(currData.IDProduct))
                {
                    newData.Add("IDProduct", currData.IDProduct);
                    newData.Add("Name", currData.Name);
                    newData.Add("ReferenceCode", currData.ReferenceCode);
                    newData.Add("PriceBeforeDiscount", currData.PriceBeforeDiscount);
                    newData.Add("Price", currData.Price);
                    newData.Add("Quantity", currData.Quantity);
                    newData.Add("Active", currData.Active);
                    newData.Add("IDManufacturer", currData.IDManufacturer);
                    newData.Add("SequenceNumber", currData.SequenceNumber);
                    newData.Add("TotalDiscount", currData.TotalDiscount);
                    newData.Add("TypeDiscountPercent", currData.TypeDiscountPercent);
                    newData.Add("Discount", currData.Discount);
                    newData.Add("Weight", currData.Weight);
                    newData.Add("ShortDescription", currData.ShortDescription);
                    newData.Add("Description", currData.Description);
                    newData.Add("Note", currData.Note);
                    newData.Add("Meta", currData.Meta);
                    newData.Add("MetaDescription", currData.MetaDescription);
                    newData.Add("MetaKeyword", currData.MetaKeyword);
                    newData.Add("Photo", currData.Photo);
                    newData.Add("Category", currData.Category);
                    resultList.Add(newData);
                }
                SameProduct.Add(currData.IDProduct);
            }
            return resultList;
            //if (take == 0)
            //{
            //    var data = Dynamic_Func_GetAll_ByIDValue(db, item).AsEnumerable().Where(x => x.IDValue == item).OrderByDescending(x => x.SequenceNumber).Select(x => new
            //    {
            //        x.IDProduct,
            //        x.Name,
            //        x.ReferenceCode,
            //        x.PriceBeforeDiscount,
            //        x.Price,
            //        x.Quantity,
            //        x.Active,
            //        x.IDManufacturer,
            //        x.SequenceNumber,
            //        x.TotalDiscount,
            //        x.TypeDiscountPercent,
            //        x.Discount,
            //        x.Weight,
            //        x.ShortDescription,
            //        x.Description,
            //        x.Note,
            //        x.Meta,
            //        x.MetaDescription,
            //        x.MetaKeyword,
            //        Photo = OurClass.ImageExists(x.Photo, "product") ? x.Photo : "noimage.jpg"
            //    });

            //    return data;
            //}
            //else
            //{
            //    var data = Dynamic_Func_GetAll_ByIDCategory(db, item).AsEnumerable().Where(x => x.IDValue == item).OrderByDescending(x => x.SequenceNumber).Select(x => new
            //    {
            //        x.IDProduct,
            //        x.Name,
            //        x.ReferenceCode,
            //        x.PriceBeforeDiscount,
            //        x.Price,
            //        x.Quantity,
            //        x.Active,
            //        x.IDManufacturer,
            //        x.SequenceNumber,
            //        x.TotalDiscount,
            //        x.TypeDiscountPercent,
            //        x.Discount,
            //        x.Weight,
            //        x.ShortDescription,
            //        x.Description,
            //        x.Note,
            //        x.Meta,
            //        x.MetaDescription,
            //        x.MetaKeyword,
            //        Photo = OurClass.ImageExists(x.Photo, "product") ? x.Photo : "noimage.jpg"
            //    }).Take(take);

            //    return data;
            //}
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_GetData_ByIDValue_And_PriceRange(List<int> idValue, List<int> priceRange, int take)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            List<TBProduct> listProduct = new List<TBProduct>();
            if (take == 0)
            {
                if (idValue.Count != 0)
                {
                    foreach (int id in idValue)
                    {
                        List<TBProduct_Combination_Detail> detail = db.TBProduct_Combination_Details.Where(x => x.TBProduct_Combination.TBProduct.Price > priceRange[0] && x.TBProduct_Combination.TBProduct.Price < priceRange[1] && x.IDValue == id && !x.TBProduct_Combination.Deflag && !x.TBProduct_Combination.TBProduct.Deflag).ToList();
                        foreach (var item in detail)
                        {
                            listProduct.Add(item.TBProduct_Combination.TBProduct);
                        }
                    }
                }
                else
                {
                    List<TBProduct> detail = db.TBProducts.Where(x => x.Price > priceRange[0] && x.Price < priceRange[1] && !x.Deflag).ToList();
                    foreach (var item in detail)
                    {
                        listProduct.Add(item);
                    }
                }

                var data = listProduct.AsEnumerable().OrderByDescending(x => x.TBProduct_Combinations.Sum(p => p.Quantity)).Select(x => new
                {
                    x.IDProduct,
                    x.Name,
                    x.ReferenceCode,
                    Price = GetPrice(db, x.IDProduct)["Price"],
                    PriceBeforeDiscount = GetPrice(db, x.IDProduct)["PriceBeforeDiscount"],
                    Quantity = x.TBProduct_Combinations.Sum(p => p.Quantity),
                    x.Active,
                    x.IDManufacturer,
                    x.SequenceNumber,
                    x.TotalDiscount,
                    x.TypeDiscountPercent,
                    x.Discount,
                    x.Weight,
                    x.ShortDescription,
                    x.Description,
                    x.Note,
                    x.Meta,
                    x.MetaDescription,
                    x.MetaKeyword,
                    Photo = OurClass.ImageExists(x.TBProduct_Photos.Where(y => y.IsCover).FirstOrDefault().Photo, "product") ? x.TBProduct_Photos.Where(y => y.IsCover).FirstOrDefault().Photo : "noimage.jpg",
                    Category = x.TBProduct_Categories.Where(c => c.IsDefault).FirstOrDefault().TBCategory.Name
                });

                return data;
            }
            else
            {
                if (idValue.Count != 0)
                {
                    foreach (int id in idValue)
                    {
                        List<TBProduct_Combination_Detail> detail = db.TBProduct_Combination_Details.Where(x => x.TBProduct_Combination.TBProduct.Price > priceRange[0] && x.TBProduct_Combination.TBProduct.Price < priceRange[1] && x.IDValue == id && !x.TBProduct_Combination.Deflag && !x.TBProduct_Combination.TBProduct.Deflag).ToList();
                        foreach (var item in detail)
                        {
                            listProduct.Add(item.TBProduct_Combination.TBProduct);
                        }
                    }
                }
                else
                {
                    List<TBProduct> detail = db.TBProducts.Where(x => x.Price > priceRange[0] && x.Price < priceRange[1] && !x.Deflag).ToList();
                    foreach (var item in detail)
                    {
                        listProduct.Add(item);
                    }
                }
                var data = listProduct.AsEnumerable().OrderByDescending(x => x.SequenceNumber).Select(x => new
                {
                    x.IDProduct,
                    x.Name,
                    x.ReferenceCode,
                    Price = GetPrice(db, x.IDProduct)["Price"],
                    PriceBeforeDiscount = GetPrice(db, x.IDProduct)["PriceBeforeDiscount"],
                    Quantity = x.TBProduct_Combinations.Sum(p => p.Quantity),
                    x.Active,
                    x.IDManufacturer,
                    x.SequenceNumber,
                    x.TotalDiscount,
                    x.TypeDiscountPercent,
                    x.Discount,
                    x.Weight,
                    x.ShortDescription,
                    x.Description,
                    x.Note,
                    x.Meta,
                    x.MetaDescription,
                    x.MetaKeyword,
                    Photo = OurClass.ImageExists(x.TBProduct_Photos.Where(y => y.IsCover).FirstOrDefault().Photo, "product") ? x.TBProduct_Photos.Where(y => y.IsCover).FirstOrDefault().Photo : "noimage.jpg",
                    Category = x.TBProduct_Categories.Where(c => c.IsDefault).FirstOrDefault().TBCategory.Name
                }).Take(take);

                return data;
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_GetData_ByIDValue_And_IDCategory_And_PriceRange(List<int> idValue, List<int> priceRange, int idCategory, int take)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            List<dynamic> listProduct = new List<dynamic>();

            if (take == 0)
            {
                if (idValue.Count != 0)
                {
                    foreach (int id in idValue)
                    {
                        List<dynamic> detail = Dynamic_GetData_ByIDCategory_And_ByIDValue_And_PriceRange(db, idCategory, id, priceRange[0], priceRange[1]).ToList();
                        foreach (var item in detail)
                        {
                            listProduct.Add(item);
                        }
                    }
                }
                else
                {
                    List<dynamic> detail = Dynamic_GetData_ByIDCategory_And_PriceRange(db, idCategory, priceRange[0], priceRange[1]).ToList();
                    foreach (var item in detail)
                    {
                        listProduct.Add(item);
                    }
                }

                return listProduct.AsEnumerable().OrderByDescending(x => x.SequenceNumber).Select(x => new
                {
                    x.IDProduct,
                    x.Name,
                    x.PriceBeforeDiscount,
                    x.Price,
                    x.Discount,
                    x.TotalDiscount,
                    x.Quantity,
                    x.Photo,
                    x.Category,
                });

                //return data;
            }
            else
            {
                if (idValue.Count != 0)
                {
                    foreach (int id in idValue)
                    {
                        List<dynamic> detail = Dynamic_GetData_ByIDCategory_And_ByIDValue_And_PriceRange(db, idCategory, id, priceRange[0], priceRange[1]).ToList();
                        foreach (var item in detail)
                        {
                            listProduct.Add(item);
                        }
                    }
                }
                else
                {
                    List<dynamic> detail = Dynamic_GetData_ByIDCategory_And_PriceRange(db, idCategory, priceRange[0], priceRange[1]).ToList();
                    foreach (var item in detail)
                    {
                        listProduct.Add(item);
                    }
                }

                var data = listProduct.AsEnumerable().OrderByDescending(x => x.SequenceNumber).Select(x => new
                {
                    x.IDProduct,
                    x.Name,
                    x.PriceBeforeDiscount,
                    x.Price,
                    x.Discount,
                    x.TotalDiscount,
                    x.Quantity,
                    x.Photo,
                    x.Category,
                }).Take(take);

                return data;
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public ReturnData AJAX_GetData_ByIDManufacturer(int idManufacturer)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var data = Dynamic_Func_GetAll(db).AsEnumerable().Where(x => x.IDManufacturer == idManufacturer).OrderByDescending(x => x.SequenceNumber).Select(x => new
            {
                x.IDProduct,
                x.Name,
                x.ReferenceCode,
                x.PriceBeforeDiscount,
                x.Price,
                x.Quantity,
                x.Active,
                x.IDManufacturer,
                x.SequenceNumber,
                x.TotalDiscount,
                x.TypeDiscountPercent,
                x.Discount,
                x.Weight,
                x.ShortDescription,
                x.Description,
                x.Note,
                x.Meta,
                x.MetaDescription,
                x.MetaKeyword,
                Photo = OurClass.ImageExists(x.Photo, "product") ? x.Photo : "noimage.jpg",
                x.Manufacturer,
                x.Category
            });

            return ReturnData.MessageSuccess("OK", data);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_GetBestSeller(int take)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            List<TBProduct> product = new List<TBProduct>();
            if (take == 0)
                product = db.TBOrder_Details.GroupBy(x => x.TBProduct).Select(x => x.Key).ToList();
            else
                product = db.TBOrder_Details.GroupBy(x => x.TBProduct).Select(x => x.Key).Take(take).ToList();
            var data = product.Select(x => new
            {
                x.IDProduct,
                x.Name,
                Photo = x.TBProduct_Photos.Where(p => p.IsCover).FirstOrDefault() != null ? (OurClass.ImageExists(x.TBProduct_Photos.Where(p => p.IsCover).FirstOrDefault().Photo, "product") ? x.TBProduct_Photos.Where(p => p.IsCover).FirstOrDefault().Photo : "noimage.jpg") : "noimage.jpg",
            });

            return ReturnData.MessageSuccess("OK", data);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_GetSale()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var data = Dynamic_Func_GetAll(db).AsEnumerable().Where(x => x.Discount != 0).OrderByDescending(x => x.SequenceNumber).Select(x => new
            {
                x.IDProduct,
                x.Name,
                x.ReferenceCode,
                x.PriceBeforeDiscount,
                x.Price,
                x.Quantity,
                x.Active,
                x.IDManufacturer,
                x.SequenceNumber,
                x.TotalDiscount,
                x.TypeDiscountPercent,
                x.Discount,
                x.Weight,
                x.ShortDescription,
                x.Description,
                x.Note,
                x.Meta,
                x.MetaDescription,
                x.MetaKeyword,
                Photo = OurClass.ImageExists(x.Photo, "product") ? x.Photo : "noimage.jpg",
                x.Manufacturer,
                x.Category
            });

            return ReturnData.MessageSuccess("OK", data);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_Filter_Product(int idcategory_product, int idcategory_gender, int idcategory_price)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            List<TBProduct> result = new List<TBProduct>();
            List<TBProduct> data = db.TBProducts.Where(x => !x.Deflag).ToList();
            List<TBProduct_Category> cat = new List<TBProduct_Category>();

            //if (idcategory_gender != 0 && idcategory_price != 0)
            //    data = db.TBProduct_Categories.Where(x => x.IDCategory == idcategory_product && x.IDCategory == idcategory_gender).GroupBy(x => x.TBProduct).Select(x => x.Key).OrderByDescending(x => x.IDProduct).ToList();
            //else if (idcategory_gender == 0 && idcategory_product == 0 && idcategory_price == 0)
            //    return ReturnData.MessageSuccess("OK", Dynamic_GetAll(db));//data = db.TBProducts.Where(x => !x.Deflag).OrderByDescending(x => x.IDProduct).ToList();
            //else if (idcategory_gender == 0 && idcategory_product == 0 && idcategory_price != 0)
            //    data = db.TBProducts.Where(x => !x.Deflag).OrderByDescending(x => x.IDProduct).ToList();
            //else if (idcategory_gender == 0 && idcategory_product == 0)
            //    data = db.TBProducts.Where(x => !x.Deflag).OrderByDescending(x => x.IDProduct).ToList();
            //else
            //    data = db.TBProduct_Categories.Where(x => x.IDCategory == idcategory_product || x.IDCategory == idcategory_gender).GroupBy(x => x.TBProduct).Select(x => x.Key).OrderByDescending(x => x.IDProduct).ToList();



            if (idcategory_product != 0)
                cat = db.TBProduct_Categories.Where(x => x.IDCategory == idcategory_product).ToList();
            if (idcategory_gender != 0)
                cat.AddRange(db.TBProduct_Categories.Where(x => x.IDCategory == idcategory_gender).ToList());

            data = cat.GroupBy(x => x.TBProduct).Select(x => x.Key).Where(x => !x.Deflag && x.Active).OrderByDescending(x => x.IDProduct).ToList();

            foreach (var item in data)
            {
                if (!item.Deflag)
                {
                    switch (idcategory_price)
                    {
                        case 1:
                            {
                                if (item.Price <= 99999)
                                    result.Add(item);
                            }
                            break;
                        case 2:
                            {
                                if (item.Price > 99999 && item.Price <= 200000)
                                    result.Add(item);
                            }
                            break;
                        case 3:
                            {
                                if (item.Price > 200000)
                                    result.Add(item);
                            }
                            break;
                        default:
                            {
                                result.Add(item);
                                break;
                            }
                    }
                }

            }
            return ReturnData.MessageSuccess("OK", result.Select(x => new
            {
                x.IDProduct,
                x.Name,
                Photo = x.TBProduct_Photos.Where(p => p.IsCover).FirstOrDefault() != null ? (OurClass.ImageExists(x.TBProduct_Photos.Where(p => p.IsCover).FirstOrDefault().Photo, "product") ? x.TBProduct_Photos.Where(p => p.IsCover).FirstOrDefault().Photo : "noimage.jpg") : "noimage.jpg",
            }));
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_Search_Product(string keyword)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var data = db.TBProducts.Where(x => !x.Deflag && x.Name.Contains(keyword) && x.Active).OrderByDescending(x => x.SequenceNumber).Select(x => new
            {
                x.Name,
                x.Price,
                x.IDProduct,
                Photo = x.TBProduct_Photos.Where(y => y.IsCover).FirstOrDefault().Photo
            });
            if (data != null)
            {
                return ReturnData.MessageSuccess("OK", data);
            }
            else
            {
                return ReturnData.MessageSuccess("No Product Found", null);
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

    #region DYNAMIC
    // FUNCTION
    private Func<DataClassesDataContext, IEnumerable<dynamic>> Dynamic_Func_GetAll
    {
        get
        {
            Func<DataClassesDataContext, IEnumerable<dynamic>> func =
              CompiledQuery.Compile<DataClassesDataContext, IEnumerable<dynamic>>
              ((DataClassesDataContext context) => context.FUNC_Product_GetAll()
                  .AsEnumerable().OrderBy(x => x.SequenceNumber).ToArray());
            return func;
        }
    }

    private Func<DataClassesDataContext, int, IEnumerable<dynamic>> Dynamic_Func_GetAll_ByIDCategory
    {
        get
        {
            Func<DataClassesDataContext, int, IEnumerable<dynamic>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, IEnumerable<dynamic>>
              ((DataClassesDataContext context, int idCategory) => context.FUNC_Product_GetAll_ByIDCategory(idCategory)
                  .AsEnumerable().OrderBy(x => x.SequenceNumber).ToArray());
            return func;
        }
    }

    //private Func<DataClassesDataContext, int, int, IEnumerable<dynamic>> Dynamic_Func_Get_ByIDCategory
    //{
    //    get
    //    {
    //        Func<DataClassesDataContext, int, int, IEnumerable<dynamic>> func =
    //          CompiledQuery.Compile<DataClassesDataContext, int, int, IEnumerable<dynamic>>
    //          ((DataClassesDataContext context, int idCategory, int take) => context.FUNC_Product_Get_ByIDCategory(idCategory, take)
    //              .AsEnumerable().OrderBy(x => x.SequenceNumber).ToArray());
    //        return func;
    //    }
    //}

    private Func<DataClassesDataContext, int, int, int, IEnumerable<dynamic>> Dynamic_Func_Get_ByIDCategory_Page
    {
        get
        {
            Func<DataClassesDataContext, int, int, int, IEnumerable<dynamic>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, int, int, IEnumerable<dynamic>>
              ((DataClassesDataContext context, int idCategory, int take, int page) => context.FUNC_Product_GetAll_ByIDCategory(idCategory)
                  .AsEnumerable().OrderBy(x => x.SequenceNumber).ToArray());
            return func;
        }
    }

    private Func<DataClassesDataContext, IEnumerable<dynamic>> Dynamic_Func_GetTable
    {
        get
        {
            Func<DataClassesDataContext, IEnumerable<dynamic>> func =
              CompiledQuery.Compile<DataClassesDataContext, IEnumerable<dynamic>>
              ((DataClassesDataContext context) => context.FUNC_Product_GetTable()
                  .AsEnumerable().OrderBy(x => x.SequenceNumber).ToArray());
            return func;
        }
    }
    private Func<DataClassesDataContext, int, IEnumerable<dynamic>> Dynamic_Func_GetDetail
    {
        get
        {
            Func<DataClassesDataContext, int, IEnumerable<dynamic>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, IEnumerable<dynamic>>
              ((DataClassesDataContext context, int idProduct) => context.FUNC_Product_GetDetail(idProduct)
                  .AsEnumerable().ToArray());
            return func;
        }
    }

    private Func<DataClassesDataContext, int, int, decimal, decimal, IEnumerable<dynamic>> Dynamic_Func_GetAll_ByIDCategory_And_IDValue_And_PriceRange
    {
        get
        {
            Func<DataClassesDataContext, int, int, decimal, decimal, IEnumerable<dynamic>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, int, decimal, decimal, IEnumerable<dynamic>>
              ((DataClassesDataContext context, int IDCategory, int IDValue, decimal MinPrice, decimal MaxPrice) => context.FUNC_Product_GetAll_ByIDValue_And_IDCategory_And_PriceRange(IDValue, IDCategory, MinPrice, MaxPrice)
                  .AsEnumerable().OrderBy(x => x.SequenceNumber).ToArray());
            return func;
        }
    }

    private Func<DataClassesDataContext, int, int, IEnumerable<dynamic>> Dynamic_Func_GetAll_ByIDCategory_And_IDValue
    {
        get
        {
            Func<DataClassesDataContext, int, int, IEnumerable<dynamic>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, int, IEnumerable<dynamic>>
              ((DataClassesDataContext context, int IDCategory, int IDValue) => context.FUNC_Product_GetAll_ByIDValue_And_IDCategory(IDValue, IDCategory)
                  .AsEnumerable().OrderBy(x => x.SequenceNumber).ToArray());
            return func;
        }
    }

    private Func<DataClassesDataContext, int, decimal, decimal, IEnumerable<dynamic>> Dynamic_Func_GetAll_ByIDCategory_And_PriceRange
    {
        get
        {
            Func<DataClassesDataContext, int, decimal, decimal, IEnumerable<dynamic>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, decimal, decimal, IEnumerable<dynamic>>
              ((DataClassesDataContext context, int IDCategory, decimal MinPrice, decimal MaxPrice) => context.FUNC_Product_GetAll_ByIDCategory_And_PriceRange(IDCategory, MinPrice, MaxPrice)
                  .AsEnumerable().OrderBy(x => x.SequenceNumber).ToArray());
            return func;
        }
    }

    // DYNAMIC

    public dynamic Dynamic_GetCurrencyByIDProduct(int idproduct)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBProduct_Currencies.Where(x => x.IDProduct == idproduct).Select(x => new
            {
                x.IDProduct_Currency,
                x.IDProduct,
                x.IDCurrency,
                x.Price,
                x.PriceBeforeDiscount,
                Name = x.TBCurrency.Name,
                Sign = x.TBCurrency.Sign,
                ConversionRate = x.TBCurrency.ConversionRate,
                x.TBCurrency.ISOCode
            });
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public IEnumerable<dynamic> Dynamic_GetAll(DataClassesDataContext db)
    {
        try
        {
            return Dynamic_Func_GetAll(db).AsEnumerable().OrderByDescending(x => x.IDProduct).Select(x => new
            {
                x.IDProduct,
                x.Name,
                x.ReferenceCode,
                x.PriceBeforeDiscount,
                x.Price,
                x.Quantity,
                x.Active,
                x.IDManufacturer,
                x.SequenceNumber,
                x.TotalDiscount,
                x.TypeDiscountPercent,
                x.Discount,
                x.Weight,
                x.ShortDescription,
                x.Description,
                x.Note,
                x.Meta,
                x.MetaDescription,
                x.MetaKeyword,
                Photo = OurClass.ImageExists(x.Photo, "product") ? x.Photo : "noimage.jpg",
                x.Manufacturer,
                x.Category
            }).ToArray();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public IEnumerable<dynamic> Dynamic_GetTable(DataClassesDataContext db)
    {
        try
        {
            return Dynamic_Func_GetTable(db).AsEnumerable().Select(x => new
            {
                x.IDProduct,
                x.Name,
                x.ReferenceCode,
                x.PriceBeforeDiscount,
                x.Price,
                Quantity = x.Quantity == null ? 0 : x.Quantity,
                x.Active,
                x.SequenceNumber,
                x.Photo
            }).ToArray();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public dynamic Dynamic_GetDetail(DataClassesDataContext db, int idProduct)
    {
        try
        {
            return Dynamic_Func_GetDetail(db, idProduct).AsEnumerable().Select(x => new
            {
                x.IDProduct,
                x.Name,
                x.ReferenceCode,
                x.PriceBeforeDiscount,
                x.Price,
                x.Quantity,
                x.Active,
                x.IDManufacturer,
                x.SequenceNumber,
                x.TotalDiscount,
                x.TypeDiscountPercent,
                x.Discount,
                x.Weight,
                x.ShortDescription,
                x.Description,
                x.Note,
                x.Meta,
                x.MetaDescription,
                x.MetaKeyword,
                Photo = OurClass.ImageExists(x.Photo, "product") ? x.Photo : "noimage.jpg",
                x.Manufacturer,
                x.Category
            }).FirstOrDefault();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public IEnumerable<dynamic> Dynamic_GetData_ByIDCategory_And_ByIDValue_And_PriceRange(DataClassesDataContext db, int idCategory, int idValue, decimal MinPrice, decimal MaxPrice)
    {
        try
        {
            return Dynamic_Func_GetAll_ByIDCategory_And_IDValue_And_PriceRange(db, idCategory, idValue, MinPrice, MaxPrice).AsEnumerable().Select(x => new
            {
                x.IDProduct,
                x.Name,
                x.ReferenceCode,
                x.PriceBeforeDiscount,
                x.Price,
                x.Quantity,
                x.Active,
                x.IDManufacturer,
                x.SequenceNumber,
                x.TotalDiscount,
                x.TypeDiscountPercent,
                x.Discount,
                x.Weight,
                x.ShortDescription,
                x.Description,
                x.Note,
                x.Meta,
                x.MetaDescription,
                x.MetaKeyword,
                Photo = OurClass.ImageExists(x.Photo, "product") ? x.Photo : "noimage.jpg",
                x.Category
            }).ToArray();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public IEnumerable<dynamic> Dynamic_GetData_ByIDCategory_And_ByIDValue(DataClassesDataContext db, int idCategory, int idValue)
    {
        try
        {
            return Dynamic_Func_GetAll_ByIDCategory_And_IDValue(db, idCategory, idValue).AsEnumerable().Select(x => new
            {
                x.IDProduct,
                x.Name,
                x.ReferenceCode,
                x.PriceBeforeDiscount,
                x.Price,
                x.Quantity,
                x.Active,
                x.IDManufacturer,
                x.SequenceNumber,
                x.TotalDiscount,
                x.TypeDiscountPercent,
                x.Discount,
                x.Weight,
                x.ShortDescription,
                x.Description,
                x.Note,
                x.Meta,
                x.MetaDescription,
                x.MetaKeyword,
                Photo = OurClass.ImageExists(x.Photo, "product") ? x.Photo : "noimage.jpg",
                x.Category
            }).ToArray();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public IEnumerable<dynamic> Dynamic_GetData_ByIDCategory_And_PriceRange(DataClassesDataContext db, int idCategory, decimal MinPrice, decimal MaxPrice)
    {
        try
        {
            return Dynamic_Func_GetAll_ByIDCategory_And_PriceRange(db, idCategory, MinPrice, MaxPrice).AsEnumerable().Select(x => new
            {
                x.IDProduct,
                x.Name,
                x.ReferenceCode,
                x.PriceBeforeDiscount,
                x.Price,
                x.Quantity,
                x.Active,
                x.IDManufacturer,
                x.SequenceNumber,
                x.TotalDiscount,
                x.TypeDiscountPercent,
                x.Discount,
                x.Weight,
                x.ShortDescription,
                x.Description,
                x.Note,
                x.Meta,
                x.MetaDescription,
                x.MetaKeyword,
                Photo = OurClass.ImageExists(x.Photo, "product") ? x.Photo : "noimage.jpg",
                x.Category
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
    // FUNCTION
    private Func<DataClassesDataContext, IEnumerable<TBProduct>> Func_GetAll
    {
        get
        {
            Func<DataClassesDataContext, IEnumerable<TBProduct>> func =
              CompiledQuery.Compile<DataClassesDataContext, IEnumerable<TBProduct>>
              ((DataClassesDataContext context) => context.TBProducts
                  .AsEnumerable().OrderBy(x => x.SequenceNumber).ToArray());
            return func;
        }
    }
    private Func<DataClassesDataContext, int, TBProduct> Func_GetDetail
    {
        get
        {
            Func<DataClassesDataContext, int, TBProduct> func =
              CompiledQuery.Compile<DataClassesDataContext, int, TBProduct>
              ((DataClassesDataContext context, int idProduct) => context.TBProducts
                  .AsEnumerable().Where(x => x.IDProduct == idProduct).FirstOrDefault());
            return func;
        }
    }

    // LINQ
    public IEnumerable<TBProduct> GetAll(DataClassesDataContext db)
    {
        try
        {
            return Func_GetAll(db);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public TBProduct GetDetail(DataClassesDataContext db, int idProduct)
    {
        try
        {
            return Func_GetDetail(db, idProduct);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    #endregion
    #endregion

    #region INFORMATION
    #region AJAX
    public ReturnData AJAX_Insert_Information(int idManufacturer, string referenceCode, string name, decimal priceBeforeDiscount, bool typeDiscountPercent, decimal discount, decimal weight, string shortDescription, string description, string note, bool active, List<Dictionary<string, object>> currency)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                if (referenceCode.Trim() != "")
                    if (!ValidationReferenceCode_Insert(db, referenceCode))
                        return ReturnData.MessageFailed(referenceCode + " already exists.", null);

                Class_Manufacturer _manufacturer = new Class_Manufacturer();
                if (_manufacturer.Dynamic_GetDetail(db, idManufacturer) == null)
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);

                TBProduct _newData = new TBProduct
                {
                    IDManufacturer = idManufacturer,
                    SequenceNumber = 0,
                    ReferenceCode = referenceCode,
                    Name = name,
                    PriceBeforeDiscount = priceBeforeDiscount,
                    TypeDiscountPercent = typeDiscountPercent,
                    Discount = discount,
                    Weight = weight,
                    ShortDescription = shortDescription,
                    Description = description,
                    Note = note,
                    Active = active,
                    Deflag = false,
                    DateInsert = DateTime.Now,
                    DateLastUpdate = DateTime.Now
                };
                _newData.TotalDiscount = (_newData.TypeDiscountPercent) ? _newData.PriceBeforeDiscount * (_newData.Discount / 100) : _newData.Discount;
                _newData.Price = _newData.PriceBeforeDiscount - _newData.TotalDiscount;
                db.TBProducts.InsertOnSubmit(_newData);
                db.SubmitChanges();

                //ADD CURRENCY TBProduct_Currency
                foreach (var item in currency)
                {
                    TBProduct_Currency newCurrency = new TBProduct_Currency();
                    newCurrency.IDProduct = _newData.IDProduct;

                    string price = item["Price"].ToString();
                    string priceBefore = item["PriceBeforeDiscount"].ToString();

                    newCurrency.Price = decimal.Parse(price, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, CultureInfo.CreateSpecificCulture("en-US"));
                    newCurrency.PriceBeforeDiscount = decimal.Parse(priceBefore, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, CultureInfo.CreateSpecificCulture("en-US"));
                    newCurrency.IDCurrency = (int)item["IDCurrency"];

                    db.TBProduct_Currencies.InsertOnSubmit(newCurrency);
                }

                db.SubmitChanges();

                if (_newData != null)
                {
                    if (Update_SequenceNumber(db))
                        return ReturnData.MessageSuccess(name + " has been successfully inserted.", _newData.IDProduct);
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
    public ReturnData AJAX_Update_Information(int idProduct, string referenceCode, string name, decimal priceBeforeDiscount, bool typeDiscountPercent, decimal discount, decimal weight, string shortDescription, string description, string note, bool active)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                TBProduct _updateData = GetDetail(db, idProduct);
                if (_updateData == null)
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);

                if (referenceCode.Trim() != "")
                    if (ValidationReferenceCode_Update(db, idProduct, referenceCode))
                        return ReturnData.MessageFailed(referenceCode + " already exists.", null);

                string _nameBefore = _updateData.Name;
                _updateData.ReferenceCode = referenceCode;
                _updateData.Name = name;
                _updateData.PriceBeforeDiscount = priceBeforeDiscount;
                _updateData.TypeDiscountPercent = typeDiscountPercent;
                _updateData.Discount = discount;
                _updateData.TotalDiscount = (_updateData.TypeDiscountPercent) ? _updateData.PriceBeforeDiscount * (_updateData.Discount / 100) : _updateData.Discount;
                _updateData.Price = _updateData.PriceBeforeDiscount - _updateData.TotalDiscount;
                _updateData.Weight = weight;
                _updateData.ShortDescription = shortDescription;
                _updateData.Description = description;
                _updateData.Note = note;
                _updateData.Active = active;
                _updateData.DateLastUpdate = DateTime.Now;
                foreach (TBProduct_Combination item in _updateData.TBProduct_Combinations.AsEnumerable().ToArray())
                {
                    item.PriceBeforeImpact = _updateData.PriceBeforeDiscount;
                    item.PriceAfterImpact = _updateData.PriceBeforeDiscount + item.ImpactPrice;
                    item.TypeDiscountPercent = _updateData.TypeDiscountPercent;
                    item.Discount = _updateData.Discount;
                    item.TotalDiscount = (item.TypeDiscountPercent) ? item.PriceAfterImpact * (item.Discount / 100) : item.Discount;
                    item.Price = item.PriceAfterImpact - item.TotalDiscount;
                    item.WeightBeforeImpact = _updateData.Weight;
                    item.Weight = item.WeightBeforeImpact + item.ImpactWeight;
                }
                db.SubmitChanges();

                if (_updateData != null)
                    return ReturnData.MessageSuccess(_nameBefore + " has been successfully updated.", null);
                return ReturnData.MessageFailed(_nameBefore + " failed to update.", null);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public ReturnData AJAX_Delete_Information(int idProduct)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                TBProduct _updateData = GetDetail(db, idProduct);
                if (_updateData == null)
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);

                string _nameBefore = _updateData.Name;
                _updateData.Deflag = true;
                _updateData.DateLastUpdate = DateTime.Now;
                db.SubmitChanges();

                foreach (var item in _updateData.TBProduct_Combinations)
                {
                    item.Deflag = true;
                    //db.TBProduct_Combination_Details.DeleteAllOnSubmit(item.TBProduct_Combination_Details.Where(x => x.IDProduct_Combination == item.IDProduct_Combination));
                    db.SubmitChanges();
                }

                if (_updateData != null)
                    return ReturnData.MessageSuccess(_nameBefore + " has been successfully deleted.", null);
                return ReturnData.MessageFailed(_nameBefore + " failed to delete.", null);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public ReturnData AJAX_ChangeActive_Information(int idProduct)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                TBProduct _updateData = GetDetail(db, idProduct);
                if (_updateData == null)
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);

                string _nameBefore = _updateData.Name;
                _updateData.Active = !_updateData.Active;
                _updateData.DateLastUpdate = DateTime.Now;
                db.SubmitChanges();

                if (_updateData != null)
                {
                    if (_updateData.Active)
                        return ReturnData.MessageSuccess(_nameBefore + " has been successfully activated.", null);
                    return ReturnData.MessageSuccess(_nameBefore + " has been successfully deactivated.", null);
                }
                return ReturnData.MessageFailed(_nameBefore + " failed to change active status.", null);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public ReturnData AJAX_UpSequence_Information(int idProduct)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                TBProduct _selectedProduct = GetDetail(db, idProduct);
                if (_selectedProduct != null)
                {
                    if (_selectedProduct.SequenceNumber == 0)
                    {
                        int last = GetAll(db).AsEnumerable().Where(x => !x.Deflag && x.SequenceNumber != 0).OrderByDescending(x => x.SequenceNumber).FirstOrDefault().SequenceNumber;
                        last++;
                        _selectedProduct.SequenceNumber = last;
                        db.SubmitChanges();
                        return ReturnData.MessageSuccess(_selectedProduct.Name + " has been successfully moved.", null);
                    }
                    TBProduct _changeProduct = GetAll(db).AsEnumerable().Where(x => x.SequenceNumber == _selectedProduct.SequenceNumber - 1).FirstOrDefault();
                    if (_changeProduct != null)
                    {
                        int _temp = _changeProduct.SequenceNumber;
                        _changeProduct.SequenceNumber = _selectedProduct.SequenceNumber;
                        _selectedProduct.SequenceNumber = _temp;
                        db.SubmitChanges();
                        return ReturnData.MessageSuccess(_selectedProduct.Name + " has been successfully moved.", null);
                    }
                    return ReturnData.MessageFailed(_selectedProduct.Name + " failed to move.", null);
                }
                return ReturnData.MessageFailed("The requested resource does not exist.", null);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public ReturnData AJAX_DownSequence_Information(int idProduct)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                TBProduct _selectedProduct = GetDetail(db, idProduct);
                if (_selectedProduct != null)
                {
                    TBProduct _changeProduct = GetAll(db).AsEnumerable().Where(x => x.SequenceNumber == _selectedProduct.SequenceNumber + 1).FirstOrDefault();
                    if (_changeProduct != null)
                    {
                        int _temp = _changeProduct.SequenceNumber;
                        _changeProduct.SequenceNumber = _selectedProduct.SequenceNumber;
                        _selectedProduct.SequenceNumber = _temp;
                        db.SubmitChanges();
                        return ReturnData.MessageSuccess(_selectedProduct.Name + " has been successfully moved.", null);
                    }
                    return ReturnData.MessageFailed(_selectedProduct.Name + " failed to move.", null);
                }
                return ReturnData.MessageFailed("The requested resource does not exist.", null);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public bool ValidationReferenceCode_Insert(DataClassesDataContext db, string referenceCode)
    {
        try
        {
            if (Dynamic_GetAll_Information(db).AsEnumerable().Where(x => x.ReferenceCode.ToLower() == referenceCode.ToLower()).FirstOrDefault() != null)
                return false;
            return true;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return true;
        }
    }
    public bool ValidationReferenceCode_Update(DataClassesDataContext db, int idProduct, string referenceCode)
    {
        try
        {
            if (Dynamic_GetAll_Information(db).AsEnumerable().Where(x => x.ReferenceCode.ToLower() == referenceCode.ToLower() && x.IDProduct != idProduct).FirstOrDefault() != null)
                return true;
            return false;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return true;
        }
    }
    public bool Update_SequenceNumber(DataClassesDataContext db)
    {
        try
        {
            TBProduct[] _products = GetAll(db).ToArray();
            for (int i = 0; i < _products.Count(); i++)
            {
                _products[i].SequenceNumber = i + 1;
            }
            db.SubmitChanges();
            return true;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return false;
        }
    }
    #endregion

    #region DYNAMIC
    // FUNCTION
    private Func<DataClassesDataContext, IEnumerable<dynamic>> Dynamic_Func_GetAll_Information
    {
        get
        {
            Func<DataClassesDataContext, IEnumerable<dynamic>> func =
              CompiledQuery.Compile<DataClassesDataContext, IEnumerable<dynamic>>
              ((DataClassesDataContext context) => context.FUNC_Product_GetAll_Information()
                  .AsEnumerable().OrderBy(x => x.SequenceNumber).ToArray());
            return func;
        }
    }
    private Func<DataClassesDataContext, int, dynamic> Dynamic_Func_GetDetail_Information
    {
        get
        {
            Func<DataClassesDataContext, int, dynamic> func =
              CompiledQuery.Compile<DataClassesDataContext, int, dynamic>
              ((DataClassesDataContext context, int idProduct) => context.FUNC_Product_GetDetail_Information(idProduct)
                  .AsEnumerable().FirstOrDefault());
            return func;
        }
    }

    // DYNAMIC
    public IEnumerable<dynamic> Dynamic_GetAll_Information(DataClassesDataContext db)
    {
        try
        {
            return Dynamic_Func_GetAll_Information(db);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public dynamic Dynamic_GetDetail_Information(DataClassesDataContext db, int idProduct)
    {
        try
        {
            return Dynamic_Func_GetDetail_Information(db, idProduct);
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
    #endregion

    #region META
    #region AJAX
    public ReturnData AJAX_Update_Meta(int idProduct, string meta, string metaDescription, string metaKeyword)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                TBProduct _updateData = GetDetail(db, idProduct);
                if (_updateData == null)
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);

                string _nameBefore = _updateData.Name;
                _updateData.Meta = meta;
                _updateData.MetaDescription = metaDescription;
                _updateData.MetaKeyword = metaKeyword;
                _updateData.DateLastUpdate = DateTime.Now;
                db.SubmitChanges();

                if (_updateData != null)
                    return ReturnData.MessageSuccess("SEO for " + _nameBefore + " has been successfully updated.", null);
                return ReturnData.MessageFailed(_nameBefore + " failed to update.", null);
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
    #endregion

    #region CATEGORIES
    #region AJAX
    public ReturnData AJAX_Update_Categories(int idProduct, int[] categories)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                if (categories.Count() == 0)
                    return ReturnData.MessageFailed("No selected category to insert.", null);

                dynamic _product = Dynamic_GetDetail_Information(db, idProduct);
                if (_product == null)
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);

                TBProduct_Category[] _oldCategories = GetData_Category_ByIDProduct(db, _product.IDProduct);
                List<TBCategory> _categories = new List<TBCategory>();
                Class_Category _category = new Class_Category();
                foreach (int item in categories)
                {
                    if (_oldCategories.AsEnumerable().Where(x => x.IDCategory == item).FirstOrDefault() == null)
                    {
                        TBCategory _newData = _category.GetDetail(db, item);
                        if (_categories == null)
                            return ReturnData.MessageFailed("Some category does not exists.", null);
                        if (!_newData.Active)
                            return ReturnData.MessageFailed(_newData.Name + " failed to insert, because " + _newData.Name + " does not active.", null);
                        _categories.Add(_newData);
                    }
                }
                foreach (TBProduct_Category item in _oldCategories)
                {
                    dynamic _item = categories.Where(x => x == item.IDCategory).FirstOrDefault();
                    if (_item == 0)
                    {
                        if (item.IsDefault)
                            return ReturnData.MessageFailed(item.TBCategory.Name + " failed to insert, because " + item.TBCategory.Name + " set as default category for this product.", null);
                        db.TBProduct_Categories.DeleteOnSubmit(item);
                    }
                    else
                    {
                        if (!item.TBCategory.Active)
                            return ReturnData.MessageFailed(item.TBCategory.Name + " failed to insert, because " + item.TBCategory.Name + " does not active.", null);
                    }
                }
                db.SubmitChanges();

                foreach (dynamic item in _categories)
                {
                    TBProduct_Category _newData = GetData_Category_ByIDProduct(db, idProduct).Where(x => x.IDCategory == item.IDCategory).FirstOrDefault();
                    if (_newData == null)
                    {
                        _newData = new TBProduct_Category
                        {
                            IDProduct = idProduct,
                            IDCategory = item.IDCategory,
                            IsDefault = (Dynamic_GetData_Categories_ByIDProduct(db, idProduct).Where(x => x.IsDefault).Count() == 0) ? true : false,
                            DateInsert = DateTime.Now,
                            DateLastUpdate = DateTime.Now,
                        };

                        db.TBProduct_Categories.InsertOnSubmit(_newData);
                    }
                }
                db.SubmitChanges();

                Dictionary<string, dynamic> _result = new Dictionary<string, dynamic>();
                _result.Add("DefaultCategory", Dynamic_GetDefault_Categories(db, idProduct));
                _result.Add("SelectedCategories", Dynamic_GetData_Categories_ByIDProduct(db, idProduct));
                _result.Add("TreeCategories", GetTree_Category(db, idProduct));
                return ReturnData.MessageSuccess(categories.Count() + " categories has been successfully updated.", _result);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public ReturnData AJAX_ChangeDefault_Categories(int idProduct_Categories)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                string _nameBefore = "";
                dynamic _changeData = Dynamic_GetDetail_Categories(db, idProduct_Categories);
                if (_changeData == null)
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);

                _nameBefore = _changeData.Name;
                if (_changeData.IsDefault)
                    return ReturnData.MessageFailed(_nameBefore + " already default category for this product.", null);

                foreach (TBProduct_Category item in GetData_Category_ByIDProduct(db, _changeData.IDProduct))
                {
                    if (item.IDCategory != _changeData.IDCategory)
                    {
                        if (item.IsDefault)
                        {
                            item.IsDefault = false;
                            item.DateLastUpdate = DateTime.Now;
                        }
                    }
                    else
                    {
                        if (!item.TBCategory.Active)
                            return ReturnData.MessageFailed(_nameBefore + " failed to set as default, because " + _nameBefore + " does not active.", null);
                        item.IsDefault = true;
                        item.DateLastUpdate = DateTime.Now;
                    }
                }
                db.SubmitChanges();
                Dictionary<string, dynamic> _result = new Dictionary<string, dynamic>();
                _result.Add("SelectedCategories", Dynamic_GetData_Categories_ByIDProduct(db, _changeData.IDProduct));
                _result.Add("DefaultCategory", Dynamic_GetDefault_Categories(db, _changeData.IDProduct));
                return ReturnData.MessageSuccess(_nameBefore + " has been successfully default.", _result);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    #region TREE
    public IEnumerable<dynamic> GetTree_Category(DataClassesDataContext db, int idProduct)
    {
        Class_Category _category = new Class_Category();
        return GenerateUL(db, _category, _category.GetDataBy_IDCategoryParent(db, 0), idProduct);

    }
    private IEnumerable<WITLibrary.JsTreeModel> GenerateUL(DataClassesDataContext db, Class_Category _category, IEnumerable<TBCategory> menus, int idProduct)
    {
        List<WITLibrary.JsTreeModel> _newCategory = new List<WITLibrary.JsTreeModel>();
        foreach (var menu in menus)
        {
            if (menu.TBCategories.AsEnumerable().Any())
            {
                _newCategory.Add(new WITLibrary.JsTreeModel
                {
                    text = menu.Name,
                    icon = menu.Active ? "fa fa-tags icon-state-success" : "fa fa-tags icon-state-danger",
                    state = new WITLibrary.JsTreeState { selected = menu.TBProduct_Categories.AsEnumerable().FirstOrDefault(x => x.IDProduct == idProduct) != null },
                    li_attr = new WITLibrary.JsTreeAttr { id = menu.IDCategory.ToString() },
                    children = GenerateUL(db, _category, _category.GetDataBy_IDCategoryParent(db, menu.IDCategory), idProduct).ToArray()
                });
            }
            else
            {
                _newCategory.Add(new WITLibrary.JsTreeModel
                {
                    text = menu.Name,
                    icon = menu.Active ? "fa fa-tags icon-state-success" : "fa fa-tags icon-state-danger",
                    li_attr = new WITLibrary.JsTreeAttr { id = menu.IDCategory.ToString() },
                    state = new WITLibrary.JsTreeState { selected = menu.TBProduct_Categories.AsEnumerable().FirstOrDefault(x => x.IDProduct == idProduct) != null }
                });
            }
        }
        return _newCategory.ToArray();
    }
    #endregion
    #endregion

    #region DYNAMIC
    // FUNCTION
    private Func<DataClassesDataContext, IEnumerable<dynamic>> Dynamic_Func_GetAll_Category
    {
        get
        {
            Func<DataClassesDataContext, IEnumerable<dynamic>> func =
              CompiledQuery.Compile<DataClassesDataContext, IEnumerable<dynamic>>
              ((DataClassesDataContext context) => context.FUNC_Product_Category_GetAll()
                  .AsEnumerable().ToArray());
            return func;
        }
    }
    private Func<DataClassesDataContext, int, IEnumerable<dynamic>> Dynamic_Func_GetData_ByIDProduct_Category
    {
        get
        {
            Func<DataClassesDataContext, int, IEnumerable<dynamic>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, IEnumerable<dynamic>>
              ((DataClassesDataContext context, int idProduct) => context.FUNC_Product_Category_GetData_ByIDProduct(idProduct)
                  .AsEnumerable().ToArray());
            return func;
        }
    }
    private Func<DataClassesDataContext, int, dynamic> Dynamic_Func_GetData_Default_Category
    {
        get
        {
            Func<DataClassesDataContext, int, dynamic> func =
              CompiledQuery.Compile<DataClassesDataContext, int, dynamic>
              ((DataClassesDataContext context, int idProduct) => context.FUNC_Product_Category_GetData_Default(idProduct)
                  .AsEnumerable().FirstOrDefault());
            return func;
        }
    }
    private Func<DataClassesDataContext, int, dynamic> Dynamic_Func_GetDetail_Category
    {
        get
        {
            Func<DataClassesDataContext, int, dynamic> func =
              CompiledQuery.Compile<DataClassesDataContext, int, dynamic>
              ((DataClassesDataContext context, int idProduct_Category) => context.FUNC_Product_Category_GetDetail(idProduct_Category)
                  .AsEnumerable().FirstOrDefault());
            return func;
        }
    }

    // DYNAMIC
    public IEnumerable<dynamic> Dynamic_GetAll_Categories(DataClassesDataContext db)
    {
        try
        {
            return Dynamic_Func_GetAll_Category(db);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public IEnumerable<dynamic> Dynamic_GetData_Categories_ByIDProduct(DataClassesDataContext db, int idProduct)
    {
        try
        {
            return Dynamic_Func_GetData_ByIDProduct_Category(db, idProduct);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public dynamic Dynamic_GetDetail_Categories(DataClassesDataContext db, int idProduct_Category)
    {
        try
        {
            return Dynamic_Func_GetDetail_Category(db, idProduct_Category);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public dynamic Dynamic_GetDefault_Categories(DataClassesDataContext db, int idProduct)
    {
        try
        {
            return Dynamic_Func_GetData_Default_Category(db, idProduct);
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
    // FUNCTION
    private Func<DataClassesDataContext, IEnumerable<TBProduct_Category>> Func_GetAll_Category
    {
        get
        {
            Func<DataClassesDataContext, IEnumerable<TBProduct_Category>> func =
              CompiledQuery.Compile<DataClassesDataContext, IEnumerable<TBProduct_Category>>
              ((DataClassesDataContext context) => context.TBProduct_Categories.AsEnumerable()
                .Where(x => !x.TBCategory.Deflag).ToArray());
            return func;
        }
    }
    private Func<DataClassesDataContext, int, IEnumerable<TBProduct_Category>> Func_GetData_ByIDProduct_Category
    {
        get
        {
            Func<DataClassesDataContext, int, IEnumerable<TBProduct_Category>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, IEnumerable<TBProduct_Category>>
              ((DataClassesDataContext context, int idProduct) => context.TBProduct_Categories.AsEnumerable()
                .Where(x => !x.TBCategory.Deflag && x.IDProduct == idProduct).ToArray());
            return func;
        }
    }
    private Func<DataClassesDataContext, int, TBProduct_Category> Func_GetDetail_Category
    {
        get
        {
            Func<DataClassesDataContext, int, TBProduct_Category> func =
              CompiledQuery.Compile<DataClassesDataContext, int, TBProduct_Category>
              ((DataClassesDataContext context, int idProduct_Category) => context.TBProduct_Categories.AsEnumerable()
                .Where(x => !x.TBCategory.Deflag && x.IDProduct_Category == idProduct_Category).FirstOrDefault());
            return func;
        }
    }

    // LINQ
    public IEnumerable<TBProduct_Category> GetAll_Category(DataClassesDataContext db)
    {
        try
        {
            return Func_GetAll_Category(db).ToArray();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public IEnumerable<TBProduct_Category> GetData_Category_ByIDProduct(DataClassesDataContext db, int idProduct)
    {
        try
        {
            return Func_GetData_ByIDProduct_Category(db, idProduct).ToArray();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public TBProduct_Category GetDetail_Category(DataClassesDataContext db, int idProduct_Category)
    {
        try
        {
            return Func_GetDetail_Category(db, idProduct_Category);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    #endregion
    #endregion

    #region PHOTO
    #region AJAX
    public ReturnData AJAX_Insert_Photo(int idProduct, HttpPostedFile file)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                dynamic _product = Dynamic_GetDetail_Information(db, idProduct);
                if (_product == null)
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);

                List<TBProduct_Photo> _renameFile = GetData_Photo_ByIDProduct(db, idProduct).ToList();
                for (int i = 0; i < _renameFile.Count; i++)
                {
                    string oldPhoto = _renameFile[i].Photo;
                    _renameFile[i].Photo = ((string)_product.Name).ToLower().Replace(" ", "-") + "-" + (i + 1).ToString() + WITLibrary.ConvertCustom.GetExtention(oldPhoto);
                    if (OurClass.ImageExists(oldPhoto, "product"))
                        System.IO.File.Move(HttpContext.Current.Server.MapPath("~/assets/images/product/" + oldPhoto), HttpContext.Current.Server.MapPath("~/assets/images/product/" + _renameFile[i].Photo));
                }

                int fileSizeInBytes = file.ContentLength;
                string fileName = file.FileName;
                string fileExtension = System.IO.Path.GetExtension(fileName);

                TBProduct_Photo _newData = new TBProduct_Photo
                {
                    IDProduct = idProduct,
                    IsCover = (_renameFile.Where(x => x.IsCover).Count() == 0) ? true : false,
                    Photo = ((string)_product.Name).ToLower().Replace(" ", "-") + "-" + (_renameFile.Count() + 1).ToString() + fileExtension,
                    DateInsert = DateTime.Now,
                    DateLastUpdate = DateTime.Now,
                };

                db.TBProduct_Photos.InsertOnSubmit(_newData);
                db.SubmitChanges();
                if (_newData != null)
                {
                    file.SaveAs(HttpContext.Current.Server.MapPath("/assets/images/product/" + _newData.Photo));
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
    public ReturnData AJAX_ChangeCover_Photo(int idProduct_Photo)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                string _nameBefore = "";
                dynamic _changeData = Dynamic_GetDetail_Photo(db, idProduct_Photo);
                if (_changeData == null)
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);

                _nameBefore = _changeData.Photo;
                if (_changeData.IsCover)
                    return ReturnData.MessageFailed(_nameBefore + " already cover for this product.", null);

                foreach (TBProduct_Photo item in GetData_Photo_ByIDProduct(db, _changeData.IDProduct))
                {
                    if (item.IDProduct_Photo != _changeData.IDProduct_Photo)
                    {
                        if (item.IsCover)
                        {
                            item.IsCover = false;
                            item.DateLastUpdate = DateTime.Now;
                        }
                    }
                    else
                    {
                        item.IsCover = true;
                        item.DateLastUpdate = DateTime.Now;
                    }
                }
                db.SubmitChanges();
                Dictionary<string, dynamic> _result = new Dictionary<string, dynamic>();
                _result.Add("Photos", Dynamic_GetData_Photo_ByIDProduct(db, _changeData.IDProduct));
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
    public ReturnData AJAX_Delete_Photo(int idProduct_Photo)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                TBProduct_Photo _deleteData = GetDetail_Photo(db, idProduct_Photo);
                if (_deleteData == null)
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);
                string _nameBefore = _deleteData.Photo;
                if (_deleteData.IsCover)
                    return ReturnData.MessageFailed(_nameBefore + " failed to delete, because photo already cover for this product.", null);
                if (_deleteData.TBProduct_Combination_Photos.AsEnumerable().ToArray().Count() != 0)
                    return ReturnData.MessageFailed(_nameBefore + " failed to delete, because photo already use for some combination.", null);

                db.TBProduct_Photos.DeleteOnSubmit(_deleteData);
                db.SubmitChanges();

                OurClass.DeleteFile(_deleteData.Photo, "product");
                Dictionary<string, dynamic> _result = new Dictionary<string, dynamic>();
                _result.Add("Photos", Dynamic_GetData_Photo_ByIDProduct(db, _deleteData.IDProduct));
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
    #endregion

    #region DYNAMIC
    // FUNCTION
    private Func<DataClassesDataContext, IEnumerable<dynamic>> Dynamic_Func_GetAll_Photo
    {
        get
        {
            Func<DataClassesDataContext, IEnumerable<dynamic>> func =
              CompiledQuery.Compile<DataClassesDataContext, IEnumerable<dynamic>>
              ((DataClassesDataContext context) => context.FUNC_Product_Photo_GetAll()
                  .AsEnumerable().OrderByDescending(x => x.IsCover).ToArray());
            return func;
        }
    }
    private Func<DataClassesDataContext, int, IEnumerable<dynamic>> Dynamic_Func_GetData_ByIDProduct_Photo
    {
        get
        {
            Func<DataClassesDataContext, int, IEnumerable<dynamic>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, IEnumerable<dynamic>>
              ((DataClassesDataContext context, int idProduct) => context.FUNC_Product_Photo_GetData_ByIDProduct(idProduct)
                  .AsEnumerable().OrderByDescending(x => x.IsCover).ToArray());
            return func;
        }
    }
    private Func<DataClassesDataContext, int, IEnumerable<dynamic>> Dynamic_Func_GetDetail_Photo
    {
        get
        {
            Func<DataClassesDataContext, int, IEnumerable<dynamic>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, IEnumerable<dynamic>>
              ((DataClassesDataContext context, int idProduct_Photo) => context.FUNC_Product_Photo_GetDetail(idProduct_Photo)
                  .AsEnumerable().ToArray());
            return func;
        }
    }

    // DYNAMIC
    public IEnumerable<dynamic> Dynamic_GetAll_Photo(DataClassesDataContext db)
    {
        try
        {
            return Dynamic_Func_GetAll_Photo(db).AsEnumerable().Select(x => new
            {
                x.IDProduct_Photo,
                x.IDProduct,
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
    public IEnumerable<dynamic> Dynamic_GetData_Photo_ByIDProduct(DataClassesDataContext db, int idProduct)
    {
        try
        {
            return Dynamic_Func_GetData_ByIDProduct_Photo(db, idProduct).AsEnumerable().Select(x => new
            {
                x.IDProduct_Photo,
                x.IDProduct,
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
    public dynamic Dynamic_GetDetail_Photo(DataClassesDataContext db, int idProduct_Photo)
    {
        try
        {
            return Dynamic_Func_GetDetail_Photo(db, idProduct_Photo).AsEnumerable().Select(x => new
            {
                x.IDProduct_Photo,
                x.IDProduct,
                x.Photo,
                Preview = OurClass.ImageExists(x.Photo, "product") ? x.Photo : "noimage.jpg",
                x.IsCover
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
    // FUNCTION
    private Func<DataClassesDataContext, IEnumerable<TBProduct_Photo>> Func_GetAll_Photo
    {
        get
        {
            Func<DataClassesDataContext, IEnumerable<TBProduct_Photo>> func =
              CompiledQuery.Compile<DataClassesDataContext, IEnumerable<TBProduct_Photo>>
              ((DataClassesDataContext context) => context.TBProduct_Photos.AsEnumerable()
                .ToArray());
            return func;
        }
    }
    private Func<DataClassesDataContext, int, IEnumerable<TBProduct_Photo>> Func_GetData_ByIDProduct_Photo
    {
        get
        {
            Func<DataClassesDataContext, int, IEnumerable<TBProduct_Photo>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, IEnumerable<TBProduct_Photo>>
              ((DataClassesDataContext context, int idProduct) => context.TBProduct_Photos.AsEnumerable()
                .Where(x => x.IDProduct == idProduct).ToArray());
            return func;
        }
    }
    private Func<DataClassesDataContext, int, TBProduct_Photo> Func_GetDetail_Photo
    {
        get
        {
            Func<DataClassesDataContext, int, TBProduct_Photo> func =
              CompiledQuery.Compile<DataClassesDataContext, int, TBProduct_Photo>
              ((DataClassesDataContext context, int idProduct_Photo) => context.TBProduct_Photos.AsEnumerable()
                .Where(x => x.IDProduct_Photo == idProduct_Photo).FirstOrDefault());
            return func;
        }
    }

    // LINQ
    public IEnumerable<TBProduct_Photo> GetAll_Photo(DataClassesDataContext db)
    {
        try
        {
            return Func_GetAll_Photo(db);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public IEnumerable<TBProduct_Photo> GetData_Photo_ByIDProduct(DataClassesDataContext db, int idProduct)
    {
        try
        {
            return Func_GetData_ByIDProduct_Photo(db, idProduct);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public TBProduct_Photo GetDetail_Photo(DataClassesDataContext db, int idProduct_Photo)
    {
        try
        {
            return Func_GetDetail_Photo(db, idProduct_Photo);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    #endregion
    #endregion

    #region COMBINATION
    #region AJAX
    public ReturnData AJAX_Insert_Combination(int idProduct, string referenceCode, decimal basePrice, decimal impactPrice, decimal impactWeight, int quantity, int[] idProduct_Photos, int[] idValues)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                Class_Employee emp = new Class_Employee();
                var employee = emp.GetData_By_Token(HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()].Value);

                TBProduct _product = GetDetail(db, idProduct);
                if (_product == null)
                    return ReturnData.MessageFailed("Product does not exist.", null);

                if (idValues.Count() == 0)
                    return ReturnData.MessageFailed("No values selected.", null);
                //kombinasi wajib pilih foto ???
                //if (idProduct_Photos.Count() == 0)
                //    return ReturnData.MessageFailed("No photos selected.", null);

                TBProduct_Combination _productCombination = new TBProduct_Combination
                {
                    IDProduct = _product.IDProduct,
                    SequenceNumber = 0,
                    ReferenceCode = referenceCode,
                    BasePrice = basePrice,
                    PriceBeforeImpact = _product.Price,
                    ImpactPrice = impactPrice,
                    //Discount = _product.Discount,
                    Discount = 0,// feature discount combination masih disable
                    TypeDiscountPercent = _product.TypeDiscountPercent,
                    WeightBeforeImpact = _product.Weight,
                    ImpactWeight = impactWeight,
                    Quantity = quantity,
                    Deflag = false,
                    DateInsert = DateTime.Now,
                    DateLastUpdate = DateTime.Now
                };
                _productCombination.PriceAfterImpact = _productCombination.PriceBeforeImpact + _productCombination.ImpactPrice;
                _productCombination.TotalDiscount = _productCombination.TypeDiscountPercent ? _productCombination.PriceAfterImpact * _productCombination.Discount / 100 : _productCombination.Discount;
                _productCombination.Price = _productCombination.PriceAfterImpact - _productCombination.TotalDiscount;
                _productCombination.Weight = _productCombination.WeightBeforeImpact + _productCombination.ImpactWeight;


                Class_Value _value = new Class_Value();
                string _combinationName = "";
                foreach (int item in idValues)
                {
                    dynamic _item = _value.Dynamic_GetDetail(db, item);
                    if (_item == null)
                        return ReturnData.MessageFailed("Value does not exist.", null);

                    _productCombination.TBProduct_Combination_Details.Add(new TBProduct_Combination_Detail
                    {
                        IDValue = _item.IDValue,
                        NameAttribute = _item.AttributeName,
                        NameValue = _item.Name,
                        DateInsert = DateTime.Now
                    });
                    _combinationName += _item.AttributeName + " : " + _item.Name + ", ";
                    _productCombination.Name = _combinationName.Substring(0, _combinationName.Length - 2);
                }

                foreach (int item in idProduct_Photos)
                {
                    dynamic _item = Dynamic_GetDetail_Photo(db, item);
                    if (_item == null)
                        return ReturnData.MessageFailed("Photo does not exist.", null);

                    _productCombination.TBProduct_Combination_Photos.Add(new TBProduct_Combination_Photo
                    {
                        IDProduct_Photo = _item.IDProduct_Photo,
                        DateInsert = DateTime.Now
                    });
                }
                db.TBProduct_Combinations.InsertOnSubmit(_productCombination);

                db.SubmitChanges();
                //LOG STOCK
                Class_Log_Stock log = new Class_Log_Stock();
                log.Insert(employee.IDEmployee, _productCombination.IDProduct_Combination, _productCombination.Name, 0, quantity, quantity, "increase", "initial stock by " + employee.Name + "( " + employee.Email + " )");
                return ReturnData.MessageSuccess("Combination has been successfully inserted.", GetData_Combination_ByIDProduct(db, _productCombination.IDProduct));
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public ReturnData AJAX_Update_Combination(int idProduct_combination, int idProduct, string referenceCode, decimal basePrice, decimal impactPrice, decimal impactWeight, int quantity, int[] idProduct_Photos, int[] idValues)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {

                Class_Employee emp = new Class_Employee();
                var employee = emp.GetData_By_Token(HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()].Value);

                TBProduct _product = GetDetail(db, idProduct);
                if (_product == null)
                    return ReturnData.MessageFailed("Product does not exist.", null);

                if (idValues.Count() == 0)
                    return ReturnData.MessageFailed("No values selected.", null);

                TBProduct_Combination _productCombination = db.TBProduct_Combinations.Where(x => x.IDProduct_Combination == idProduct_combination).FirstOrDefault();
                if (_productCombination == null)
                    return ReturnData.MessageFailed("Data not found", null);

                //SAVE OLD QUANTITY FOR WISHLIST
                int oldQty = _productCombination.Quantity;

                _productCombination.ReferenceCode = referenceCode;
                _productCombination.BasePrice = basePrice;
                _productCombination.PriceBeforeImpact = _product.Price;
                _productCombination.ImpactPrice = impactPrice;
                //_productCombination.Discount = _product.Discount;
                _productCombination.Discount = 0; // feature discount combination masih disable
                _productCombination.TypeDiscountPercent = _product.TypeDiscountPercent;
                _productCombination.WeightBeforeImpact = _product.Weight;
                _productCombination.ImpactWeight = impactWeight;
                //INSERT LOG STOCK
                Class_Log_Stock log = new Class_Log_Stock();
                if (_productCombination.Quantity > quantity)
                    log.Insert(employee.IDEmployee, _productCombination.IDProduct_Combination, _productCombination.Name, _productCombination.Quantity, _productCombination.Quantity - quantity, quantity, "decrease", "Update Combination by " + employee.Name + "( " + employee.Email + " )");
                else if (_productCombination.Quantity < quantity)
                    log.Insert(employee.IDEmployee, _productCombination.IDProduct_Combination, _productCombination.Name, _productCombination.Quantity, quantity - _productCombination.Quantity, quantity, "increase", "Update Combination by " + employee.Name + "( " + employee.Email + " )");
                _productCombination.Quantity = quantity;
                _productCombination.DateLastUpdate = DateTime.Now;

                _productCombination.PriceAfterImpact = _productCombination.PriceBeforeImpact + _productCombination.ImpactPrice;
                _productCombination.TotalDiscount = _productCombination.TypeDiscountPercent ? _productCombination.PriceAfterImpact * _productCombination.Discount / 100 : _productCombination.Discount;
                _productCombination.Price = _productCombination.PriceAfterImpact - _productCombination.TotalDiscount;
                _productCombination.Weight = _productCombination.WeightBeforeImpact + _productCombination.ImpactWeight;

                //DELETE ALL VALUES AND PHOTOS
                db.TBProduct_Combination_Details.DeleteAllOnSubmit(_productCombination.TBProduct_Combination_Details);
                db.TBProduct_Combination_Photos.DeleteAllOnSubmit(_productCombination.TBProduct_Combination_Photos);

                Class_Value _value = new Class_Value();
                string _combinationName = "";
                foreach (int item in idValues)
                {
                    dynamic _item = _value.Dynamic_GetDetail(db, item);
                    if (_item == null)
                        return ReturnData.MessageFailed("Value does not exist.", null);

                    _productCombination.TBProduct_Combination_Details.Add(new TBProduct_Combination_Detail
                    {
                        IDValue = _item.IDValue,
                        NameAttribute = _item.AttributeName,
                        NameValue = _item.Name,
                        DateInsert = DateTime.Now
                    });
                    _combinationName += _item.AttributeName + " : " + _item.Name + ", ";
                    _productCombination.Name = _combinationName.Substring(0, _combinationName.Length - 2);
                }

                foreach (int item in idProduct_Photos)
                {
                    dynamic _item = Dynamic_GetDetail_Photo(db, item);
                    if (_item == null)
                        return ReturnData.MessageFailed("Photo does not exist.", null);

                    _productCombination.TBProduct_Combination_Photos.Add(new TBProduct_Combination_Photo
                    {
                        IDProduct_Photo = _item.IDProduct_Photo,
                        DateInsert = DateTime.Now
                    });
                }

                db.SubmitChanges();

                if (oldQty < quantity)
                {
                    List<string> emailList = new List<string>();
                    var wishlist = db.TBWishlists.Where(x => x.IDProduct_Combination == idProduct_combination);
                    foreach (var item in wishlist)
                    {
                        emailList.Add(item.Email);
                    }
                    Class_Configuration _config = new Class_Configuration();
                    var emailLogo = _config.Dynamic_Get_EmailLogo();
                    //SEND EMAIL TO CUSTOMER WISHLIST
                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/template-email-wishlist.html")))
                    {
                        string body = "";
                        body = reader.ReadToEnd();
                        body = body.Replace("{ProductName}", _product.Name + " - " + _productCombination.Name);
                        body = body.Replace("{IDProduct}", _product.IDProduct.ToString());
                        body = body.Replace("{title}", System.Configuration.ConfigurationManager.AppSettings["Title"]);
                        body = body.Replace("{email_logo}", System.Configuration.ConfigurationManager.AppSettings["url"] + "/assets/images/email_logo/" + emailLogo);
                        body = body.Replace("{shop_url}", System.Configuration.ConfigurationManager.AppSettings["url"]);
                        OurClass.SendMultipleEmail(emailList, System.Web.Configuration.WebConfigurationManager.AppSettings["Title"] + " - Wishlist Notification", body, "");
                    }
                }

                return ReturnData.MessageSuccess("Combination succesfully updated", GetData_Combination_ByIDProduct(db, idProduct));
            }
        }
        catch (Exception ex)
        {
            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public ReturnData AJAX_Delete_Combination(int idProduct_combination)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBProduct_Combination data = db.TBProduct_Combinations.Where(x => !x.Deflag && x.IDProduct_Combination == idProduct_combination).FirstOrDefault();
            if (data == null)
                return ReturnData.MessageFailed("Data not found", null);
            else
            {
                data.Deflag = true;
                data.DateLastUpdate = DateTime.Now;
                db.SubmitChanges();
                return ReturnData.MessageSuccess("Combination '" + data.Name + "' successfully deleted", this.GetData_Combination_ByIDProduct(db, data.IDProduct));
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public ReturnData AJAX_Preload_Update_Combination(int idProduct_combination)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                //Dictionary<string, dynamic> _result = new Dictionary<string, dynamic>();
                TBProduct_Combination data = db.TBProduct_Combinations.Where(x => !x.Deflag && x.IDProduct_Combination == idProduct_combination).FirstOrDefault();
                if (data == null)
                    return ReturnData.MessageFailed("Data not found", null);
                else
                {
                    Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
                    result.Add("IDProduct_Combination", data.IDProduct_Combination);
                    result.Add("ReferenceCode", data.ReferenceCode);
                    result.Add("Discount", data.Discount);
                    result.Add("TypeDiscountPercent", data.TypeDiscountPercent);
                    result.Add("PriceBeforeImpact", data.PriceBeforeImpact);
                    result.Add("ImpactPrice", data.ImpactPrice);
                    result.Add("Price", data.Price);
                    result.Add("BasePrice", data.BasePrice);
                    result.Add("Quantity", data.Quantity);
                    result.Add("Weight", data.Weight);
                    result.Add("ImpactWeight", data.ImpactWeight);
                    result.Add("WeightBeforeImpact", data.WeightBeforeImpact);
                    result.Add("Photos", GetIDPhotos(db, data));
                    result.Add("Values", GetIDValues(db, data));
                    return ReturnData.MessageSuccess("OK", result);
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

    public ReturnData AJAX_Update_Quantity(int idProduct_combination, int qty)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                //Dictionary<string, dynamic> _result = new Dictionary<string, dynamic>();
                TBProduct_Combination data = db.TBProduct_Combinations.Where(x => !x.Deflag && x.IDProduct_Combination == idProduct_combination).FirstOrDefault();

                Class_Employee emp = new Class_Employee();
                var employee = emp.GetData_By_Token(HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()].Value);

                if (data == null)
                    return ReturnData.MessageFailed("Data not found", false);
                else
                {
                    int oldQty = data.Quantity;

                    data.Quantity = qty;
                    db.SubmitChanges();

                    //LOG STOCK
                    //INSERT LOG STOCK
                    Class_Log_Stock log = new Class_Log_Stock();

                    if (oldQty > qty)
                        log.Insert(employee.IDEmployee, data.IDProduct_Combination, data.TBProduct.Name + " - " + data.Name, oldQty, data.Quantity, oldQty - data.Quantity, "decrease", "Update Stock by " + employee.Name + "( " + employee.Email + " )");
                    else if (oldQty < qty)
                        log.Insert(employee.IDEmployee, data.IDProduct_Combination, data.TBProduct.Name + " - " + data.Name, oldQty, data.Quantity, data.Quantity - oldQty, "increase", "Update Stock by " + employee.Name + "( " + employee.Email + " )");

                    return ReturnData.MessageSuccess("Quantity successfully updated", true);
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

    public ReturnData AJAX_Save_Quantity(List<int> idProduct_combination, int qty)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                foreach (var idCom in idProduct_combination)
                {
                    //Dictionary<string, dynamic> _result = new Dictionary<string, dynamic>();
                    TBProduct_Combination data = db.TBProduct_Combinations.Where(x => !x.Deflag && x.IDProduct_Combination == idCom).FirstOrDefault();

                    Class_Employee emp = new Class_Employee();
                    var employee = emp.GetData_By_Token(HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()].Value);

                    if (data == null)
                        return ReturnData.MessageFailed("Data not found", null);
                    else
                    {
                        int oldQty = data.Quantity;

                        data.Quantity = qty;
                        db.SubmitChanges();

                        //LOG STOCK
                        //INSERT LOG STOCK
                        Class_Log_Stock log = new Class_Log_Stock();

                        if (oldQty > qty)
                            log.Insert(employee.IDEmployee, data.IDProduct_Combination, data.TBProduct.Name + " - " + data.Name, oldQty, data.Quantity, oldQty - data.Quantity, "decrease", "Update Stock by " + employee.Name + "( " + employee.Email + " )");
                        else if (oldQty < qty)
                            log.Insert(employee.IDEmployee, data.IDProduct_Combination, data.TBProduct.Name + " - " + data.Name, oldQty, data.Quantity, data.Quantity - oldQty, "increase", "Update Stock by " + employee.Name + "( " + employee.Email + " )");

                        
                    }
                }
                return ReturnData.MessageSuccess("Quantity successfully updated", null);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    private dynamic[] GetIDValues(DataClassesDataContext db, TBProduct_Combination data)
    {
        List<Dictionary<string, dynamic>> result = new List<Dictionary<string, dynamic>>();
        Dictionary<string, dynamic> _item;
        foreach (var item in data.TBProduct_Combination_Details)
        {
            _item = new Dictionary<string, dynamic>();
            _item.Add("IDValue", item.IDValue);
            _item.Add("Name", item.NameAttribute + " : " + item.NameValue);
            result.Add(_item);
        }
        return result.ToArray();
    }

    private int[] GetIDPhotos(DataClassesDataContext db, TBProduct_Combination data)
    {
        List<int> result = new List<int>();
        foreach (var item in data.TBProduct_Combination_Photos)
        {
            result.Add(item.IDProduct_Photo);
        }
        return result.ToArray();
    }
    #endregion

    #region LINQ
    // FUNCTION
    private Func<DataClassesDataContext, IEnumerable<TBProduct_Combination>> Func_GetAll_Combination
    {
        get
        {
            Func<DataClassesDataContext, IEnumerable<TBProduct_Combination>> func =
              CompiledQuery.Compile<DataClassesDataContext, IEnumerable<TBProduct_Combination>>
              ((DataClassesDataContext context) => context.TBProduct_Combinations.AsEnumerable()
                .Where(x => !x.Deflag).ToArray());
            return func;
        }
    }
    private Func<DataClassesDataContext, int, IEnumerable<TBProduct_Combination>> Func_GetData_ByIDProduct_Combination
    {
        get
        {
            Func<DataClassesDataContext, int, IEnumerable<TBProduct_Combination>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, IEnumerable<TBProduct_Combination>>
              ((DataClassesDataContext context, int idProduct) => context.TBProduct_Combinations.AsEnumerable()
                .Where(x => x.IDProduct == idProduct && !x.Deflag).ToArray());
            return func;
        }
    }
    private Func<DataClassesDataContext, int, TBProduct_Combination> Func_GetDetail_Combination
    {
        get
        {
            Func<DataClassesDataContext, int, TBProduct_Combination> func =
              CompiledQuery.Compile<DataClassesDataContext, int, TBProduct_Combination>
              ((DataClassesDataContext context, int idProduct_Combination) => context.TBProduct_Combinations.AsEnumerable()
                .Where(x => x.IDProduct_Combination == idProduct_Combination && !x.Deflag).FirstOrDefault());
            return func;
        }
    }

    // LINQ
    public IEnumerable<TBProduct_Combination> GetAll_Combination(DataClassesDataContext db)
    {
        try
        {
            return Func_GetAll_Combination(db).ToArray();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public dynamic GetData_Quantity_ByIDCombination(DataClassesDataContext db, int idCombination)
    {
        try
        {
            using (db)
            {
                return db.FUNC_Product_Combination_GetDetail_ByIDCombination(idCombination).FirstOrDefault();
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public dynamic GetData_Combination_ByIDProduct(DataClassesDataContext db, int idProduct)
    {
        try
        {
            DataClassesDataContext _db = new DataClassesDataContext();
            return _db.TBProduct_Combinations.Where(x => !x.Deflag && x.IDProduct == idProduct).Select(x => new
            {
                x.IDProduct_Combination,
                x.Name,
                x.SequenceNumber,
                x.ReferenceCode,
                x.BasePrice,
                x.PriceBeforeImpact,
                x.ImpactPrice,
                x.PriceAfterImpact,
                x.Discount,
                x.TypeDiscountPercent,
                x.TotalDiscount,
                x.Price,
                x.Weight,
                x.ImpactWeight,
                x.Quantity
            }).ToArray();
            //return Func_GetData_ByIDProduct_Combination(db, idProduct).ToArray();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    public TBProduct_Combination GetDetail_Combination(DataClassesDataContext db, int idProduct_Combination)
    {
        try
        {
            return Func_GetDetail_Combination(db, idProduct_Combination);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
    #endregion
    #endregion

    #region FRONTEND
    public ReturnData AJAX_FE_GetAll(int take)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            {
                Class_Product_Photo _photo = new Class_Product_Photo();

                if (take == 0)
                {
                    return ReturnData.MessageSuccess("OK", db.FUNC_Product_GetAll().OrderByDescending(x => x.Quantity).Select(x => new
                    {
                        x.IDProduct,
                        x.Name,
                        x.Photo,
                        x.Discount,
                        Price = GetPrice(db, x.IDProduct)["Price"],
                        PriceBeforeDiscount = GetPrice(db, x.IDProduct)["PriceBeforeDiscount"],
                        x.Category,
                        x.Quantity
                    }));
                }
                else
                {
                    return ReturnData.MessageSuccess("OK", db.FUNC_Product_GetAll().OrderByDescending(x => x.Quantity).Select(x => new
                    {
                        x.IDProduct,
                        x.Name,
                        x.Photo,
                        x.Discount,
                        Price = GetPrice(db, x.IDProduct)["Price"],
                        PriceBeforeDiscount = GetPrice(db, x.IDProduct)["PriceBeforeDiscount"],
                        x.Category,
                        x.Quantity
                    }).Take(take));
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

    //public ReturnData AJAX_FE_GetAll_Paging(int currentPage)
    //{
    //    try
    //    {
    //        DataClassesDataContext db = new DataClassesDataContext();
    //        {
    //            Class_Product_Photo _photo = new Class_Product_Photo();
    //            int productPerPage = int.Parse(ConfigurationManager.AppSettings["ProductPerPage"]);
    //            int take = productPerPage;

    //            int _skipCounter = (currentPage - 1) * productPerPage;
    //            int _totalPage = (int)Math.Ceiling((double)db.FUNC_Product_GetAll().Count() / (double)productPerPage);

    //            int _startPage = 0;
    //            int _endPage = 0;

    //            _startPage = currentPage - 10;
    //            _endPage = currentPage + 10;

    //            if (_startPage <= 0)
    //            {
    //                _endPage -= (_startPage - 1);
    //                _startPage = 1;
    //            }

    //            if (_endPage > _totalPage)
    //                _endPage = _totalPage;

    //            if (take == 0)
    //            {
    //                return ReturnData.MessageSuccess("OK", db.FUNC_Product_GetAll().OrderBy(x => x.SequenceNumber).OrderByDescending(x => x.IDProduct).Skip(_skipCounter).Select(x => new
    //                {
    //                    x.IDProduct,
    //                    x.Name,
    //                    x.Photo,
    //                    x.Discount,
    //                    x.Price,
    //                    x.PriceBeforeDiscount,
    //                    x.Category,
    //                    x.Quantity,
    //                    StartPage = _startPage,
    //                    EndPage = _endPage,
    //                    TotalPage = _totalPage,
    //                    CurrentPage = currentPage
    //                }));
    //            }
    //            else
    //            {
    //                return ReturnData.MessageSuccess("OK", db.FUNC_Product_GetAll().OrderBy(x => x.SequenceNumber).OrderByDescending(x => x.IDProduct).Skip(_skipCounter).Select(x => new
    //                {
    //                    x.IDProduct,
    //                    x.Name,
    //                    x.Photo,
    //                    x.Discount,
    //                    x.Price,
    //                    x.PriceBeforeDiscount,
    //                    x.Category,
    //                    x.Quantity,
    //                    StartPage = _startPage,
    //                    EndPage = _endPage,
    //                    TotalPage = _totalPage,
    //                    CurrentPage = currentPage
    //                }).Take(take));
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        return ReturnData.MessageFailed(ex.Message, null);
    //    }
    //}

    public ReturnData AJAX_FE_GetAll_Paging(int currentPage)
    {
        try
        {
            int idCurrency;
            DataClassesDataContext db = new DataClassesDataContext();
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCurrency"]];
                if (cookie == null)
                    idCurrency = 1;
                else
                {
                    int _idCurrency = 0;
                    int.TryParse(cookie.Value.ToString(), out _idCurrency);
                    idCurrency = _idCurrency;
                }

                Class_Product_Photo _photo = new Class_Product_Photo();
                int productPerPage = int.Parse(ConfigurationManager.AppSettings["ProductPerPage"]);
                int take = productPerPage;

                int _skipCounter = (currentPage - 1) * productPerPage;
                int _totalPage = (int)Math.Ceiling((double)db.FUNC_Product_GetAll().Count() / (double)productPerPage);

                int _startPage = 0;
                int _endPage = 0;

                _startPage = currentPage - 10;
                _endPage = currentPage + 10;

                if (_startPage <= 0)
                {
                    _endPage -= (_startPage - 1);
                    _startPage = 1;
                }

                if (_endPage > _totalPage)
                    _endPage = _totalPage;

                if (take == 0)
                {
                    return ReturnData.MessageSuccess("OK", db.FUNC_Product_GetAll().OrderByDescending(x => x.Quantity).Skip(_skipCounter).Select(x => new
                    {
                        x.IDProduct,
                        x.Name,
                        x.Photo,
                        x.Discount,
                        Price = GetPrice(db, x.IDProduct)["Price"],
                        PriceBeforeDiscount = GetPrice(db, x.IDProduct)["PriceBeforeDiscount"],
                        x.Category,
                        x.Quantity,
                        x.Description,
                        x.ShortDescription,
                        x.IDCombination,
                        x.CombinationName,
                        x.CombinationPrice,
                        Combination = GetData_Combination_ByIDProduct(db, x.IDProduct),
                        x.Note,
                        StartPage = _startPage,
                        EndPage = _endPage,
                        TotalPage = _totalPage,
                        CurrentPage = currentPage
                    }).GroupBy(x => x.IDProduct).SelectMany(x => x.Take(1)));
                }
                else
                {
                    return ReturnData.MessageSuccess("OK", db.FUNC_Product_GetAll().OrderByDescending(x => x.Quantity).Skip(_skipCounter).Select(x => new
                    {
                        x.IDProduct,
                        x.Name,
                        x.Photo,
                        x.Discount,
                        Price = GetPrice(db, x.IDProduct)["Price"],
                        PriceBeforeDiscount = GetPrice(db, x.IDProduct)["PriceBeforeDiscount"],
                        x.Category,
                        x.Quantity,
                        x.Description,
                        x.ShortDescription,
                        x.Note,
                        x.IDCombination,
                        x.CombinationName,
                        x.CombinationPrice,
                        Combination = GetData_Combination_ByIDProduct(db, x.IDProduct),
                        StartPage = _startPage,
                        EndPage = _endPage,
                        TotalPage = _totalPage,
                        CurrentPage = currentPage
                    }).GroupBy(x => x.IDProduct).SelectMany(x => x.Take(1)).Take(take));
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

    public dynamic Dynamic_GetData_ByIDManufacturer_Paging(int idManufacturer, int currentPage)
    {
        #region obselete
        //try
        //{
        //    DataClassesDataContext db = new DataClassesDataContext();
        //    var data = Dynamic_Func_GetAll(db).AsEnumerable().Where(x => x.IDManufacturer == idManufacturer).OrderByDescending(x => x.SequenceNumber).Select(x => new
        //    {
        //        x.IDProduct,
        //        x.Name,
        //        x.ReferenceCode,
        //        x.PriceBeforeDiscount,
        //        x.Price,
        //        x.Quantity,
        //        x.Active,
        //        x.IDManufacturer,
        //        x.SequenceNumber,
        //        x.TotalDiscount,
        //        x.TypeDiscountPercent,
        //        x.Discount,
        //        x.Weight,
        //        x.ShortDescription,
        //        x.Description,
        //        x.Note,
        //        x.Meta,
        //        x.MetaDescription,
        //        x.MetaKeyword,
        //        Photo = OurClass.ImageExists(x.Photo, "product") ? x.Photo : "noimage.jpg",
        //        x.Manufacturer,
        //        x.Category
        //    });

        //    return data;
        //}
        //catch (Exception ex)
        //{
        //    return null;
        //}
        #endregion
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            {
                Class_Product_Photo _photo = new Class_Product_Photo();
                int productPerPage = int.Parse(ConfigurationManager.AppSettings["ProductPerPage"]);
                int take = productPerPage;

                int _skipCounter = (currentPage - 1) * productPerPage;
                int _totalPage = (int)Math.Ceiling((double)db.FUNC_Product_GetAll().Where(x => x.IDManufacturer == idManufacturer).Count() / (double)productPerPage);

                int _startPage = 0;
                int _endPage = 0;

                _startPage = currentPage - 10;
                _endPage = currentPage + 10;

                if (_startPage <= 0)
                {
                    _endPage -= (_startPage - 1);
                    _startPage = 1;
                }

                if (_endPage > _totalPage)
                    _endPage = _totalPage;

                if (take == 0)
                {
                    return ReturnData.MessageSuccess("OK", db.FUNC_Product_GetAll().Where(x => x.IDManufacturer == idManufacturer).OrderByDescending(x => x.Quantity).Skip(_skipCounter).Select(x => new
                    {
                        x.IDProduct,
                        x.Name,
                        x.Photo,
                        x.Discount,
                        Price = GetPrice(db, x.IDProduct)["Price"],
                        PriceBeforeDiscount = GetPrice(db, x.IDProduct)["PriceBeforeDiscount"],
                        x.Category,
                        x.Quantity,
                        StartPage = _startPage,
                        EndPage = _endPage,
                        TotalPage = _totalPage,
                        CurrentPage = currentPage
                    }).GroupBy(x => x.IDProduct).SelectMany(x => x.Take(1)));
                }
                else
                {
                    return ReturnData.MessageSuccess("OK", db.FUNC_Product_GetAll().Where(x => x.IDManufacturer == idManufacturer).OrderByDescending(x => x.Quantity).Skip(_skipCounter).Select(x => new
                    {
                        x.IDProduct,
                        x.Name,
                        x.Photo,
                        x.Discount,
                        Price = GetPrice(db, x.IDProduct)["Price"],
                        PriceBeforeDiscount = GetPrice(db, x.IDProduct)["PriceBeforeDiscount"],
                        x.Category,
                        x.Quantity,
                        StartPage = _startPage,
                        EndPage = _endPage,
                        TotalPage = _totalPage,
                        CurrentPage = currentPage
                    }).GroupBy(x => x.IDProduct).SelectMany(x => x.Take(1)).Take(take));
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

    public dynamic Dynamic_GetData_ByIDCategory_And_PriceRange_Paging(List<int> idCategory, List<decimal> priceRange, int currentPage)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            List<TBProduct> listProduct = new List<TBProduct>();
            int productPerPage = int.Parse(ConfigurationManager.AppSettings["ProductPerPage"]);
            int take = productPerPage;

            int _skipCounter = (currentPage - 1) * productPerPage;
            int _totalPage = 0;

            int _startPage = 0;
            int _endPage = 0;

            if (take == 0)
            {
                if (idCategory.Count != 0)
                {
                    List<TBProduct_Category> productCategory = new List<TBProduct_Category>();
                    foreach (int id in idCategory)
                    {
                        List<TBProduct_Category> items = db.TBProduct_Categories.Where(x => x.IDCategory == id).ToList();

                        if (items.Count > 0)
                        {
                            foreach (var item in items)
                            {
                                if (productCategory.Where(x => x.IDProduct == item.IDProduct).FirstOrDefault() == null)
                                {
                                    productCategory.Add(item);
                                }
                            }
                        }
                    }
                    //List<TBProduct_Combination_Detail> detail = db.TBProduct_Combination_Details.Where(x => x.TBProduct_Combination.TBProduct.Price > priceRange[0] && x.TBProduct_Combination.TBProduct.Price < priceRange[1] && x.IDValue == id && !x.TBProduct_Combination.Deflag && !x.TBProduct_Combination.TBProduct.Deflag && x.TBProduct_Combination.TBProduct.Active).ToList();
                    foreach (var item in productCategory)
                    {
                        listProduct.Add(item.TBProduct);
                    }
                    listProduct = listProduct.Where(x => x.Price > priceRange[0] && x.Price < priceRange[1] && !x.Deflag && x.Active).ToList();
                    _totalPage = (int)Math.Ceiling((double)listProduct.Where(x => x.Price > priceRange[0] && x.Price < priceRange[1] && !x.Deflag).Count() / (double)productPerPage);
                }
                else
                {
                    List<TBProduct> detail = db.TBProducts.Where(x => x.Price > priceRange[0] && x.Price < priceRange[1] && !x.Deflag && x.Active).ToList();
                    _totalPage = (int)Math.Ceiling((double)db.TBProducts.Where(x => x.Price > priceRange[0] && x.Price < priceRange[1] && !x.Deflag).Count() / (double)productPerPage);
                    foreach (var item in detail)
                    {
                        listProduct.Add(item);
                    }
                }

                _startPage = currentPage - 10;
                _endPage = currentPage + 10;

                if (_startPage <= 0)
                {
                    _endPage -= (_startPage - 1);
                    _startPage = 1;
                }

                if (_endPage > _totalPage)
                    _endPage = _totalPage;

                var data = listProduct.AsEnumerable().OrderByDescending(x => x.SequenceNumber).Skip(_skipCounter).Select(x => new
                {
                    x.IDProduct,
                    x.Name,
                    x.PriceBeforeDiscount,
                    x.Price,
                    x.Discount,
                    x.TotalDiscount,
                    Quantity = x.TBProduct_Combinations.Where(y => y.IDProduct == x.IDProduct).Sum(y => y.Quantity) == null ? 0 : x.TBProduct_Combinations.Where(y => y.IDProduct == x.IDProduct).Sum(y => y.Quantity),
                    Photo = OurClass.ImageExists(x.TBProduct_Photos.Where(y => y.IsCover).FirstOrDefault().Photo, "product") ? x.TBProduct_Photos.Where(y => y.IsCover).FirstOrDefault().Photo : "noimage.jpg",
                    Category = x.TBProduct_Categories.Where(c => c.IsDefault).FirstOrDefault().TBCategory.Name,
                    StartPage = _startPage,
                    EndPage = _endPage,
                    TotalPage = _totalPage,
                    CurrentPage = currentPage
                });

                return data;
            }
            else
            {
                if (idCategory.Count != 0)
                {
                    List<TBProduct_Category> productCategory = new List<TBProduct_Category>();
                    foreach (int id in idCategory)
                    {
                        List<TBProduct_Category> items = db.TBProduct_Categories.Where(x => x.IDCategory == id).ToList();

                        if (items.Count > 0)
                        {
                            foreach (var item in items)
                            {
                                if (productCategory.Where(x => x.IDProduct == item.IDProduct).FirstOrDefault() == null)
                                {
                                    productCategory.Add(item);
                                }
                            }
                        }
                    }
                    //List<TBProduct_Combination_Detail> detail = db.TBProduct_Combination_Details.Where(x => x.TBProduct_Combination.TBProduct.Price > priceRange[0] && x.TBProduct_Combination.TBProduct.Price < priceRange[1] && x.IDValue == id && !x.TBProduct_Combination.Deflag && !x.TBProduct_Combination.TBProduct.Deflag && x.TBProduct_Combination.TBProduct.Active).ToList();
                    foreach (var item in productCategory)
                    {
                        listProduct.Add(item.TBProduct);
                    }
                    listProduct = listProduct.Where(x => x.Price > priceRange[0] && x.Price < priceRange[1] && !x.Deflag && x.Active).ToList();
                    _totalPage = (int)Math.Ceiling((double)listProduct.Where(x => x.Price > priceRange[0] && x.Price < priceRange[1] && !x.Deflag).Count() / (double)productPerPage);
                }
                else
                {
                    List<TBProduct> detail = db.TBProducts.Where(x => x.Price > priceRange[0] && x.Price < priceRange[1] && !x.Deflag && x.Active).ToList();
                    _totalPage = (int)Math.Ceiling((double)db.TBProducts.Where(x => x.Price > priceRange[0] && x.Price < priceRange[1] && !x.Deflag).Count() / (double)productPerPage);
                    foreach (var item in detail)
                    {
                        listProduct.Add(item);
                    }
                }

                _startPage = currentPage - 10;
                _endPage = currentPage + 10;

                if (_startPage <= 0)
                {
                    _endPage -= (_startPage - 1);
                    _startPage = 1;
                }

                if (_endPage > _totalPage)
                    _endPage = _totalPage;

                var data = listProduct.AsEnumerable().OrderByDescending(x => x.SequenceNumber).Select(x => new
                {
                    x.IDProduct,
                    x.Name,
                    x.PriceBeforeDiscount,
                    x.Price,
                    x.Discount,
                    x.TotalDiscount,
                    Quantity = x.TBProduct_Combinations.Where(y => y.IDProduct == x.IDProduct).Sum(y => y.Quantity) == null ? 0 : x.TBProduct_Combinations.Where(y => y.IDProduct == x.IDProduct).Sum(y => y.Quantity),
                    Photo = OurClass.ImageExists(x.TBProduct_Photos.Where(y => y.IsCover).FirstOrDefault().Photo, "product") ? x.TBProduct_Photos.Where(y => y.IsCover).FirstOrDefault().Photo : "noimage.jpg",
                    Category = x.TBProduct_Categories.Where(c => c.IsDefault).FirstOrDefault().TBCategory.Name,
                    StartPage = _startPage,
                    EndPage = _endPage,
                    TotalPage = _totalPage,
                    CurrentPage = currentPage
                }).Take(take);

                return data;
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_GetData_ByIDCategory_Paging(int idCategory, int currentPage)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            int productPerPage = int.Parse(ConfigurationManager.AppSettings["ProductPerPage"]);
            int take = productPerPage;

            int _skipCounter = (currentPage - 1) * productPerPage;
            int _totalPage = (int)Math.Ceiling((double)Dynamic_Func_GetAll_ByIDCategory(db, idCategory).Count() / (double)productPerPage);

            int _startPage = 0;
            int _endPage = 0;

            _startPage = currentPage - 10;
            _endPage = currentPage + 10;

            if (_startPage <= 0)
            {
                _endPage -= (_startPage - 1);
                _startPage = 1;
            }

            if (_endPage > _totalPage)
                _endPage = _totalPage;

            if (take == 0)
            {
                var data = Dynamic_Func_Get_ByIDCategory_Page(db, idCategory, take, currentPage).AsEnumerable().OrderByDescending(x => x.Quantity).Select(x => new
                {
                    x.IDProduct,
                    x.Name,
                    PriceBeforeDiscount = GetPrice(db, x.IDProduct)["PriceBeforeDiscount"],
                    Price = GetPrice(db, x.IDProduct)["Price"],
                    x.TotalDiscount,
                    x.Discount,
                    Photo = OurClass.ImageExists(x.Photo, "product") ? x.Photo : "noimage.jpg",
                    x.Category,
                    x.Quantity,
                    x.Description,
                    x.ShortDescription,
                    x.Note,
                    x.IDCombination,
                    x.CombinationName,
                    x.CombinationPrice,
                    StartPage = _startPage,
                    EndPage = _endPage,
                    TotalPage = _totalPage,
                    CurrentPage = currentPage
                });

                return data;
            }
            else
            {
                var data = Dynamic_Func_Get_ByIDCategory_Page(db, idCategory, take, currentPage).AsEnumerable().OrderByDescending(x => x.Quantity).Select(x => new
                {
                    x.IDProduct,
                    x.Name,
                    PriceBeforeDiscount = GetPrice(db, x.IDProduct)["PriceBeforeDiscount"],
                    Price = GetPrice(db, x.IDProduct)["Price"],
                    x.TotalDiscount,
                    x.Discount,
                    Photo = OurClass.ImageExists(x.Photo, "product") ? x.Photo : "noimage.jpg",
                    x.Category,
                    x.Quantity,
                    x.Description,
                    x.ShortDescription,
                    x.Note,
                    x.IDCombination,
                    x.CombinationName,
                    x.CombinationPrice,
                    StartPage = _startPage,
                    EndPage = _endPage,
                    TotalPage = _totalPage,
                    CurrentPage = currentPage
                }).Take(take);

                return data;
            }

        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_GetData_ByIDValue_And_PriceRange_Paging(List<int> idValue, List<decimal> priceRange, int currentPage)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            List<TBProduct> listProduct = new List<TBProduct>();
            int productPerPage = int.Parse(ConfigurationManager.AppSettings["ProductPerPage"]);
            int take = productPerPage;

            int _skipCounter = (currentPage - 1) * productPerPage;
            int _totalPage = 0;

            int _startPage = 0;
            int _endPage = 0;

            if (take == 0)
            {
                if (idValue.Count != 0)
                {
                    foreach (int id in idValue)
                    {
                        List<TBProduct_Combination_Detail> detail = db.TBProduct_Combination_Details.Where(x => x.TBProduct_Combination.TBProduct.Price > priceRange[0] && x.TBProduct_Combination.TBProduct.Price < priceRange[1] && x.IDValue == id && !x.TBProduct_Combination.Deflag && !x.TBProduct_Combination.TBProduct.Deflag && x.TBProduct_Combination.TBProduct.Active).Distinct().ToList();
                        _totalPage = (int)Math.Ceiling((double)db.TBProduct_Combination_Details.Where(x => x.TBProduct_Combination.TBProduct.Price > priceRange[0] && x.TBProduct_Combination.TBProduct.Price < priceRange[1] && x.IDValue == id && !x.TBProduct_Combination.Deflag && !x.TBProduct_Combination.TBProduct.Deflag).Count() / (double)productPerPage);
                        foreach (var item in detail)
                        {
                            listProduct.Add(item.TBProduct_Combination.TBProduct);
                        }
                    }
                }
                else
                {
                    List<TBProduct> detail = db.TBProducts.Where(x => x.Price > priceRange[0] && x.Price < priceRange[1] && !x.Deflag && x.Active).Distinct().ToList();
                    _totalPage = (int)Math.Ceiling((double)db.TBProducts.Where(x => x.Price > priceRange[0] && x.Price < priceRange[1] && !x.Deflag).Count() / (double)productPerPage);
                    foreach (var item in detail)
                    {
                        listProduct.Add(item);
                    }
                }

                _startPage = currentPage - 10;
                _endPage = currentPage + 10;

                if (_startPage <= 0)
                {
                    _endPage -= (_startPage - 1);
                    _startPage = 1;
                }

                if (_endPage > _totalPage)
                    _endPage = _totalPage;

                var data = listProduct.AsEnumerable().OrderByDescending(x => x.TBProduct_Combinations.Where(y => y.IDProduct == x.IDProduct).Sum(y => y.Quantity)).Skip(_skipCounter).Select(x => new
                {
                    x.IDProduct,
                    x.Name,
                    PriceBeforeDiscount = GetPrice(db, x.IDProduct)["PriceBeforeDiscount"],
                    Price = GetPrice(db, x.IDProduct)["Price"],
                    x.Discount,
                    x.TotalDiscount,
                    Quantity = x.TBProduct_Combinations.Where(y => y.IDProduct == x.IDProduct).Sum(y => y.Quantity) == null ? 0 : x.TBProduct_Combinations.Where(y => y.IDProduct == x.IDProduct).Sum(y => y.Quantity),
                    Photo = OurClass.ImageExists(x.TBProduct_Photos.Where(y => y.IsCover).FirstOrDefault().Photo, "product") ? x.TBProduct_Photos.Where(y => y.IsCover).FirstOrDefault().Photo : "noimage.jpg",
                    Category = x.TBProduct_Categories.Where(c => c.IsDefault).FirstOrDefault().TBCategory.Name,
                    StartPage = _startPage,
                    EndPage = _endPage,
                    TotalPage = _totalPage,
                    CurrentPage = currentPage
                }).GroupBy(x => x.IDProduct).SelectMany(x => x.Take(1));

                return data;
            }
            else
            {
                if (idValue.Count != 0)
                {
                    foreach (int id in idValue)
                    {
                        List<TBProduct_Combination_Detail> detail = db.TBProduct_Combination_Details.Where(x => x.TBProduct_Combination.TBProduct.Price > priceRange[0] && x.TBProduct_Combination.TBProduct.Price < priceRange[1] && x.IDValue == id && !x.TBProduct_Combination.Deflag && !x.TBProduct_Combination.TBProduct.Deflag && x.TBProduct_Combination.TBProduct.Active).ToList();
                        _totalPage = (int)Math.Ceiling((double)db.TBProduct_Combination_Details.Where(x => x.TBProduct_Combination.TBProduct.Price > priceRange[0] && x.TBProduct_Combination.TBProduct.Price < priceRange[1] && x.IDValue == id && !x.TBProduct_Combination.Deflag && !x.TBProduct_Combination.TBProduct.Deflag).Count() / (double)productPerPage);
                        foreach (var item in detail)
                        {
                            listProduct.Add(item.TBProduct_Combination.TBProduct);
                        }
                    }
                }
                else
                {
                    List<TBProduct> detail = db.TBProducts.Where(x => x.Price > priceRange[0] && x.Price < priceRange[1] && !x.Deflag && x.Active).ToList();
                    _totalPage = (int)Math.Ceiling((double)db.TBProducts.Where(x => x.Price > priceRange[0] && x.Price < priceRange[1] && !x.Deflag).Count() / (double)productPerPage);
                    foreach (var item in detail)
                    {
                        listProduct.Add(item);
                    }
                }

                _startPage = currentPage - 10;
                _endPage = currentPage + 10;

                if (_startPage <= 0)
                {
                    _endPage -= (_startPage - 1);
                    _startPage = 1;
                }

                if (_endPage > _totalPage)
                    _endPage = _totalPage;

                var data = listProduct.AsEnumerable().OrderByDescending(x => x.TBProduct_Combinations.Where(y => y.IDProduct == x.IDProduct).Sum(y => y.Quantity)).Skip(_skipCounter).Select(x => new
                {
                    x.IDProduct,
                    x.Name,
                    PriceBeforeDiscount = GetPrice(db, x.IDProduct)["PriceBeforeDiscount"],
                    Price = GetPrice(db, x.IDProduct)["Price"],
                    x.Discount,
                    x.TotalDiscount,
                    Quantity = x.TBProduct_Combinations.Where(y => y.IDProduct == x.IDProduct).Sum(y => y.Quantity) == null ? 0 : x.TBProduct_Combinations.Where(y => y.IDProduct == x.IDProduct).Sum(y => y.Quantity),
                    Photo = OurClass.ImageExists(x.TBProduct_Photos.Where(y => y.IsCover).FirstOrDefault().Photo, "product") ? x.TBProduct_Photos.Where(y => y.IsCover).FirstOrDefault().Photo : "noimage.jpg",
                    Category = x.TBProduct_Categories.Where(c => c.IsDefault).FirstOrDefault().TBCategory.Name,
                    StartPage = _startPage,
                    EndPage = _endPage,
                    TotalPage = _totalPage,
                    CurrentPage = currentPage
                }).GroupBy(x => x.IDProduct).SelectMany(x => x.Take(1)).Take(take);

                return data;
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_GetData_ByIDValue_And_IDCategory_And_PriceRange_Paging(List<int> idValue, List<decimal> priceRange, int idCategory, int currentPage)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            List<dynamic> listProduct = new List<dynamic>();
            int productPerPage = int.Parse(ConfigurationManager.AppSettings["ProductPerPage"]);
            int take = productPerPage;

            int _skipCounter = (currentPage - 1) * productPerPage;
            int _totalPage = 0;

            int _startPage = 0;
            int _endPage = 0;

            if (take == 0)
            {
                if (idValue.Count != 0)
                {
                    foreach (int id in idValue)
                    {
                        List<dynamic> detail = Dynamic_GetData_ByIDCategory_And_ByIDValue_And_PriceRange(db, idCategory, id, priceRange[0], priceRange[1]).Distinct().ToList();
                        _totalPage = (int)Math.Ceiling((double)db.TBProduct_Combination_Details.Where(x => x.TBProduct_Combination.TBProduct.Price > priceRange[0] && x.TBProduct_Combination.TBProduct.Price < priceRange[1] && x.IDValue == id && !x.TBProduct_Combination.Deflag && !x.TBProduct_Combination.TBProduct.Deflag).Count() / (double)productPerPage);
                        foreach (var item in detail)
                        {
                            listProduct.Add(item.TBProduct_Combination.TBProduct);
                        }
                    }
                }
                else
                {
                    List<dynamic> detail = Dynamic_GetData_ByIDCategory_And_PriceRange(db, idCategory, priceRange[0], priceRange[1]).Distinct().ToList();
                    _totalPage = (int)Math.Ceiling((double)db.TBProducts.Where(x => x.Price > priceRange[0] && x.Price < priceRange[1] && !x.Deflag).Count() / (double)productPerPage);
                    foreach (var item in detail)
                    {
                        listProduct.Add(item);
                    }
                }

                _startPage = currentPage - 10;
                _endPage = currentPage + 10;

                if (_startPage <= 0)
                {
                    _endPage -= (_startPage - 1);
                    _startPage = 1;
                }

                if (_endPage > _totalPage)
                    _endPage = _totalPage;

                var data = listProduct.AsEnumerable().OrderByDescending(x => x.Quantity).Skip(_skipCounter).Select(x => new
                {
                    x.IDProduct,
                    x.Name,
                    PriceBeforeDiscount = GetPrice(db, x.IDProduct)["PriceBeforeDiscount"],
                    Price = GetPrice(db, x.IDProduct)["Price"],
                    x.Discount,
                    x.TotalDiscount,
                    x.Quantity,
                    x.Photo,
                    x.Category,
                    StartPage = _startPage,
                    EndPage = _endPage,
                    TotalPage = _totalPage,
                    CurrentPage = currentPage
                }).GroupBy(x => x.IDProduct).SelectMany(x => x.Take(1));

                return data;
            }
            else
            {
                if (idValue.Count != 0)
                {
                    foreach (int id in idValue)
                    {
                        List<dynamic> detail = Dynamic_GetData_ByIDCategory_And_ByIDValue_And_PriceRange(db, idCategory, id, priceRange[0], priceRange[1]).Distinct().ToList();
                        _totalPage = (int)Math.Ceiling((double)db.TBProduct_Combination_Details.Where(x => x.TBProduct_Combination.TBProduct.Price > priceRange[0] && x.TBProduct_Combination.TBProduct.Price < priceRange[1] && x.IDValue == id && !x.TBProduct_Combination.Deflag && !x.TBProduct_Combination.TBProduct.Deflag).Count() / (double)productPerPage);
                        foreach (var item in detail)
                        {
                            listProduct.Add(item);
                        }
                    }
                }
                else
                {
                    List<dynamic> detail = Dynamic_GetData_ByIDCategory_And_PriceRange(db, idCategory, priceRange[0], priceRange[1]).Distinct().ToList();
                    _totalPage = (int)Math.Ceiling((double)db.TBProducts.Where(x => x.Price > priceRange[0] && x.Price < priceRange[1] && !x.Deflag).Count() / (double)productPerPage);
                    foreach (var item in detail)
                    {
                        listProduct.Add(item);
                    }
                }

                _startPage = currentPage - 10;
                _endPage = currentPage + 10;

                if (_startPage <= 0)
                {
                    _endPage -= (_startPage - 1);
                    _startPage = 1;
                }

                if (_endPage > _totalPage)
                    _endPage = _totalPage;

                var data = listProduct.AsEnumerable().OrderByDescending(x => x.Quantity).Skip(_skipCounter).Select(x => new
                {
                    x.IDProduct,
                    x.Name,
                    PriceBeforeDiscount = GetPrice(db, x.IDProduct)["PriceBeforeDiscount"],
                    Price = GetPrice(db, x.IDProduct)["Price"],
                    x.Discount,
                    x.TotalDiscount,
                    x.Quantity,
                    x.Photo,
                    x.Category,
                    StartPage = _startPage,
                    EndPage = _endPage,
                    TotalPage = _totalPage,
                    CurrentPage = currentPage
                }).GroupBy(x => x.IDProduct).SelectMany(x => x.Take(1)).Take(take);

                return data;
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic AJAX_FE_GetTotalProduct()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            {
                Class_Product_Photo _photo = new Class_Product_Photo();

                return ReturnData.MessageSuccess("OK", db.FUNC_Product_GetAll().OrderBy(x => x.SequenceNumber).OrderByDescending(x => x.IDProduct).Count());
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public dynamic AJAX_FE_GetTotalProduct_ByIDCategory(int idCategory)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            {
                Class_Product_Photo _photo = new Class_Product_Photo();

                return ReturnData.MessageSuccess("OK", db.FUNC_Product_GetAll_ByIDCategory(idCategory).OrderBy(x => x.SequenceNumber).OrderByDescending(x => x.IDProduct).Count());
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public dynamic Dynamic_GetSimiliarProduct(int idProduct, int take)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            {
                Class_Product_Photo _photo = new Class_Product_Photo();
                var product = db.TBProducts.Where(x => x.IDProduct == idProduct).FirstOrDefault();
                int idCategory = product.TBProduct_Categories.Where(x => x.IsDefault).FirstOrDefault().IDCategory;
                if (product == null)
                    return ReturnData.MessageFailed("Data not found", null);
                if (take == 0)
                {
                    return db.TBProduct_Categories.Where(x => x.IDCategory == idCategory && x.TBProduct.IDProduct != idProduct && !x.TBProduct.Deflag && x.TBProduct.Active).Select(x => new
                    {
                        x.TBProduct.Name,
                        x.TBProduct.IDProduct,
                        Quantity = x.TBProduct.TBProduct_Combinations.Sum(y => y.Quantity),
                        Price = GetPrice(db, x.IDProduct)["Price"],
                        PriceBeforeDiscount = GetPrice(db, x.IDProduct)["PriceBeforeDiscount"],
                        TotalDiscount = GetPrice(db, x.IDProduct)["TotalDiscount"],
                        x.TBProduct.TBProduct_Photos.Where(p => p.IsCover).FirstOrDefault().Photo
                    });
                }
                else
                {
                    return db.TBProduct_Categories.Where(x => x.IDCategory == idCategory && x.TBProduct.IDProduct != idProduct && !x.TBProduct.Deflag && x.TBProduct.Active).Select(x => new
                    {
                        x.TBProduct.Name,
                        x.TBProduct.IDProduct,
                        Quantity = x.TBProduct.TBProduct_Combinations.Sum(y => y.Quantity),
                        Price = GetPrice(db, x.IDProduct)["Price"],
                        PriceBeforeDiscount = GetPrice(db, x.IDProduct)["PriceBeforeDiscount"],
                        TotalDiscount = GetPrice(db, x.IDProduct)["TotalDiscount"],
                        x.TBProduct.TBProduct_Photos.Where(p => p.IsCover).FirstOrDefault().Photo
                    }).Take(take);
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

    private dynamic GetSizeByIDProductAndIDColor(DataClassesDataContext db, int idProduct, int idColor)
    {
        List<TBProduct_Combination> combination = db.TBProduct_Combinations.Where(x => !x.Deflag && x.IDProduct == idProduct).ToList();
        List<TBProduct_Combination_Detail> _result = new List<TBProduct_Combination_Detail>();
        Class_Attribute _attr = new Class_Attribute();
        if (combination != null)
        {
            foreach (var item in combination)
            {
                bool found = false;
                foreach (var detail in item.TBProduct_Combination_Details.OrderBy(x => x.TBValue.IDAttribute))
                {
                    if (detail.IDValue == idColor)
                        found = true;
                    else if (found)
                    {
                        if (_result.Where(x => x.IDProduct_Combination_Detail == detail.IDProduct_Combination_Detail).FirstOrDefault() == null)
                            _result.Add(detail);
                    }
                }
            }
        }

        return _result.Select(x => new
        {
            x.NameValue,
            x.IDProduct_Combination,
            x.IDProduct_Combination_Detail
        });
    }

    public ReturnData AJAX_FE_GetDetail_Preload(int idProduct)
    {
        try
        {
            //using (DataClassesDataContext db = new DataClassesDataContext())
            DataClassesDataContext db = new DataClassesDataContext();
            {
                int activeCurrency = 0;
                if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCurrency"].ToString()] != null)
                {
                    string id = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCurrency"].ToString()].Value;
                    int.TryParse(id, out activeCurrency);
                }
                else
                    activeCurrency = 1;

                Class_Value _value = new Class_Value();
                var product = db.FUNC_Product_GetDetail(idProduct).FirstOrDefault();

                //PRICE CONVERSION
                product.Price = Class_Currency.GetPriceConversionCurrency(product.Price);
                product.PriceBeforeDiscount = Class_Currency.GetPriceConversionCurrency(product.PriceBeforeDiscount);
                product.TotalDiscount = Class_Currency.GetPriceConversionCurrency(product.TotalDiscount);

                if (product == null)
                    return ReturnData.MessageFailed("Data not found", null);
                else
                {
                    Dictionary<string, dynamic> _result = new Dictionary<string, dynamic>();
                    //dynamic _product = Dynamic_GetDetail_Information(db, idProduct);
                    if (product != null)
                    {
                        var combination = db.FUNC_Product_Combination_Getdata_ByIDProduct(idProduct).FirstOrDefault();
                        var color = db.FUNC_GetColor_ByIDProduct(idProduct);
                        if (combination == null)
                            return ReturnData.MessageFailed("Combination is not exists for this product", null);
                        Class_Attribute _attribute = new Class_Attribute();
                        _result.Add("Product", product);
                        _result.Add("DefaultCategory", Dynamic_GetDefault_Categories(db, idProduct).Name);
                        _result.Add("Photos", Dynamic_GetData_Photo_ByIDProduct(db, idProduct));
                        //_result.Add("Combination", combination);
                        int aa = color.Count();
                        Class_Attribute attr = new Class_Attribute();
                        Class_Value val = new Class_Value();

                        _result.Add("Color", color);
                        if (color != null && color.Count() != 0)
                            _result.Add("Size", db.SP_GetSizeCombination_ByIDProductAndIDColor(idProduct, db.FUNC_GetColor_ByIDProduct(idProduct).FirstOrDefault().IDValue));
                        else
                            _result.Add("Size", db.SP_GetSizeCombination_ByIDProduct(idProduct));
                        return ReturnData.MessageSuccess("OK", _result);
                    }
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);
                    //dynamic _result = db.FUNC_Product_GetDetail(idProduct).Select(x => new
                    //{
                    //    x.IDProduct,
                    //    x.Name,
                    //    x.Price,
                    //    x.PriceBeforeDiscount,
                    //    x.ReferenceCode,
                    //    x.ShortDescription,
                    //    x.TotalDiscount,
                    //    x.TypeDiscountPercent,
                    //    x.Weight,
                    //    x.Description,
                    //    x.Discount,
                    //    x.Manufacturer,
                    //    Size = _value.AJAX_FE_GetSizeByIDProduct(x.IDProduct),
                    //    //Color = _value.AJAX_FE_GetColorByIDProduct(x.IDProduct),
                    //    Photos = (new Class_Product_Photo().GetPhotosByIDProduct(db, idProduct))
                    //}).FirstOrDefault();
                    //return ReturnData.MessageSuccess("OK", _result);
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

    public ReturnData AJAX_FE_GetSize_ByIDProductAndIDColor(int idproduct, int idcolor)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return ReturnData.MessageSuccess("OK", db.SP_GetSizeCombination_ByIDProductAndIDColor(idproduct, idcolor));
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    #endregion

    private Dictionary<string, decimal> GetPrice(DataClassesDataContext db, int idProduct)
    {
        try
        {
            #region custom price currency
            //int activeCurrency = 0;
            //if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCurrency"].ToString()] != null)
            //{
            //    string id = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCurrency"].ToString()].Value;
            //    int.TryParse(id, out activeCurrency);
            //}
            //else
            //    activeCurrency = 1;

            //Dictionary<string, decimal> result = new Dictionary<string, decimal>();
            //if (activeCurrency != 1)
            //{
            //    TBProduct_Currency curr = db.TBProduct_Currencies.Where(x => x.IDProduct == idProduct && x.IDCurrency == activeCurrency).FirstOrDefault();
            //    result.Add("Price", curr.Price);
            //    result.Add("PriceBeforeDiscount", curr.PriceBeforeDiscount);
            //    result.Add("TotalDiscount", curr.TotalDiscount);
            //}
            //else
            //{
            //    var product = db.TBProducts.Where(x => x.IDProduct == idProduct && !x.Deflag).FirstOrDefault();
            //    if (product != null)
            //    {
            //        result.Add("Price", product.Price);
            //        result.Add("PriceBeforeDiscount", product.PriceBeforeDiscount);
            //        result.Add("TotalDiscount", product.TotalDiscount);
            //    }

            //}
            #endregion

            int activeCurrency = 0;
            if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCurrency"].ToString()] != null)
            {
                string id = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCurrency"].ToString()].Value;
                int.TryParse(id, out activeCurrency);
            }
            else
                activeCurrency = 1;

            var product = db.TBProducts.Where(x => x.IDProduct == idProduct & !x.Deflag).FirstOrDefault();
            Dictionary<string, decimal> result = new Dictionary<string, decimal>();

            var currency = db.TBCurrencies.Where(x => x.IDCurrency == activeCurrency && !x.Deflag).FirstOrDefault();
            if (product.Price <= 0)
                result.Add("Price", 0);
            else
                result.Add("Price", product.Price / currency.ConversionRate);
            if (product.PriceBeforeDiscount <= 0)
                result.Add("PriceBeforeDiscount", 0);
            else
                result.Add("PriceBeforeDiscount", product.PriceBeforeDiscount / currency.ConversionRate);
            if (product.TotalDiscount <= 0)
                result.Add("TotalDiscount", 0);
            else
                result.Add("TotalDiscount", product.TotalDiscount / currency.ConversionRate);

            return result;
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            throw;
        }
    }
}
