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
/// Summary description for Class_Register
/// </summary>
public class Class_Customer
{
    public Class_Customer()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Datatable AJAX_GetTable(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            IEnumerable<dynamic> data = Dynamic_GetAll();
            int count = data.Count();
            if (!string.IsNullOrEmpty(search))
                data = data.Where(x =>
                    x.FirstName.ToLower().Contains(search.ToLower()) ||
            x.LastName.ToLower().Contains(search.ToLower()) ||
                    x.Email.ToLower().Contains(search.ToLower())
                    ).ToArray();
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (dynamic currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("IDCustomer", currData.IDCustomer);
                newData.Add("Name", currData.FirstName + ' ' + currData.LastName);
                newData.Add("Email", currData.Email);
                newData.Add("Active", currData.Active);
                newData.Add("PhoneNumber", currData.PhoneNumber);
                resultList.Add(newData);
            }
            return OurClass.ParseTable(resultList, count, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public Datatable AJAX_GetTableByIDCustomerGroup(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search, int idCustomerGroup)
    {
        try
        {
            IEnumerable<dynamic> data = Dynamic_GetAll_ByIDCustomerGroup(idCustomerGroup);
            int count = data.Count();
            if (!string.IsNullOrEmpty(search))
                data = data.Where(x =>
                    x.FirstName.ToLower().Contains(search.ToLower()) ||
            x.LastName.ToLower().Contains(search.ToLower()) ||
                    x.Email.ToLower().Contains(search.ToLower())
                    ).ToArray();
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (dynamic currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("IDCustomer", currData.IDCustomer);
                newData.Add("Name", currData.FirstName + ' ' + currData.LastName);
                newData.Add("Email", currData.Email);
                newData.Add("Active", currData.Active);
                newData.Add("PhoneNumber", currData.PhoneNumber);
                resultList.Add(newData);
            }
            return OurClass.ParseTable(resultList, count, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public WITLibrary.Datatable AJAX_GetTable_BestCustomer(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            dynamic[] data = Dynamic_Get_BestCustomer();
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
                newData.Add("IDCustomer", data[i].IDCustomer);
                newData.Add("Email", data[i].Email);
                newData.Add("Name", data[i].FirstName + " " + data[i].LastName);
                newData.Add("Quantity", data[i].Quantity);
                newData.Add("Amount", data[i].Amount);
                resultList[i] = newData;
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

    public Datatable AJAX_GetTable_IsSubscribe(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            IEnumerable<dynamic> data = Dynamic_GetAll_ByIsSubscribe();
            int count = data.Count();
            if (!string.IsNullOrEmpty(search))
                data = data.Where(x =>
                    x.FirstName.ToLower().Contains(search.ToLower()) ||
            x.LastName.ToLower().Contains(search.ToLower()) ||
                    x.Email.ToLower().Contains(search.ToLower())
                    ).ToArray();
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (dynamic currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("IDCustomer", currData.IDCustomer);
                newData.Add("Name", currData.FirstName + ' ' + currData.LastName);
                newData.Add("Email", currData.Email);
                newData.Add("PhoneNumber", currData.PhoneNumber);
                newData.Add("DateInsert", currData.DateInsert.ToString("dd-MM-yyyy") + " " + currData.DateInsert.ToLongTimeString());
                resultList.Add(newData);
            }
            return OurClass.ParseTable(resultList, count, iDisplayLength, iDisplayStart, sEcho, iSortingCols, iSortCol, sSortDir);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private bool IsExistsEmail(string email)
    {
        DataClassesDataContext db = new DataClassesDataContext();
        if (db.TBCustomers.Where(x => !x.Deflag && x.Email == email).FirstOrDefault() != null)
            return true;
        else
            return false;
    }

    public string EncryptPassword(string clearText)
    {
        string EncryptionKey = System.Configuration.ConfigurationManager.AppSettings["key"].ToString();
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }

    private string DecryptPassword(string cipherText)
    {
        string EncryptionKey = System.Configuration.ConfigurationManager.AppSettings["key"].ToString();
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }

    public dynamic Dynamic_GetAll()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBCustomers.Where(x => !x.Deflag && x.IDCustomer_Group != 2).Select(x => new
            {
                x.IDCustomer,
                x.FirstName,
                x.LastName,
                x.Email,
                x.Birthday,
                x.Gender,
                x.PhoneNumber,
                x.Active
            });
        }
        catch (Exception)
        {

            throw;
        }
    }

    public dynamic Dynamic_GetAll_ByIDCustomerGroup(int idCustomerGroup)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBCustomers.Where(x => !x.Deflag && x.IDCustomer_Group == idCustomerGroup).Select(x => new
            {
                x.IDCustomer,
                x.FirstName,
                x.LastName,
                x.Email,
                x.Birthday,
                x.Gender,
                x.PhoneNumber,
                x.Active
            });
        }
        catch (Exception)
        {

            throw;
        }
    }

    public dynamic Dynamic_GetData_ByEmail(string email)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBCustomers.Where(x => !x.Deflag && x.Active && x.Email == email).Select(x => new
            {
                x.IDCustomer,
                x.FirstName,
                x.LastName,
                x.Email,
                x.Birthday,
                x.Gender,
                x.PhoneNumber,
                x.IsSubscribe
            }).FirstOrDefault();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public dynamic Dynamic_GetData_ByIDCustomer(int id)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBCustomers.Where(x => !x.Deflag && x.Active && x.IDCustomer == id).AsEnumerable().Select(x => new
            {
                x.IDCustomer,
                x.FirstName,
                x.LastName,
                x.Email,
                x.Birthday,
                x.Gender,
                x.PhoneNumber
            }).FirstOrDefault();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public dynamic Dynamic_GetAll_ByIsSubscribe()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBCustomers.Where(x => !x.Deflag && x.Active && x.IsSubscribe).Select(x => new
            {
                x.IDCustomer,
                x.FirstName,
                x.LastName,
                x.Email,
                x.Birthday,
                x.Gender,
                x.PhoneNumber,
                x.DateInsert
            });
        }
        catch (Exception)
        {

            throw;
        }
    }

    public dynamic Dynamic_GetData_CustomerDetail(int id)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            Class_Address _address = new Class_Address();
            Class_Voucher _voucher = new Class_Voucher();
            Class_Order _order = new Class_Order();
            return db.TBCustomers.Where(x => !x.Deflag && x.Active && x.IDCustomer == id).AsEnumerable().Select(x => new
            {
                x.IDCustomer,
                x.FirstName,
                x.LastName,
                x.Email,
                x.Birthday,
                x.Gender,
                x.PhoneNumber,
                Subscribe = x.IsSubscribe,
                Status = x.Active,
                Age = DateTime.Now.Year - x.Birthday.Year,
                DateRegistered = x.DateInsert,
                CustomerGroup = x.TBCustomer_Group.Name,
                TotalValidOrder = db.TBOrders.Where(y => y.IDCustomer == x.IDCustomer && !y.Deflag && y.TBOrder_Status.Paid).Count(),
                TotalInvalidOrder = db.TBOrders.Where(y => y.IDCustomer == x.IDCustomer && !y.Deflag && !y.TBOrder_Status.Paid).Count(),
                TotalValidBuy = db.TBOrders.Where(y => y.IDCustomer == x.IDCustomer && !y.Deflag && y.TBOrder_Status.Paid).Sum(y => y.TotalPaid),
                TotalInvalidBuy = db.TBOrders.Where(y => y.IDCustomer == x.IDCustomer && !y.Deflag && !y.TBOrder_Status.Paid).Sum(y => y.TotalPaid),
                Address = _address.Dynamic_GetAll_ByEmail(x.Email),
                Voucher = _voucher.Dynamic_GetData_ByIDCustomer(x.IDCustomer),
                Order = _order.Dynamic_GetData_ByIDCustomer(x.IDCustomer),
                Product = _order.Dynamic_GetData_ProductBought_ByIDCustomer(x.IDCustomer)
            }).FirstOrDefault();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public IEnumerable<TBCustomer> GetAll_IsSbuscribe()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return Func_GetAll_IsSubscribe(db);
        }
        catch (Exception)
        {

            throw;
        }
    }

    private Func<DataClassesDataContext, IEnumerable<TBCustomer>> Func_GetAll_IsSubscribe
    {
        get
        {
            Func<DataClassesDataContext, IEnumerable<TBCustomer>> func =
              CompiledQuery.Compile<DataClassesDataContext, IEnumerable<TBCustomer>>
              ((DataClassesDataContext context) => context.TBCustomers.AsEnumerable()
                .Where(x => x.Deflag == false).ToArray());
            return func;
        }
    }

    public ReturnData AJAX_Update_PersonalInformation(int idCustomer, string firstName, string lastName, string gender, string email, string phoneNumber, DateTime birthday, bool isSubscribe)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBCustomer cust = db.TBCustomers.Where(x => !x.Deflag && x.IDCustomer == idCustomer).FirstOrDefault();
            if (cust == null)
                return ReturnData.MessageFailed("Data not found", null);
            cust.FirstName = firstName;
            cust.LastName = lastName;
            cust.Gender = gender;
            cust.Email = email;
            //cust.Password = EncryptPassword(password);
            cust.PhoneNumber = phoneNumber;
            cust.Birthday = birthday;
            cust.IsSubscribe = isSubscribe;
            cust.DateLastUpdate = DateTime.Now;
            db.SubmitChanges();
            var token = EncryptToken(email, DecryptPassword(cust.Password));
            HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()].Value = token;
            return ReturnData.MessageSuccess("Data updated successfully", token);
        }
        catch (Exception ex)
        {
            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_Update_Password(int idCustomer, string oldPassword, string newPassword)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBCustomer cust = db.TBCustomers.Where(x => !x.Deflag && x.IDCustomer == idCustomer).FirstOrDefault();
            if (cust == null)
                return ReturnData.MessageFailed("Data not found", null);
            if (DecryptPassword(cust.Password) != oldPassword)
                return ReturnData.MessageFailed("Your old password is wrong", null);
            cust.Password = EncryptPassword(newPassword);
            cust.DateLastUpdate = DateTime.Now;
            db.SubmitChanges();
            var token = EncryptToken(cust.Email, newPassword);
            HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()].Value = token;
            return ReturnData.MessageSuccess("Password updated successfully", token);
        }
        catch (Exception ex)
        {
            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_Reset_Password(string token, string newPassword)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var cust = db.TBCustomers.Where(x => !x.Deflag && x.Password == token).FirstOrDefault();
            if (cust == null)
                return ReturnData.MessageFailed("Customer not found", null);

            cust.Password = EncryptPassword(newPassword);
            db.SubmitChanges();
            return ReturnData.MessageSuccess("Password reset successfully", null);
        }
        catch (Exception ex)
        {
            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_Forget_Password(string email)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            if (IsExistsEmail(email))
            {
                var cust = db.TBCustomers.Where(x => !x.Deflag && x.Email == email).FirstOrDefault();
                if (cust != null)
                {
                    string text = email + "-" + cust.FirstName.Substring(0, 2);
                    string token = this.EncryptPassword(text);
                    int aa = token.Length;
                    string pass = EncryptToken(email, DateTime.Now.ToString("MMddHHmmss"));
                    aa = pass.Length;

                    cust.Password = pass;
                    db.SubmitChanges();
                    //SEND EMAIL
                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/template-email-forgot-password.html")))
                    {
                        Class_Configuration _config = new Class_Configuration();
                        var emailLogo = _config.Dynamic_Get_EmailLogo();
                        string body = "";
                        body = reader.ReadToEnd();
                        body = body.Replace("{name}", cust.FirstName);
                        body = body.Replace("{Url}", WebConfigurationManager.AppSettings["url"].ToString() + "/reset-password.aspx?token=" + cust.Password);
                        body = body.Replace("{title}", System.Configuration.ConfigurationManager.AppSettings["Title"]);
                        body = body.Replace("{email_logo}", System.Configuration.ConfigurationManager.AppSettings["url"] + "/assets/images/email_logo/" + emailLogo);
                        body = body.Replace("{shop_url}", System.Configuration.ConfigurationManager.AppSettings["url"]);
                        OurClass.SendEmail(cust.Email, WebConfigurationManager.AppSettings["Title"] + " Reset Password", body, "");
                    }
                    return ReturnData.MessageSuccess("OK", null);
                }
                return ReturnData.MessageSuccess("", null);
            }
            else
                return ReturnData.MessageSuccess("", null);
        }
        catch (Exception ex)
        {
            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    [WebMethod]
    public dynamic DYNAMIC_GetData_ByEmailAndPassword(string email, string password)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                return db.TBCustomers.Where(x => !x.Deflag && x.Email == email && x.Password == EncryptPassword(password)).Select(x => new
                {
                    x.FirstName,
                    x.IDCustomer,
                    x.LastName,
                    x.Email,
                    x.Gender,
                    x.Birthday,
                    x.PhoneNumber,
                    x.IsSubscribe,
                    x.IDCustomer_Group
                }).FirstOrDefault();

            }
        }
        catch (Exception)
        {
            return null;
        }
    }

    [WebMethod]
    public dynamic DYNAMIC_GetData_AdminOrderAccount()
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                return db.TBCustomers.Where(x => !x.Deflag && x.IDCustomer_Group == 4).Select(x => new
                {
                    x.FirstName,
                    x.IDCustomer,
                    x.LastName,
                    x.Email,
                    x.Gender,
                    x.Birthday,
                    x.PhoneNumber,
                    x.IsSubscribe,
                    x.IDCustomer_Group
                }).FirstOrDefault();

            }
        }
        catch (Exception)
        {
            return null;
        }
    }

    [WebMethod]
    public ReturnData AJAX_FE_Register(string firstName, string lastName, string gender, string email, string password, string phoneNumber, DateTime birthday, bool isSubscribe, string addressname, string address, string postalCode, int idCountry, int idProvince, int idCity, int idDistrict)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                if (IsExistsEmail(email))
                    return ReturnData.MessageFailed("Email is already registered", null);
                TBCustomer _newCust = new TBCustomer();
                _newCust.FirstName = firstName;
                _newCust.LastName = lastName;
                _newCust.Gender = gender;
                _newCust.Email = email;
                _newCust.Password = EncryptPassword(password);
                _newCust.PhoneNumber = phoneNumber;
                _newCust.Birthday = birthday;
                _newCust.IsSubscribe = isSubscribe;
                _newCust.Active = true;
                _newCust.DateInsert = DateTime.Now;
                _newCust.DateLastUpdate = DateTime.Now;
                _newCust.IDCustomer_Group = 1;
                db.TBCustomers.InsertOnSubmit(_newCust);
                //db.SubmitChanges();

                TBAddress _newAddress = new TBAddress();
                _newAddress.TBCustomer = _newCust;
                _newAddress.IDCountry = idCountry;
                _newAddress.IDProvince = idProvince;
                _newAddress.IDCity = idCity;
                _newAddress.IDDistrict = idDistrict;
                _newAddress.Name = addressname;
                _newAddress.Address = address;
                _newAddress.Phone = _newCust.PhoneNumber;
                _newAddress.PostalCode = postalCode;
                _newAddress.PeopleName = firstName + ' ' + lastName;
                _newAddress.Active = true;
                _newAddress.DateInsert = DateTime.Now;
                _newAddress.DateLastUpdate = DateTime.Now;
                db.TBAddresses.InsertOnSubmit(_newAddress);
                db.SubmitChanges();

                //SEND EMAIL
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/template-email-register.html")))
                {
                    Class_Configuration _config = new Class_Configuration();
                    var emailLogo = _config.Dynamic_Get_EmailLogo();
                    string body = "";
                    body = reader.ReadToEnd();
                    body = body.Replace("{name}", _newCust.FirstName);
                    body = body.Replace("{email}", _newCust.Email);
                    body = body.Replace("{password}", password);
                    body = body.Replace("{title}", System.Configuration.ConfigurationManager.AppSettings["Title"]);
                    body = body.Replace("{email_logo}", System.Configuration.ConfigurationManager.AppSettings["url"] + "/assets/images/email_logo/" + emailLogo);
                    body = body.Replace("{shop_url}", System.Configuration.ConfigurationManager.AppSettings["url"]);
                    OurClass.SendEmail(_newCust.Email, WebConfigurationManager.AppSettings["Title"] + " Account Registration", body, "");
                }

                return ReturnData.MessageSuccess("Register success", null);
            }
        }
        catch (Exception ex)
        {
            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_BE_Register_AdminOrder(string firstName, string lastName, string gender, string email, string password, string phoneNumber, DateTime birthday, bool isSubscribe)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                var adminOrder = db.TBCustomers.Where(x => x.IDCustomer_Group == 4).FirstOrDefault();
                if (adminOrder == null)
                {
                    TBCustomer _newCust = new TBCustomer();
                    _newCust.FirstName = firstName;
                    _newCust.LastName = lastName;
                    _newCust.Gender = gender;
                    _newCust.Email = email;
                    _newCust.Password = EncryptPassword(password);
                    _newCust.PhoneNumber = phoneNumber;
                    _newCust.Birthday = birthday;
                    _newCust.IsSubscribe = isSubscribe;
                    _newCust.Active = true;
                    _newCust.DateInsert = DateTime.Now;
                    _newCust.DateLastUpdate = DateTime.Now;
                    _newCust.IDCustomer_Group = 4;
                    db.TBCustomers.InsertOnSubmit(_newCust);
                    db.SubmitChanges();

                    return ReturnData.MessageSuccess("Register success", null);
                }
                else
                {
                    adminOrder.FirstName = firstName;
                    adminOrder.LastName = lastName;
                    adminOrder.Gender = gender;
                    adminOrder.Email = email;
                    adminOrder.Password = EncryptPassword(password);
                    adminOrder.PhoneNumber = phoneNumber;
                    adminOrder.Birthday = birthday;
                    adminOrder.IsSubscribe = isSubscribe;
                    adminOrder.Active = true;
                    adminOrder.DateLastUpdate = DateTime.Now;
                    db.SubmitChanges();

                    return ReturnData.MessageSuccess("Account Updated", null);
                }


                
            }
        }
        catch (Exception ex)
        {
            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    [WebMethod]
    public ReturnData AJAX_FE_RegisterGuest(string firstName, string lastName, string gender, string email, string password, string phoneNumber, DateTime birthday, bool isSubscribe, string addressname, string address, string postalCode, int idCountry, int idProvince, int idCity, int idDistrict)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                //if (IsExistsEmail(db, email))
                //    return ReturnData.MessageFailed("Email is already registered", null);
                SP_RegisterGuestCustomerResult result = db.SP_RegisterGuestCustomer(firstName, lastName, gender, email, EncryptPassword(password), phoneNumber, birthday, idCountry, idProvince, idCity, idDistrict, address, postalCode, "").FirstOrDefault();
                if (result != null)
                {
                    if (AJAX_FE_Login(email, password).success)
                    {
                        Class_Order order = new Class_Order();
                        order.AJAX_SaveAddressToCart(result.IDAddress.Value, result.IDAddress.Value, "");
                        return ReturnData.MessageSuccess("Register success", result.IDCustomer);
                    }
                    else
                        return ReturnData.MessageFailed("authentication invalid", null);
                }
                else
                    return ReturnData.MessageFailed("Result null", null);
            }
        }
        catch (Exception ex)
        {
            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    [WebMethod]
    public ReturnData AJAX_FE_RegisterCustomGroup(string firstName, string lastName, string gender, string email, string password, string phoneNumber, DateTime birthday, bool isSubscribe, string addressname, string address, string postalCode, int idCountry, int idProvince, int idCity, int idDistrict, int idCustomerGroup, string DeliveryAddressName)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                if (idCustomerGroup == 3)
                {
                    if (IsExistsEmail(email))
                    {
                        return ReturnData.MessageFailed("Email is already registered, Please update following customer data", null);
                    }

                    var customerGroup = db.TBCustomer_Groups.Where(x => x.IDCustomer_Group == idCustomerGroup).FirstOrDefault();
                    TBCustomer _newCust = new TBCustomer();
                    _newCust.FirstName = firstName;
                    _newCust.LastName = lastName;
                    _newCust.Gender = gender;
                    _newCust.Email = email;
                    _newCust.Password = EncryptPassword(password);
                    _newCust.PhoneNumber = phoneNumber;
                    _newCust.Birthday = birthday;
                    _newCust.IsSubscribe = isSubscribe;
                    _newCust.Active = true;
                    _newCust.DateInsert = DateTime.Now;
                    _newCust.DateLastUpdate = DateTime.Now;
                    _newCust.IDCustomer_Group = idCustomerGroup;
                    db.TBCustomers.InsertOnSubmit(_newCust);
                    //db.SubmitChanges();

                    TBAddress _newAddress = new TBAddress();
                    _newAddress.TBCustomer = _newCust;
                    _newAddress.IDCountry = idCountry;
                    _newAddress.IDProvince = idProvince;
                    _newAddress.IDCity = idCity;
                    _newAddress.IDDistrict = idDistrict;
                    _newAddress.Name = addressname;
                    _newAddress.Address = address;
                    _newAddress.Phone = _newCust.PhoneNumber;
                    _newAddress.PostalCode = postalCode;
                    _newAddress.PeopleName = DeliveryAddressName;
                    _newAddress.Active = true;
                    _newAddress.DateInsert = DateTime.Now;
                    _newAddress.DateLastUpdate = DateTime.Now;
                    db.TBAddresses.InsertOnSubmit(_newAddress);
                    db.SubmitChanges();

                    Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
                    result.Add("IDCustomer", _newCust.IDCustomer);
                    result.Add("IDAddress", _newAddress.IDAddress);

                    //SEND EMAIL
                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/assets/email-template/template-email-register.html")))
                    {
                        Class_Configuration _config = new Class_Configuration();
                        var emailLogo = _config.Dynamic_Get_EmailLogo();
                        string body = "";
                        body = reader.ReadToEnd();
                        body = body.Replace("{name}", _newCust.FirstName);
                        body = body.Replace("{email}", _newCust.Email);
                        body = body.Replace("{password}", password);
                        body = body.Replace("{title}", System.Configuration.ConfigurationManager.AppSettings["Title"]);
                        body = body.Replace("{email_logo}", System.Configuration.ConfigurationManager.AppSettings["url"] + "/assets/images/email_logo/" + emailLogo);
                        body = body.Replace("{shop_url}", System.Configuration.ConfigurationManager.AppSettings["url"]);
                        OurClass.SendEmail(_newCust.Email, WebConfigurationManager.AppSettings["Title"] + " Account Registration", body, "");
                    }

                    return ReturnData.MessageSuccess("Register success", result);
                }
                else
                {
                    var adminOrder = db.TBCustomers.Where(x => x.IDCustomer_Group == 4 && x.Email.ToLower() == email).FirstOrDefault();
                    if (adminOrder == null)
                        return ReturnData.MessageFailed("Account not found", null);

                    TBAddress _newAddress = new TBAddress();
                    _newAddress.IDCustomer = adminOrder.IDCustomer;
                    _newAddress.IDCountry = idCountry;
                    _newAddress.IDProvince = idProvince;
                    _newAddress.IDCity = idCity;
                    _newAddress.IDDistrict = idDistrict;
                    _newAddress.Name = addressname;
                    _newAddress.Address = address;
                    _newAddress.Phone = phoneNumber;
                    _newAddress.PostalCode = postalCode;
                    _newAddress.PeopleName = DeliveryAddressName;
                    _newAddress.Active = true;
                    _newAddress.DateInsert = DateTime.Now;
                    _newAddress.DateLastUpdate = DateTime.Now;
                    db.TBAddresses.InsertOnSubmit(_newAddress);
                    db.SubmitChanges();

                    Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
                    result.Add("IDCustomer", adminOrder.IDCustomer);
                    result.Add("IDAddress", _newAddress.IDAddress);

                    return ReturnData.MessageSuccess("Register success", result);
                }
                
            }
        }
        catch (Exception ex)
        {
            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    [WebMethod]
    public ReturnData AJAX_FE_Login(string email, string password)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                var data = db.TBCustomers.Where(x => !x.Deflag && x.Email == email && x.Password == EncryptPassword(password)).FirstOrDefault();
                if (data == null)
                    return ReturnData.MessageFailed("Username/password is incorrect", null);
                else
                {
                    var token = EncryptToken(email, password);
                    HttpContext.Current.Response.Cookies.Add(new HttpCookie(System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString(), token));
                    HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()].Expires = DateTime.Now.AddMinutes(120);
                    return ReturnData.MessageSuccess("OK", token);
                }
            }
        }
        catch (Exception ex)
        {
            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    [WebMethod]
    public ReturnData AJAX_FE_Email_Check(string email)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                var data = db.TBCustomers.Where(x => !x.Deflag && x.Email == email && x.IDCustomer_Group != 2).FirstOrDefault();
                if (data == null)
                    return ReturnData.MessageSuccess("Your email not registered before", null);
                else
                {
                    return ReturnData.MessageFailed("Your email has been registered", email);
                }
            }
        }
        catch (Exception ex)
        {
            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    [WebMethod]
    public ReturnData AJAX_FE_Logout(string token)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                if (HttpContext.Current.Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()].Value != token)
                    return ReturnData.MessageFailed("Invalid token!", null);
                else
                {
                    HttpContext.Current.Response.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieUser"].ToString()].Expires = DateTime.Now.AddDays(-1);
                    return ReturnData.MessageSuccess("OK", null);
                }
            }
        }
        catch (Exception ex)
        {
            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_BE_StatusToggle(int idCustomer)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBCustomer selected = db.TBCustomers.Where(x => x.IDCustomer == idCustomer && !x.Deflag).FirstOrDefault();
            if (selected == null)
                return ReturnData.MessageFailed("Data not found", null);
            selected.Active = !selected.Active;
            selected.DateLastUpdate = DateTime.Now;
            db.SubmitChanges();
            return ReturnData.MessageSuccess(selected.FirstName + " status has changed", null);
        }
        catch (Exception ex)
        {
            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_BE_Delete(int idCustomer)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBCustomer selected = db.TBCustomers.Where(x => x.IDCustomer == idCustomer && !x.Deflag).FirstOrDefault();
            if (selected == null)
                return ReturnData.MessageFailed("Data not found", null);
            selected.Deflag = true;
            selected.DateLastUpdate = DateTime.Now;
            db.SubmitChanges();
            return ReturnData.MessageSuccess(selected.FirstName + " has deleted", null);
        }
        catch (Exception ex)
        {
            return ReturnData.MessageFailed(ex.Message, null);
        }
    }
    public dynamic EncryptToken(string email, string password)
    {
        var data = new Dictionary<string, object>() {
            {"email",email},
            {"password",password}
        };
        var key = System.Configuration.ConfigurationManager.AppSettings["key"].ToString();
        return JWT.JsonWebToken.Encode(data, key, JwtHashAlgorithm.HS256);

    }

    public object DecryptToken(string token)
    {
        try
        {
            var key = System.Configuration.ConfigurationManager.AppSettings["key"].ToString();
            var result = JWT.JsonWebToken.DecodeToObject(token, key) as IDictionary<string, object>;
            return result;
        }
        catch (JWT.SignatureVerificationException)
        {
            return "Invalid token!";
        }
    }

    #region REPORTING
    public dynamic Dynamic_Get_NewCustomer(int take)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            if (take == 0)
            {
                return db.TBCustomers.Where(x => !x.Deflag).Select(x => new
                {
                    x.IDCustomer,
                    x.FirstName,
                    x.LastName,
                    x.Email,
                    x.Birthday,
                    x.Gender,
                    x.PhoneNumber,
                    x.Active
                });
            }
            else
            {
                return db.TBCustomers.Where(x => !x.Deflag).Select(x => new
                {
                    x.IDCustomer,
                    x.FirstName,
                    x.LastName,
                    x.Email,
                    x.Birthday,
                    x.Gender,
                    x.PhoneNumber,
                    x.Active
                }).Take(take);
            }
        }
        catch (Exception)
        {
            return null;
        }
    }
    public dynamic dynamic_Get_TotalCustomer()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBCustomers.Where(x => x.Active && !x.Deflag).Count();
        }
        catch (Exception)
        {
            return null;
        }
    }

    public dynamic dynamic_Get_TotalCustomer_FilterDate(DateTime startDate, DateTime endDate)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBCustomers.Where(x => x.Active && !x.Deflag && x.DateInsert >= startDate && x.DateInsert <= endDate).Count();
        }
        catch (Exception)
        {
            return null;
        }
    }
    public dynamic Dynamic_Get_BestCustomer()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.SP_Stats_GetBestCustomer().OrderByDescending(x => x.Amount).ToArray();
        }
        catch (Exception)
        {
            return null;
        }
    }
    #endregion
}