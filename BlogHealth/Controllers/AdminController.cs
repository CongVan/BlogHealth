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
        public ActionResult GetCategories(int kind=1)
        {
            using (var ctx = new BlogHealthEntities())
            {
               
                if (kind == 1)//get all
                {
                  //   var result = ctx.Categories.OrderBy(c => c.Priority).ToList();
                    return Json(ctx.Categories.OrderBy(c => c.Priority).ToList(), JsonRequestBehavior.AllowGet);
                }
                else if(kind==2)//get parent title,id
                {
                   // var result = ctx.Categories.Select(c => new { ID=c.ID, Name=c.Name }).OrderBy(c => c.ID).ToList();
                    return Json(ctx.Categories.Where(c=>c.Level==1 || c.ParentID==null).Select(c => new { ID = c.ID, Name = c.Name }).OrderBy(c => c.ID).ToList(), JsonRequestBehavior.AllowGet);
                }
                return Json("", JsonRequestBehavior.AllowGet);
               // return Json(result, JsonRequestBehavior.AllowGet);
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
        public ActionResult DeleteCategories(int? ID)
        {
            if (!ID.HasValue)
            {
                return Json(new { result = 0, error = "ID không tồn tại" }, JsonRequestBehavior.AllowGet);
            }
           using(var ctx= new BlogHealthEntities())
            {
                var cate = ctx.Categories.Where(c => c.ID == ID).FirstOrDefault();
                try
                {
                    if (cate != null)
                    {
                        ctx.Categories.Remove(cate);
                        ctx.SaveChanges();
                        return Json(new { result = 1, error = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = 0, error = "Lỗi" }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { result = 0, error = ex.Message }, JsonRequestBehavior.AllowGet);
                }

              
            }     
        }
        [HttpPost]
        public ActionResult AddCategory(Categories model)
        {
            if (String.IsNullOrEmpty(model.Name))
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            using(var ctx=new BlogHealthEntities())
            {
                ctx.Categories.Add(model);
                ctx.SaveChanges();
            }
            return Json("1",JsonRequestBehavior.AllowGet);
        }
    }
}