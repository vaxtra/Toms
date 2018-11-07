using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShippingLabel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //if (Request.QueryString["id"] != null)
            //{
            //    LoadData(Request.QueryString["id"].ToString());
            //}
            //else Response.Redirect("Default.aspx", false);

            //lblShopName.Text = System.Configuration.ConfigurationManager.AppSettings["Title"];
        }
    }

    //private void LoadData(string idTransaksi)
    //{
    //    using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
    //    {
    //        var data = db.TBTransaksis.Where(x => x.IDTransaksi == Custom.StringToInt(idTransaksi)).OrderByDescending(x => x.Tanggal).Select(x => new
    //        {
    //            IDTransaksi = x.IDTransaksi,
    //            NamaPelanggan = x.TBPelanggan.NamaLengkap,
    //            NegaraAlamatPengiriman = x.TBTransaksiOnline.IDAlamatPengiriman.HasValue ? x.TBTransaksiOnline.TBAlamat.TBWilayah1.Nama : null,
    //            ZonaAlamatPengiriman = x.TBTransaksiOnline.IDAlamatPengiriman.HasValue ? x.TBTransaksiOnline.TBAlamat.TBWilayah3.Nama : null,
    //            AlamatPengiriman = x.TBTransaksiOnline.IDAlamatPengiriman.HasValue ? x.TBTransaksiOnline.TBAlamat.AlamatLengkap : null,
    //            KodePosAlamatPengiriman = x.TBTransaksiOnline.IDAlamatPengiriman.HasValue ? x.TBTransaksiOnline.TBAlamat.KodePos : null,
    //            HandphonePengiriman = x.TBTransaksiOnline.IDAlamatPengiriman.HasValue ? x.TBTransaksiOnline.TBAlamat.Handphone : null,
    //            Kurir = x.TBTransaksiOnline.TBBiayaPengiriman.TBKurir.Nama + " " + x.TBTransaksiOnline.TBBiayaPengiriman.TBKurir.JenisLayanan
    //        }).FirstOrDefault();
    //        lblNama.Text = data.NamaPelanggan;
    //        lblAlamat.Text = data.AlamatPengiriman + "<br/>" + data.ZonaAlamatPengiriman + " - " + data.KodePosAlamatPengiriman + "<br/>" + data.NegaraAlamatPengiriman + "<br/>" + data.Kurir;
    //        lblNotelepon.Text = data.HandphonePengiriman;
    //        lblNamaBarang.Text = "";
    //        foreach (var item in db.TBDetailTransaksis.Where(x => x.IDTransaksi.ToString() == idTransaksi))
    //        {
    //            lblNamaBarang.Text += item.TBKombinasiProduk.TBProduk.Nama + " [" + item.TBKombinasiProduk.NamaKombinasiProduk + "] ;";
    //        }
    //    }
    //}
}