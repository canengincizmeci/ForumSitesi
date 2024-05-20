using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminTabani_01_05_24.Models
{
    public class TartismaDetayPage
    {
        
        public Tartisma tartisma { get; set; }
        public List<TartismaYorumlari> tartisma_yorumlari { get; set; }
    }
}