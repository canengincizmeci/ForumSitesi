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
            dbContext model = new dbContext();
            KullaniciHomeViewModel giden_model = new KullaniciHomeViewModel();
          


            return View();
        }
    }
}