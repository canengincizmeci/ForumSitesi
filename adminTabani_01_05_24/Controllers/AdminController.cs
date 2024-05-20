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
            dbContext model = new dbContext();
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
            dbContext model = new dbContext();
            var admin = model.Admin.Where(p => p.id == 1).FirstOrDefault();
            var cred = new NetworkCredential("canncizmeci@gmail.com", "jben gmyx obrj vhtj");
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
            dbContext model = new dbContext();
            var sonVeri = model.AdminGirisler.OrderByDescending(p => p.giris_id).FirstOrDefault();
            var koddegeri = sonVeri.girisKod;
            if (kod==koddegeri)
            {
                sonVeri.girisBasarisi2 = true;
                model.SaveChanges();
                return View("AdminHome");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}