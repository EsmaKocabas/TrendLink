using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCOnlineTicariOtomasyon.Models.Siniflar;

namespace MVCOnlineTicariOtomasyon.Controllers
{
    public class FaturaController : Controller
    {
        // GET: Fatura
        Context c=new Context();
        public ActionResult Index()
        {
            var deger=c.Faturalars.ToList();
            return View(deger);
        }
        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FaturaEkle(Faturalar f)
        {
            c.Faturalars.Add(f);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FaturaGetir(int id)
        {
            var fatura = c.Faturalars.Find(id);
            return View("FaturaGetir", fatura);
        }
        public ActionResult FaturaGuncelle(Faturalar f)
        {
            var fatura = c.Faturalars.Find(f.FaturaID);
            fatura.FaturaSeriNo = f.FaturaSeriNo;
            fatura.FaturaSıraNo = f.FaturaSıraNo;
            fatura.Tarih=f.Tarih;
            fatura.VergiDairesi = f.VergiDairesi;
            fatura.Saat = f.Saat;
            fatura.TeslimEden=f.TeslimEden;
            f.TeslimAlan=f.TeslimAlan;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FaturaDetay(int id)
        {
            var deger = c.FaturaKalems.Where(x => x.Faturaid == id).ToList();
            var dpt = c.FaturaKalems.Where(x => x.Faturaid == id).Select(y => y.Faturalar.FaturaID).FirstOrDefault();
            ViewBag.d = dpt;
            return View(deger);
        }
        [HttpGet]
        public ActionResult YeniKalem()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKalem(FaturaKalem k)
        {
            c.FaturaKalems.Add(k);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}