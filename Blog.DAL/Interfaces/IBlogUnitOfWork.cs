using Blog.DAL.Identity;
using Blog.DAL.Entities;
using System;
using System.Threading.Tasks;

namespace Blog.DAL.Interfaces
{
    public interface IBlogUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        IPostRepository PostRep { get; }
        ICommendRepository CommentRep { get; }
        ApplicationRoleManager RoleManager { get; }
        Task SaveAsync();
    }
}
