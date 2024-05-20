using adminTabani_01_05_24.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}