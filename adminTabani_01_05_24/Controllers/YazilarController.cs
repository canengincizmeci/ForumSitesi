using adminTabani_01_05_24.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Web;
using System.Web.Mvc;


namespace adminTabani_01_05_24.Controllers
{
    public class YazilarController : Controller
    {
        // GET: Yazilar
        public ActionResult Index()
        {
            db_Context model = new db_Context();
            var veriler = model.Yazilar.Where(p => p.onay == true).OrderByDescending(p => p.yazi_id).Select(p => new Yazi
            {
                yazar_id = p.yazi_id,
                Baslik = p.Baslik,
                icerik = p.icerik,
                tarih = p.tarih,
                onay = p.onay,
                yazi_id = p.yazi_id,
                yazar_ad = p.Kullanicilar.Ad
            }).ToList();
            return View(veriler);
        }
        [HttpGet]
        public ActionResult yaziDetay(int yaziID)
        {
            db_Context model = new db_Context();
            YaziDetayPage yaziDetay = new YaziDetayPage();
            yaziDetay.yazi = model.Yazilar.Where(p => p.yazi_id == yaziID).Select(p => new Yazi
            {
                yazi_id = p.yazi_id,
                Baslik = p.Baslik,
                onay = p.onay,
                tarih = p.tarih,
                yazar_ad = p.Kullanicilar.Ad,
                yazar_id = p.yazar_id,
                icerik = p.icerik
            }).FirstOrDefault();
            yaziDetay.yaziyorumlari = model.YaziYorumlar.Where(p => p.yaziID == yaziID & p.onay == true).OrderByDescending(p => p.YaziYorumID).Select(p => new YaziYorumlari
            {
                _yaziID = p.yaziID,
                _icerik = p.icerik,
                _onay = p.onay,
                _tarih = p.tarih,
                _YaziYorumID = p.YaziYorumID,
                _yorumcuID = p.yorumcuID,
                yorumcuAd = p.Kullanicilar.Ad
            }).ToList();
            return View(yaziDetay);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YorumYap(int _yaziID, string _yorum)
        {
            int id = (int)Session["kullanici_id"];
            db_Context model = new db_Context();
            model.YaziYorumlar.Add(new YaziYorumlar
            {
                yorumcuID = id,
                tarih = DateTime.Now,
                icerik = _yorum,
                yaziID = _yaziID,
                onay = true
            });
            model.SaveChanges();
            return RedirectToAction("yaziDetay", new { yaziID = _yaziID });
        }
        [HttpGet]
        public ActionResult Bildir(int _yaziID)
        {

            ViewBag.YaziID = _yaziID;
            return View();



        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Bildir(int _yaziID, string sebep)
        {
            int kullaniciID = (int)Session["kullanici_id"];
            db_Context model = new db_Context();
            model.YaziSikayetler.Add(new YaziSikayetler
            {
                sebep = sebep,
                tarih = DateTime.Now,
                yazi_id = _yaziID,
                sikayetci_id = kullaniciID
            });
            model.SaveChanges();
            return RedirectToAction("yazi_sikayetTamam", new { yaziID = _yaziID });
        }
        /// <summary>
        /// Bu metot Yazi hakkındaki şikayetin başarıyla tamamlandığını gösterir
        /// </summary>
        /// <param name="yaziID"></param>
        /// <returns></returns>
        public ActionResult yazi_sikayetTamam(int yaziID)
        {
            int kullanici_id = (int)Session["kullanici_id"];
            db_Context model = new db_Context();
            string _baslik = model.Yazilar.FirstOrDefault(p => p.yazi_id == yaziID).Baslik;
            string kisiAd = model.Kullanicilar.Find(kullanici_id).Ad;

            ViewBag.kisi_ad = kisiAd;
            ViewBag.Baslik = _baslik;
            ViewBag.YaziID = yaziID;
            return View();
        }
        public ActionResult LostedUser()
        {
            Session.Remove("kullanici_id");
            return View();
        }
        public ActionResult ZiyaretciYazıDetay(int _yaziID)
        {
            db_Context model = new db_Context();
            var yazi = model.Yazilar.Find(_yaziID);
            YaziDetayPage detayPage = new YaziDetayPage();
            detayPage.yazi = model.Yazilar.Where(p => p.yazi_id == _yaziID).Select(p => new Yazi
            {
                Baslik = p.Baslik,
                yazar_ad = p.Kullanicilar.Ad,
                onay = true,
                yazar_id = p.yazar_id,
                tarih = p.tarih,
                yazi_id = p.yazi_id,
                icerik = p.icerik
            }).FirstOrDefault();
            detayPage.yaziyorumlari = model.YaziYorumlar.Where(p => p.yaziID == _yaziID).OrderByDescending(p => p.YaziYorumID).Select(p => new YaziYorumlari
            {
                _icerik = p.icerik,
                _onay = p.onay,
                yorumcuAd = p.Kullanicilar.Ad,
                _tarih = p.tarih,
                _yaziID = p.yaziID,
                _YaziYorumID = p.YaziYorumID,
                _yorumcuID = p.yorumcuID
            }).ToList();
            return View(detayPage);
        }
    }
}