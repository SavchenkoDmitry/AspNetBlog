namespace Blog.BLL.DTO
{
    public class CommentDTO
    {
        public string time { get; set; }
        public string text { get; set; }
        public int id { get; set; }
        public string author { get; set; }
        public bool isAuthor { get; set; }
    }
}
