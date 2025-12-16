using MVCOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCOnlineTicariOtomasyon.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        Context c=new Context();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult partial1()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult partial1(Cari p, bool? KurallariKabul)
        {
            // E-posta normalizasyonu
            var email = (p.CariMail ?? string.Empty).Trim().ToLower();
            p.CariMail = email;

            // Kurallar onaylanmadıysa kayıt yapma fakat uyarı göstermeden formu yeniden çiz
            if (KurallariKabul != true)
            {
                return PartialView("partial1", p);
            }

            // Aynı e‑posta ile daha önce kayıt yapılmış mı kontrol et
            var mevcutCari = c.Caris.FirstOrDefault(x => x.CariMail.ToLower() == email);
            if (mevcutCari != null)
            {
                ViewBag.KayitHata = "Bu e-posta adresi ile daha önce kayıt yapılmış. Lütfen farklı bir e-posta deneyin.";
                return PartialView("partial1", p);
            }

            p.durum = true;
            c.Caris.Add(p);
            c.SaveChanges();

            ViewBag.KayitBasarili = "Kayıt işleminiz başarılı. Giriş yapabilirsiniz.";
            return PartialView("partial1", new Cari());
        }
        [HttpGet]
        public ActionResult CariLogin1()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CariLogin1(Cari k)
        {
            var bilgi = c.Caris.FirstOrDefault(x => x.CariMail == k.CariMail && x.Sifre == k.Sifre);
            if(bilgi != null)
            {
                // Rol bilgisini içeren FormsAuthenticationTicket oluştur
                var ticket = new FormsAuthenticationTicket(
                    1,
                    bilgi.CariMail,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    false,
                    "Cari",
                    FormsAuthentication.FormsCookiePath
                );

                string encrypted = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                cookie.Expires = DateTime.Now.AddMinutes(30);
                Response.Cookies.Add(cookie);

                Session["CariMail"]=bilgi.CariMail.ToString();
                return RedirectToAction("Index","CariPanel");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpGet]
        public PartialViewResult partial3()
        {
            return PartialView();
        }
        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(Admin p)
        {
            var bilgi = c.Admins.FirstOrDefault(x => x.KullanıcıAd == p.KullanıcıAd && x.Sifre == p.Sifre);
            if (bilgi != null)
            {
                // Rol bilgisini içeren FormsAuthenticationTicket oluştur
                var ticket = new FormsAuthenticationTicket(
                    1,
                    bilgi.KullanıcıAd,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    false,
                    "Admin",
                    FormsAuthentication.FormsCookiePath
                );

                string encrypted = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                cookie.Expires = DateTime.Now.AddMinutes(30);
                Response.Cookies.Add(cookie);

                Session["KullanıcıAd"] = bilgi.KullanıcıAd.ToString();
                return RedirectToAction("Index", "Kategori");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [AllowAnonymous]
        public ActionResult AdminLogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("AdminLogin", "Login");
        }
    }
}