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
        public ActionResult UpdateCategories(Categories model)
        {
            using (var ctx = new BlogHealthEntities())
            {
                var cate = ctx.Categories.Where(c => c.ID == model.ID).FirstOrDefault();
                if (cate != null)
                {
                    cate.Name = model.Name;
                    cate.Priority = model.Priority;
                    cate.Slug = model.Slug;
                    cate.Color = model.Color;
                    ctx.Entry(cate).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
                
        }
    }
}