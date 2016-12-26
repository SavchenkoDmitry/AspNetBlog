using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.BLL.DTO;

namespace Blog.Models
{
    public class PostsViewModel
    {
        public List<PostDTO> content { get; set; }
        public bool morePosts { get; set; }
    }
}