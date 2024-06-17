using adminTabani_01_05_24.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace adminTabani_01_05_24.Controllers
{
    public class AyarlarController : Controller
    {
        // GET: Ayarlar
        public ActionResult Index()
        {
            int id;
            try
            {
                id = (int)Session["kullanici_id"];
            }
            catch (Exception)
            {
                Session.RemoveAll();
                return RedirectToAction("Login", "Kullanici");
            }
            db_Context model = new db_Context();
            var kisi = model.Kullanicilar.Find(id);
            ViewBag.Ad = kisi.Ad;
            return View();
        }
        [HttpGet]
        public ActionResult KullaniciAdiDegistir(string mesaj)
        {
            int id;
            try
            {
                id = (int)Session["kullanici_id"];
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Kullanici");

            }
            if (mesaj == null)
            {
                ViewBag.Mesaj = "Yeni Kullanici Adınızı Giriniz";
            }
            else
            {
                ViewBag.Mesaj = mesaj;
            }
            db_Context model = new db_Context();
            var kisi = model.Kullanicilar.Find(id);
            ViewBag.Ad = kisi.Ad;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KullaniciAdiDegistir(string kullanici_adi, string yeniAd)
        {
            int id;
            try
            {
                id = (int)Session["kullanici_id"];
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Kullanici");
            }
            if (kullanici_adi == yeniAd)
            {
                return RedirectToAction("KullaniciAdiDegistir", new { mesaj = "Kullanıcı Adı Eskiyle Aynı" });
            }
            else
            {
                db_Context model = new db_Context();
                if (model.Kullanicilar.Any(p => p.Ad == yeniAd))
                {
                    return RedirectToAction("KullaniciAdiDegistir", new { mesaj = "Bu kullanıcı adı alınmış" });
                }
                else
                {
                    model.Kullanicilar.Find(id).Ad = yeniAd;
                    model.SaveChanges();
                    return RedirectToAction("AdDegistirmeTamam");
                }
            }
        }
        public ActionResult AdDegistirmeTamam()
        {
            int id;
            try
            {
                id = (int)Session["kullanici_id"];
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Kullanici");
            }
            db_Context model = new db_Context();
            ViewBag.Ad = model.Kullanicilar.Find(id).Ad;
            return View();
        }
        [HttpGet]
        public ActionResult SifreDegistir(string mesaj)
        {
            int id;
            try
            {
                id = (int)Session["kullanici_id"];
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Kullanici");
            }
            if (mesaj == null)
            {
                ViewBag.Mesaj = "Yeni Şifrenizi giriniz";
            }
            else
            {
                ViewBag.Mesaj = mesaj;
            }
            db_Context model = new db_Context();
            ViewBag.Sifre = model.Kullanicilar.Find(id).kullanici_sifre;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SifreDegistir(string eskiSifre, string yeniSifre)
        {
            int id;
            try
            {
                id = (int)Session["kullanici_id"];
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Kullanici");
            }
            if (eskiSifre == yeniSifre)
            {
                return RedirectToAction("SifreDegistir", new { mesaj = "Eski şifre yeni şifre ile aynı olamaz" });
            }
            db_Context model = new db_Context();
            var kisi = model.Kullanicilar.Find(id);
            kisi.kullanici_sifre = yeniSifre;
            model.SaveChanges();
            return RedirectToAction("SifreDegistirmeTamam");
        }
        public ActionResult SifreDegistirmeTamam()
        {
            int id;
            try
            {
                id = (int)Session["kullanici_id"];
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Kullanici");
            }
            db_Context model = new db_Context();
            ViewBag.Ad = model.Kullanicilar.Find(id).Ad;
            return View();
        }
    }

}