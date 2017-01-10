using System.Collections.Generic;

namespace Blog.ViewModels
{
    public class ContentViewModel
    {
        public List<PostViewModel> Content { get; set; }
        public bool MorePosts { get; set; }
    }
}
