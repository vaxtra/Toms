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
/// Summary description for Class_Menu
/// </summary>
public class Class_Menu
{
    public Class_Menu()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region AJAX

    #endregion

    #region DYNAMIC
    public dynamic Dynamic_GetAll()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBMenus.Select(x => new
            {
                x.IDMenu,
                x.IDMenuParent,
                x.Name,
                x.Link,
                x.DateInsert,
                x.DateLastUpdate
            });
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public dynamic Dynamic_GetAllMenu_ParentNull()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            return db.TBMenus.Where(x => x.IDMenuParent == null).Select(x => new
            {
                x.IDMenu,
                x.IDMenuParent,
                x.Name,
                x.Link,
                x.DateInsert,
                x.DateLastUpdate,
                x.Icon,
                AttributeValue = x.Name.Replace(" ", "_").ToLower(),
                SubMenu = Dynamic_GetDataBy_IDMenuParent(db, x.IDMenu)
            });
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public IEnumerable<dynamic> Dynamic_GetDataBy_IDMenuParent(DataClassesDataContext db, int idMenuParent)
    {
        try
        {
            Class_Product _product = new Class_Product();
            if (idMenuParent == 0)
                return db.TBMenus.Where(x => x.IDMenuParent == idMenuParent).AsEnumerable().Select(x => new
                {
                    x.IDMenu,
                    x.IDMenuParent,
                    x.Name,
                    x.Link,
                    AttributeValue = x.Name.Replace(" ", "_").ToLower(),
                    x.DateInsert,
                    x.DateLastUpdate
                }).ToArray();
            return db.TBMenus.Where(x => x.IDMenuParent == idMenuParent).AsEnumerable().Select(x => new
            {
                x.IDMenu,
                x.IDMenuParent,
                x.Name,
                x.Link,
                AttributeValue = x.Name.Replace(" ", "_").ToLower(),
                x.DateInsert,
                x.DateLastUpdate
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
    private Func<DataClassesDataContext, IEnumerable<TBMenu>> Func_GetData_ByIDMenuParentNull
    {
        get
        {
            Func<DataClassesDataContext, IEnumerable<TBMenu>> func =
              CompiledQuery.Compile<DataClassesDataContext, IEnumerable<TBMenu>>
              ((DataClassesDataContext context) => context.TBMenus.AsEnumerable()
                .Where(x => x.IDMenuParent == null).ToArray());
            return func;
        }
    }

    private Func<DataClassesDataContext, int, IEnumerable<TBMenu>> Func_GetData_ByIDMenuParent
    {
        get
        {
            Func<DataClassesDataContext, int, IEnumerable<TBMenu>> func =
              CompiledQuery.Compile<DataClassesDataContext, int, IEnumerable<TBMenu>>
              ((DataClassesDataContext context, int idMenu) => context.TBMenus.AsEnumerable()
                .Where(x => x.IDMenuParent == idMenu).ToArray());
            return func;
        }
    }

    private Func<DataClassesDataContext, int, TBMenu> Func_GetDetail
    {
        get
        {
            Func<DataClassesDataContext, int, TBMenu> func =
              CompiledQuery.Compile<DataClassesDataContext, int, TBMenu>
              ((DataClassesDataContext context, int idMenu) => context.TBMenus.AsEnumerable()
                .Where(x => x.IDMenu == idMenu).FirstOrDefault());
            return func;
        }
    }

    public IEnumerable<TBMenu> GetDataBy_IDMenuParent(DataClassesDataContext db, int idMenuParent)
    {
        try
        {
            if (idMenuParent == 0)
                return Func_GetData_ByIDMenuParentNull(db);
            else
                return Func_GetData_ByIDMenuParent(db, idMenuParent);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public TBMenu GetDetail(DataClassesDataContext db, int idMenu)
    {
        try
        {
            return Func_GetDetail(db, idMenu);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    #endregion
}