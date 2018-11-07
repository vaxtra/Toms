using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CSV_Product
/// </summary>
public class CSV_Product
{
    public string ReferenceCodeProduct { get; set; }
    public string ReferenceCodeCombination { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string Size { get; set; }
    public string Color { get; set; }
    public string HexaColor { get; set; }
    public decimal Price { get; set; }
    public decimal Weight { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public string ShortDescription { get; set; }
    public int Active { get; set; }
    public string MetaTitle { get; set; }
    public string MetaDescription { get; set; }
    public string MetaKeyword { get; set; }

}