using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminTabani_01_05_24.Models
{
    public class YaziDetayPage
    {
        public Yazi yazi { get; set; }
        public List<YaziYorumlari> yaziyorumlari { get; set; }
    }
}