using DrawHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace DrawHub.Controllers
{
    public class UsuarioController : Controller
    {
        // Métodos [GET]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            return View();
        }

        public IActionResult ConfirmarExclusao(int id)
        {
            return View();
        }

        public IActionResult ExibirDesenhos(int id)
        {
            return View();
        }

        // Métodos [POST]
        [HttpPost]
        public IActionResult Criar(Usuario usuario)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Editar(UsuarioBasico usuario)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Excluir(int id)
        {
            return View();
        }
    }
}
