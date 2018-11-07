using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CSV_Product_New
/// </summary>
public class CSV_Product_New
{
	public CSV_Product_New()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string ProductCode { get; set; }
    public string CombinationCode { get; set; }
    public string Manufacturer { get; set; }
    public string Categories { get; set; }
    public string ProductName { get; set; }
    public string Size { get; set; }
    public string Color { get; set; }
    public string Hexacolor { get; set; }
    public decimal Price { get; set; }
    public decimal Weight { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public string ShortDescription { get; set; }
    public int Active { get; set; }
}