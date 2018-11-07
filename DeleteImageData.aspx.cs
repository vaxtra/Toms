using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DeleteImageData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataClassesDataContext db = new DataClassesDataContext();

        var delprods = db.TBProducts.Where(x => x.Deflag);
        int count = 0;
        foreach (var item in delprods)
        {
            var imageprods = db.TBProduct_Photos.Where(x => x.IDProduct == item.IDProduct);
            var checkProds = db.TBProducts.Where(x => !x.Deflag && x.Name.ToLower() == item.Name.ToLower());
            if (checkProds == null)
            {
                Response.Write(item.Name + " (This Product is Still Active) <br/>");
            }
            else
            {
                foreach (var image in imageprods)
                {

                    Response.Write(image.Photo + "<br/>");
                    OurClass.DeleteFile(image.Photo, "product");
                    count++;
                    var combFoto = db.TBProduct_Combination_Photos.Where(x => x.IDProduct_Photo == image.IDProduct_Photo);
                    db.TBProduct_Combination_Photos.DeleteAllOnSubmit(combFoto);
                    db.SubmitChanges();
                    db.TBProduct_Photos.DeleteOnSubmit(image);
                    db.SubmitChanges();
                }
            }

        }
        Response.Write(count + " Image Product Deleted");
    }
}