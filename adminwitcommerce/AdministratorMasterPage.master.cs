using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adminwitcommerce_AdministratorMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Cache.SetExpires(DateTime.Now.AddSeconds(60));
        //Response.Cache.SetCacheability(HttpCacheability.Public);
        //Response.Cache.SetValidUntilExpires(false);
        if (Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()] == null)
        {
            Response.Redirect("/adminwitcommerce/login-soft.aspx?returnUrl=" + Request.RawUrl);
        }
        else
        {
            Class_Employee emp = new Class_Employee();
            dynamic employee = emp.Dynamic_GetData_By_Token(Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["cookieAdmin"].ToString()].Value.ToString());
            string name = employee.GetType().GetProperty("Name").GetValue(employee, null);
            lblEmployee.Text = name;

            //MASUKIN ROLE KE HIDDEN ID
            HiddenIDRole.Value = employee.GetType().GetProperty("IDRole").GetValue(employee, null).ToString();
        }

        
    }

}
