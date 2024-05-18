using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminTabani_01_05_24.Models
{
    public class KullaniciMesaj
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int k_mesaj_id { get; set; }
        public string mesaj { get; set; }
        public DateTime? tarih { get; set; }
        public int? gonderen_id { get; set; }
        public int? alici_id { get; set; }
        public string GonderenAd { get; set; }
        public string aliciAd { get; set; }
    }
}