using adminTabani_01_05_24.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace adminTabani_01_05_24.Controllers
{
    public class TartismaController : Controller
    {
        // GET: Tartisma
        public ActionResult Index()
        {
            dbContext model = new dbContext();

            var veriler = model.Tartismalar.Where(p => p.onay == true).Select(p => new Tartisma
            {
                TartismaID = p.TartismaID,
                Baslik = p.Baslik,
                aktiflik = p.aktiflik,
                icerik = p.icerik,
                KullaniciAd = p.Kullanicilar.Ad,
                kullanici_id = p.kullanici_id,
                onay = p.onay,
                tarih = p.tarih
            }).ToList();
            return View(veriler);
        }
        [HttpGet]
        public ActionResult TartismaDetay(int? tartisma_id)
        {
            dbContext model = new dbContext();
            TartismaDetayPage tartismaDetay = new TartismaDetayPage();
            tartismaDetay.tartisma = model.Tartismalar.Where(p => p.TartismaID == tartisma_id).Select(p => new Tartisma
            {
                TartismaID = p.TartismaID,
                Baslik = p.Baslik,
                aktiflik = p.aktiflik,
                icerik = p.icerik,
                KullaniciAd = p.Kullanicilar.Ad,
                kullanici_id = p.kullanici_id,
                onay = p.onay,
                tarih = p.tarih
            }).FirstOrDefault();
            tartismaDetay.tartisma_yorumlari = model.TartismaYorumlar.Where(p => p.tartismaID == tartisma_id & p.onay == true).OrderByDescending(p => p.tartismaYorumID).Select(p => new TartismaYorumlari
            {
                _onay = p.onay,
                _tarih = p.tarih,
                _tartismaID = p.tartismaID,
                _tartismaYorumID = p.tartismaYorumID,
                _yorum = p.yorum,
                _yorumcuID = p.yorumcuID,
                yorumcu_ad = p.Kullanicilar.Ad
            }).ToList();
            return View(tartismaDetay);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YorumYap(string _yorum, int _tartisma_id)
        {
            int id = (int)Session["kullanici_id"];
            dbContext model = new dbContext();
            model.TartismaYorumlar.Add(new TartismaYorumlar
            {
                onay = false,
                tarih = DateTime.Now,
                yorum = _yorum,
                tartismaID = _tartisma_id,
                yorumcuID = id
            });
            model.SaveChanges();
            return RedirectToAction("TartismaDetay", new { tartisma_id = _tartisma_id });
        }
    }
}