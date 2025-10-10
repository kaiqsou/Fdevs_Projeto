using DrawHub.Enums;

namespace DrawHub.Models
{
    public class Desenho
    {
        // Propriedades
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public string? Imagem { get; set; }
        public bool Privacidade { get; set; } = false;
        public DateTime? DataEnvio { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public virtual List<Categoria>? Categorias { get; set; }
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        // Métodos
        public void MudarImagem(string imagem)
        {
            Imagem = imagem;
        }

        public void PrivarDesenho()
        {
            Privacidade = true;
        }
    }
}
