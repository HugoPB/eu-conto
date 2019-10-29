using EuConto.Models;
using EuConto.Models.UserModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EuConto.Components
{
    public class BarUserMenuViewComponent: ViewComponent
    {
        protected SignInManager<ApplicationUserModel> _signInManager;

        public BarUserMenuViewComponent(SignInManager<ApplicationUserModel> signInManager)
        {
            _signInManager = signInManager;
        }

        public IViewComponentResult Invoke()
        {
            BarUserMenuModel Model = new BarUserMenuModel();

            Model.LoggedIn = _signInManager.Context.User.Identity.IsAuthenticated == true ? _signInManager.Context.User.Identity.IsAuthenticated : false;

            return View(Model);
        }
    }
}
