using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminTabani_01_05_24.Models
{
    public class KullaniciCezalar
    {
        public Kullanici kullanici { get; set; }
        public List<HaberSikayetleri> haberSikayetleri { get; set; }
        public List<SiirSikayetleri> siirSikayetleri { get; set; }
        public List<YaziSikayetleri> yaziSikayetleri { get; set; }
        public List<ResimSikayetleri> resimSikayetleri { get; set; }
        public List<KullaniciMesajlarSikayetleri> mesajSikayetleri { get; set; }
        public List<TartismaSikayetleri> tartismaSikayetler { get; set; }

    }
}