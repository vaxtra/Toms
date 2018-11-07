using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adminwitcommerce_test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public void Test()
    {
        DataClassesDataContext db = new DataClassesDataContext();
        JavaScriptSerializer ser = new JavaScriptSerializer();

        int iDisplayLength = Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]);
        int iDisplayStart = Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]);
        int iEcho = Convert.ToInt32(HttpContext.Current.Request["sEcho"]);
        int iSortingCols = Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]);

        int iSortCol = Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]);
        string sSortDir = HttpContext.Current.Request["sSortDir_0"];



        IQueryable<TBProduct> jurnals = db.TBProducts.AsQueryable();
        IQueryable<TBProduct> filteredJurnals = jurnals;
        if (HttpContext.Current.Request["sSearch"] != null)
        {
            // there is a search field....
            string searchString = HttpContext.Current.Request["sSearch"];
            filteredJurnals = from s in filteredJurnals
                              select s;
        }

        // iSortCol_0  //    0=cust Ac     1=cust name    2=rep
        // iSortDir_0  // asc or desc...
        if (iSortingCols == 1)
        {
            //if (sSortDir == "asc" && iSortCol == 0) { filteredJurnals = filteredJurnals.OrderBy(p => p.Judul); }
            //if (sSortDir == "desc" && iSortCol == 0) { filteredJurnals = filteredJurnals.OrderByDescending(p => p.Judul); }

            //if (sSortDir == "asc" && iSortCol == 1) { filteredJurnals = filteredJurnals.OrderBy(p => p.NamaMajalah); }
            //if (sSortDir == "desc" && iSortCol == 1) { filteredJurnals = filteredJurnals.OrderByDescending(p => p.NamaMajalah); }

            //if (sSortDir == "asc" && iSortCol == 2) { filteredJurnals = filteredJurnals.OrderBy(p => p.Vol); }
            //if (sSortDir == "desc" && iSortCol == 2) { filteredJurnals = filteredJurnals.OrderByDescending(p => p.Vol); }

            //if (sSortDir == "asc" && iSortCol == 3) { filteredJurnals = filteredJurnals.OrderBy(p => p.NoMajalah); }
            //if (sSortDir == "desc" && iSortCol == 3) { filteredJurnals = filteredJurnals.OrderByDescending(p => p.NoMajalah); }

            //if (sSortDir == "asc" && iSortCol == 4) { filteredJurnals = filteredJurnals.OrderBy(p => p.Bulan); }
            //if (sSortDir == "desc" && iSortCol == 4) { filteredJurnals = filteredJurnals.OrderByDescending(p => p.Bulan); }

            //if (sSortDir == "asc" && iSortCol == 5) { filteredJurnals = filteredJurnals.OrderBy(p => p.Tahun); }
            //if (sSortDir == "desc" && iSortCol == 5) { filteredJurnals = filteredJurnals.OrderByDescending(p => p.Tahun); }

            //if (sSortDir == "asc" && iSortCol == 6) { filteredJurnals = filteredJurnals.OrderBy(p => p.KodeMajalah); }
            //if (sSortDir == "desc" && iSortCol == 6) { filteredJurnals = filteredJurnals.OrderByDescending(p => p.KodeMajalah); }
        }


        filteredJurnals = filteredJurnals.Skip(iDisplayStart).Take(iDisplayLength);
        List<List<string>> jurnalList = new List<List<string>>();
        List<Dictionary<string, dynamic>> testList = new List<Dictionary<string, dynamic>>();
        foreach (TBProduct currJurnal in filteredJurnals)
        {
            //List<string> newCustomer = new List<string>();
            Dictionary<string, dynamic> newCustomer = new Dictionary<string, dynamic>();
            newCustomer.Add("Name", currJurnal.Name);
            //newCustomer.Add("aksi","<a href='edit.aspx?id=" + currJurnal.IDJurnal + "' data-toggle='tooltip' data-original-title='Edit' class='btn btn-xs btn-warning btn-mini'><i class='icon-edit'></i></a>" + "<a data-toggle='tooltip' data-original-title='Hapus' class='btn btn-xs btn-danger btn-mini'><i class='icon-remove'></i></a>");
            //jurnalList.Add(newCustomer);
            testList.Add(newCustomer);
        }
        // lightCustomerList;

        DatatablesReturn returnTable = new DatatablesReturn();
        returnTable.sEcho = iEcho;
        returnTable.iTotalRecords = jurnals.Count();
        returnTable.iTotalDisplayRecords = jurnals.Count();
        returnTable.aaData = testList;

        HttpContext.Current.Response.Clear();

        HttpContext.Current.Response.ContentType = ("text/html");
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(ser.Serialize(returnTable));
        HttpContext.Current.Response.End();
    }
}