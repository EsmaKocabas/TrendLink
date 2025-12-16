using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCOnlineTicariOtomasyon.Models.Siniflar;

namespace MVCOnlineTicariOtomasyon.Controllers
{
    public class DepartmanController : Controller
    {
        // GET: Departman
        Context c=new Context();
        public ActionResult Index()
        {
            var deger = c.Departmans.Where(x=>x.durum==true).ToList();
            return View(deger);
        }
        [HttpGet]
        public ActionResult DepartmanEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DepartmanEkle(Departman d)
        {
            c.Departmans.Add(d);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanSil(int id)
        {
            var deger = c.Departmans.Find(id);
            deger.durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanGetir(int id)
        {
            var departman = c.Departmans.Find(id);
            return View("DepartmanGetir", departman);
        }
        public ActionResult DepartmanGuncelle(Departman d)
        {
            var dprt = c.Departmans.Find(d.DepartmanID);
            dprt.DepartmanAd = d.DepartmanAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanDetay(int id)
        {
            var deger = c.Personels.Where(x => x.Departmanid == id).ToList();
            var dpt=c.Departmans.Where(x=>x.DepartmanID==id).Select(y=>y.DepartmanAd).FirstOrDefault();
            ViewBag.d = dpt;
            return View(deger);
        }
        public ActionResult DepartmanPersonelSatis(int id)
        {
            var deger=c.SatisHarekets.Where(x=>x.Personelid==id).ToList();
            var per=c.Personels.Where(x=>x.PersonelID==id).Select(y=>y.PersonelAd+" "+y.PersonelSoyad).FirstOrDefault();
            ViewBag.dpers = per;
            return View(deger);
        }
    }
}