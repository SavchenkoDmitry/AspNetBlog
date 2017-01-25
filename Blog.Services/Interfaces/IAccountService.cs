using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Blog.Services.Common;
using Blog.ViewModels.AccountViewModels;

namespace Blog.Services.Interfaces
{
    public interface IAccountService
    {
        Task<OperationDetails> CreateUser(UserViewModel userVM);
        Task<ClaimsIdentity> Authenticate(UserViewModel userVM);
    }
}
