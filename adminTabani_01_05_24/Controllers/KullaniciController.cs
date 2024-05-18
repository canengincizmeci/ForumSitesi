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
using System.Xml.Serialization;
using System.Security.Cryptography;

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
            Session["kullanici_id"] = id;
            return RedirectToAction("IlkSifreBelirleme");
        }
        [HttpGet]
        public ActionResult IlkSifreBelirleme()
        {
            int _id = (int)Session["kullanici_id"];
            blogAdminli_01_05_24Entities model = new blogAdminli_01_05_24Entities();
            int id = model.Kullanicilar.FirstOrDefault(p => p.kullanici_id == _id).kullanici_id;
            ViewBag.id = id;
            return View();
        }
        [HttpPost]
        public ActionResult IlkSifreBelirleme(string sifre1, string sifre2)
        {
            int id = (int)Session["kullanici_id"];
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
        //public ActionResult Login(string adi, string sifre)
        public ActionResult Login(FormCollection form)
        {
            string adi = form["adi"].ToString();
            string sifre = form["sifre"].ToString();
            blogAdminli_01_05_24Entities model = new blogAdminli_01_05_24Entities();
            var kisi = model.Kullanicilar.FirstOrDefault(p => p.Ad == adi);





            if (kisi != null)
            {
                if (kisi.kullanici_sifre == sifre)
                {
                    int sonuc = girisEmailKod(kisi.kullaniciMail);
                    //Session.Add("Kullanici", kisi.kullanici_id);
                    //Response.Redirect("girisKodDogrulama");
                    Session["kullanici_id"] = kisi.kullanici_id;
                    model.KullaniciGirisler.Add(new KullaniciGirisler
                    {
                        kullanici_id = kisi.kullanici_id,
                        GirisBasarisi1 = true,
                        GirisBasarisi2 = false,
                        girisKod = sonuc,
                        girisTarih = DateTime.Now,
                        kodgirisTarih = null
                    });
                    model.SaveChanges();
                    return RedirectToAction("girisKodDogrulama", new { kod = sonuc });
                    //return RedirectToAction("KullaniciHome", new { id = kisi.kullanici_id });
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
        [HttpGet]
        public ActionResult girisKodDogrulama(int kod)
        {
            int id = (int)Session["kullanici_id"];
            //ViewBag.k_id = id;
            ViewBag.deger = kod;
            return View();
        }
        // public ActionResult girisKodDogrulama(int k_id, int kod1, int kod2)
        [HttpPost]
        public ActionResult girisKodDogrulama(FormCollection form)
        {
            //int deger = Convert.ToInt32(form["k_id"]);
            int id = (int)Session["kullanici_id"];
            string kod1 = form["kod1"].ToString();
            string kod2 = form["kod2"].ToString();
            blogAdminli_01_05_24Entities model = new blogAdminli_01_05_24Entities();
            //var kisi = model.KullaniciGirisler.FirstOrDefault(p => p.kullanici_id == id);
            //var kisi = model.KullaniciGirisler
            var kisi = model.KullaniciGirisler.Where(p => p.kullanici_id == id).OrderByDescending(p => p.girisTarih).FirstOrDefault();
            if (kod1 == kod2)
            {
                kisi.GirisBasarisi2 = true;
                kisi.kodgirisTarih = DateTime.Now;
                model.SaveChanges();
                return RedirectToAction("KullaniciHome");
            }
            else
            {
                Session.Remove("kullanici_id");
                kisi.GirisBasarisi2 = false;
                kisi.kodgirisTarih = null;
                return RedirectToAction("HataliKod");
            }
        }
        public ActionResult HataliKod()
        {
            return View();
        }
        public int girisEmailKod(string mail)
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
        public ActionResult KullaniciHome()
        {
            int id = (int)Session["kullanici_id"];
            blogAdminli_01_05_24Entities model = new blogAdminli_01_05_24Entities();
            var kisi = model.Kullanicilar.FirstOrDefault(p => p.kullanici_id == id);
            ViewBag.Ad = kisi.Ad;
            KullaniciHomeViewModel Gidenmodel = new KullaniciHomeViewModel();
            Gidenmodel.resimler = model.Resimler.Select(p => new Resim
            {
                resimID = p.resimID,
                aciklama = p.aciklama,
                baslik = p.baslik,
                dosyaYolu = p.dosyaYolu,
                paylasan = p.paylasan,
                PaylasanAd = p.Kullanicilar.Ad,
                tarih = p.tarih,
                _onay = p.onay
            }).ToList();
            Gidenmodel.haberler = model.Haberler.Where(p => p.onay == true).Select(p => new Haber
            {
                HaberID = p.HaberID,
                Baslik = p.Baslik,
                icerik = p.icerik,
                paylasan = p.paylasan,
                tarih = p.tarih,
                _onay = p.onay,
                PaylasanAd = p.Kullanicilar.Ad
            }).ToList();


            Gidenmodel.yazilar = model.Yazilar.Where(p => p.onay == true).Select(p => new Yazi
            {
                yazi_id = p.yazi_id,
                yazar_id = p.yazar_id,
                Baslik = p.Baslik,
                onay = p.onay,
                İcerik = p.İcerik,
                yazar_ad = p.Kullanicilar.Ad,
                tarih = p.tarih
            }).ToList();
            Gidenmodel.tartismalar = model.Tartismalar.Select(p => new Tartisma
            {
                TartismaID = p.TartismaID,
                aktiflik = p.aktiflik,
                kullanici_id = p.kullanici_id,
                Baslik = p.Baslik,
                icerik = p.icerik,
                onay = p.onay,
                tarih = p.tarih,
                KullaniciAd = p.Kullanicilar.Ad
            }).ToList();
            Gidenmodel.siirler = model.Siirler.Select(p => new Siir
            {
                siirID = p.siirID,
                icerik = p.icerik,
                siirBaslik = p.siirBaslik,
                tarih = p.tarih,
                yazar = p.yazar,
                YazarAd = p.Kullanicilar.Ad,
                _onay = p.onay
            }).ToList();

            return View(Gidenmodel);
        }
        [HttpGet]
        public ActionResult SifremiUnuttum()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SifremiUnuttum(string mail)
        {
            blogAdminli_01_05_24Entities model = new blogAdminli_01_05_24Entities();
            if (model.Kullanicilar.Where(p => p.kullaniciMail == mail) != null)
            {
                int sonuc = SifreUnutmaDogrulamaMaili(mail);
                return RedirectToAction("MailKodDogrulama", new { maili = mail, kod = sonuc });
            }
            else
            {
                return View("HataliMail");
            }
        }
        public ActionResult HataliMail()
        {
            return View();
        }
        public int SifreUnutmaDogrulamaMaili(string mail)
        {
            Random rnd = new Random();
            int random = rnd.Next(1000, 9999 + 1);
            var cred = new NetworkCredential("canncizmeci@gmail.com", "jben gmyx obrj vhtj");
            var client = new SmtpClient("smtp.gmail.com", 587);
            var msg = new System.Net.Mail.MailMessage();
            msg.To.Add(mail);
            msg.Subject = "Şifre unutma kodu";
            msg.Body = $"Şifrenizi değiştirmek için bu kodu girin {random}";
            msg.IsBodyHtml = false;
            msg.From = new MailAddress("canncizmeci@gmail.com", "Email Doğrulama Kodu", Encoding.UTF8);
            client.Credentials = cred;
            client.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            client.Send(msg);
            return random;
        }
        [HttpGet]
        public ActionResult MailKodDogrulama(string maili, int kod)
        {
            blogAdminli_01_05_24Entities model = new blogAdminli_01_05_24Entities();
            int id = (model.Kullanicilar.FirstOrDefault(p => p.kullaniciMail == maili)).kullanici_id;
            ViewBag.ID = id;
            ViewBag.Kod = kod;
            return View();
        }
        [HttpPost]
        public ActionResult MailKodDogrulama(int id, int kod1, int kod2)
        {
            blogAdminli_01_05_24Entities model = new blogAdminli_01_05_24Entities();

            var kisi = model.Kullanicilar.Find(id);
            if (kod1 == kod2)
            {
                return RedirectToAction("IlkSifreBelirleme", new { _id = kisi.kullanici_id });
            }
            else
            {
                return View("HataliKod");
            }
        }

    }
}
