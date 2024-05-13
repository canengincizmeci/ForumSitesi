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
            blogAdminli_01_05_24Entities model = new blogAdminli_01_05_24Entities();
            KullaniciHomeViewModel veriler = new KullaniciHomeViewModel();           
            veriler.haberler = model.Haberler.OrderByDescending(p=>p.tarih).Take(5).Select(p => new Haber
            {
                HaberID=p.HaberID,
                Baslik=p.Baslik,
                icerik=p.icerik,
                tarih=p.tarih,
                paylasan = p.paylasan,
                PaylasanAd=p.Kullanicilar.Ad
            }).ToList();
            veriler.yazilar = model.Yazilar.OrderByDescending(p => p.tarih).Take(5).Select(p => new Yazi
            {
                yazi_id=p.yazi_id,
                yazar_id=p.yazar_id,
                Baslik=p.Baslik,
                İcerik=p.İcerik,
                tarih=p.tarih,
                onay=p.onay,
                yazar_ad=p.Kullanicilar.Ad
            }).ToList();
            veriler.resimler = model.Resimler.OrderByDescending(p => p.resimID).Take(5).Select(p => new Resim
            {
                resimID=p.resimID,
                baslik=p.baslik,
                aciklama=p.aciklama,
                dosyaYolu=p.dosyaYolu,
                paylasan=p.paylasan,
                PaylasanAd=p.Kullanicilar.Ad,
                tarih=p.tarih
            }).ToList();



            return View(veriler);
        }

        
    }
}
