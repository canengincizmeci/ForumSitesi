using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminTabani_01_05_24.Models
{
    public class Ceza
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int _ceza_id { get; set; }
        public string _ceza { get; set; }
        public int? _kullanici_id { get; set; }
        public DateTime? _tarih { get; set; }

    }
}