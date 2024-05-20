using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace adminTabani_01_05_24.Controllers
{
    public class MesajController : Controller
    {
        // GET: Mesaj
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AdminMesaj()
        {


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminMesaj(string icerik)
        {
            dbContext model = new dbContext();
            int id = (int)Session["kullanici_id"];
            var kisi = model.Kullanicilar.FirstOrDefault(p => p.kullanici_id == id);
            model.AdminMesajlar.Add(new AdminMesajlar
            {
                mesaj = icerik,
                gonderen_id = id,
                tarih = DateTime.Now
            });
            model.SaveChanges();
            TempData["MesajGonder"] = "Mesajınız başarıyla gönderildi";
            return RedirectToAction("AdminMesaj");
        }
    }
}