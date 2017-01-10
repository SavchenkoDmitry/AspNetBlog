using System;
using System.Collections.Generic;

namespace Blog.DAL.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string Topic { get; set; }
        public string Text { get; set; }
        public virtual ApplicationUser Author { get; set; }
    }
}
