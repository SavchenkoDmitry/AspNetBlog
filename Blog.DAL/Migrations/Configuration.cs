namespace Blog.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Blog.DAL.EF.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Blog.DAL.EF.ApplicationContext";
        }

        protected override void Seed(Blog.DAL.EF.ApplicationContext context)
        {
        }
    }
}
