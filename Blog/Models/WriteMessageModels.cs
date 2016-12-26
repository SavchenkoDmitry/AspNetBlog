using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class WriteCommentModel
    {
        public string text { get; set; }
        public int id { get; set; }
    }
    public class WritePostModel
    {
        public string text { get; set; }
        public string topic { get; set; }
    }
}