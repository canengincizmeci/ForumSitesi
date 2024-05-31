using adminTabani_01_05_24.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public ActionResult UyeAdminMesaj()
        {


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UyeAdminMesaj(string icerik)
        {
            db_Context model = new db_Context();
            int id = (int)Session["kullanici_id"];
            var kisi = model.Kullanicilar.FirstOrDefault(p => p.kullanici_id == id);
            model.AdminMesajlar.Add(new AdminMesajlar
            {
                mesaj = icerik,
                gonderen_id = id,
                tarih = DateTime.Now
            });
            model.SaveChanges();



            return RedirectToAction("UyeAdminMesajTamam");
        }
        public ActionResult UyeAdminMesajTamam()
        {
            int id = (int)Session["kullanici_id"];
            db_Context model = new db_Context();
            string ad = model.Kullanicilar.Find(id).Ad;
            ViewBag.Ad = ad;
            return View();
        }
        public ActionResult KullancilarArasiMesaj()
        {

            db_Context model = new db_Context();
            var kisiler = model.Kullanicilar.Select(p => new Kullanici
            {
                Ad = p.Ad,
                kullanici_id = p.kullanici_id,
            }).ToList();
            return View(kisiler);
        }
        //public ActionResult KullaniciMesaj(int alici_id)
        //{



        //}
    }
}

