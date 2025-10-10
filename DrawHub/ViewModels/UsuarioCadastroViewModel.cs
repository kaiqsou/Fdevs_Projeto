using DrawHub.Enums;
using DrawHub.Models;
using System.ComponentModel.DataAnnotations;

namespace DrawHub.ViewModels
{
    public class UsuarioCadastroViewModel
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Nome é obrigatório!")]
        [StringLength(100, ErrorMessage = "Máximo de 100 caracteres!")]
        public string? Nome { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "E-mail é obrigatório!")]
        [EmailAddress(ErrorMessage = "E-mail inválido!"), MaxLength(100, ErrorMessage = "Máximo de 100 caracteres!")]
        public string? Email { get; set; }

        [Display(Name = "Telefone")]
        public string? Telefone { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Senha é obrigatória!")]
//      [MinLength(8, ErrorMessage = "Mínimo de 8 caracteres!")]
        [MaxLength(50, ErrorMessage = "Máximo de 50 caracteres!")]
        public string? Senha { get; set; }

        [Display(Name = "Confirme a Senha")]
        [Required(ErrorMessage = "Por favor, confirme a senha!")]
        [Compare("Senha", ErrorMessage = "As senhas não coincidem!")]
        public string? ConfirmaSenha { get; set; }
    }
}