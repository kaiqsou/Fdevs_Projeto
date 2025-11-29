using DrawHub.Enums;
using System.ComponentModel.DataAnnotations;

namespace DrawHub.Models
{
    public class Usuario
    {
        // Propriedades
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; } = null;
        public string? Senha { get; set; }
        public RoleEnum? Tipo { get; set; } = RoleEnum.Padrão;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime? DataAtualizacao { get; set; }
        public virtual List<Desenho>? Desenhos { get; set; }

        // Métodos
        public bool SenhaValida(string senha)
        {
            return Senha == senha;
        }

        public void AtualizarSenha(string novaSenha)
        {
            Senha = novaSenha;
        }
    }
}
