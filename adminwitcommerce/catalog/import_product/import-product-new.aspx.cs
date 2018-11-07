using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adminwitcommerce_catalog_import_product_import_product_new : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private int IsProductExists(string referenceCode)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            if (db.TBProducts.Where(x => !x.Deflag && x.ReferenceCode == referenceCode).FirstOrDefault() == null)
                return 0;
            else
                return db.TBProducts.Where(x => !x.Deflag && x.ReferenceCode == referenceCode).FirstOrDefault().IDProduct;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    private int ImportManufacturer(string name)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var existData = db.TBManufacturers.Where(x => !x.Deflag && x.Name.ToLower() == name.ToLower()).FirstOrDefault();
            if (existData == null)
            {
                TBManufacturer newData = new TBManufacturer();
                newData.Name = name;
                newData.Active = true;
                newData.DateInsert = DateTime.Now;
                newData.DateLastUpdate = DateTime.Now;
                newData.Deflag = false;
                db.TBManufacturers.InsertOnSubmit(newData);
                db.SubmitChanges();
                return newData.IDManufacturer;
            }
            else
                return existData.IDManufacturer;
        }
        catch (Exception)
        {

            throw;
        }
    }
    private int[] ImportCategory(string[] listCategory)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            List<int> listIDCategory = new List<int>();
            int? idParent = null;


            for (int i = 0; i < listCategory.Length; i++)
            {
                var cekData = db.TBCategories.Where(x => !x.Deflag && x.Name.ToLower() == listCategory[i].ToLower()).FirstOrDefault();
                if (cekData == null)
                {
                    TBCategory _newCategory = new TBCategory { Name = listCategory[i], DateInsert = DateTime.Now, DateLastUpdate = DateTime.Now, IDCategoryParent = idParent, Active = true };
                    db.TBCategories.InsertOnSubmit(_newCategory);
                    db.SubmitChanges();
                    listIDCategory.Add(_newCategory.IDCategory);
                }
                else
                {
                    listIDCategory.Add(cekData.IDCategory);
                }
                //TBCategory cat = db.TBCategories.Where(x => !x.Deflag && x.Name.ToLower() == listCategory[i].ToLower()).FirstOrDefault();
                //if (cat == null)
                //{
                //    TBCategory _newCategory = new TBCategory { Name = listCategory[i], DateInsert = DateTime.Now, DateLastUpdate = DateTime.Now, IDCategoryParent = idParent };
                //    db.TBCategories.InsertOnSubmit(_newCategory);
                //    db.SubmitChanges();
                //    listIDCategory.Add(_newCategory.IDCategory);
                //}
                //else
                //{
                //    idParent = cat.IDCategory;
                //    listIDCategory.Add(cat.IDCategory);
                //}
            }

            return listIDCategory.ToArray();
        }
        catch (Exception)
        {

            throw;
        }
    }
    private int ImportProduct(int idManufacturer, string name, string referenceCode, decimal price, decimal weight, int active, string description, string shortDesc)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBProduct _newProduct = new TBProduct();
            int sequenceNumber = 0;
            if (db.TBProducts.Where(x => !x.Deflag && x.SequenceNumber != 0).OrderByDescending(x => x.SequenceNumber).FirstOrDefault() != null)
                sequenceNumber = db.TBProducts.Where(x => !x.Deflag && x.SequenceNumber != 0).OrderByDescending(x => x.SequenceNumber).FirstOrDefault().SequenceNumber;
            sequenceNumber++;
            _newProduct.Name = name;
            _newProduct.IDManufacturer = idManufacturer;
            _newProduct.ReferenceCode = referenceCode;
            _newProduct.Price = price;
            _newProduct.PriceBeforeDiscount = price;
            _newProduct.Weight = weight;
            _newProduct.Active = active == 0 ? false : true;
            _newProduct.Description = description;
            _newProduct.ShortDescription = shortDesc;
            _newProduct.Meta = name;
            _newProduct.MetaKeyword = name;
            _newProduct.MetaDescription = name;
            _newProduct.DateInsert = DateTime.Now;
            _newProduct.DateLastUpdate = DateTime.Now;
            _newProduct.SequenceNumber = sequenceNumber;
            db.TBProducts.InsertOnSubmit(_newProduct);
            db.SubmitChanges();

            TBProduct_Photo photo = new TBProduct_Photo();
            photo.IDProduct = _newProduct.IDProduct;
            photo.Photo = _newProduct.Name.ToLower().Replace(' ', '-') + "-1.jpg";
            photo.IsCover = true;
            photo.DateInsert = DateTime.Now;
            photo.DateLastUpdate = DateTime.Now;
            db.TBProduct_Photos.InsertOnSubmit(photo);
            db.SubmitChanges();
            return _newProduct.IDProduct;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    private int ImportSize(string Size)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            Class_Attribute attr = new Class_Attribute();
            TBValue size = db.TBValues.Where(x => !x.Deflag && x.IDAttribute == attr.IDSize && x.Name.ToLower() == Size.ToLower()).FirstOrDefault();
            if (size == null)
            {
                TBValue _newVal = new TBValue();
                _newVal.IDAttribute = attr.IDSize;
                _newVal.Name = Size;
				_newVal.DateInsert = DateTime.Now;
				_newVal.DateLastUpdate = DateTime.Now;
                db.TBValues.InsertOnSubmit(_newVal);
                db.SubmitChanges();

                return _newVal.IDValue;
            }
            else
            {
                return size.IDValue;
            }
        }
        catch (Exception)
        {
            return 0;
        }
    }

    private int ImportColor(string name, string hexaColor)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            Class_Attribute attr = new Class_Attribute();
            TBValue size = db.TBValues.Where(x => !x.Deflag && x.IDAttribute == attr.IDColor && x.Name.ToLower() == name.ToLower()).FirstOrDefault();
            if (size == null)
            {
                TBValue _newVal = new TBValue();
                _newVal.IDAttribute = attr.IDColor;
                _newVal.Name = name;
                _newVal.RGBColor = hexaColor;
				_newVal.DateInsert = DateTime.Now;
				_newVal.DateLastUpdate = DateTime.Now;
                db.TBValues.InsertOnSubmit(_newVal);
                db.SubmitChanges();

                return _newVal.IDValue;
            }
            else
            {
                return size.IDValue;
            }
        }
        catch (Exception)
        {
            return 0;
        }
    }

    private int ImportCombination(int idProduct, string referenceCode, decimal basePrice, decimal priceBeforeImpact, decimal PriceAfterImpact, decimal price, decimal weight, int Quantity, int idColor, int idSize)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            int firstQty = 0;
            Class_Employee emp = new Class_Employee();
            var employee = emp.GetData_By_Token(HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()].Value);

            if (db.TBProduct_Combinations.Where(x => !x.Deflag && x.ReferenceCode == referenceCode).FirstOrDefault() != null)
            {
                TBProduct_Combination data = db.TBProduct_Combinations.Where(x => !x.Deflag && x.ReferenceCode == referenceCode).FirstOrDefault();
                firstQty = data.Quantity;
                data.Quantity = Quantity;

                TBValue _color = db.TBValues.Where(x => !x.Deflag && x.IDValue == idColor).FirstOrDefault();
                TBValue _size = db.TBValues.Where(x => !x.Deflag && x.IDValue == idSize).FirstOrDefault();
                data.Name = _color.TBAttribute.Name + " : " + _color.Name + ", " + _size.TBAttribute.Name + " : " + _size.Name;
                data.DateInsert = DateTime.Now;
                data.DateLastUpdate = DateTime.Now;
                data.BasePrice = basePrice;
                data.PriceBeforeImpact = priceBeforeImpact;
                data.PriceAfterImpact = PriceAfterImpact;
                data.Price = price;
                data.WeightBeforeImpact = weight;
                data.Weight = weight;

                db.SubmitChanges();
                return data.IDProduct_Combination;
            }

            TBProduct_Combination _newData = new TBProduct_Combination();
            TBValue color = db.TBValues.Where(x => !x.Deflag && x.IDValue == idColor).FirstOrDefault();
            TBValue size = db.TBValues.Where(x => !x.Deflag && x.IDValue == idSize).FirstOrDefault();
            _newData.IDProduct = idProduct;
            _newData.ReferenceCode = referenceCode;
            _newData.Name = color.TBAttribute.Name + " : " + color.Name + ", " + size.TBAttribute.Name + " : " + size.Name;
            _newData.DateInsert = DateTime.Now;
            _newData.DateLastUpdate = DateTime.Now;
            _newData.BasePrice = basePrice;
            _newData.PriceBeforeImpact = priceBeforeImpact;
            _newData.PriceAfterImpact = PriceAfterImpact;
            _newData.Price = price;
            _newData.WeightBeforeImpact = weight;
            _newData.Weight = weight;
            _newData.Quantity = Quantity;
            _newData.DateLastUpdate = DateTime.Now;
            _newData.DateInsert = DateTime.Now;
            db.TBProduct_Combinations.InsertOnSubmit(_newData);
            db.SubmitChanges();

            //INSERT DETAIL COMBINATAION COLOR
            TBProduct_Combination_Detail _detail = new TBProduct_Combination_Detail();
            _detail.IDProduct_Combination = _newData.IDProduct_Combination;
            _detail.IDValue = color.IDValue;
            _detail.NameAttribute = color.TBAttribute.Name;
            _detail.NameValue = color.Name;
            _detail.DateInsert = DateTime.Now;
            db.TBProduct_Combination_Details.InsertOnSubmit(_detail);

            //INSERT DETAIL COMBINATAION Size
            TBProduct_Combination_Detail _detailSize = new TBProduct_Combination_Detail();
            _detailSize.IDProduct_Combination = _newData.IDProduct_Combination;
            _detailSize.IDValue = size.IDValue;
            _detailSize.NameAttribute = size.TBAttribute.Name;
            _detailSize.NameValue = size.Name;
            _detailSize.DateInsert = DateTime.Now;
            db.TBProduct_Combination_Details.InsertOnSubmit(_detailSize);
            db.SubmitChanges();

            //INSERT LOG
            Class_Log_Stock log = new Class_Log_Stock();
            log.Insert(employee.IDEmployee, _detail.TBProduct_Combination.IDProduct_Combination, _detail.TBProduct_Combination.Name, firstQty, Quantity, Quantity, "initial", "Import master product" + " by " + employee.Name + "( " + employee.Email + " )");

            return _newData.IDProduct_Combination;

        }
        catch (Exception)
        {
            return 0;
        }
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (fuFile.HasFile)
        {
            lblSuccess.Text = "";
            lblError.Text = "";
            try
            {
                DataClassesDataContext db = new DataClassesDataContext();
                fuFile.SaveAs(Server.MapPath("~/assets/import/product/" + fuFile.FileName));
                StreamReader sr = new StreamReader(Server.MapPath("~/assets/import/product/" + fuFile.FileName));
                try
                {
                    CsvReader csvread = new CsvReader(sr);
                    csvread.Configuration.Delimiter = ";";
                    csvread.Configuration.HasHeaderRecord = false;
                    char[] delimeter = { ',' };

                    List<CSV_Product_New> record = csvread.GetRecords<CSV_Product_New>().ToList();
                    int totalRead = 0;
                    int totalInsert = 0;
                    int totalUpdate = 0;
                    foreach (var item in record)
                    {
                        totalRead++;
                        #region KALO Artikel Master PRODUCT BELUM ADA
                        if (IsProductExists(item.ProductCode) == 0)
                        {
                            string[] category = item.Categories.Split(delimeter);
                            int[] listIDCategory = ImportCategory(category);
                            int idSize = ImportSize(item.Size);
                            int idColor = ImportColor(item.Color, item.Hexacolor);
                            int idManufacturer = ImportManufacturer(item.Manufacturer);
                            int idProduct = ImportProduct(idManufacturer, item.ProductName, item.ProductCode, item.Price, item.Weight, item.Active, item.Description, item.ShortDescription);

                            //INSERT COMBINATION COLOR
                            ImportCombination(idProduct, item.CombinationCode, item.Price, item.Price, item.Price, item.Price, item.Weight,item.Quantity, idColor, idSize);
                            totalInsert++;
                            //INSERT Category Product
                            for (int i = 0; i < listIDCategory.Length; i++)
                            {
                                TBProduct_Category _newData = new TBProduct_Category();
                                _newData.IDCategory = listIDCategory[i];
                                _newData.IDProduct = idProduct;
                                _newData.IsDefault = i == 0 ? true : false;
                                _newData.DateInsert = DateTime.Now;
                                _newData.DateLastUpdate = DateTime.Now;
                                db.TBProduct_Categories.InsertOnSubmit(_newData);
                                db.SubmitChanges();
                            }
                        }
                        #endregion
                        else
                        {
                            totalUpdate++;
                            int idProduct = IsProductExists(item.ProductCode);
                            string[] category = item.Categories.Split(delimeter);
                            int[] listIDCategory = ImportCategory(category);
                            int idSize = ImportSize(item.Size);
                            int idColor = ImportColor(item.Color, item.Hexacolor);
                            //INSERT COMBINATION COLOR
                            ImportCombination(idProduct, item.CombinationCode, item.Price, item.Price, item.Price, item.Price, item.Weight, item.Quantity, idColor, idSize);

                        }
                        //if (db.TBProvinces.Where(X => X.IDProvince == item.IDProvince).FirstOrDefault() != null)
                        //{
                        //    if (db.TBCities.Where(x => x.Name.ToLower() == item.Name.ToLower()).FirstOrDefault() == null)
                        //    {
                        //        TBCity newCity = new TBCity { IDProvince = item.IDProvince, Name = item.Name, CityType = item.CityType, Deflag = false, DateInsert = DateTime.Now, DateLastUpdate = DateTime.Now };
                        //        db.TBCities.InsertOnSubmit(newCity);
                        //        totalInsert++;
                        //    }
                        //    else
                        //    {
                        //        lblSuccess.Text += item.Name + "--";
                        //    }
                        //}
                        //else
                        //{
                        //    lblSuccess.Text += item.Name + "(" + item.IDProvince + ")--";
                        //}
                    }

                    sr.Close();
                    lblSuccess.Text += totalRead + " row(s) read, " + totalInsert + " row(s) inserted, " + totalUpdate + " row(s) updated";
                    alertError.Style.Add(HtmlTextWriterStyle.Display, "none");
                    alertSuccess.Style.Add(HtmlTextWriterStyle.Display, "block");
                }
                catch (Exception ex)
                {
                    sr.Close();
                    lblError.Text = ex.Message;
                    alertError.Style.Add(HtmlTextWriterStyle.Display, "block");
                    alertSuccess.Style.Add(HtmlTextWriterStyle.Display, "none");
                }

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                alertError.Style.Add(HtmlTextWriterStyle.Display, "block");
                alertSuccess.Style.Add(HtmlTextWriterStyle.Display, "none");
            }
        }
    }
}