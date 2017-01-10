using System;

namespace Blog.DAL.Entities
{
    public class Comment
    {
        public int Id { set; get; }
        public DateTime Time { set; get; }
        public string Text { set; get; }
        public virtual ApplicationUser Author { set; get; }
        public virtual Post Post { set; get; }
    }
}
