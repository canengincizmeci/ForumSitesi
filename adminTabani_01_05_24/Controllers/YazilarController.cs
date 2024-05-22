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
            dbContext model = new dbContext();
            var veriler = model.Yazilar.Where(p => p.onay == true).OrderByDescending(p => p.yazi_id).Select(p => new Yazi
            {
                yazar_id = p.yazi_id,
                Baslik = p.Baslik,
                İcerik = p.İcerik,
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
            dbContext model = new dbContext();
            YaziDetayPage yaziDetay = new YaziDetayPage();
            yaziDetay.yazi = model.Yazilar.Where(p => p.yazi_id == yaziID).Select(p => new Yazi
            {
                yazi_id = p.yazi_id,
                Baslik = p.Baslik,
                onay = p.onay,
                tarih = p.tarih,
                yazar_ad = p.Kullanicilar.Ad,
                yazar_id = p.yazar_id,
                İcerik = p.İcerik
            }).FirstOrDefault();
            yaziDetay.yaziyorumlari = model.YaziYorumlar.Where(p => p.yaziID == yaziID & p.onay == true).Select(p => new YaziYorumlari
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
            dbContext model = new dbContext();
            model.YaziYorumlar.Add(new YaziYorumlar
            {
                yorumcuID = id,
                tarih = DateTime.Now,
                icerik = _yorum,
                yaziID = _yaziID,
                onay = false
            });
            model.SaveChanges();
            return RedirectToAction("yaziDetay", new { yaziID = _yaziID });
        }
        [HttpGet]
        public ActionResult Bildir(int _yaziID)
        {
            if (_yaziID != null)
            {
                ViewBag.YaziID = _yaziID;
                return View();
            }
            else
            {
                return RedirectToAction("LostedUser");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Bildir(int _yaziID, string sebep)
        {
            int kullaniciID = (int)Session["kullanici_id"];
            dbContext model = new dbContext();
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
            dbContext model = new dbContext();
            var _baslik = model.Yazilar.FirstOrDefault(p => p.yazi_id == yaziID).Baslik;
            string kisiAd = model.Kullanicilar.Find(kullanici_id).Ad;
            ViewBag.kisi_ad = kisiAd;
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
    }
}