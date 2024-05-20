using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminTabani_01_05_24.Models
{
    public class YaziYorumlari
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int _YaziYorumID { get; set; }
        public string _icerik { get; set; }
        public int? _yorumcuID { get; set; }
        public int? _yaziID { get; set; }
        public bool? _onay { get; set; }
        public DateTime? _tarih { get; set; }
    }
}