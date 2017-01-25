using Blog.Services.Common;
using Blog.DAL.Entities;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Blog.Services.Interfaces;
using System.Collections.Generic;
using System;
using Blog.DAL.EF;
using Blog.DAL.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Blog.ViewModels.AccountViewModels;

namespace Blog.Services.Services
{
    public class AccountService : IAccountService
    {
        const string StandartRole = "user";

        private ApplicationContext db;
        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;

        public AccountService()
        {
            db = new ApplicationContextFactory().Create();
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            db.Posts.Load();
            db.Comments.Load();
        }
        
        public async Task<OperationDetails> CreateUser(UserViewModel userVM)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(userVM.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userVM.Email, UserName = userVM.Email };
                await userManager.CreateAsync(user, userVM.Password);
                await userManager.AddToRoleAsync(user.Id, userVM.Role);
                if (userVM.Role != StandartRole)
                {
                    await userManager.AddToRoleAsync(user.Id, StandartRole);
                }
                await db.SaveChangesAsync();
                return new OperationDetails(true, "Register successful", "");

            }
            else
            {
                return new OperationDetails(false, "User with this email already exists", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserViewModel userVM)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await userManager.FindAsync(userVM.Email, userVM.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
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
                }
                this.disposed = true;
            }
        }

    }
}
