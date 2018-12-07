using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using WITLibrary;

/// <summary>
/// Summary description for Class_SAAS
/// </summary>
public class Class_SAAS
{
    public dynamic DYNAMIC_AddToCart_Renew(int idProduct, int idCustomer, int idCustomerProduct, int idCombination)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            Class_Order _Order = new Class_Order();
            var dataOrder = db.TBProducts.Where(x => x.IDProduct == idProduct).FirstOrDefault();
            //HttpCookie _cookie = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()];
            string anonID = HttpContext.Current.Request.AnonymousID;
            TBOrder_temp _cookie = _Order.GetEncodedDataOrder(anonID);

            string _token = "";
            Dictionary<string, dynamic> _product = new Dictionary<string, dynamic>();
            List<Dictionary<string, dynamic>> _listProduct = new List<Dictionary<string, dynamic>>();
            Dictionary<string, dynamic> _tokenData = new Dictionary<string, dynamic>();
            TBProduct_Combination productCombination = db.TBProduct_Combinations.Where(x => !x.Deflag && x.IDProduct_Combination == dataOrder.TBProduct_Combinations.FirstOrDefault().IDProduct_Combination).FirstOrDefault();
            if (productCombination == null)
                return ReturnData.MessageFailed("Data not found", null);

            _product.Add("IDProduct", dataOrder.IDProduct);
            _product.Add("IDCombination", idCombination);
            _product.Add("Price", dataOrder.TBProduct_Combinations.Where(x => x.IDProduct_Combination == idCombination).FirstOrDefault().Price);
            _product.Add("CombinationName", dataOrder.TBProduct_Combinations.FirstOrDefault().Name);
            _product.Add("Quantity", 1);
            _product.Add("ProductName", dataOrder.Name);
            _product.Add("PricePerUnit", dataOrder.TBProduct_Combinations.Where(x => x.IDProduct_Combination == idCombination).FirstOrDefault().Price);
            _product.Add("Weight", productCombination.Weight);
            _product.Add("WeightPerUnit", productCombination.Weight);

            _listProduct.Add(_product);
            _tokenData.Add("Product", _listProduct);
            _tokenData.Add("TotalPrice", dataOrder.TBProduct_Combinations.Where(x => x.IDProduct_Combination == idCombination).FirstOrDefault().Price);
            _tokenData.Add("TotalQuantity", 1);
            _tokenData.Add("TotalWeight", productCombination.Weight);
            _tokenData.Add("Subtotal", dataOrder.TBProduct_Combinations.Where(x => x.IDProduct_Combination == idCombination).FirstOrDefault().Price);
            _tokenData.Add("IDCurrency", Class_Currency.GetActiveCurrencyID());
            _tokenData.Add("OrderType", "renew");
            _tokenData.Add("IDCustomerProduct", idCustomerProduct);
            _token = OurClass.EncryptToken(_tokenData);
            //HttpContext.Current.Response.Cookies.Add(new HttpCookie(System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString(), _token));
            _Order.SaveEncodeDataOrder(HttpContext.Current.Request.AnonymousID, _token);
            //HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()].Expires = DateTime.Now.AddMinutes(120);
            return ReturnData.MessageSuccess("Product add to cart successfully", _tokenData);

            //cek cookies nya ada ngga, kalo ada ganti token
            //kalo ga ada tambahin dulu _witcomcrt_
            //cek kalo token nya kosong
            // kalo ngga kosong, decrypt dulu, ambil object nya.
            //trus tambahin sama cart yang baru
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public dynamic DYNAMIC_AddToCart_Upgrade(int idProduct, int idCustomer, int idCustomerProduct)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            Class_Order _Order = new Class_Order();
            var dataOrder = db.TBProducts.Where(x => x.IDProduct == idProduct).FirstOrDefault();
            //HttpCookie _cookie = HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()];
            string anonID = HttpContext.Current.Request.AnonymousID;
            TBOrder_temp _cookie = _Order.GetEncodedDataOrder(anonID);
            var customerProduct = db.TBCustomer_Products.Where(x => x.IDCustomer_Product == idCustomerProduct).FirstOrDefault();
            int totalRemainingMonth = ((customerProduct.EndDate.Value.Year - DateTime.Now.Year) * 12) + customerProduct.EndDate.Value.Month - DateTime.Now.Month;
            int totalRemainingYear = (customerProduct.EndDate.Value.Year - DateTime.Now.Year) * 12;

            if (totalRemainingMonth <= 0 && totalRemainingYear <= 0)
            {
                totalRemainingMonth = 1;
                totalRemainingYear = 1;
            }

            string _token = "";
            Dictionary<string, dynamic> _product = new Dictionary<string, dynamic>();
            List<Dictionary<string, dynamic>> _listProduct = new List<Dictionary<string, dynamic>>();
            Dictionary<string, dynamic> _tokenData = new Dictionary<string, dynamic>();
            TBProduct_Combination productCombination = db.TBProduct_Combinations.Where(x => !x.Deflag && x.IDProduct_Combination == dataOrder.TBProduct_Combinations.FirstOrDefault().IDProduct_Combination).FirstOrDefault();
            if (productCombination == null)
                return ReturnData.MessageFailed("Data not found", null);

            if (dataOrder.Price <= customerProduct.TBProduct.Price)
            {
                _product.Add("IDProduct", dataOrder.IDProduct);
                _product.Add("IDCombination", dataOrder.TBProduct_Combinations.FirstOrDefault().IDProduct_Combination);
                _product.Add("Price", 0);
                _product.Add("CombinationName", dataOrder.TBProduct_Combinations.FirstOrDefault().Name);
                _product.Add("Quantity", 1);
                _product.Add("ProductName", dataOrder.Name);
                _product.Add("PricePerUnit", 0);
                _product.Add("Weight", productCombination.Weight);
                _product.Add("WeightPerUnit", productCombination.Weight);

                _listProduct.Add(_product);
                _tokenData.Add("Product", _listProduct);
                _tokenData.Add("TotalPrice", 0);
                _tokenData.Add("TotalQuantity", 1);
                _tokenData.Add("TotalWeight", productCombination.Weight);
                _tokenData.Add("Subtotal", 0);
                _tokenData.Add("IDCurrency", Class_Currency.GetActiveCurrencyID());
                _tokenData.Add("OrderType", "upgrade");
                _tokenData.Add("IDCustomerProduct", idCustomerProduct);
            }
            else
            {
                _product.Add("IDProduct", dataOrder.IDProduct);
                _product.Add("IDCombination", dataOrder.TBProduct_Combinations.FirstOrDefault().IDProduct_Combination);
                _product.Add("Price", Math.Ceiling(dataOrder.Price * totalRemainingMonth / totalRemainingYear));
                _product.Add("CombinationName", dataOrder.TBProduct_Combinations.FirstOrDefault().Name);
                _product.Add("Quantity", 1);
                _product.Add("ProductName", dataOrder.Name);
                _product.Add("PricePerUnit", Math.Ceiling(dataOrder.Price * totalRemainingMonth / totalRemainingYear));
                _product.Add("Weight", productCombination.Weight);
                _product.Add("WeightPerUnit", productCombination.Weight);

                _listProduct.Add(_product);
                _tokenData.Add("Product", _listProduct);
                _tokenData.Add("TotalPrice", Math.Ceiling(dataOrder.Price * totalRemainingMonth / totalRemainingYear));
                _tokenData.Add("TotalQuantity", 1);
                _tokenData.Add("TotalWeight", productCombination.Weight);
                _tokenData.Add("Subtotal", Math.Ceiling(dataOrder.Price * totalRemainingMonth / totalRemainingYear));
                _tokenData.Add("IDCurrency", Class_Currency.GetActiveCurrencyID());
                _tokenData.Add("OrderType", "upgrade");
                _tokenData.Add("IDCustomerProduct", idCustomerProduct);
            }
            _token = OurClass.EncryptToken(_tokenData);
            //HttpContext.Current.Response.Cookies.Add(new HttpCookie(System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString(), _token));
            _Order.SaveEncodeDataOrder(HttpContext.Current.Request.AnonymousID, _token);
            //HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieCart"].ToString()].Expires = DateTime.Now.AddMinutes(120);
            return ReturnData.MessageSuccess("Product add to cart successfully", _tokenData);

            //cek cookies nya ada ngga, kalo ada ganti token
            //kalo ga ada tambahin dulu _witcomcrt_
            //cek kalo token nya kosong
            // kalo ngga kosong, decrypt dulu, ambil object nya.
            //trus tambahin sama cart yang baru
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public dynamic GetNotificationExpiredPackage(int idCustomer)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var package = db.TBCustomer_Products.Where(x => x.IDCustomer == idCustomer && !x.TBCustomer.Deflag).Select(x => new
            {
                x.IDCustomer_Product,
                x.IDCustomer,
                x.IDProduct,
                x.StartDate,
                x.EndDate,
                ProductName = x.TBProduct.Name,
                EndDateDay = x.EndDate.Value.Day,
                EndDateMonth = x.EndDate.Value.Month,
                EndDateYear = x.EndDate.Value.Year,
                EndDateHour = x.EndDate.Value.Hour,
                EndDateMinute = x.EndDate.Value.Minute,
                EndDateSecond = x.EndDate.Value.Second,
                EndDateMiliSecond = x.EndDate.Value.Millisecond
            });
            if (package == null)
            {
                return null;
            }
            else
            {
                List<dynamic> listNotif = new List<dynamic>();
                foreach (var item in package)
                {
                    if ((item.EndDate.Value - DateTime.Now).TotalDays <= 30)
                    {
                        listNotif.Add(item);
                    }
                }
                return listNotif;
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic GetAllCustomerProduct_ByIDCustomer(int idCustomer)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            Class_Product _pro = new Class_Product();
            return db.TBCustomer_Products.Where(x => x.IDCustomer == idCustomer && !x.TBCustomer.Deflag).Select(x => new
            {
                x.IDCustomer_Product,
                x.IDCustomer,
                x.IDProduct,
                x.StartDate,
                x.EndDate,
                ProductName = x.TBProduct.Name,
                ProductCombination = _pro.GetData_Combination_ByIDProduct(db, x.TBProduct.IDProduct)
            });
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic GetCustomerProduct_ByIDCustomer_AndIDProduct(int idCustomer, int idProduct)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBCustomer_Products.Where(x => x.IDCustomer == idCustomer && !x.TBCustomer.Deflag && x.IDProduct == idProduct).Select(x => new
            {
                x.IDCustomer_Product,
                x.IDCustomer,
                x.IDProduct,
                x.StartDate,
                x.EndDate,
                ProductName = x.TBProduct.Name
            });
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }
}