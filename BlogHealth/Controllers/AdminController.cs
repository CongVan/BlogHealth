using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogHealth.Models;
using System.IO;
using System.Threading.Tasks;

namespace BlogHealth.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        #region Categories


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
                else if (kind == 3)//get child title,id
                {
                    // var result = ctx.Categories.Select(c => new { ID=c.ID, Name=c.Name }).OrderBy(c => c.ID).ToList();
                    return Json(ctx.Categories.Where(c => c.Level == 2 || c.ParentID != null).Select(c => new { ID = c.ID, Name = c.Name }).OrderBy(c => c.ID).ToList(), JsonRequestBehavior.AllowGet);
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
        #endregion

        #region Post
        public ActionResult Posts()
        {
            return View();
        }
        public ActionResult AddPost()
        {
            ViewBag.Status = TempData["Status"]!=null? TempData["Status"].ToString():"";
            ViewBag.Error = TempData["Error"] !=null? TempData["Error"].ToString():null;
            ViewBag.ID = TempData["ID"] !=null? TempData["ID"].ToString():null;
            ViewBag.Success = TempData["Success"] != null ? TempData["Success"].ToString() : null;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddPost(Posts model)
        {

            IEnumerable<HttpPostedFileBase> files = TempData["ListImage"] as IEnumerable<HttpPostedFileBase>;
            using (var ctx =new BlogHealthEntities())
            {
                var post = ctx.Posts.Add(model);
                try
                {
                    ctx.SaveChanges();
                    string auDirPath = Server.MapPath("~/Images/Posts");
                    string targetDirPath = Path.Combine(auDirPath, post.ID.ToString());
                    Directory.CreateDirectory(targetDirPath);
                    int i = 0;
                    foreach (var file in files)
                    {
                        if (file.ContentLength > 0 && file != null)
                        {
                            string pathOfFile = Path.Combine(targetDirPath, post.Slug+$"-images-{i++}.jpg");
                              file.SaveAs(pathOfFile);
                        }
                    }

                    TempData["Status"] = "Success";
                    TempData["ID"] = post.ID;
                    return  RedirectToAction("AddPost","Admin");
                }
                catch (Exception ex)
                {
                    if(post != null)
                    {
                        ctx.Posts.Remove(post);
                        ctx.SaveChanges();
                    }
                    TempData["Error"] = "Lỗi đăng bài. " + ex.Message;
                    TempData["Status"] = "Error";

                    return RedirectToAction("AddPost", "Admin");
                }
                
                
            }
            return RedirectToAction("AddPost", "Admin");
        }
        [HttpPost]
        public ActionResult UpImagePost(HttpPostedFileBase[] files)
        {
            List<HttpPostedFileBase> allFiles = Enumerable.Range(0, Request.Files.Count)
                                                .Select(x => Request.Files[x])
                                                .Where(x => !string.IsNullOrEmpty(x.FileName))
                                                .ToList();

            
            TempData["ListImage"] = allFiles;
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateContentPost(int ID,string Content)
        {
            using(var ctx=new BlogHealthEntities())
            {
                var post = ctx.Posts.Where(c => c.ID == ID).FirstOrDefault();
                if (post == null)
                {
                    TempData["Error"] = "Không tìm thấy bài viết. Thử lại!";
                    return RedirectToAction("AddPost", "Admin");
                }
                post.Content = Content;
                ctx.Entry(post).State = System.Data.Entity.EntityState.Modified;
                try
                {
                    ctx.SaveChanges();
                    TempData["Success"] = "Đăng thành công!";
                    return RedirectToAction("AddPost", "Admin");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Lỗi đăng bài. " + ex.Message;
                    return RedirectToAction("AddPost", "Admin");
                }
            }
        }
        #endregion
    }
}