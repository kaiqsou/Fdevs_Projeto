using DrawHub.Models;

namespace DrawHub.ViewModels
{
    public class DetalhesDesenhoViewModel
    {
        public Desenho? Desenho { get; set; }
        public Categoria? Categoria { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
