using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class PostPreviewListViewModel
    {
        public List<PostPreviewViewModel> Previews;
        public bool IsAdmin { get; set; }
        public bool IsUser { get; set; }
    }
}
