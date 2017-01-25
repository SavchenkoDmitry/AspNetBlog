using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.DAL.Entities;
using Blog.DAL.EF;
using System.Data.Entity;

namespace Blog.DAL.Repositories
{
    public class CommentRepository : GenericRepository<Comment>
    {
        public CommentRepository(ApplicationContext c) : base(c)
        {
        }     
        public List<Comment> GetInRange(List<Post> pl)
        {
            return Context.Comments.ToList().Where(c => pl.Contains(c.Post)).ToList();
        }
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
