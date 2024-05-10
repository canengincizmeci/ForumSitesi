using adminTabani_01_05_24.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace adminTabani_01_05_24.Controllers
{
    public class ZiyaretciController : Controller
    {
        // GET: Ziyaretci
        public ActionResult Index()
        {
            blogAdminli_01_05_24Entities model = new blogAdminli_01_05_24Entities();
            KullaniciHomeViewModel giden_model = new KullaniciHomeViewModel();
          


            return View();
        }
    }
}