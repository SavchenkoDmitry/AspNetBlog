using System;
using System.Collections.Generic;
using Blog.DAL.Entities.Enums;

namespace Blog.DAL.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public Theme Topic { get; set; }
        public string Text { get; set; }
        public virtual ApplicationUser Author { get; set; }
    }
}
