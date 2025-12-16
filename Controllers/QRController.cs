using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCOnlineTicariOtomasyon.Controllers
{
    public class QRController : Controller
    {
        // GET: QR
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string kod)
        {
            using(MemoryStream ms=new MemoryStream())
            {
                QRCodeGenerator koduret = new QRCodeGenerator();
                QRCodeData qrCodeData = koduret.CreateQrCode(kod, QRCodeGenerator.ECCLevel.Q);
                QRCode karekod = new QRCode(qrCodeData);
                using (Bitmap resim = karekod.GetGraphic(10))
                {
                    resim.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    ViewBag.karekodimage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }
            return View();
        }
    }
}