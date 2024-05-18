using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminTabani_01_05_24.Models
{
    public class Resim
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int resimID { get; set; }
        public string dosyaYolu { get; set; }
        public string baslik { get; set; }
        public string aciklama { get; set; }
        public int? paylasan { get; set; }
        public string PaylasanAd { get; set; }
        public DateTime? tarih { get; set; }
        public bool? _onay { get; set; }
    }
}