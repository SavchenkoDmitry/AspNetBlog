using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.DAL.Entities;

namespace Blog.DAL.Interfaces
{
    public interface ICommendRepository : IGenericRepository<Comment>
    {
        List<Comment> GetInRange(List<Post> pl);
    }
}
