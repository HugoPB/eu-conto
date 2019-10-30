using EuConto.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace EuConto.Repositorys.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUserModel> GetUserByID(string userId);
        Task<IdentityResult> InsertUser(ApplicationUserModel user, string password);
        Task<IdentityResult> UpdateUser(ApplicationUserModel user);
    }
}