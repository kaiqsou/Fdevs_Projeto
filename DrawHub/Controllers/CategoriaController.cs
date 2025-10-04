using DrawHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace DrawHub.Controllers
{
    public class CategoriaController : Controller
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
        public IActionResult Criar(Categoria categoria)
        {
            return View();
        }

        public IActionResult Editar(Categoria categoria)
        {
            return View();
        }

        public IActionResult Excluir(int id)
        {
            return View();
        }
    }
