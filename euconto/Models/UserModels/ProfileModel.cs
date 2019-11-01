using EuConto.Models.Story;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EuConto.Models.UserModels
{
    public class ProfileModel
    {
        [Display(Name = "Usuário")]
        public string Username { get; set; }

        [Display(Name = "Nome Completo")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Escreva seu nome real, este campo é obrigatório")]
        public string FullName { get; set; }

        [Display(Name = "Biografia")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Escreva qualquer coisa, este campo é obrigatório")]
        public string Bio { get; set; }

        public IFormFile ProfileImg { get; set; }

        public bool IsEditable { get; set; }

        public bool Follower { get; set; }

        public int FollowerCount { get; set; }

        public List<StoryModel> Storys { get; set; }
    }
}
