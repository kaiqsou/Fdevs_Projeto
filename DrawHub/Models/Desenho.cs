using DrawHub.Enums;
using System.ComponentModel.DataAnnotations;

namespace DrawHub.Models
{
    public class Desenho
    {
        // Propriedades
        public int Id { get; set; }

        [Display(Name = "Título do Desenho")]
        [Required(ErrorMessage = "Título é obrigatório!")]
        [StringLength(100, ErrorMessage = "Máximo de 100 caracteres!")]
        public string? Titulo { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(500, ErrorMessage = "Máximo de 500 caracteres!")]
        public string? Descricao { get; set; }

        [Display(Name = "Imagem do Desenho")]
        public string? Imagem { get; set; }

        [Display(Name = "Privacidade")]
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
