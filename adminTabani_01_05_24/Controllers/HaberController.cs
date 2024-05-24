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
            dbContext model = new dbContext();
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
            dbContext model = new dbContext();
            var haber = model.Haberler.FirstOrDefault(p => p.HaberID == id);
            ViewBag.Baslik = haber.Baslik;
            ViewBag.Icerik = haber.icerik;
            ViewBag.Yazar = haber.Kullanicilar.Ad;
            ViewBag.Tarih = haber.tarih;
            return View();
        }
        public ActionResult UyeIndex()
        {
            dbContext model = new dbContext();
            var veriler = model.Haberler.OrderByDescending(p => p.HaberID).Select(p => new Haber
            {
                HaberID = p.HaberID,
                Baslik = p.Baslik,
                icerik = p.icerik,
                paylasan = p.paylasan,
                PaylasanAd = p.Kullanicilar.Ad,
                tarih = p.tarih,
                _onay = p.onay
            }).ToList();
            return View(veriler);
        }
        public ActionResult UyeHaberDetay(int id)
        {
            dbContext model = new dbContext();
            UyeHaberDetayPage detayPage = new UyeHaberDetayPage();
            detayPage.haber = model.Haberler.Where(p => p.HaberID == id).Select(p => new Haber
            {
                Baslik = p.Baslik,
                HaberID = p.HaberID,
                icerik = p.icerik,
                paylasan = p.paylasan,
                PaylasanAd = p.Kullanicilar.Ad,
                tarih = p.tarih,
                _onay = p.onay
            }).FirstOrDefault();
            detayPage.haber_yorumlari = model.HaberYorumlar.Where(p => p.onay == true).OrderByDescending(p => p.HaberYorumID).Select(p => new HaberYorumlari
            {
                _HaberYorumID = p.HaberYorumID,
                _haberID = p.haberID,
                _icerik = p.icerik,
                _onay = p.onay,
                _tarih = p.tarih,
                _yorumcuID = p.yorumcuID,
                yorumcu_ad = p.Kullanicilar.Ad
            }).ToList();
            return View(detayPage);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Bildir(int haberId)
        {



        }
    }
}