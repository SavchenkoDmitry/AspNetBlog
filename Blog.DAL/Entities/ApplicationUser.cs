using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace Blog.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual List<Post> Posts { get; set; }
        public virtual List<Comment> Commnts { get; set; }
    }
}