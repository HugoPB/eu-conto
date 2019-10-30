using EuConto.Models;
using EuConto.Models.UserModels;
using EuConto.Repositorys;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace EuConto.Services
{
    public class UserServices
    {
        protected UserRepository _userRepository;

        public UserServices(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IdentityResult> RegisterNewUserAsync(RegisterModel newUser)
        {
            return await _userRepository.InsertUser(new ApplicationUserModel
            {
                UserName = newUser.Username,
                Email = newUser.Email,
                FullName = newUser.FullName,
                Bio = newUser.Bio
            }, newUser.Password);
        }

        public async Task<ApplicationUserModel> GetByIdAsync(string userId)
        {
            return await _userRepository.GetUserByID(userId);
        }

        public async Task<IdentityResult> UpdateUserAsync(ApplicationUserModel user)
        {
            return await _userRepository.UpdateUser(user);
        }

        public bool UserIsAthenticated()
        {
            return _userRepository.UserIsAthenticated();
        }

        public async Task<SignInResult> UserSignIn(string username, string password)
        {
            return await _userRepository.SignIn(username, password);
        }

        public void UserSignOut()
        {
            _userRepository.SignOut();
        }
    }
}