using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogHealth.Models;
namespace BlogHealth.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Categories()
        {
            return View();
        }
        public ActionResult GetCategories()
        {
            using (var ctx = new BlogHealthEntities())
            {
                return Json(ctx.Categories.OrderBy(c=>c.Priority).ToList(), JsonRequestBehavior.AllowGet);
            }

        }
    }
}