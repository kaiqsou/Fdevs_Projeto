using DrawHub.Enums;
using System.ComponentModel.DataAnnotations;

namespace DrawHub.Models
{
    public class Categoria
    {
        // Propriedades
        public int Id { get; set; }
        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "O nome é obrigatório")]
        public CategoriaEnum Nome { get; set; }
        public virtual List<Desenho>? Desenhos { get; set; }
    }
}
