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
/// Summary description for Class_Order_Status
/// </summary>
public class Class_Order_Status
{
    public Class_Order_Status()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region AJAX

    public ReturnData AJAX_GetAll()
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            dynamic data = db.TBOrder_Status.Where(x => !x.Deflag).Select(x => new { x.Name, x.IDOrderStatus, x.Color, x.Delivery, x.Logable, x.Paid, x.SendEmail, x.Shipped });
            return ReturnData.MessageSuccess("OK", data);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return ReturnData.MessageFailed(ex.Message, null);
        }
    }

    public ReturnData AJAX_GetAll_ByIDRole(int idRole)
    {
        try
        {
            DataClassesDataContext db = new DataClassesDataContext();
            dynamic data = db.TBRole_OrderStatus.Where(x => x.IDRole == idRole && !x.TBOrder_Status.Deflag).Select(x => new
            {
                Name = x.TBOrder_Status.Name,
                IDOrderStatus = x.TBOrder_Status.IDOrderStatus,
                Color = x.TBOrder_Status.Color,
                Delivery = x.TBOrder_Status.Delivery,
                Logable = x.TBOrder_Status.Logable,
                Paid = x.TBOrder_Status.Paid,
                SendEmail = x.TBOrder_Status.SendEmail,
                Shipped = x.TBOrder_Status.Shipped
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
    
    #endregion

    #region DYNAMIC

    #endregion

    #region LINQ

    private Func<DataClassesDataContext, IEnumerable<TBOrder_Status>> Func_GetData_ByIDOrderStatus
    {
        get
        {
            Func<DataClassesDataContext, IEnumerable<TBOrder_Status>> func =
              CompiledQuery.Compile<DataClassesDataContext, IEnumerable<TBOrder_Status>>
              ((DataClassesDataContext context) => context.TBOrder_Status.AsEnumerable()
                .Where(x => !x.Deflag).ToArray());
            return func;
        }
    }

    private Func<DataClassesDataContext, int, TBOrder_Status> Func_GetDetail
    {
        get
        {
            Func<DataClassesDataContext, int, TBOrder_Status> func =
              CompiledQuery.Compile<DataClassesDataContext, int, TBOrder_Status>
              ((DataClassesDataContext context, int idOrderStatus) => context.TBOrder_Status.AsEnumerable()
                .Where(x => x.IDOrderStatus == idOrderStatus).FirstOrDefault());
            return func;
        }
    }

    public TBOrder_Status GetDetail(DataClassesDataContext db, int idOrderStatus)
    {
        try
        {
            return Func_GetDetail(db, idOrderStatus);
        }
        catch (Exception ex)
        {
            Class_Log_Error log = new Class_Log_Error();
            log.Insert(ex.Message, ex.StackTrace);

            return null;
        }
    }

    public IEnumerable<TBOrder_Status> GetDataBy_IDOrderStatus(DataClassesDataContext db)
    {
        try
        {
            return Func_GetData_ByIDOrderStatus(db);
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