using EuConto.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EuConto.Data
{
    public class InteractionDataModel
    {
        [Key]
        public string Id { get; set; }

        public virtual List<LikesDataModel> Likes { get; set; }

        public virtual List<ComentaryDataModel> Comentaries { get; set; }
    }

    public class LikesDataModel
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public virtual ApplicationUserModel User { get; set; }
    }

    public class ComentaryDataModel
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Text { get; set; }

        //Relations
        [Required]
        public virtual ApplicationUserModel User { get; set; }

        public SubInteractionDataModel SubInteraction { get; set; }
    }

    public class SubInteractionDataModel
    {
        [Key]
        public string Id { get; set; }

        //Relations
        public InteractionDataModel Interaction { get; set; }
    }
}
