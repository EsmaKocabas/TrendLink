using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCOnlineTicariOtomasyon.Models.Siniflar;

namespace MVCOnlineTicariOtomasyon.Controllers
{
    public class UrunDetayController : Controller
    {
        // GET: UrunDetay
        Context c=new Context();
        public ActionResult Index()
        {
            Class1 cs=new Class1();
            //var degerler=c.Uruns.Where(x=>x.UrunID==1).ToList();
            cs.Urun = c.Uruns.Where(x => x.UrunID == 1).ToList();
            cs.Detay=c.Detays.Where(y=>y.DetayID==1).ToList();
            return View(cs);
        }
    }
}