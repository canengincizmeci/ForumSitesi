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
using System.Web.Mail;
using System.Web.Mvc;

namespace adminTabani_01_05_24.Controllers
{
    public class KullaniciController : Controller
    {
        // GET: Kullanici
        public ActionResult Kayit()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(string ad, string mail)
        {
            blogAdminli_01_05_24Entities model = new blogAdminli_01_05_24Entities();
            var ad_kontrol = model.Kullanicilar.FirstOrDefault(p => p.Ad == ad);
            var mail_kontrol = model.Kullanicilar.FirstOrDefault(p => p.kullaniciMail == mail);
            if (ad_kontrol != null)
            {
                return RedirectToAction("KayitHatasi", new { mesaj = "Bu kullanıcı adı alınmış" });
            }
            if (mail_kontrol != null)
            {
                return RedirectToAction("KayitHatasi", new { mesaj = "Bu mail başka hesaba ait" });
            }
            int sayi = MailGonder(mail);
            return RedirectToAction("KodGirisi", new { kod = sayi, ad = ad, mail = mail });
        }
        public ActionResult KayitHatasi(string mesaj)
        {
            ViewBag.Mesaj = mesaj;

            return View();
        }

        public int MailGonder(string mail)
        {
            Random rnd = new Random();
            int random = rnd.Next(1000, 9999 + 1);
            var cred = new NetworkCredential("canncizmeci@gmail.com", "jben gmyx obrj vhtj");
            var client = new SmtpClient("smtp.gmail.com", 587);
            var msg = new System.Net.Mail.MailMessage();
            msg.To.Add(mail);
            msg.Subject = "Kayıt Onay Kodu";
            msg.Body = $"Kayıt için mailinizi şu kodu girerek doğrulayınız {random}";
            msg.IsBodyHtml = false;
            msg.From = new MailAddress("canncizmeci@gmail.com", "Doğrulama Kodu", Encoding.UTF8);
            client.Credentials = cred;
            client.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            client.Send(msg);
            return random;
        }

        public ActionResult KodGirisi(int kod, string ad, string mail)
        {
            ViewBag.kod = kod;
            ViewBag.ad = ad;
            ViewBag.mail = mail;
            return View();
        }
        public ActionResult KodKontrol(int kod, int kullanici_kod, string ad, string mail)
        {
            if (kod == kullanici_kod)
            {
                return RedirectToAction("KayitTamamla", new { adi = ad, maili = mail });
            }
            else
            {
                return RedirectToAction("KayitHatasi", new { mesaj = "Girilen kod yanlış" });
            }
        }
        //public void SifreMaili(string mail, string kod)
        //{

        //    var cred = new NetworkCredential("canncizmeci@gmail.com", "jben gmyx obrj vhtj");
        //    var client = new SmtpClient("smtp.gmail.com", 587);
        //    var msg = new System.Net.Mail.MailMessage();
        //    msg.To.Add(mail);
        //    msg.Subject = "Tek Kullanımlık Şifreniz";
        //    msg.Body = $"Tek Kullanımlık şifreniz {kod}";
        //    msg.IsBodyHtml = false;
        //    msg.From = new MailAddress("canncizmeci@gmail.com", "Doğrulama Kodu", Encoding.UTF8);
        //    client.Credentials = cred;
        //    client.EnableSsl = true;
        //    ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
        //    client.Send(msg);
        //}
        public ActionResult KayitTamamla(string adi, string maili)
        {
            Random rnd = new Random();
            int random = rnd.Next(1000, 9999 + 1);
            blogAdminli_01_05_24Entities model = new blogAdminli_01_05_24Entities();
            model.Kullanicilar.Add(new Kullanicilar
            {
                Ad = adi,
                kullaniciMail = maili,
                kullanici_sifre = random.ToString()
            });
            model.SaveChanges();
            //SifreMaili(maili, random.ToString());
            int id = model.Kullanicilar.FirstOrDefault(p => p.Ad == adi).kullanici_id;
            return RedirectToAction("IlkSifreBelirleme", new { _id = id });
        }
        [HttpGet]
        public ActionResult IlkSifreBelirleme(int _id)
        {
            blogAdminli_01_05_24Entities model = new blogAdminli_01_05_24Entities();
            int id = model.Kullanicilar.FirstOrDefault(p => p.kullanici_id == _id).kullanici_id;
            ViewBag.id = id;
            return View();
        }
        [HttpPost]
        public ActionResult IlkSifreBelirleme(int id, string sifre1, string sifre2)
        {
            blogAdminli_01_05_24Entities model = new blogAdminli_01_05_24Entities();
            var kisi = model.Kullanicilar.FirstOrDefault(p => p.kullanici_id == id);

            kisi.kullanici_sifre = sifre1;
            model.SaveChanges();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string adi, string sifre)
        {
            blogAdminli_01_05_24Entities model = new blogAdminli_01_05_24Entities();
            var kisi = model.Kullanicilar.FirstOrDefault(p => p.Ad == adi);
            if (kisi != null)
            {
                if (kisi.kullanici_sifre == sifre)
                {
                    return RedirectToAction("KullaniciHome", new { id = kisi.kullanici_id });
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult KullaniciHome(int id)
        {
            blogAdminli_01_05_24Entities model = new blogAdminli_01_05_24Entities();
            var kisi = model.Kullanicilar.FirstOrDefault(p => p.kullanici_id == id);
            ViewBag.Ad = kisi.Ad;
            return View();
        }
    }
}