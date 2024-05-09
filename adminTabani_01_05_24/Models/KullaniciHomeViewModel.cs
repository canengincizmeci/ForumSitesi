using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminTabani_01_05_24.Models
{
    public class KullaniciHomeViewModel
    {
        public List<Yazi> yazilar { get; set; }
        public List<Tartisma> tartismalar { get; set; }
        public List<Haber> haberler { get; set; }
        public List<Resim> resimler { get; set; }
        public List<Siir> siirler { get; set; }
    }
}