using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogHealth.Models
{
    public class PostCate
    {
       
        public int ID { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public int IDCate { get; set; }
        public string CateName { get; set; }
        public int Likes { get; set; }
        public int Views { get; set; }
        public int Shares { get; set; }
        public int Comments { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Tag { get; set; }
        public int Rates { get; set; }
        public DateTime? Update { get; set; }
        public string CateColor { get; set; }
        public string CateSlug { get; set; }

        public string ShortContent { get; set; }

        public string Content { get; set; }
        public string Tags { get; set; }
    }
}