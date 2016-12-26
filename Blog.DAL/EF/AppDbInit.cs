using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using Blog.DAL.Identity;
using Blog.DAL.Entities;

namespace Blog.DAL.EF
{
    public class AppDbInit : CreateDatabaseIfNotExists<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));

            ApplicationRole role1 = new ApplicationRole { Name = "admin" };
            ApplicationRole role2 = new ApplicationRole { Name = "user" };

            roleManager.Create(role1);
            roleManager.Create(role2);

            ApplicationUser admin = new ApplicationUser { Email = "dkdimondk@gmail.com", UserName = "dkdimondk@gmail.com" };
            string password = "phenomenonONE";
            IdentityResult result = userManager.Create(admin, password);

            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
            }

            Post firstPost = new Post() { topic = "First publication", text = "Hello world! This is new blog.", time = DateTime.Now, author = admin };

            context.Posts.Add(firstPost);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
