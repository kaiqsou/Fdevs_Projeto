using DrawHub.Models;

namespace DrawHub.Helpers
{
    public interface ISessao
    {
        Usuario BuscarSessao();
        void CriarSessao(Usuario usuario);
        void EncerrarSessao();
    }
}
