using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminTabani_01_05_24.Models
{
    public class TartismaYorumlari
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int _tartismaYorumID { get; set; }
        public string _yorum { get; set; }
        public int? _yorumcuID { get; set; }
        public int? _tartismaID { get; set; }
        public DateTime? _tarih { get; set; }
        public bool? _onay { get; set; }
        public string yorumcu_ad { get; set; }
    }
}