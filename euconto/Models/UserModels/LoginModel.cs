using System.ComponentModel.DataAnnotations;

namespace EuConto.Models.UserModels
{
    public class LoginModel
    {
        [Display(Name = "Usuário")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Talvez, você precise criar uma conta")]
        public string Username { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Digitar a senha, se quiser logar é claro")]
        public string Password { get; set; }
    }
}
