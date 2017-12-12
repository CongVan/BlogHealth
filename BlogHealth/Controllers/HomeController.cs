using BlogHealth.Models;
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
            using (var ctx=new BlogHealthEntities())
            {
                var posts = ctx.Posts.Join(ctx.Categories, c => c.IDCategory, b => b.ID,
                      (c, b) => new  PostCate{
                          ID = c.ID,
                          Title = c.Title,
                          Slug = c.Slug,
                          IDCate = b.ID,
                          CateName = b.Name,
                          CateColor=b.Color,
                          Likes = c.Likes ?? 0,
                          Views = c.Views ?? 0,
                          Shares = c.Shares ?? 0,
                          Comments = c.Comments ?? 0,
                          Rates = c.Rates ?? 0,
                          CreateDate = c.CreateDate,
                          Update = c.Update,
                          ShortContent =c.ShortContent,
                      }).OrderByDescending(c => new { c.Update, c.CreateDate }).Take(10).ToList();
                
                return View(posts);
            }
        }
        public ActionResult PartialMenu()
        {
            using(var ctx=new BlogHealthEntities())
            {
                var cates = ctx.Categories.Where(c => c.Level == 1).ToList();
                return PartialView(cates);
            }
        }
        public ActionResult PartialMenuChild(int id)
        {
            using (var ctx = new BlogHealthEntities())
            {
                var cates = ctx.Categories.Where(c => c.ParentID==id).ToList();
                return PartialView(cates);
            }
        }
        public ActionResult PartialMostViewPosts()
        {
            using (var ctx = new BlogHealthEntities())
            {
                var posts = ctx.Posts.Join(ctx.Categories, c => c.IDCategory, b => b.ID,
                      (c, b) => new PostCate
                      {
                          ID = c.ID,
                          Title = c.Title,
                          Slug = c.Slug,
                          IDCate = b.ID,
                          CateName = b.Name,
                          CateColor = b.Color,
                          Likes = c.Likes ?? 0,
                          Views = c.Views ?? 0,
                          Rates = c.Rates ?? 0,
                          CreateDate = c.CreateDate,
                          Update = c.Update,
                         
                      }).OrderByDescending(c => new { c.Views }).Take(5).ToList();
                return PartialView(posts);
            }
        }
        public ActionResult Post(int? id)
        {
            if (!id.HasValue)
            {
                return View();
            }
            using (var ctx = new BlogHealthEntities())
            {
                var post = ctx.Posts.Join(ctx.Categories, c => c.IDCategory, b => b.ID,
                      (c, b) => new PostCate
                      {
                          ID = c.ID,
                          Title = c.Title,
                          Slug = c.Slug,
                          IDCate = b.ID,
                          CateName = b.Name,
                          CateColor = b.Color,
                          Likes = c.Likes ?? 0,
                          Views = c.Views ?? 0,
                          Shares = c.Shares ?? 0,
                          Comments = c.Comments ?? 0,
                          Rates = c.Rates ?? 0,
                          CreateDate = c.CreateDate,
                          Update = c.Update,
                          ShortContent = c.ShortContent,
                          Content = c.Content,
                          Tags = c.Tag,
                      }).FirstOrDefault();
                if (post == null)
                {
                    return View();
                }
                return View(post);
                
            }
            //return View();

        }
        public ActionResult Posts()
        {
         
            return View();
        }
        public ActionResult Test()
        {
            return View();
        }
    }
}