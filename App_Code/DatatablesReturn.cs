using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DatatablesReturn
/// </summary>
public class DatatablesReturn
{
    public int sEcho { get; set; }
    public int iTotalRecords { get; set; }
    public int iTotalDisplayRecords { get; set; }
    public List<Dictionary<string, dynamic>> aaData { get; set; }
}