using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using MVCOnlineTicariOtomasyon.Models.Siniflar;

namespace MVCOnlineTicariOtomasyon.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        Context c=new Context();
        public ActionResult Index(int sayfa=1)
        {

            var degerler=c.Kategoris.ToList().ToPagedList(sayfa, 5);
            return View(degerler);
        }
        [HttpGet]//SAYFA YÜKLENDİĞİ ZAMAN ÇALIŞIR.
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]//BİR BUTONA BASILDIĞINDA VEYA BENZER DURUM OLDUĞUNDA ÇALIŞIR.
        public ActionResult KategoriEkle(Kategori k)
        {
            c.Kategoris.Add(k);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriSil(int id)
        {
            var ktg=c.Kategoris.Find(id);
            c.Kategoris.Remove(ktg);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {
            //READ OPERATION
            var ktg= c.Kategoris.Find(id);
            return View("KategoriGetir",ktg);
        }
        public ActionResult KategoriGuncelle(Kategori k)
        {
            var ktgr = c.Kategoris.Find(k.KategoriID);
            ktgr.KategoriAd = k.KategoriAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}