using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogHealth.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var t = DateTime.Now.ToString("ddd MMM YYYYY", CultureInfo.CreateSpecificCulture("vi-VN"));
            return View();
        }

        public ActionResult Post()
        {
            return View();
        }
        public ActionResult Posts()
        {
            return View();
        }
    }
}