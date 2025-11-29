using DrawHub.Filters;
using DrawHub.Models;
using DrawHub.Repositorio;
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

            return View(categorias);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            Console.WriteLine($"[GET] TENTANDO ENCONTRAR ID: {id}");
            var categoria = _categoriaRepositorio.BuscarPorId(id);

            if (categoria == null)
            {
                Console.WriteLine("NAO ENCONTRADO!");
                TempData["MsgErro"] = "Categoria não encontrada!";
                return RedirectToAction("Index", "Categoria");
            }

            return View(categoria);
        }

        public IActionResult Excluir(int id)
        {
            try
            {
                bool apagado = _categoriaRepositorio.Excluir(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Categoria excluída com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Erro na exclusão da categoria!";
                }

                return RedirectToAction("Index", "Categoria");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro na exclusão do desenho! Detalhes do erro: {erro.Message}";
                return RedirectToAction("Index", "Categoria");
            }
        }

        public IActionResult ConfirmarExclusao(int id)
        {
            Categoria categoria = _categoriaRepositorio.BuscarPorId(id);

            return View(categoria);
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
                catch (Exception erro)
                {
                    TempData["MsgErro"] = $"Erro ao adicionar a categoria! Mais detalhes: {erro.Message}";
                }
            }

            return View(categoria);
        }

        [HttpPost]
        public IActionResult Editar(Categoria categoria)
        {
            Console.WriteLine("Entrei no editar");
            try
            {
                Console.WriteLine("Entrei no TRY do editar");
                var categoriaDb = _categoriaRepositorio.BuscarPorId(categoria.Id);

                categoriaDb.Nome = categoria.Nome;
                categoriaDb.DataAtualizacao = DateTime.Now;

                _categoriaRepositorio.Atualizar(categoriaDb);

                TempData["MsgSucesso"] = "Categoria atualizada com sucesso!";
                return RedirectToAction("Index", "Categoria");
            }
            catch (Exception error)
            {
                TempData["MsgErro"] = $"Erro ao atualizar o desenho! Mais detalhes: {error.Message}";
                return RedirectToAction("Index", "Categoria");
            }
        }
    }
}