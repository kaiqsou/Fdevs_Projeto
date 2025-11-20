using DrawHub.Models;
using DrawHub.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace DrawHub.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaRepositorio _categoriaRepositorio;
        public CategoriaController(ICategoriaRepositorio categoriaRepositorio)
        {
            _categoriaRepositorio = categoriaRepositorio;
        }

        // Métodos [GET]
        public IActionResult Index()
        {
            var categorias = _categoriaRepositorio.BuscarTodos();

            return View(categorias);
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
        [HttpPost]
        public IActionResult Criar(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var novaCategoria = _categoriaRepositorio.Adicionar(categoria);

                    return RedirectToAction("Index");
                }
                catch (Exception erro)
                {
                    TempData["MsgErro"] = $"Erro ao adicionar a categoria! Mais detalhes: {erro.Message}";
                }
            }

            return View(categoria);
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
}
