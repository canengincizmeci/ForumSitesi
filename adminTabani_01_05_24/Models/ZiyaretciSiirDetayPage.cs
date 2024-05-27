using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminTabani_01_05_24.Models
{
    public class ZiyaretciSiirDetayPage
    {
        public Siir siir { get; set; }
        public List<SiirYorumlari> siir_yorumlari { get; set; }
    }
}