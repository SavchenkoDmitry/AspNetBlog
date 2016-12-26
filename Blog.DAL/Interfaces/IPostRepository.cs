using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.DAL.Entities;

namespace Blog.DAL.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        List<Post> GetForPage(int skip, int take);
    }
}
