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
            blogAdminli_01_05_24Entities model = new blogAdminli_01_05_24Entities();
            var veri = model.Admin.Where(p => p.id == 1).FirstOrDefault();
            if (veri.Email == email && veri.Sifre == sifre)
            {
                
                return View("AdminHome");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public void DogrulamaKodu()
        {
            Random rnd = new Random();
            int random = rnd.Next(9999, 1000 + 1);
            blogAdminli_01_05_24Entities model = new blogAdminli_01_05_24Entities();
            var admin=model.Admin.Where(p=>p.id==1).FirstOrDefault();
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
          
        }
    }
}