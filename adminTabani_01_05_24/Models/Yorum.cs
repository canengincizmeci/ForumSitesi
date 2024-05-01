using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminTabani_01_05_24.Models
{
    public class Yorum
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int yorum_id { get; set; }
        public int? kullanıci_id { get; set; }
        public string icerik { get; set; }
        public bool? onay { get; set; }
        public int? yazi_id { get; set; }
        
        public virtual Kullanicilar Kullanicilar { get; set; }
        public virtual Yazilar Yazilar { get; set; }
    }
}