using System.Collections.Generic;

namespace Blog.BLL.DTO
{
    public class PostDTO
    {
        public string time { get; set; }
        public string topic { get; set; }
        public string text { get; set; }
        public string author { get; set; }
        public int id { get; set; }
        public List<CommentDTO> coments { get; set; }
    }
}
