using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminTabani_01_05_24.Models
{
    public class Tartisma
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TartismaID { get; set; }
        public string Baslik { get; set; }
        public string icerik { get; set; }
        public DateTime? tarih { get; set; }
        public bool? onay { get; set; }
        public bool? aktiflik { get; set; }
        public int? kullanici_id { get; set; }
        public string KullaniciAd { get; set; }
    }
}