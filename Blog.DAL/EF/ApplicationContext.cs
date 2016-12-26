using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Blog.DAL.Entities;

namespace Blog.DAL.EF
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(string conectionString) : base(conectionString)
        {
            Database.SetInitializer<ApplicationContext>(new AppDbInit());
        }
        
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}