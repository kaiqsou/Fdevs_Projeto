using DrawHub.Filters;
using DrawHub.Models;
using DrawHub.Repositorio;
using DrawHub.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DrawHub.Controllers
{
    [AdminPage]
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

            var lista = categorias.Select(categoria => new QtdCategoriaViewModel
            {
                Categoria = categoria,
                QtdDesenhos = _categoriaRepositorio.ContarDesenhos(categoria.Id)
            }).ToList();

            return View(lista);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            try
            {
                var categoria = _categoriaRepositorio.BuscarPorId(id);

                if (categoria == null) return RedirectToAction("Index", "Categoria");

                return View(categoria);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na edição da categoria! Detalhes do erro: {ex.Message}");
                return RedirectToAction("Index", "Categoria");
            }
        }

        public IActionResult Excluir(int id)
        {
            try
            {
                bool apagado = _categoriaRepositorio.Excluir(id);

                return RedirectToAction("Index", "Categoria");
            }
            catch (Exception erro)
            {
                Console.WriteLine($"Erro na exclusão da categoria! Detalhes do erro: {erro.Message}");
                return RedirectToAction("Index", "Categoria");
            }
        }

        public IActionResult ConfirmarExclusao(int id)
        {
            try
            {
                Categoria categoria = _categoriaRepositorio.BuscarPorId(id);

                return View(categoria);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na exclusão de categoria! Detalhes do erro: {ex.Message}");
                return RedirectToAction("Index", "Categoria");
            }
            
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
                    return RedirectToAction("Index", "Categoria");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao adicionar a categoria! Detalhes do erro: {ex.Message}");
                    return RedirectToAction("Index", "Categoria");
                }
            }

            return View(categoria);
        }

        [HttpPost]
        public IActionResult Editar(Categoria categoria)
        {
            try
            {
                var categoriaDb = _categoriaRepositorio.BuscarPorId(categoria.Id);

                categoriaDb.Nome = categoria.Nome;

                _categoriaRepositorio.Atualizar(categoriaDb);

                return RedirectToAction("Index", "Categoria");
            }
            catch (Exception error)
            {
                Console.WriteLine($"Erro ao atualizar a categoria! Mais detalhes: {error.Message}");
                return RedirectToAction("Index", "Categoria");
            }
        }
    }
}