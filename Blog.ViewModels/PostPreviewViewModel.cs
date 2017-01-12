using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class PostPreviewViewModel
    {
        public string Time { get; set; }
        public string Topic { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public int Id { get; set; }
    }
}
