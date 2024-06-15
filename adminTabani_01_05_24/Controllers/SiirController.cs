using adminTabani_01_05_24.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            db_Context model = new db_Context();
            var siirler = model.Siirler.OrderByDescending(p => p.siirID).Where(p => p.onay == true).Select(p => new Siir
            {
                siirID = p.siirID,
                icerik = p.icerik,
                siirBaslik = p.siirBaslik,
                tarih = p.tarih,
                yazar = p.yazar,
                YazarAd = p.Kullanicilar.Ad,
                _onay = p.onay
            }).ToList();
            return View(siirler);
        }
        public ActionResult ZiyaretciSiirDetay(int siir_id)
        {
            db_Context model = new db_Context();
            ZiyaretciSiirDetayPage detayPage = new ZiyaretciSiirDetayPage();
            detayPage.siir = model.Siirler.Where(p => p.siirID == siir_id).Select(p => new Siir
            {
                siirID = p.siirID,
                icerik = p.icerik,
                siirBaslik = p.siirBaslik,
                tarih = p.tarih,
                yazar = p.yazar,
                YazarAd = p.Kullanicilar.Ad,
                _onay = p.onay

            }).FirstOrDefault();
            detayPage.siir_yorumlari = model.SiirYorumlar.OrderByDescending(p => p.SiirYorumID).Where(p => p.onay == true).Select(p => new SiirYorumlari
            {
                _SiirYorumID = p.SiirYorumID,
                _icerik = p.icerik,
                _onay = p.onay,
                _siirID = p.siirID,
                _tarih = p.tarih,
                _yorumcuID = p.yorumcuID,
                YorumcuAd = p.Kullanicilar.Ad
            }).ToList();
            return View(detayPage);
        }
        public ActionResult UyeSiirIndex()
        {
            int id = (int)Session["kullanici_id"];
            db_Context model = new db_Context();
            var veriler = model.Siirler.Where(p => p.onay == true).OrderByDescending(p => p.siirID).Select(p => new Siir
            {
                siirID = p.siirID,
                icerik = p.icerik,
                siirBaslik = p.siirBaslik,
                tarih = p.tarih,
                yazar = p.yazar,
                YazarAd = p.Kullanicilar.Ad,
                _onay = p.onay
            }).ToList();
            return View(veriler);
        }
        public ActionResult UyeSiirDetay(int siir_id)
        {
            int id = (int)Session["kullanici_id"];
            bool deger = true;
            db_Context model = new db_Context();
            ZiyaretciSiirDetayPage detayPage = new ZiyaretciSiirDetayPage();
            if (model.Siirler.Find(siir_id).yazar == id)
            {
                deger = false;
                ViewBag.Deger = deger;
            }
            else
            {
                ViewBag.Deger = deger;

            }
            detayPage.siir = model.Siirler.Where(p => p.siirID == siir_id).Select(p => new Siir
            {
                siirID = p.siirID,
                icerik = p.icerik,
                siirBaslik = p.siirBaslik,
                tarih = p.tarih,
                yazar = p.yazar,
                YazarAd = p.Kullanicilar.Ad,
                _onay = p.onay
            }).FirstOrDefault();
            detayPage.siir_yorumlari = model.SiirYorumlar.OrderByDescending(p => p.SiirYorumID).Where(p => p.onay == true).Select(p => new SiirYorumlari
            {
                _SiirYorumID = p.SiirYorumID,
                _onay = p.onay,
                YorumcuAd = p.Kullanicilar.Ad,
                _icerik = p.icerik,
                _siirID = p.siirID,
                _tarih = p.tarih,
                _yorumcuID = p.yorumcuID
            }).ToList();
            return View(detayPage);
        }
        [HttpGet]
        public ActionResult Bildir(int _siirID)
        {
            ViewBag.SiirID = _siirID;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Bildir(int _siirID, string _sebep)
        {
            int id = (int)Session["kullanici_id"];
            db_Context model = new db_Context();
            model.SiirSikayetler.Add(new SiirSikayetler
            {
                sebep = _sebep,
                siir_id = _siirID,
                sikayetci_id = id,
                tarih = DateTime.Now

            });
            model.SaveChanges();
            return RedirectToAction("SiirSikayetTamam", new { siir_id = _siirID });
        }
        public ActionResult SiirSikayetTamam(int siir_id)
        {
            int id = (int)Session["kullanici_id"];
            db_Context model = new db_Context();
            var baslik = model.Siirler.Find(siir_id).siirBaslik;
            var yazarAd = model.Kullanicilar.Find(id).Ad;
            ViewBag.Baslik = baslik;
            ViewBag.Yazar = yazarAd;
            ViewBag.SiirID = siir_id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YorumYap(int _siirID, string _yorum)
        {
            int id = (int)Session["kullanici_id"];
            db_Context model = new db_Context();
            model.SiirYorumlar.Add(new SiirYorumlar
            {
                icerik = _yorum,
                onay = true,
                siirID = _siirID,
                tarih = DateTime.Now,
                yorumcuID = id,
            });
            model.SaveChanges();
            return RedirectToAction("UyeSiirDetay", new { siir_id = _siirID });
        }
        [HttpGet]
        public ActionResult SiirYaz()
        {
            int id = (int)Session["kullanici_id"];
            db_Context model = new db_Context();
            ViewBag.Ad = model.Kullanicilar.Find(id).Ad;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SiirYaz(string _icerik, string _baslik)
        {
            int id = (int)Session["kullanici_id"];
            db_Context model = new db_Context();
            model.Siirler.Add(new Siirler
            {
                icerik = _icerik,
                onay = true,
                siirBaslik = _baslik,
                tarih = DateTime.Now,
                yazar = id
            });
            model.SaveChanges();
            return RedirectToAction("IcerikEklendi", "Home", new { _tur = "Şiir" });
        }
    }
}