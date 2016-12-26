using System;

namespace Blog.DAL.Entities
{
    public class Comment
    {
        public int id { set; get; }
        public DateTime time { set; get; }
        public string text { set; get; }
        public virtual ApplicationUser author { set; get; }
        public virtual Post post { set; get; }
    }
}
