using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminTabani_01_05_24.Models
{
    public class HaberSikayetleri
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int sikayetID { get; set; }
        public string _sebep { get; set; }
        public DateTime? _tarih { get; set; }
        public int? _sikayetci_id { get; set; }
        public int? _haber_id { get; set; }
        public string sikayetciAd { get; set; }
        public string haberBaslik { get; set; }
    }
}