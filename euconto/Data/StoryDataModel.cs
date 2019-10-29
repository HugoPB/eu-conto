using EuConto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EuConto.Data
{
    public class StoryDataModel
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Gender { get; set; }

        public bool Published { get; set; }

        public DateTime DtLastPublish { get; set; }

        public DateTime DtCreation { get; set; }

        //Relations
        [Required]
        public virtual ApplicationUserModel User { get; set; }

        public virtual InteractionDataModel Interaction { get; set; }

        public virtual List<ChapterDataModel> Chapters { get; set; }
    }

    public class ChapterDataModel
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public int Seq { get; set; }

        [Required]
        [MaxLength(256)]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool Published { get; set; }

        public DateTime DtLastPublish { get; set; }

        public DateTime DtCreation { get; set; }

        //Relations
        [Required]
        public virtual StoryDataModel Story { get; set; }

        public virtual List<SectionDataModel> Sections { get; set; }

        public virtual InteractionDataModel Interaction { get; set; }
    }

    public class SectionDataModel
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public int Seq { get; set; }

        public string Text { get; set; }

        //Relations
        [Required]
        public virtual ChapterDataModel Chapter { get; set; }
    }
}