using System.ComponentModel.DataAnnotations;

namespace EuConto.Models.UserModels
{
    public class RegisterModel
    {
        [Display(Name = "Usuário")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Crie um nome de usuário, este campo é obrigatório")]
        public string Username { get; set; }

        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Dúvido que você não tenha E-Mail")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Tem certeza de que isso é um E-Mail ?")]
        public string Email { get; set; }

        [Display(Name = "Nome Completo")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Escreva seu nome real, este campo é obrigatório")]
        public string FullName { get; set; }

        [Display(Name = "Biografia")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Escreva qualquer coisa, este campo é obrigatório")]
        public string Bio { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Criatividade pra senha, este campo é obrigatório")]
        public string Password { get; set; }

        [Display(Name = "Confirmar Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Digita igual a senha, mas vazio não pode deixar")]
        public string ConfirmPassword { get; set; }
    }
}
