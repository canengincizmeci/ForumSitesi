using adminTabani_01_05_24.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace adminTabani_01_05_24.Controllers
{
    public class HaberController : Controller
    {
        // GET: Haber
        public ActionResult Index()
        {
            blogAdminli_01_05_24Entities model = new blogAdminli_01_05_24Entities();
            var veriler = model.Haberler.Select(p => new Haber
            {
                HaberID = p.HaberID,
                Baslik = p.Baslik,
                icerik = p.icerik,
                paylasan = p.paylasan,
                PaylasanAd = p.Kullanicilar.Ad,
                tarih = p.tarih
            }).ToList();
            return View(veriler);
        }
        public ActionResult HaberDetay(int id)
        {
            blogAdminli_01_05_24Entities model = new blogAdminli_01_05_24Entities();
            var haber = model.Haberler.FirstOrDefault(p => p.HaberID == id);
            ViewBag.Baslik = haber.Baslik;
            ViewBag.Icerik = haber.icerik;
            ViewBag.Yazar = haber.Kullanicilar.Ad;
            ViewBag.Tarih = haber.tarih;
            return View();
        }
    }
}