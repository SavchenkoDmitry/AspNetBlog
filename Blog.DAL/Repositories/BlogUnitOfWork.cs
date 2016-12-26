using Blog.DAL.Interfaces;
using Blog.DAL.EF;
using System;
using System.Threading.Tasks;
using Blog.DAL.Entities;
using Blog.DAL.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Blog.DAL.Repositories
{
    public class BlogUnitOfWork : IBlogUnitOfWork
    {
        private ApplicationContext db;

        private ICommendRepository commentRep;
        private IPostRepository postRep;
        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;

        public BlogUnitOfWork()
        {
            db = new ApplicationContextFactory().Create();
            commentRep = new CommentRepository(db);
            postRep = new PostRepository(db);
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
        }

        public ICommendRepository CommentRep
        {
            get
            {
                return commentRep;
            }
        }

        public IPostRepository PostRep
        {
            get
            {
                return postRep;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return userManager;
            }
        }
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return roleManager;
            }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                    commentRep.Dispose();
                    postRep.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
