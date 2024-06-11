using adminTabani_01_05_24.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
            db_Context model = new db_Context();
            var veriler = model.Haberler.Select(p => new Haber
            {
                HaberID = p.HaberID,
                Baslik = p.Baslik,
                icerik = p.icerik,
                paylasan = p.paylasan,
                PaylasanAd = p.Kullanicilar.Ad,
                tarih = p.tarih,
                _onay = true
            }).ToList();
            return View(veriler);
        }
        public ActionResult HaberDetay(int id)
        {
            db_Context model = new db_Context();
            var haber = model.Haberler.FirstOrDefault(p => p.HaberID == id);
            ViewBag.Baslik = haber.Baslik;
            ViewBag.Icerik = haber.icerik;
            ViewBag.Yazar = haber.Kullanicilar.Ad;
            ViewBag.Tarih = haber.tarih;
            return View();
        }
        public ActionResult UyeIndex()
        {
            db_Context model = new db_Context();
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
            db_Context model = new db_Context();
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
        public ActionResult YorumYap(int _haberID, string _yorum)
        {
            int id = (int)Session["kullanici_id"];
            db_Context model = new db_Context();
            model.HaberYorumlar.Add(new HaberYorumlar
            {
                haberID = _haberID,
                icerik = _yorum,
                onay = true,
                tarih = DateTime.Now,
                yorumcuID = id
            });
            model.SaveChanges();
            return RedirectToAction("UyeHaberDetay", new { id = _haberID });
        }
        [HttpGet]
        public ActionResult Bildir(int haberId)
        {
            ViewBag.HaberID = haberId;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Bildir(int haberId, string _sebep)
        {
            int id = (int)Session["kullanici_id"];
            db_Context model = new db_Context();
            //model.HaberYorumlar.Add(new HaberYorumlar
            //{
            //    haberID = haberId,
            //    icerik = _sebep,
            //    onay = true,
            //    tarih = DateTime.Now,
            //    yorumcuID = id
            //});
            model.HaberSikayetler.Add(new HaberSikayetler
            {
                haber_id = haberId,
                sebep = _sebep,
                sikayetci_id = id,
                tarih = DateTime.Now
            });
            model.SaveChanges();
            return RedirectToAction("HaberSikayetTamam", new { _haberID = haberId });
        }
        public ActionResult HaberSikayetTamam(int _haberID)
        {
            int id = (int)Session["kullanici_id"];
            db_Context model = new db_Context();
            string baslik = model.Haberler.Find(_haberID).Baslik;
            string kisi_ad = model.Kullanicilar.Find(id).Ad;
            ViewBag.Baslik = baslik;
            ViewBag.KullaniciAd = kisi_ad;
            ViewBag.HaberID = _haberID;
            return View();
        }
        [HttpGet]
        public ActionResult HaberPaylas()
        {
            int id = (int)Session["kullanici_id"];
            db_Context model = new db_Context();
            ViewBag.Ad = model.Kullanicilar.Find(id).Ad;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HaberPaylas(string _baslik, string _icerik)
        {
            int id = (int)Session["kullanici_id"];
            db_Context model = new db_Context();
            model.Haberler.Add(new Haberler
            {
                Baslik = _baslik,
                icerik = _icerik,
                onay = true,
                paylasan = id,
                tarih = DateTime.Now
            });
            model.SaveChanges();
            return RedirectToAction("IcerikEklendi", "Home", new { _tur = "Haber" });
        }
    }
}