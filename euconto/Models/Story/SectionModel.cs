using Newtonsoft.Json;
using System.Collections.Generic;

namespace EuConto.Models.Story
{
    public class ChapterSectionModel
    {
        public StoryModel Story { get; set; }

        public StoryChapters Chapter { get; set; }
        
        public List<SectionModel> Sections { get; set; }
    }

    public class SectionModel
    {
        public string Id { get; set; }
        
        public int Seq { get; set; }
        
        public string Text { get; set; }
    }
}
