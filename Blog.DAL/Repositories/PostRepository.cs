using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.DAL.Interfaces;
using Blog.DAL.Entities;
using Blog.DAL.EF;
using System.Data.Entity;

namespace Blog.DAL.Repositories
{
    public class PostRepository : IPostRepository
    {
        ApplicationContext db;
        public PostRepository(ApplicationContext _db)
        {
            db = _db;
            db.Posts.Load();
        }
        
        public List<Post> GetForPage(int skip, int take)
        {
            return db.Posts.OrderByDescending(p => p.time).Skip(skip).Take(take).ToList();
        }

        public IEnumerable<Post> GetAll()
        {
            return db.Posts;
        }
        public Post Get(int id)
        {
            return db.Posts.Find(id);
        }
        
        public void Create(Post item)
        {
            db.Posts.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Post p = Get(id);
            if (p != null)
            {
                Delete(p);
            }
        }
        public void Delete(Post p)
        {
            db.Posts.Remove(p);
        }

        public void Update(Post item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
