using DrawHub.Enums;
using DrawHub.Helpers;
using System.ComponentModel.DataAnnotations;

namespace DrawHub.Models
{
    public class Usuario
    {
        // Propriedades
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public RoleEnum? Tipo { get; set; } = RoleEnum.Padrão;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public virtual List<Desenho>? Desenhos { get; set; }

        // Métodos
        public bool SenhaValida(string senha)
        {
            return Senha == senha.GerarHash();
        }

        public void SetSenhaHash()
        {
            Senha = Senha.GerarHash();
        }
    }
}
