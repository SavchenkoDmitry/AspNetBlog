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
    public class CommentRepository : ICommendRepository
    {
        ApplicationContext db;
        public CommentRepository(ApplicationContext _db)
        {
            db = _db;
            db.Comments.Load();
        }

        public IEnumerable<Comment> GetAll()
        {
            return db.Comments;
        }
        public Comment Get(int id)
        {
            return db.Comments.Find(id);
        }
        public List<Comment> GetForPage(int skip, int take)
        {
            return db.Comments.OrderBy(c => c.time).Skip(skip).Take(take).ToList();
        }

        public void Create(Comment item)
        {
            db.Comments.Add(item);
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            Comment c = Get(id);
            if (c != null)
            {
                Delete(c);
            }
        }
        public void Delete(Comment c)
        {
            db.Comments.Remove(c);
        }
        public void Update(Comment item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public List<Comment> GetInRange(List<Post> pl)
        {
            return db.Comments.ToList().Where(c => pl.Contains(c.post)).ToList();
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
