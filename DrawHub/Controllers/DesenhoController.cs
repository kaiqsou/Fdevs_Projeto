using DrawHub.Enums;
using DrawHub.Filters;
using DrawHub.Helpers;
using DrawHub.Models;
using DrawHub.Repositorio;
using DrawHub.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrawHub.Controllers
{
    public class DesenhoController : Controller
    {
        private readonly IDesenhoRepositorio _desenhoRepositorio;
        private readonly ICategoriaRepositorio _categoriaRepositorio;
        private readonly ISessao _sessao;
        public DesenhoController(IDesenhoRepositorio desenhoRepositorio, ICategoriaRepositorio categoriaRepositorio, ISessao sessao)
        {
            _desenhoRepositorio = desenhoRepositorio;
            _categoriaRepositorio = categoriaRepositorio;
            _sessao = sessao;
        }

        // Métodos [GET]
        public IActionResult Index()
        {
            Usuario userLogado = _sessao.BuscarSessao();
            List<Desenho> desenhos = _desenhoRepositorio.BuscarTodos();

            return View(desenhos);
        }

        [UserPage]
        public IActionResult Criar()
        {
            var viewModel = new DesenhoCategoriaViewModel
            {
                Desenho = new Desenho(),
                Categorias = _categoriaRepositorio.BuscarTodos()
            };

            Console.WriteLine($"Desenho: {viewModel}");

            return View(viewModel);
        }

        [UserPage]
        public IActionResult Editar(int id)
        {
            Usuario userLogado = _sessao.BuscarSessao();
            var desenho = _desenhoRepositorio.BuscarPorId(id);

            if (desenho.UsuarioId != userLogado.Id)
            {
                return RedirectToAction("Index", "Restricao");
            }

            var categorias = _categoriaRepositorio.BuscarTodos();

            if (desenho == null)
            {
                TempData["MsgErro"] = "Desenho não encontrado!";
                return RedirectToAction("MeusDesenhos", "Desenho");
            }

            var viewModel = new DesenhoCategoriaViewModel
            {
                Desenho = desenho,
                CategoriaId = desenho.CategoriaId,
                Categorias = categorias
            };

            return View(viewModel);
        }

        public IActionResult Detalhes(int id)
        {   
            Desenho desenho = _desenhoRepositorio.BuscarPorId(id);

            if (desenho == null) return NotFound();

            Categoria categoria = _categoriaRepositorio.BuscarPorId(desenho.CategoriaId);

            DetalhesDesenhoViewModel detalhesDesenho = new DetalhesDesenhoViewModel
            {
                Desenho = desenho,
                Categoria = categoria,
                Usuario = _sessao.BuscarSessao(),
            };

            return View(detalhesDesenho);
        }

        public IActionResult MeusDesenhos(int id)
        {
            Usuario userLogado = _sessao.BuscarSessao();

            if (_sessao.BuscarSessao() == null) return RedirectToAction("Cadastrar", "Usuario");

            List<Desenho> desenhosUser = _desenhoRepositorio.BuscarTodosPorUser(userLogado.Id);

            return View(desenhosUser);
        }

        [UserPage]
        public IActionResult ConfirmarExclusao(int id)
        {
            Usuario userLogado = _sessao.BuscarSessao();
            Desenho desenho = _desenhoRepositorio.BuscarPorId(id);

            if (desenho.UsuarioId != userLogado.Id)
            {
                return RedirectToAction("Index", "Restricao");
            }

            return View(desenho);
        }

        [UserPage]
        public IActionResult Excluir(int id)
        {
            try
            {
                bool apagado = _desenhoRepositorio.Excluir(id);

                return RedirectToAction("MeusDesenhos", "Desenho");
            }
            catch (Exception erro)
            {
                Console.WriteLine($"Erro na exclusão do desenho! Detalhes do erro: {erro.Message}");
                return RedirectToAction("MeusDesenhos", "Desenho");
            }
        }

        // Métodos [POST]
        [HttpPost]
        [UserPage]
        public IActionResult Criar(DesenhoCategoriaViewModel desenhoViewModel)
        {
            Console.WriteLine($"Desenho: {desenhoViewModel}");

            desenhoViewModel.Categorias = _categoriaRepositorio.BuscarTodos();
            var imagem = desenhoViewModel.Desenho.ArquivoImagem;

            if (imagem == null || imagem.Length == 0)
            {
                ModelState.AddModelError("Desenho.ArquivoImagem", "Por favor, selecione uma imagem.");
                return View(desenhoViewModel);
            }

            var extensoesValidas = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var extensao = Path.GetExtension(imagem.FileName).ToLower();

            if (!extensoesValidas.Contains(extensao))
            {
                ModelState.AddModelError("Desenho.ArquivoImagem", "Formato de arquivo inválido!");
                return View(desenhoViewModel);
            }

            if (desenhoViewModel.CategoriaId == 0)
            {
                ModelState.AddModelError("Desenho.CategoriaId", "Por favor, selecione uma categoria.");
                return View(desenhoViewModel);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var nomeImagem = $"{Guid.NewGuid()}{extensao}";
                    var pastaImg = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");

                    if (!Directory.Exists(pastaImg))
                    {
                        Directory.CreateDirectory(pastaImg);
                    }

                    var caminho = Path.Combine(pastaImg, nomeImagem);

                    using (var stream = new FileStream(caminho, FileMode.Create))
                    {
                        imagem.CopyTo(stream);
                    }

                    var desenho = new Desenho();
                    var categoria = _categoriaRepositorio.BuscarPorId(desenho.CategoriaId);

                    desenho = desenhoViewModel.Desenho;
                    desenho.ImagemCaminho = $"/img/{nomeImagem}";
                    desenho.UsuarioId = _sessao.BuscarSessao().Id;
                    desenho.CategoriaId = desenhoViewModel.CategoriaId;
                    desenho.Categoria = categoria;

                    _desenhoRepositorio.Adicionar(desenho);

                    return RedirectToAction("MeusDesenhos", "Desenho");
                }
                catch (Exception erro)
                {
                    Console.WriteLine($"Erro ao adicionar o desenho! Mais detalhes: {erro.Message}");
                }
            }

            return View(desenhoViewModel);
        }

        [HttpPost]
        [UserPage]
        public IActionResult Editar(DesenhoCategoriaViewModel desenhoViewModel)
        {
            try
            {
                var desenhoDb = _desenhoRepositorio.BuscarPorId(desenhoViewModel.Desenho.Id);
                var userLogado = _sessao.BuscarSessao();
                var imagem = desenhoViewModel.Desenho.ArquivoImagem;

                if (desenhoDb == null)
                {
                    Console.WriteLine("Desenho não encontrado!");
                    return RedirectToAction("MeusDesenhos", "Desenho");
                }

                if (desenhoDb.UsuarioId != userLogado.Id)
                {
                    Console.WriteLine("Você não tem permissão para editar este desenho!");
                    return RedirectToAction("MeusDesenhos", "Desenho");
                }

                if (imagem != null && imagem.Length > 0)
                {
                    var extensoesValidas = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                    var extensao = Path.GetExtension(imagem.FileName).ToLower();

                    if (!extensoesValidas.Contains(extensao))
                    {
                        TempData["MsgErro"] = "Formato de arquivo inválido!";
                        return RedirectToAction("Editar", "Desenho");
                    }

                    var nomeImagem = $"{Guid.NewGuid()}{extensao}";
                    var caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", nomeImagem);

                    using (var stream = new FileStream(caminho, FileMode.Create))
                    {
                        imagem.CopyTo(stream);
                    }

                    if (!string.IsNullOrEmpty(desenhoDb.ImagemCaminho))
                    {
                        var caminhoImagemAntiga = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", desenhoDb.ImagemCaminho.TrimStart('/'));
                        if (System.IO.File.Exists(caminhoImagemAntiga))
                        {
                            System.IO.File.Delete(caminhoImagemAntiga);
                        }
                    }

                    desenhoDb.ImagemCaminho = $"/img/{nomeImagem}";
                }

                desenhoDb.Titulo = desenhoViewModel.Desenho.Titulo;
                desenhoDb.Descricao = desenhoViewModel.Desenho.Descricao;
                desenhoDb.Privacidade = desenhoViewModel.Desenho.Privacidade;
                desenhoDb.CategoriaId = desenhoViewModel.CategoriaId;
                desenhoDb.DataAtualizacao = DateTime.Now;

                _desenhoRepositorio.Atualizar(desenhoDb);

                return RedirectToAction("MeusDesenhos", "Desenho");
            }
            catch (Exception error)
            {
                Console.WriteLine($"Erro ao atualizar o desenho! Mais detalhes: {error.Message}");
                return RedirectToAction("MeusDesenhos", "Desenho");
            }
        }
    }
}
