using System.Collections.Generic;

namespace Blog.ViewModels
{
    public class PostViewModel
    {
        public string Time { get; set; }
        public string Topic { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public int Id { get; set; }
        public List<CommentViewModel> Coments { get; set; }
    }
}
