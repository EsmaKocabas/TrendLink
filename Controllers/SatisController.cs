using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCOnlineTicariOtomasyon.Models.Siniflar;

namespace MVCOnlineTicariOtomasyon.Controllers
{
    public class SatisController : Controller
    {
        // GET: Satis
        Context c= new Context();
        public ActionResult Index()
        {
            var deger=c.SatisHarekets.ToList();
            return View(deger);
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            List<SelectListItem> deger1= (from x in c.Uruns.ToList()
                                          select new SelectListItem
                                          {
                                              Text=x.UrunAD,
                                              Value=x.UrunID.ToString(),
                                          }).ToList();
            List<SelectListItem> deger2 = (from x in c.Caris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd+" "+x.CariSoyad,
                                               Value = x.CariID.ToString(),
                                           }).ToList();
            List<SelectListItem> deger3 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelID.ToString(),
                                           }).ToList();
            ViewBag.dgr1=deger1;
            ViewBag.dgr2=deger2;
            ViewBag.dgr3=deger3;
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(SatisHareket s)
        {
            s.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(s);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SatisGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Uruns.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.UrunAD,
                                               Value = x.UrunID.ToString(),
                                           }).ToList();
            List<SelectListItem> deger2 = (from x in c.Caris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd + " " + x.CariSoyad,
                                               Value = x.CariID.ToString(),
                                           }).ToList();
            List<SelectListItem> deger3 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelID.ToString(),
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            var deger = c.SatisHarekets.Find(id);
            return View("SatisGetir",deger);
        }
        public ActionResult SatisGuncelle(SatisHareket s)
        {
            var sts = c.SatisHarekets.Find(s.SatisID);
            sts.Urunid = s.Urunid;
            sts.Cariid = s.Cariid;
            sts.Personelid = s.Personelid;
            sts.Adet = s.Adet;
            sts.Fiyat = s.Fiyat;
            sts.ToplamTutar = s.ToplamTutar;
            sts.Tarih= s.Tarih;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SatisDetay(int id)
        {
            var deger=c.SatisHarekets.Where(x=>x.SatisID==id).ToList();
            return View(deger);
        }
    }
}