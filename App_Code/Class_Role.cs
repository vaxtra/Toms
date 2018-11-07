using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Globalization;
using JWT;
using WITLibrary;

/// <summary>
/// Summary description for Class_Role
/// </summary>
public class Class_Role
{
    public Class_Role()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region AJAX

    public Datatable AJAX_GetTable(int iDisplayLength, int iDisplayStart, int sEcho, int iSortingCols, int iSortCol, string sSortDir, string search)
    {
        try
        {
            IEnumerable<dynamic> data = Dynamic_GetAll();
            int count = data.Count();
            if (!string.IsNullOrEmpty(search))
                data = data.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToArray();
            List<Dictionary<string, dynamic>> resultList = new List<Dictionary<string, dynamic>>();
            foreach (dynamic currData in data)
            {
                Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();
                newData.Add("IDRole", currData.IDRole);
                newData.Add("Name", currData.Name);
                newData.Add("DateInsert", currData.DateInsert);
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

    public ReturnData AJAX_BE_Insert(string name)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();

            var existRole = db.TBRoles.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();
            if (existRole != null)
                return ReturnData.MessageFailed("Role's name is already registered", null);
            TBRole role = new TBRole();
            role.Name = name;
            role.Deflag = false;
            role.DateInsert = DateTime.Now;
            role.DateLastUpdate = DateTime.Now;
            db.TBRoles.InsertOnSubmit(role);
            db.SubmitChanges();

            return ReturnData.MessageSuccess("New role was added", role);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_BE_Updates(int idRole, string name)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBRole role = db.TBRoles.Where(x => !x.Deflag && x.IDRole == idRole).FirstOrDefault();
            if (role == null)
                return ReturnData.MessageFailed("Role not found", null);
            role.IDRole = idRole;
            role.Name = name;
            if (role.Name != name)
            {
                var existRole = db.TBRoles.Where(x => !x.Deflag && x.Name == name).FirstOrDefault();
                if (existRole != null)
                    return ReturnData.MessageFailed("Role's name is already registered", null);
            }
            role.DateLastUpdate = DateTime.Now;
            db.SubmitChanges();

            return ReturnData.MessageSuccess("Data updated successfully", null);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_BE_Delete(int idRole)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TBRole role = db.TBRoles.Where(x => !x.Deflag && x.IDRole == idRole).FirstOrDefault();
            if (role == null)
                return ReturnData.MessageFailed("Role not found", null);
            role.Deflag = true;
            role.DateLastUpdate = DateTime.Now;
            db.SubmitChanges();

            return ReturnData.MessageSuccess("Data deleted successfully", null);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_GetDetail(int idRole)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            Class_Role role = new Class_Role();
            Class_Menu menu = new Class_Menu();
            Class_Order_Status orderStatus = new Class_Order_Status();
            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
            data.Add("Role", Dynamic_GetDetail(db, idRole));
            data.Add("TreeMenus", role.GetTree_Menu(db, idRole));
            data.Add("SelectedMenus", role.Dynamic_GetData_Menu_ByIDRole(idRole));
            data.Add("TreeOrderStatus", GetTree_OrderStatus(db, idRole));
            data.Add("SelectedOrderStatus", Dynamic_GetData_OrderStatus_ByIDRole(idRole));
            data.Add("Notification", Dynamic_GetNotification_OrderStatus(db, idRole));
            return ReturnData.MessageSuccess("OK", data);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_Update_Menu(int idRole, int[] menus)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                List<int> disctMenu = new List<int>();
                disctMenu = menus.Distinct().ToList();
                if (disctMenu.Count() == 0)
                    return ReturnData.MessageFailed("No selected menu to insert.", null);

                dynamic _role = Dynamic_GetDetail(db, idRole);
                if (_role == null)
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);

                TBRole_Menu[] _oldMenu = GetData_Menu_ByIDRole(db, _role.IDRole);
                List<TBMenu> _menus = new List<TBMenu>();
                Class_Menu _menu = new Class_Menu();
                foreach (int item in disctMenu)
                {
                    if (_oldMenu.AsEnumerable().Where(x => x.IDMenu == item).FirstOrDefault() == null)
                    {
                        TBMenu _newData = _menu.GetDetail(db, item);
                        if (_menu == null)
                            return ReturnData.MessageFailed("Some menu does not exists.", null);

                        _menus.Add(_newData);
                    }
                }
                foreach (TBRole_Menu item in _oldMenu)
                {
                    dynamic _item = disctMenu.Where(x => x == item.IDMenu).FirstOrDefault();
                    if (_item == 0)
                    {
                        db.TBRole_Menus.DeleteOnSubmit(item);
                    }
                    //else
                    //{
                    //    if (!item.TBCategory.Active)
                    //        return ReturnData.MessageFailed(item.TBCategory.Name + " failed to insert, because " + item.TBCategory.Name + " does not active.", null);
                    //}
                }
                db.SubmitChanges();

                foreach (dynamic item in _menus)
                {
                    TBRole_Menu _newData = GetData_Menu_ByIDRole(db, idRole).Where(x => x.IDMenu == item.IDMenu && x.IDRole == idRole).FirstOrDefault();
                    if (_newData == null)
                    {
                        _newData = new TBRole_Menu
                        {
                            IDRole = idRole,
                            IDMenu = item.IDMenu,
                            DateInsert = DateTime.Now,
                            DateLastUpdate = DateTime.Now,
                        };

                        db.TBRole_Menus.InsertOnSubmit(_newData);
                    }
                }
                db.SubmitChanges();

                Dictionary<string, dynamic> _result = new Dictionary<string, dynamic>();
                _result.Add("Role", Dynamic_GetDetail(db, idRole));
                _result.Add("SelectedMenus", Dynamic_GetData_Menu_ByIDRole(idRole));
                _result.Add("SelectedOrderStatus", Dynamic_GetData_OrderStatus_ByIDRole(idRole));
                _result.Add("TreeMenus", GetTree_Menu(db, idRole));
                _result.Add("TreeOrderStatus", GetTree_OrderStatus(db, idRole));
                _result.Add("Notification", Dynamic_GetNotification_OrderStatus(db, idRole));
                return ReturnData.MessageSuccess(disctMenu.Count() + " menus has been successfully updated.", _result);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_Update_OrderStatus(int idRole, int[] orderStatuses)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                if (orderStatuses.Count() == 0)
                    return ReturnData.MessageFailed("No selected menu to insert.", null);

                dynamic _role = Dynamic_GetDetail(db, idRole);
                if (_role == null)
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);

                TBRole_OrderStatus[] _oldOrdStatus = GetData_OrderStatus_ByIDRole(db, _role.IDRole);
                List<TBOrder_Status> _ordStatuses = new List<TBOrder_Status>();
                Class_Order_Status _ordStatus = new Class_Order_Status();
                foreach (int item in orderStatuses)
                {
                    if (_oldOrdStatus.AsEnumerable().Where(x => x.IDOrderStatus == item).FirstOrDefault() == null)
                    {
                        TBOrder_Status _newData = _ordStatus.GetDetail(db, item);
                        if (_ordStatus == null)
                            return ReturnData.MessageFailed("Some order status does not exists.", null);

                        _ordStatuses.Add(_newData);
                    }
                }
                foreach (TBRole_OrderStatus item in _oldOrdStatus)
                {
                    dynamic _item = orderStatuses.Where(x => x == item.IDOrderStatus).FirstOrDefault();
                    if (_item == 0)
                    {
                        db.TBRole_OrderStatus.DeleteOnSubmit(item);
                    }
                    //else
                    //{
                    //    if (!item.TBCategory.Active)
                    //        return ReturnData.MessageFailed(item.TBCategory.Name + " failed to insert, because " + item.TBCategory.Name + " does not active.", null);
                    //}
                }
                db.SubmitChanges();

                foreach (dynamic item in _ordStatuses)
                {
                    TBRole_OrderStatus _newData = GetData_OrderStatus_ByIDRole(db, idRole).Where(x => x.IDOrderStatus == item.IDOrderStatus).FirstOrDefault();
                    if (_newData == null)
                    {
                        _newData = new TBRole_OrderStatus
                        {
                            IDRole = idRole,
                            IDOrderStatus = item.IDOrderStatus,
                            IsNotif = (GetData_OrderStatus_ByIDRole(db, idRole).Where(x => x.IsNotif == true).Count() == 0) ? true : false,
                            DateInsert = DateTime.Now,
                            DateLastUpdate = DateTime.Now,
                        };

                        db.TBRole_OrderStatus.InsertOnSubmit(_newData);
                    }
                }
                db.SubmitChanges();

                Dictionary<string, dynamic> _result = new Dictionary<string, dynamic>();
                Class_Order_Status orderStatus = new Class_Order_Status();
                _result.Add("Role", Dynamic_GetDetail(db, idRole));
                _result.Add("SelectedMenus", Dynamic_GetData_Menu_ByIDRole(idRole));
                _result.Add("SelectedOrderStatus", Dynamic_GetData_OrderStatus_ByIDRole(idRole));
                _result.Add("TreeMenus", GetTree_Menu(db, idRole));
                _result.Add("TreeOrderStatus", GetTree_OrderStatus(db, idRole));
                _result.Add("Notification", Dynamic_GetNotification_OrderStatus(db, idRole));
                return ReturnData.MessageSuccess(orderStatuses.Count() + " order status has been successfully updated.", _result);
            }
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_Notification_OrderStatus(int idRole, int idOrderStatus)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                string _nameBefore = "";
                dynamic _changeData = Dynamic_GetDetail_RoleOrderStatus(db, idRole, idOrderStatus);
                if (_changeData == null)
                    return ReturnData.MessageFailed("The requested resource does not exist.", null);

                _nameBefore = _changeData.Name;
                if (_changeData.IsNotif)
                    return ReturnData.MessageFailed(_nameBefore + " already notification for this role.", null);

                foreach (TBRole_OrderStatus item in GetData_OrderStatus_ByIDRole(db, _changeData.IDRole))
                {
                    if (item.IDRole != _changeData.IDRole)
                    {
                        if (item.IsNotif == true)
                        {
                            item.IsNotif = false;
                            item.DateLastUpdate = DateTime.Now;
                        }
                    }
                    else
                    {
                        item.IsNotif = true;
                        item.DateLastUpdate = DateTime.Now;
                    }
                }
                db.SubmitChanges();
                Dictionary<string, dynamic> _result = new Dictionary<string, dynamic>();
                _result.Add("Role", Dynamic_GetDetail(db, idRole));
                _result.Add("SelectedMenus", Dynamic_GetData_Menu_ByIDRole(idRole));
                _result.Add("SelectedOrderStatus", Dynamic_GetData_OrderStatus_ByIDRole(idRole));
                _result.Add("TreeMenus", GetTree_Menu(db, idRole));
                _result.Add("TreeOrderStatus", GetTree_OrderStatus(db, idRole));
                _result.Add("Notification", Dynamic_GetNotification_OrderStatus(db, idRole));
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

    #endregion

    #region DYNAMIC

    public dynamic Dynamic_GetAll()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBRoles.Where(x => !x.Deflag).Select(x => new
            {
                x.IDRole,
                x.Name,
                x.DateInsert,
            });
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_GetDetail(DataClassesDataContext db, int idRole)
    {
        try
        {
            return db.TBRoles.Where(x => x.IDRole == idRole && !x.Deflag).Select(x => new
            {
                x.Name,
                x.IDRole,
                x.Deflag,
                x.DateInsert,
                x.DateLastUpdate,
            }).FirstOrDefault();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            throw;
        }
    }

    public IEnumerable<dynamic> Dynamic_GetData_Menu_ByIDRole(int idRole)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return Dynamic_Func_GetData_ByIDRole_Menu(db, idRole);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public IEnumerable<dynamic> Dynamic_GetData_OrderStatus_ByIDRole(int idRole)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return Dynamic_Func_GetData_ByIDRole_OrderStatus(db, idRole);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_GetNotification_OrderStatus(DataClassesDataContext db, int idRole)
    {
        try
        {
            return db.TBRole_OrderStatus.Where(x => x.IDRole == idRole && x.IsNotif == true).Select(x => new { 
                x.IDRole,
                x.IDOrderStatus,
                x.TBOrder_Status.Name,
                x.IsNotif,
                x.DateInsert,
                x.DateLastUpdate
            }).FirstOrDefault();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_GetDetail_RoleOrderStatus(DataClassesDataContext db, int idRole, int idOrderStatus)
    {
        try
        {
            return db.TBRole_OrderStatus.Where(x => x.IDRole == idRole && x.IDOrderStatus == idOrderStatus).Select(x => new
            {
                x.IDOrderStatus,
                x.IDRole,
                x.IsNotif,
                x.DateInsert,
                x.DateLastUpdate,
                Name = x.TBOrder_Status.Name
            }).FirstOrDefault();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_OrderNotification_ByIDRole(int idRole)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            int ordNotif = Dynamic_GetNotification_OrderStatus(db, idRole).IDOrderStatus;
            return db.TBOrders.Where(x => x.IDOrderStatus == ordNotif && !x.TBOrder_Status.Deflag).Select(x => new
            {
                x.IDOrder,
                x.Reference,
                x.DateInsert,
                x.DateLastUpdate,
                StatusName = x.TBOrder_Status.Name,
                Color = x.TBOrder_Status.Color,
                DateTime = DateTime.Now - x.DateInsert
            });
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

    public IEnumerable<TBRole_Menu> GetData_Menu_ByIDRole(DataClassesDataContext db, int idRole)
    {
        try
        {
            return Func_GetData_Menu_ByIDRole(db, idRole).ToArray();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public IEnumerable<TBRole_OrderStatus> GetData_OrderStatus_ByIDRole(DataClassesDataContext db, int idRole)
    {
        try
        {
            return Func_GetData_OrderStatus_ByIDRole(db, idRole).ToArray();
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    private Func<DataClassesDataContext, int, IEnumerable<dynamic>> Dynamic_Func_GetData_ByIDRole_Menu
    {
        get
        {
            Func<DataClassesDataContext, int, IEnumerable<dynamic>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, IEnumerable<dynamic>>
              ((DataClassesDataContext context, int idRole) => context.FUNC_Role_Menu_GetData_ByIDRole(idRole)
                  .AsEnumerable().ToArray());
            return func;
        }
    }

    private Func<DataClassesDataContext, int, IEnumerable<TBRole_Menu>> Func_GetData_Menu_ByIDRole
    {
        get
        {
            Func<DataClassesDataContext, int, IEnumerable<TBRole_Menu>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, IEnumerable<TBRole_Menu>>
              ((DataClassesDataContext context, int idRole) => context.TBRole_Menus.AsEnumerable()
                .Where(x => x.IDRole == idRole).ToArray());
            return func;
        }
    }

    private Func<DataClassesDataContext, int, IEnumerable<TBRole_OrderStatus>> Func_GetData_OrderStatus_ByIDRole
    {
        get
        {
            Func<DataClassesDataContext, int, IEnumerable<TBRole_OrderStatus>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, IEnumerable<TBRole_OrderStatus>>
              ((DataClassesDataContext context, int idRole) => context.TBRole_OrderStatus.AsEnumerable()
                .Where(x => x.IDRole == idRole).ToArray());
            return func;
        }
    }


    private Func<DataClassesDataContext, int, IEnumerable<dynamic>> Dynamic_Func_GetData_ByIDRole_OrderStatus
    {
        get
        {
            Func<DataClassesDataContext, int, IEnumerable<dynamic>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, IEnumerable<dynamic>>
              ((DataClassesDataContext context, int idRole) => context.FUNC_Role_OrderStatus_GetData_ByIDRole(idRole)
                  .AsEnumerable().ToArray());
            return func;
        }
    }

    #endregion

    #region TREE
    public IEnumerable<dynamic> GetTree_Menu(DataClassesDataContext db, int idRole)
    {
        Class_Menu _menu = new Class_Menu();
        return GenerateUL(db, _menu, _menu.GetDataBy_IDMenuParent(db, 0), idRole);
    }

    private IEnumerable<WITLibrary.JsTreeModel> GenerateUL(DataClassesDataContext db, Class_Menu _menu, IEnumerable<TBMenu> menus, int idRole)
    {
        List<WITLibrary.JsTreeModel> _newMenu = new List<WITLibrary.JsTreeModel>();
        foreach (var menu in menus)
        {
            if (menu.TBMenus.AsEnumerable().Any())
            {
                _newMenu.Add(new WITLibrary.JsTreeModel
                {
                    text = menu.Name,
                    state = new WITLibrary.JsTreeState { selected = menu.TBRole_Menus.AsEnumerable().FirstOrDefault(x => x.IDRole == idRole) != null },
                    li_attr = new WITLibrary.JsTreeAttr { id = menu.IDMenu.ToString() },
                    children = GenerateUL(db, _menu, _menu.GetDataBy_IDMenuParent(db, menu.IDMenu), idRole).ToArray()
                });
            }
            else
            {
                _newMenu.Add(new WITLibrary.JsTreeModel
                {
                    text = menu.Name,
                    li_attr = new WITLibrary.JsTreeAttr { id = menu.IDMenu.ToString() },
                    state = new WITLibrary.JsTreeState { selected = menu.TBRole_Menus.AsEnumerable().FirstOrDefault(x => x.IDRole == idRole) != null },
                    children = GenerateUL(db, _menu, _menu.GetDataBy_IDMenuParent(db, menu.IDMenu), idRole).ToArray()
                });
            }
        }
        return _newMenu.ToArray();
    }

    public IEnumerable<dynamic> GetTree_OrderStatus(DataClassesDataContext db, int idRole)
    {
        Class_Order_Status _orderStatus = new Class_Order_Status();
        return GenerateUL_OrderStatus(db, _orderStatus, _orderStatus.GetDataBy_IDOrderStatus(db), idRole);
    }

    private IEnumerable<WITLibrary.JsTreeModel> GenerateUL_OrderStatus(DataClassesDataContext db, Class_Order_Status _orderStatus, IEnumerable<TBOrder_Status> orderStatuses, int idRole)
    {
        List<WITLibrary.JsTreeModel> _newOrderStatus = new List<WITLibrary.JsTreeModel>();
        foreach (var orderStatus in orderStatuses)
        {
            if (orderStatus.TBRole_OrderStatus.AsEnumerable().Any())
            {
                _newOrderStatus.Add(new WITLibrary.JsTreeModel
                {
                    text = orderStatus.Name,
                    state = new WITLibrary.JsTreeState { selected = orderStatus.TBRole_OrderStatus.AsEnumerable().FirstOrDefault(x => x.IDRole == idRole) != null },
                    li_attr = new WITLibrary.JsTreeAttr { id = orderStatus.IDOrderStatus.ToString() }
                });
            }
            else
            {
                _newOrderStatus.Add(new WITLibrary.JsTreeModel
                {
                    text = orderStatus.Name,
                    li_attr = new WITLibrary.JsTreeAttr { id = orderStatus.IDOrderStatus.ToString() },
                    state = new WITLibrary.JsTreeState { selected = orderStatus.TBRole_OrderStatus.AsEnumerable().FirstOrDefault(x => x.IDRole == idRole) != null }
                });
            }
        }
        return _newOrderStatus.ToArray();
    }
    #endregion

}