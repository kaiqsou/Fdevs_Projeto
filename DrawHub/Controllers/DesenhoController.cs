using DrawHub.Helpers;
using DrawHub.Models;
using DrawHub.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrawHub.Controllers
{
    public class DesenhoController : Controller
    {
        private readonly IDesenhoRepositorio _desenhoRepositorio;
        private readonly ISessao _sessao;
        public DesenhoController(IDesenhoRepositorio desenhoRepositorio, ISessao sessao)
        {
            _desenhoRepositorio = desenhoRepositorio;
            _sessao = sessao;
        }

        // Métodos [GET]
        public IActionResult Index()
        {
            Usuario userLogado = _sessao.BuscarSessao();
            List<Desenho> desenhos = _desenhoRepositorio.BuscarTodos();

            return View(desenhos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            Usuario userLogado = _sessao.BuscarSessao();
            Desenho desenho = _desenhoRepositorio.BuscarPorId(id);

            return View(desenho);
        }

        public IActionResult Detalhes(int id)
        {
            return View();
        }

        public IActionResult MeusDesenhos(int id)
        {
            Usuario userLogado = _sessao.BuscarSessao();
            List<Desenho> desenhosUser = _desenhoRepositorio.BuscarTodosPorUser(userLogado.Id);

            return View(desenhosUser);
        }

        public IActionResult ConfirmarExclusao(int id)
        {
            return View();
        }

        // Métodos [POST]
        [HttpPost]
        public IActionResult Criar(Desenho desenho)
        {
            if (desenho.ArquivoImagem == null || desenho.ArquivoImagem.Length == 0)
            {
                ModelState.AddModelError("ArquivoImagem", "Por favor, selecione uma imagem.");
                return View(desenho);
            }

            var extensoesValidas = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extensao = Path.GetExtension(desenho.ArquivoImagem.FileName).ToLower();

            if (!extensoesValidas.Contains(extensao))
            {
                ModelState.AddModelError("ArquivoImagem", "Formato de arquivo inválido!");
                return View(desenho);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var nomeImagem = $"{Guid.NewGuid()}{extensao}";
                    var caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", nomeImagem);

                    using (var stream = new FileStream(caminho, FileMode.Create))
                    {
                        desenho.ArquivoImagem.CopyTo(stream);
                    }

                    desenho.ImagemCaminho = $"/img/{nomeImagem}";
                    desenho.UsuarioId = _sessao.BuscarSessao().Id;
                    desenho.DataEnvio = DateTime.Now;

                    _desenhoRepositorio.Adicionar(desenho);

                    TempData["MsgSucesso"] = "Desenho adicionado com sucesso!";

                    return RedirectToAction("MeusDesenhos");
                }
                catch (Exception erro)
                {
                    TempData["MsgErro"] = $"Erro ao adicionar o desenho! Mais detalhes: {erro.Message}";
                }
            }

            return View(desenho);
        }

        [HttpPost]
        public IActionResult Editar(Desenho desenho)
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
