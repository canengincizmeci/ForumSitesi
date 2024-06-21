using adminTabani_01_05_24.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Microsoft.Ajax.Utilities;

namespace adminTabani_01_05_24.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Kontrol(string email, string sifre)
        {
            db_Context model = new db_Context();
            var veri = model.Admin.Where(p => p.id == 1).FirstOrDefault();
            if (veri.Email == email && veri.Sifre == sifre)
            {
                AdminDogrulamaKodu();
                return View("KodDogrulama");
            }
            else
            {
                var eklenecekler = model.AdminGirisler.Add(new AdminGirisler
                {
                    girisBasarisi1 = false,
                    girisBasarisi2 = false,
                    girisKod = null,
                    girisTarih = DateTime.Now,
                });
                model.SaveChanges();
                return RedirectToAction("Login");
            }
        }
        public void AdminDogrulamaKodu()
        {

            Random rnd = new Random();
            int random = rnd.Next(1000, 9999 + 1);
            db_Context model = new db_Context();
            var admin = model.Admin.Where(p => p.id == 1).FirstOrDefault();
            string sifre = admin.MailSifre;
            var cred = new NetworkCredential("canncizmeci@gmail.com", sifre);
            var client = new SmtpClient("smtp.gmail.com", 587);
            var msg = new System.Net.Mail.MailMessage();
            msg.To.Add(admin.Email);
            msg.Subject = "Giriş";
            msg.Body = $"Admin girişi için doğrulama kodunuz {random}";
            msg.IsBodyHtml = false;
            msg.From = new MailAddress("canncizmeci@gmail.com", "Doğrulama Kodu", Encoding.UTF8);
            client.Credentials = cred;
            client.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            client.Send(msg);
            var eklenecekler = model.AdminGirisler.Add(new AdminGirisler
            {
                girisBasarisi1 = true,
                girisBasarisi2 = false,
                girisTarih = DateTime.Now,
                girisKod = random
            });
            model.SaveChanges();
        }
        public ActionResult KodKontrol(int kod)
        {
            db_Context model = new db_Context();
            var sonVeri = model.AdminGirisler.OrderByDescending(p => p.giris_id).FirstOrDefault();
            var admin = model.Admin.Find(1);
            var koddegeri = sonVeri.girisKod;
            if (kod == koddegeri)
            {
                sonVeri.girisBasarisi2 = true;
                model.SaveChanges();
                Session["id"] = admin.id;
                return RedirectToAction("AdminHome");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult AdminHome()
        {
            int id;
            try
            {
                id = (int)Session["id"];
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult KullaniciListele()
        {

            int id;
            try
            {
                id = (int)Session["id"];
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
            db_Context model = new db_Context();
            var kullaniciListesi = model.Kullanicilar.Select(p => new Kullanici
            {
                Ad = p.Ad,
                KullaniciMaili = p.kullaniciMail,
                kullanici_id = p.kullanici_id,
                kullanici_sifresi = p.kullanici_sifre
            }).ToList();
            return View(kullaniciListesi);
        }
        [HttpGet]
        public ActionResult MesajGonder(int id)
        {
            int admin_id;
            try
            {
                admin_id = (int)Session["id"];
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ID = id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MesajGonder(string mesaj, int id)
        {
            int _id;
            try
            {
                _id = (int)Session["id"];
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
            db_Context model = new db_Context();
            model.AdminGidenMesajlar.Add(new AdminGidenMesajlar
            {
                alici_id = id,
                mesaj = mesaj,
                tarih = DateTime.Now
            });
            model.SaveChanges();
            return RedirectToAction("KullaniciListele", "Admin");
        }
        public ActionResult Profil(int kullaniciID)
        {
            int id;
            try
            {
                id = (int)Session["id"];
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
            db_Context model = new db_Context();
            var uye = model.Kullanicilar.Where(p => p.kullanici_id == kullaniciID).Select(p => new Kullanici
            {
                Ad = p.Ad,
                kullanici_id = p.kullanici_id,
                KullaniciMaili = p.kullaniciMail,
                kullanici_sifresi = p.kullanici_sifre
            }).FirstOrDefault();
            return View(uye);
        }
        public ActionResult CezaSayfasi(int id)
        {
            db_Context model = new db_Context();
            var kisi = model.Kullanicilar.Find(id);
            KullaniciCezalar cezalar = new KullaniciCezalar();
            cezalar.haberSikayetleri = model.HaberSikayetler.Where(p => p.Kullanicilar.kullanici_id == id).OrderByDescending(p => p.sikayet_id).Select(p => new HaberSikayetleri
            {
                haberBaslik = p.Haberler.Baslik,
                sikayetciAd = p.Kullanicilar.Ad,
                sikayetID = p.sikayet_id,
                _haber_id = p.haber_id,
                _sebep = p.sebep,
                _sikayetci_id = p.sikayetci_id,
                _tarih = p.tarih
            }).ToList();
            cezalar.tartismaSikayetler = model.TartismaSikayetler.Where(p => p.Kullanicilar.kullanici_id == id).OrderByDescending(p => p.sikayet_id).Select(p => new TartismaSikayetleri
            {
                sikayetID = p.sikayet_id,
                _sebep = p.sebep,
                _sikayetci_id = p.sikayetci_id,
                _tarih = p.tarih,
                _tartisma_id = p.tartisma_id
            }).ToList();
            cezalar.mesajSikayetleri = model.KullaniciMesajlarSikayet.Where(p => p.Kullanicilar.kullanici_id == id).OrderByDescending(p => p.sikayetID).Select(p => new KullaniciMesajlarSikayetleri
            {
                _mesajID = p.mesajID,
                _sebep = p.sebep,
                _sikayetciID = p.sikayetciID,
                _sikayetID = p.sikayetID,
                _tarih = p.tarih
            }).ToList();
            cezalar.siirSikayetleri = model.SiirSikayetler.Where(p => p.Kullanicilar.kullanici_id == id).OrderByDescending(p => p.sikayet_id).Select(p => new SiirSikayetleri
            {
                SiirBaslik = p.Siirler.siirBaslik,
                sikayetciAD = p.Kullanicilar.Ad,
                sikayetID = p.sikayet_id,
                _sebep = p.sebep,
                _siir_id = p.Siirler.siirID,
                _sikayetci_id = p.sikayetci_id,
                _tarih = p.tarih
            }).ToList();
            cezalar.yaziSikayetleri = model.YaziSikayetler.Where(p => p.Kullanicilar.kullanici_id == id).OrderByDescending(p => p.sikayet_id).Select(p => new YaziSikayetleri
            {
                SikayetciAd = p.Kullanicilar.Ad,
                YaziBaslik = p.Yazilar.Baslik,
                _sebep = p.sebep,
                _sikayetci_id = p.sikayetci_id,
                _sikayet_id = p.sikayet_id,
                _tarih = p.tarih,
                _yazi_id = p.yazi_id
            }).ToList();
            cezalar.resimSikayetleri = model.ResimSikayetler.Where(p => p.Kullanicilar.kullanici_id == id).OrderByDescending(p => p.sikayet_id).Select(p => new ResimSikayetleri
            {
                ResimBaslik = p.Resimler.baslik,
                SikayetciAd = p.Kullanicilar.Ad,
                sikayetID = p.sikayet_id,
                _resim_id = p.resim_id,
                _sebep = p.sebep,
                _sikayetci_id = p.Kullanicilar.kullanici_id,
                _tarih = p.tarih
            }).ToList();
            cezalar.kullanici = model.Kullanicilar.Where(p => p.kullanici_id == id).Select(p => new Kullanici
            {
                kullanici_id = p.kullanici_id,
                Ad = p.Ad,
                KullaniciMaili = p.kullaniciMail,
                kullanici_sifresi = p.kullanici_sifre
            }).FirstOrDefault();
            return View(cezalar);
        }
    }
}