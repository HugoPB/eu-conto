using EuConto.Models;
using EuConto.Repositorys.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace EuConto.Repositorys
{
    public class UserRepository : IUserRepository
    {
        protected UserManager<ApplicationUserModel> _userManager;
        protected SignInManager<ApplicationUserModel> _signInManager;

        public UserRepository(UserManager<ApplicationUserModel> userManager,
            SignInManager<ApplicationUserModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ApplicationUserModel> GetUserByID(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<IdentityResult> InsertUser(ApplicationUserModel user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> UpdateUser(ApplicationUserModel user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public bool UserIsAthenticated()
        {
            return _signInManager.Context.User.Identity.IsAuthenticated;
        }

        public async Task<SignInResult> SignIn(string username, string password)
        {
            return await _signInManager.PasswordSignInAsync(username, password, true, false);
        }

        public async void SignOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
