using DrawHub.Enums;

namespace DrawHub.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public CategoriaEnum Nome { get; set; }
        public virtual List<Desenho> Desenhos { get; set; }

    }
}
