using EuConto.Data;
using EuConto.Models.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EuConto.Components
{
    public class NewStoriesViewComponent : ViewComponent
    {
        protected ApplicationDbContext _context;

        public NewStoriesViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            NewStoriesModel Model = new NewStoriesModel();
            Model.SpotlightStory = new List<NewStories>();

            var Storys = _context.Storys.Include(x => x.User)
                                        .Where(x => x.Published == true)
                                        .OrderByDescending(x => x.DtCreation)
                                        .Take(30);            

            foreach (var Story in Storys)
            {
                Model.SpotlightStory.Add(new NewStories
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
