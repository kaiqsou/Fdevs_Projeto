using DrawHub.Enums;
using DrawHub.Models;
using System.ComponentModel.DataAnnotations;

namespace DrawHub.ViewModels
{
    public class UsuarioCadastroViewModel
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public string? Senha { get; set; }
        [Compare("Senha", ErrorMessage = "As senhas não coincidem!")]
        public string? ConfirmaSenha { get; set; }
    }
}