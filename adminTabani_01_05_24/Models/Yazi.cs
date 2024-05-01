using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminTabani_01_05_24.Models
{
    public class Yazi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int yazi_id { get; set; }
        public string Baslik { get; set; }
        public string İcerik { get; set; }
        public int? yazar_id { get; set; }
        public bool? onay { get; set; }
    }
}