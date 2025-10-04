using DrawHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace DrawHub.Controllers
{
    public class DesenhoController : Controller
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

        // Métodos [POST]
        public IActionResult Criar(Desenho desenho)
        {
            return View();
        }

        public IActionResult Editar(Desenho desenho)
        {
            return View();
        }

        public IActionResult Excluir(int id)
        {
            return View();
        }
    }
}
