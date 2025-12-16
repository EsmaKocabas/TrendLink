using MVCOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCOnlineTicariOtomasyon.Controllers
{
    public class KargoController : Controller
    {
        // GET: Kargo
        Context c = new Context();
        public ActionResult Index(string k)
        {
            var kargolar = from x in c.KargoDetays select x;
            if (!string.IsNullOrEmpty(k))
            {
                kargolar = kargolar.Where(y => y.TakipKodu.Contains(k));
            }
            return View(kargolar.ToList());
        }
        [HttpGet]
        public ActionResult YeniKargo()
        {
            Random rnd = new Random();
            string[] karakterler = { "A", "B", "C", "D" };
            int k1, k2, k3;
            k1 = rnd.Next(0, 4);
            k2 = rnd.Next(0, 4);
            k3 = rnd.Next(0, 4);
            int sayi1, sayi2, sayi3;
            sayi1 = rnd.Next(100, 1000);
            sayi2 = rnd.Next(10, 99);
            sayi3 = rnd.Next(10, 99);
            string kargono = karakterler[k1] + sayi1 + karakterler[k2] + sayi2 + karakterler[k3] + sayi3;
            ViewBag.kargono = kargono;
            return View();
        }
        [HttpPost]
        public ActionResult YeniKargo(KargoDetay k)
        {
            c.KargoDetays.Add(k);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KargoTakip(string id)
        {
            var deger= c.KargoTakips.Where(x => x.TakipKodu == id).ToList();
            return View(deger);
        }
    }
}