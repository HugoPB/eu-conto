using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuConto.Models.Story
{
    public class ChapterModel
    {
        public StoryModel Story { get; set; }

        public List<StoryChapters> Chapters { get; set; }

        public bool IsEditable { get; set; }

        public bool Published { get; set; }

        public DateTime DtLastPublish { get; set; }
    }

    public class StoryChapters
    {
        public string Id { get; set; }
        public int Seq { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool Published { get; set; }

        public DateTime DtLastPublish { get; set; }

        public int Likes { get; set; }

        public int Comentaries { get; set; }

        public bool Liked { get; set; }
    }
}
