using System;
using System.Collections.Generic;

namespace Blog.DAL.Entities
{
    public class Post
    {
        public int id { get; set; }
        public DateTime time { get; set; }
        public string topic { get; set; }
        public string text { get; set; }
        public virtual ApplicationUser author { get; set; }
        public virtual List<Comment> coments { get; set; }
        public Post()
        {
            coments = new List<Comment>();
        }
    }
}
