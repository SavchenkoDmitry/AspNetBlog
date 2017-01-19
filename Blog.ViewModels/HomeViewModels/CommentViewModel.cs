using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.ViewModels.HomeViewModels
{
    public class CommentViewModel
    {
        public string Time { get; set; }
        public string Text { get; set; }
        public int Id { get; set; }
        public string Author { get; set; }
        public bool IsAuthor { get; set; }
    }
}
