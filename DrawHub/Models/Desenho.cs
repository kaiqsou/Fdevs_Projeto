using DrawHub.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string? ImagemCaminho { get; set; }

        [NotMapped]
        [Display(Name = "Selecione a imagem")]
        public IFormFile? ArquivoImagem {  get; set; }

        [Display(Name = "Privacidade")]
        public bool Privacidade { get; set; } = false;
        public DateTime? DataEnvio { get; set; } = DateTime.Now;
        public DateTime? DataAtualizacao { get; set; }

        [Required]
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
        public Guid UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        // Métodos
        public void PrivarDesenho()
        {
            Privacidade = true;
        }
    }
}
