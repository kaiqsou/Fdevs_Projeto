using DrawHub.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DrawHub.ViewComponents
{
    public class Header : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessao = HttpContext.Session.GetString("sessaoLogado");

            if (string.IsNullOrEmpty(sessao))
            {
                return View((Usuario) null);
            }

            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(sessao);
            return View(usuario);
        }
    }
}