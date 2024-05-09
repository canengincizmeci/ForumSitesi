using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminTabani_01_05_24.Models
{
    public class Haber
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HaberID { get; set; }
        public string Baslik { get; set; }
        public string icerik { get; set; }
        public int? paylasan { get; set; }
        public DateTime? tarih { get; set; }
    }
}