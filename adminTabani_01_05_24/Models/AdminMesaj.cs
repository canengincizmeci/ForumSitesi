using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminTabani_01_05_24.Models
{
    public class AdminMesaj
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int _mesaj_id { get; set; }
        public string _mesaj { get; set; }
        public DateTime? _tarih { get; set; }
        public int? _gonderen_id { get; set; }
        public string gonderenAd { get; set; }
    }
}