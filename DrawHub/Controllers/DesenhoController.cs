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
            return View();
        }

        public IActionResult Detalhes(int id)
        {
            return View();
        }

        public IActionResult ConfirmarExclusao(int id)
        {
            return View();
        }

        // Métodos [POST]
        [HttpPost]
        public IActionResult Criar(Desenho desenho)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Usuario usuario = new Usuario();
                    desenho.Usuario = usuario;

                    _desenhoRepositorio.Adicionar(desenho);
                    TempData["MsgSucesso"] = "Desenho adicionado com sucesso!";

                    return RedirectToAction("Index");
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
