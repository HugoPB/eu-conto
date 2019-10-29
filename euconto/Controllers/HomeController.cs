using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EuConto.Models;
using EuConto.Data;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using EuConto.Models.Home;

namespace EuConto.Controllers
{
    public class HomeController : Controller
    {
        protected ApplicationDbContext _context;
        protected UserManager<ApplicationUserModel> _userManager;
        protected SignInManager<ApplicationUserModel> _signInManager;

        public HomeController(
            ApplicationDbContext context,
            UserManager<ApplicationUserModel> userManager,
            SignInManager<ApplicationUserModel> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            _context.Database.EnsureCreated();
            var x = _context.Settings.Count();
            if(x < 1)
            {
                _context.Settings.Add(new SettingsDataModel
                {
                    Name = "Teste",
                    Value = "1"
                });

                _context.SaveChanges();
            }

            HomeModel Model = new HomeModel();
            Model.LoggedIn = _signInManager.Context.User.Identity.IsAuthenticated == true ? _signInManager.Context.User.Identity.IsAuthenticated : false;

            //List<ComentaryDataModel> LC1 = new List<ComentaryDataModel>();
            //LC1.Add(new ComentaryDataModel
            //{
            //    Text = "Teste HOHO"
            //});

            //InteractionDataModel I = new InteractionDataModel();
            //I.Comentaries = LC1;

            //SubInteractionDataModel SI = new SubInteractionDataModel();
            //SI.Interaction = I;

            //List<ComentaryDataModel> LC = new List<ComentaryDataModel>();

            //LC.Add(new ComentaryDataModel
            //{
            //    Text = "Teste haha"
            //});
            //LC.Add(new ComentaryDataModel
            //{
            //    Text = "Teste hehe",
            //    SubInteraction = SI
            //});


            //_context.Interactions.Add(new InteractionDataModel
            //{
            //    Comentaries = LC
            //});

            _context.SaveChanges();

            //Teste comentaries
            var abc = _context.Interactions.Include(i => i.Comentaries);

            return View(Model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
