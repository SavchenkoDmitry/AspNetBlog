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
    public class PostRepository : GenericRepository<Post>
    {
        public PostRepository(ApplicationContext c) : base(c)
        { }

        public List<Post> GetForPage(int skip, int take)
        {
            return Context.Posts.OrderByDescending(p => p.Time).Skip(skip).Take(take).ToList();
        }
        
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
