using adminTabani_01_05_24.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace adminTabani_01_05_24.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            db_Context model = new db_Context();
            KullaniciHomeViewModel veriler = new KullaniciHomeViewModel();
            veriler.haberler = model.Haberler.OrderByDescending(p => p.tarih).Take(5).Select(p => new Haber
            {
                HaberID = p.HaberID,
                Baslik = p.Baslik,
                icerik = p.icerik.Substring(0, 20),
                tarih = p.tarih,
                paylasan = p.paylasan,
                PaylasanAd = p.Kullanicilar.Ad
            }).ToList();
            veriler.yazilar = model.Yazilar.OrderByDescending(p => p.tarih).Take(5).Select(p => new Yazi
            {
                yazi_id = p.yazi_id,
                yazar_id = p.yazar_id,
                Baslik = p.Baslik,
                icerik = p.icerik.Substring(0, 20),
                tarih = p.tarih,
                onay = p.onay,
                yazar_ad = p.Kullanicilar.Ad
            }).ToList();
            veriler.resimler = model.Resimler.OrderByDescending(p => p.resimID).Take(5).Select(p => new Resim
            {
                resimID = p.resimID,
                baslik = p.baslik,
                aciklama = p.aciklama,
                dosyaYolu = p.dosyaYolu,
                paylasan = p.paylasan,
                PaylasanAd = p.Kullanicilar.Ad,
                tarih = p.tarih
            }).ToList();
            veriler.siirler = model.Siirler.OrderByDescending(p => p.tarih).Take(5).Select(p => new Siir
            {
                siirID = p.siirID,
                icerik = p.icerik.Substring(0, 20),
                siirBaslik = p.siirBaslik,
                tarih = p.tarih,
                yazar = p.yazar,
                YazarAd = p.Kullanicilar.Ad
            }).ToList();
            veriler.tartismalar = model.Tartismalar.OrderByDescending(p => p.tarih).Take(5).Select(p => new Tartisma
            {
                TartismaID = p.TartismaID,
                tarih = p.tarih,
                aktiflik = p.aktiflik,
                Baslik = p.Baslik,
                icerik = p.icerik.Substring(0, 20),
                kullanici_id = p.kullanici_id,
                onay = p.onay,
                KullaniciAd = p.Kullanicilar.Ad
            }).ToList();

            return View(veriler);
        }
        public ActionResult Page403()
        {
            Response.StatusCode = 403;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
        public ActionResult Page404()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
    }
}
