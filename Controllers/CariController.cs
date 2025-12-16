using MVCOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCOnlineTicariOtomasyon.Controllers
{
    public class CariController : Controller
    {
        Context c=new Context();
        // GET: Cari
        public ActionResult Index()
        {
            var deger=c.Caris.Where(x=>x.durum==true).ToList(); // veritabanındaki durum olan verileri listele
            return View(deger);
        }
        [HttpGet]
        public ActionResult CariEkle()
        {
            return View(); 
        }
        [HttpPost]
        public ActionResult CariEkle(Cari x)
        {
            c.Caris.Add(x);
            x.durum = true;
            c.SaveChanges(); //Aktif olan verileri kaydet
            return RedirectToAction("Index"); 
        }
        public ActionResult CariSil(int id)
        {
            var deger = c.Caris.Find(id);
            deger.durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CariGetir(int id)
        {
            var deger=c.Caris.Find(id);
            return View("CariGetir",deger);
        }
        public ActionResult CariGuncelle(Cari x)
        {
            if (!ModelState.IsValid)
            {
                return View("CariGetir");
            }
            var cari=c.Caris.Find(x.CariID);
            cari.CariAd = x.CariAd;
            cari.CariSoyad = x.CariSoyad;
            cari.CariSehir = x.CariSehir;
            cari.CariMail = x.CariMail;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MüsteriSatis(int id)
        {
            var deger=c.SatisHarekets.Where(x=>x.Cariid==id).ToList();
            var cr = c.Caris.Where(x => x.CariID == id).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.cari = cr;
            return View(deger);
        }
    }
}