using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CSV_Shipping
/// </summary>
public class CSV_Shipping
{
    public int IDCarrier { get; set; }
    public int IDDistrict { get; set; }
    public string DistrictName { get; set; }
    public string CityName { get; set; }
    public decimal Price { get; set; }
}