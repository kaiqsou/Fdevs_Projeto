using DrawHub.Enums;

namespace DrawHub.Models
{
    public class UsuarioBasico
    {
        // Propriedades - as mesmas de usuário, mas sem a senha para alteração
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public RoleEnum? Tipo { get; set; }
    }
}