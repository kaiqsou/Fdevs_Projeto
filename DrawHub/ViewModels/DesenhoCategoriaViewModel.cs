using DrawHub.Enums;
using DrawHub.Models;

namespace DrawHub.ViewModels
{
    public class DesenhoCategoriaViewModel
    {
        public Desenho? Desenho { get; set; }
        public int CategoriaId { get; set; }
        public List<Categoria> Categorias { get; set; } = new List<Categoria>();
    }
}
