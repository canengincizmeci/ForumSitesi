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
            blogAdminli_01_05_24Entities model = new blogAdminli_01_05_24Entities();
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
            blogAdminli_01_05_24Entities model = new blogAdminli_01_05_24Entities();
            var tartisma = model.Tartismalar.Where(p => p.TartismaID == tartisma_id).Select(p => new Tartisma
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

            return View(tartisma);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult YorumYap(string yorum,int tartisma_id)
        //{
        //    int id = (int)Session["kullanici_id"];
        //    blogAdminli_01_05_24Entities model = new blogAdminli_01_05_24Entities();
        //    model.Yorumlar.Add(new Yorumlar
        //    {
        //        icerik = yorum,
        //        kullanıci_id =id,
        //        onay=false,
        //        yazi_id=tartisma_id,
        //        yorumTur=1,
                
        //    });

        //}
    }
}