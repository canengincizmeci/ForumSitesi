using adminTabani_01_05_24.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace adminTabani_01_05_24.Controllers
{
    public class ResimController : Controller
    {
        // GET: Resim
        public ActionResult Index()
        {
            db_Context model = new db_Context();
            var resimler = model.Resimler.Where(p => p.onay == true).Select(p => new Resim
            {
                aciklama = p.aciklama,
                baslik = p.baslik,
                dosyaYolu = p.dosyaYolu,
                paylasan = p.paylasan,
                PaylasanAd = p.Kullanicilar.Ad,
                resimID = p.resimID,
                tarih = p.tarih,
                _onay = p.onay
            }).ToList();
            return View(resimler);
        }
    }
}