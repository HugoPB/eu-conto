using EuConto.Data;
using EuConto.Models;
using EuConto.Models.Story;
using EuConto.Models.UserModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EuConto.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        protected UserManager<ApplicationUserModel> _userManager;
        protected SignInManager<ApplicationUserModel> _signInManager;
        protected ApplicationDbContext _context;

        public UserController(
            UserManager<ApplicationUserModel> userManager,
            SignInManager<ApplicationUserModel> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterModel RegisterData)
        {
            if (ModelState.IsValid)
            {
                if(RegisterData.Password != RegisterData.ConfirmPassword)
                {
                    ModelState.AddModelError("", "Então, o campo 'Senha' e 'Confirmar Senha' precisam ser iguais, tenta novamente");
                    return View();
                }

                var result = _userManager.CreateAsync(new ApplicationUserModel
                {
                    UserName = RegisterData.Username,
                    Email = RegisterData.Email,
                    FullName = RegisterData.FullName,
                    Bio = RegisterData.Bio
                }, RegisterData.Password);

                if (result.Result.Succeeded)
                    return View("UserCreated");

                foreach (var error in result.Result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View();
        }
        
        public IActionResult SignOut()
        {
            _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginModel LoginData)
        {
            if (ModelState.IsValid)
            {
                HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

                var result = _signInManager.PasswordSignInAsync(LoginData.Username, LoginData.Password, true, false);

                if (result.Result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "DICA: Usuário ou a Senha, um desses dois ta errado, ou os dois...");
            }
            
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            if (_signInManager.Context.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public async Task<IActionResult> Profile(string userId)
        {
            if (userId == "0")
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var UserProfile = await _userManager.FindByIdAsync(userId);
            var LoggedUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var FollowersDB = _context.Followers.Include(x => x.Followers);
            var Followers = FollowersDB.Where(x => x.User.Id == UserProfile.Id).FirstOrDefault();

            ProfileModel Model = new ProfileModel();

            if (UserProfile.Id == LoggedUserID)
            {
                Model.IsEditable = true;
                Model.Follower = true;
                Model.Storys = new List<StoryModel>();
                foreach (var Story in _context.Storys.Include(x => x.Interaction)
                                                        .ThenInclude(x => x.Likes)
                                                            .ThenInclude(x => x.User)
                                                     .Include(x => x.Interaction.Comentaries)
                                                     .Where(x => x.User.Id == UserProfile.Id))
                {
                    Model.Storys.Add(new StoryModel
                    {
                        Title = Story.Title,
                        Gender = Story.Gender,
                        Description = Story.Description,
                        Published = Story.Published,
                        DtLastPublish = Story.DtLastPublish,
                        Id = Story.Id,
                        Likes = Story.Interaction.Likes.Count,
                        Comentaries = Story.Interaction.Comentaries.Count,
                        Liked = Story.Interaction.Likes.Find(x => x.User.Id == LoggedUserID) != null ? true : false
                    });
                }
            }
            else
            {
                Model.IsEditable = false;
                if(Followers != null)
                    Model.Follower = Followers.Followers.Find(x => x.Id == userId) != null ? true : false;

                Model.Storys = new List<StoryModel>();
                foreach (var Story in _context.Storys.Include(x => x.Interaction)
                                                        .ThenInclude(x => x.Likes)
                                                            .ThenInclude(x => x.User)
                                                      .Include(x => x.Interaction.Comentaries)
                                                      .Where(x => x.User.Id == UserProfile.Id && x.Published == true))
                {
                    Model.Storys.Add(new StoryModel
                    {
                        Title = Story.Title,
                        Gender = Story.Gender,
                        Description = Story.Description,
                        Published = Story.Published,
                        DtLastPublish = Story.DtLastPublish,
                        Id = Story.Id,
                        Likes = Story.Interaction.Likes.Count,
                        Comentaries = Story.Interaction.Comentaries.Count,
                        Liked = Story.Interaction.Likes.Find(x => x.User.Id == LoggedUserID) != null ? true : false
                    });
                }
            }
            
            Model.Username = UserProfile.UserName;
            Model.FullName = UserProfile.FullName;
            Model.Bio = UserProfile.Bio;

            if (Followers != null)
                Model.FollowerCount = Followers.Followers.Count;
            else
                Model.FollowerCount = 0;

            return View(Model);
        }
        
        public async Task<IActionResult> ProfileEdit()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var UserProfile = await _userManager.FindByIdAsync(userId);

            ProfileModel Model = new ProfileModel();

            Model.Username = UserProfile.UserName;
            Model.FullName = UserProfile.FullName;
            Model.Bio = UserProfile.Bio;
            
            return View(Model);
        }

        [HttpPost]
        public async Task<IActionResult> ProfileEdit(ProfileModel Profile)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var UserProfile = await _userManager.FindByIdAsync(userId);

                UserProfile.FullName = Profile.FullName;
                UserProfile.Bio = Profile.Bio;

                await _userManager.UpdateAsync(UserProfile);

                return RedirectToAction("Profile", new { userId = 0 });
            }

            return View(Profile);
        }

        public IActionResult UserLikeStory(string StoryId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var UserProfile = _userManager.FindByIdAsync(userId).Result;

            var Story = _context.Storys.Include(x => x.Interaction)
                                    .ThenInclude(x => x.Likes)
                                    .ThenInclude(x => x.User)
                                    .Where(x => x.Id == StoryId)
                                    .FirstAsync().Result;

            var UserLike = Story.Interaction.Likes.Find(x => x.User.Id == userId);

            if (UserLike != null)
                _context.Remove(UserLike);
            else
            {
                Story.Interaction.Likes.Add(new LikesDataModel
                {
                    User = UserProfile
                });
            }

            _context.SaveChanges();

            return Json(new { Sucesso = 1 });
        }
        
        public IActionResult UserLikeChapter(string ChapterId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var UserProfile = _userManager.FindByIdAsync(userId).Result;

            var Chapter = _context.Chapters.Include(x => x.Interaction)
                                    .ThenInclude(x => x.Likes)
                                    .ThenInclude(x => x.User)
                                    .Where(x => x.Id == ChapterId)
                                    .FirstAsync().Result;

            var UserLike = Chapter.Interaction.Likes.Find(x => x.User.Id == userId);

            if (UserLike != null)
                _context.Remove(UserLike);
            else
            {
                Chapter.Interaction.Likes.Add(new LikesDataModel
                {
                    User = UserProfile
                });
            }

            _context.SaveChanges();

            return Json(new { Sucesso = 1 });
        }
    }
}
