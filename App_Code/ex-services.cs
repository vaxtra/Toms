using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for ex_services
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class ex_services : System.Web.Services.WebService
{

    public ex_services()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string GetToken()
    {
        return "Hello World";
    }

    [WebMethod]
    public int UpdateQty(string appKey, string combinationCode, int qty)
    {
        try
        {
            appKey = "Uxp46H91dAHVce1C";
            //string token = "Uxp46H91dAHVce1C";
            DataClassesDataContext db = new DataClassesDataContext();
            TBProduct_Combination cmb = db.TBProduct_Combinations.Where(x => !x.Deflag && x.ReferenceCode == combinationCode).FirstOrDefault();
            if (cmb == null)
                return 0;
            else
            {
                cmb.Quantity = qty;
                db.SubmitChanges();
                return 1;
            }
        }
        catch (Exception)
        {
            return -1;
        }
    }

}
