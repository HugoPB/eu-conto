using System.Collections.Generic;

namespace EuConto.Models.Components
{
    public class SpotlightStorysModel
    {
        public List<SpotlightStory> SpotlightStory { get; set; }
    }

    public class SpotlightStory
    {
        public string Username { get; set; }
        public string UserId { get; set; }
        public string StoryTitle { get; set; }
        public string StoryDescription { get; set; }
        public string StoryId { get; set; }
    }
}
