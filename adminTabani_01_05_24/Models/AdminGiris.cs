using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminTabani_01_05_24.Models
{
    public class AdminGiris
    {
        public int giris_id { get; set; }
        public DateTime? girisTarih { get; set; }
        public int? girisKod { get; set; }
        public bool? girisBasarisi1 { get; set; }
        public bool? girisBasarisi2 { get; set; }
    }
}