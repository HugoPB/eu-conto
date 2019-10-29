using EuConto.Data;
using EuConto.Models.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EuConto.Components
{
    public class SpotlightUsersViewComponent : ViewComponent
    {
        protected ApplicationDbContext _context;

        public SpotlightUsersViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            SpotlightUsersModel Model = new SpotlightUsersModel();
            Model.SpotlightUsers = new List<SpotlightUsers>();

            var Users = _context.Users.Take(30);

            foreach (var User in Users)
            {
                Model.SpotlightUsers.Add(new SpotlightUsers
                {
                    UserId = User.Id,
                    Username = User.UserName,
                    Bio = User.Bio
                });
            }

            return View(Model);
        }
    }
}
