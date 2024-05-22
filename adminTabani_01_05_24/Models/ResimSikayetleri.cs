using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminTabani_01_05_24.Models
{
    public class ResimSikayetleri
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int sikayetID { get; set; }
        public string _sebep { get; set; }
        public DateTime? _tarih { get; set; }
        public int? _sikayetci_id { get; set; }
        public int? _resim_id { get; set; }
        public string SikayetciAd { get; set; }
        public string ResimBaslik { get; set; }
    }
}