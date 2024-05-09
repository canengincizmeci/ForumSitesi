using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminTabani_01_05_24.Models
{
    public class Siir
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int siirID { get; set; }
        public string siirBaslik { get; set; }
        public string icerik { get; set; }
        public int? yazar { get; set; }
        public DateTime? tarih { get; set; }
    }
}