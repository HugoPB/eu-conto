using EuConto.Models;
using EuConto.Models.UserModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EuConto.Components
{
    public class MainMenuViewComponent : ViewComponent
    {
        protected SignInManager<ApplicationUserModel> _signInManager;

        public MainMenuViewComponent(SignInManager<ApplicationUserModel> signInManager)
        {
            _signInManager = signInManager;
        }

        public IViewComponentResult Invoke()
        {
            MainMenuModel Model = new MainMenuModel();

            Model.LoggedIn = _signInManager.Context.User.Identity.IsAuthenticated == true ? _signInManager.Context.User.Identity.IsAuthenticated : false;

            return View(Model);
        }
    }
}
