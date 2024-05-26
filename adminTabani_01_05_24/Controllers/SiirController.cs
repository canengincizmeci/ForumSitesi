using adminTabani_01_05_24.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace adminTabani_01_05_24.Controllers
{
    public class SiirController : Controller
    {
        // GET: Siir
        public ActionResult ZiyaretciIndex()
        {
            dbContext model = new dbContext();
            var siirler = model.Siirler.OrderByDescending(p => p.siirID).Where(p => p.onay == true).Select(p => new Siir
            {
                siirID=p.siirID,
                icerik=p.icerik,
                siirBaslik=p.siirBaslik,
                tarih=p.tarih,
                yazar=p.yazar,
                YazarAd= p.Kullanicilar.Ad,
                _onay = p.onay
            }).ToList();
            return View(siirler);
        }
    }
}