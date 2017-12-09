using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogHealth.Models;
using System.IO;
using System.Threading.Tasks;
using System.Dynamic;

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
          
            ViewBag.Error = TempData["Error"] !=null? TempData["Error"].ToString():null;
            
            ViewBag.Success = TempData["Success"] != null ? TempData["Success"].ToString() : null;
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> AddPost(Posts model)
        {
           
            using (var ctx =new BlogHealthEntities())
            {
                model.CreateDate = DateTime.Now;
                
                ctx.Posts.Add(model);

                try
                {
                    ctx.SaveChanges();
                    TempData["Success"] = "Đăng thành công!";
                    return  RedirectToAction("AddPost","Admin");
                }
                catch (Exception ex)    
                {
                    var post = ctx.Posts.Where(c => c.ID == model.ID).FirstOrDefault();
                    if(post != null)
                    {
                        ctx.Posts.Remove(post);
                        ctx.SaveChanges();
                    }
                    TempData["Error"] = "Lỗi đăng bài. " + ex.Message;
                    return RedirectToAction("AddPost", "Admin");
                }
            }

        }
        
        public ActionResult CheckImageThumbs(string slug)
        {
            //check did create thumbnail
            string Path1 = @"/HinhAnh/BaiViet/images/Thumbs/" + slug + "_medium.jpg";
            string Path11 = @"/HinhAnh/BaiViet/images/Thumbs/" + slug + "_medium.png";
            string Path2 = @"/HinhAnh/BaiViet/images/Thumbs/" + slug + "_large.jpg";
            string Path22 = @"/HinhAnh/BaiViet/images/Thumbs/" + slug + "_large.png";
            if (System.IO.File.Exists(Server.MapPath(Path1))==false )
            {
                if(System.IO.File.Exists(Server.MapPath(Path11)) == false)
                {
                    return Json(new { ret = "Fail", msg = "Chưa upload hình thumbnail nhỏ" }, JsonRequestBehavior.AllowGet);
                }
            }
            if (System.IO.File.Exists(Server.MapPath(Path2))==false )
            {
                if(System.IO.File.Exists(Server.MapPath(Path22)) == false)
                {
                return Json(new { ret = "Fail", msg = "Chưa upload hình thumbnail lớn" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { ret = "Ok", msg = "" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPosts(string IDCate,DateTime? fDate,DateTime? tDate)
        {

            using(var ctx= new BlogHealthEntities())
            {
               
                var posts = ctx.Posts.Join(ctx.Categories, c => c.IDCategory, b => b.ID,
                      (c, b) => new {
                        
                        Rows=0,
                        ID=c.ID,
                        Title=c.Title,
                        Slug=c.Slug,
                        IDCate = b.ID,
                        CateName =b.Name,
                        Likes=c.Likes??0,
                        Views=c.Views ?? 0,
                        Shares=c.Shares ?? 0,
                        Comments=c.Comments ?? 0,
                        CreateDate=c.CreateDate,
                        Tag=c.Tag,
                        Rates=c.Rates??0,
                    }).Where(c=>fDate.HasValue?c.CreateDate>=fDate:true
                    && tDate.HasValue?c.CreateDate<=tDate:true
                    && IDCate!=""?IDCate.Contains(c.IDCate.ToString()):true
                    ).OrderByDescending(c=>c.CreateDate ).ToList();
             
                
                return Json(posts, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeletePost(int? ID)
        {
            if (!ID.HasValue)
            {
                return Json(new { result = 0, msg = "Không tồn tại mã!" }, JsonRequestBehavior.AllowGet);
            }
            using(var ctx=new BlogHealthEntities())
            {
                try
                {
                    var post = ctx.Posts.Where(c => c.ID == ID.Value).FirstOrDefault();
                    ctx.Posts.Remove(post);
                    ctx.SaveChanges();
                    return Json(new { result = 1, msg = "Thành công" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { result = 0, msg = ex.Message}, JsonRequestBehavior.AllowGet);
                }
            }
        }
        #endregion
    }
}