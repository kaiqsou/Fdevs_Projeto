using DrawHub.Models;
using Newtonsoft.Json;

namespace DrawHub.Helpers
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _httpContext;
        public Sessao(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public Usuario BuscarSessao()
        {
            string sessao = _httpContext.HttpContext.Session.GetString("sessaoLogado");

            if (string.IsNullOrEmpty(sessao)) return null;

            return JsonConvert.DeserializeObject<Usuario>(sessao);
        }

        public void CriarSessao(Usuario usuario)
        {
            string user = JsonConvert.SerializeObject(usuario);

            _httpContext.HttpContext.Session.SetString("sessaoLogado", user);
        }

        public void EncerrarSessao()
        {
            _httpContext.HttpContext.Session.Remove("sessaoLogado");
        }
    }
}
