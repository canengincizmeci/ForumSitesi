using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminTabani_01_05_24.Models
{
    public class KullaniciGiris
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int giris_id { get; set; }
        public DateTime? girisTarih { get; set; }
        public int? kullanici_id { get; set; }
        public int? girisKod { get; set; }
        public bool? GirisBasarisi1 { get; set; }
        public bool? GirisBasarisi2 { get; set; }
    }
}