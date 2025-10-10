using System.ComponentModel.DataAnnotations;

namespace DrawHub.Models
{
    public class LoginModel
    {
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "E-mail é obrigatório!")]
        [EmailAddress(ErrorMessage = "E-mail inválido!"), MaxLength(100, ErrorMessage = "Máximo de 50 caracteres!")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Senha é obrigatória!")]
//      [MinLength(8, ErrorMessage = "Mínimo de 8 caracteres!")]
        [MaxLength(50, ErrorMessage = "Máximo de 50 caracteres!")]
        public string Senha { get; set; }
    }
}
