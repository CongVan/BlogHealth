using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogHealth.Models;
using System.Web.Mvc;
namespace BlogHealth.Utility
{
    public class CheckTool
    {
        public static bool CheckMenuHasChild(int id)
        {
            using(var ctx= new BlogHealthEntities())
            {
                var cate = ctx.Categories.Where(c => c.ParentID == id).FirstOrDefault();
                return cate != null;
            }
        }
    }
}