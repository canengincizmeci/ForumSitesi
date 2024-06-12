using adminTabani_01_05_24.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        public ActionResult ZiyaretciResimDetay(int resim_id)
        {
            
            db_Context model = new db_Context();
            ResimDetayPage detayPage = new ResimDetayPage();
            detayPage.resim = model.Resimler.Where(p => p.resimID == resim_id).OrderByDescending(p => p.resimID).Select(p => new Resim
            {
                resimID = p.resimID,
                aciklama = p.aciklama,
                baslik = p.baslik,
                dosyaYolu = p.dosyaYolu,
                paylasan = p.paylasan,
                PaylasanAd = p.Kullanicilar.Ad,
                tarih = p.tarih,
                _onay = p.onay
            }).FirstOrDefault();
            detayPage.resimYorumlari = model.ResimYorumlar.Where(p => p.resimID == resim_id).OrderByDescending(p => p.ResimYorumID).Select(p => new ResimYorumlari
            {
                _icerik = p.icerik,
                _onay = p.onay,
                _resimID = p.resimID,
                _ResimYorumID = p.ResimYorumID,
                _tarih = p.tarih,
                _yorumcuID = p.yorumcuID,
                yorumcu_ad = p.Kullanicilar.Ad
            }).ToList();
            return View(detayPage);
        }
        [HttpGet]
        public ActionResult ResimPaylas()
        {
            int id = (int)Session["kullanici_id"];
            db_Context model = new db_Context();
            ViewBag.Ad = model.Kullanicilar.Find(id).Ad;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResimPaylas(HttpPostedFileBase _dosya, string _aciklama, string _baslik)
        {
            int id = (int)Session["kullanici_id"];
            db_Context model = new db_Context();
            var dosyaAdi = Path.GetFileName(_dosya.FileName);
            var hedefYol = Path.Combine(Server.MapPath("~/Content/images/"), dosyaAdi);
            _dosya.SaveAs(hedefYol);          
            var kaydedilecekDosyaYolu = dosyaAdi;
            model.Resimler.Add(new Resimler
            {
                dosyaYolu = dosyaAdi,
                aciklama = _aciklama,
                baslik = _baslik,
                paylasan = id,
                tarih = DateTime.Now,
                onay = true
            });
            model.SaveChanges();
            return RedirectToAction("IcerikEklendi", "Home", new { _tur = "Resim" });
        }
    }
}