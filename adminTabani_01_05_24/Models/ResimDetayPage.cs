using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminTabani_01_05_24.Models
{
    public class ResimDetayPage
    {
        public Resim resim { get; set; }
        public List<ResimYorumlari> resimYorumlari { get; set; }
    }
}