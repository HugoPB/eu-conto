using System;
using System.ComponentModel.DataAnnotations;

namespace EuConto.Models.Story
{
    public class StoryModel
    {
        public string Id { get; set; }

        [Display(Name = "Título")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Titulo bem massa vem aqui, não deixe vazio")]
        public string Title { get; set; }

        [Display(Name = "Descrição")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Fala um pouquinho da estória, mas não deixe vazio")]
        public string Description { get; set; }

        [Display(Name = "Gênero")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Inventa um novo gênero mas não deixa vazio")]
        public string Gender { get; set; }

        [Display(Name = "Publicado")]
        [DataType(DataType.Text)]
        public bool Published { get; set; }

        public DateTime DtLastPublish { get; set; }

        public int Likes { get; set; }

        public int Comentaries { get; set; }

        public bool Liked { get; set; }
    }
}
