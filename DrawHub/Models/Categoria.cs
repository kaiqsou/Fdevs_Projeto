using DrawHub.Enums;
using System.ComponentModel.DataAnnotations;

namespace DrawHub.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string? Nome { get; set; }
        public DateTime? DataCriacao { get; set; } = DateTime.Now;
    }
}
