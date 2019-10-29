using EuConto.Data;
using EuConto.Models.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EuConto.Components
{
    public class SpotlightStorysViewComponent : ViewComponent
    {
        protected ApplicationDbContext _context;

        public SpotlightStorysViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            SpotlightStorysModel Model = new SpotlightStorysModel();
            Model.SpotlightStory = new List<SpotlightStory>();

            var Storys = _context.Storys.Include(x => x.User)
                                        .Include(x => x.Interaction)
                                        .ThenInclude(x => x.Likes)
                                        .Where(x => x.Published == true)
                                        .OrderByDescending(x => x.Interaction.Likes.Count)
                                        .Take(30);

            foreach(var Story in Storys)
            {
                Model.SpotlightStory.Add(new SpotlightStory
                {
                    UserId = Story.User.Id,
                    StoryId = Story.Id,
                    Username = Story.User.UserName,
                    StoryTitle = Story.Title,
                    StoryDescription = Story.Description
                });
            }

            return View(Model);
        }
    }
}
