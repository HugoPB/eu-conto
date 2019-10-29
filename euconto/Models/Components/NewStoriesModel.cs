using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuConto.Models.Components
{
    public class NewStoriesModel
    {
        public List<NewStories> SpotlightStory { get; set; }
    }

    public class NewStories
    {
        public string Username { get; set; }
        public string UserId { get; set; }
        public string StoryTitle { get; set; }
        public string StoryDescription { get; set; }
        public string StoryId { get; set; }
    }
}
