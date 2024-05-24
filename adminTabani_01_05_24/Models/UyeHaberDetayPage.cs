using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminTabani_01_05_24.Models
{
    public class UyeHaberDetayPage
    {
        public Haber haber { get; set; }
        public List<HaberYorumlari> haber_yorumlari { get; set; }
    }
}