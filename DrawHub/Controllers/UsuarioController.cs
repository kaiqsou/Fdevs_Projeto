using DrawHub.Models;
using DrawHub.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace DrawHub.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IDesenhoRepositorio _desenhoRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, IDesenhoRepositorio desenhoRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _desenhoRepositorio = desenhoRepositorio;
        }

        // Métodos [GET]
        public IActionResult Index()
        {
            List<Usuario> usuarios = _usuarioRepositorio.BuscarTodos();

            return View(usuarios);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            Usuario usuario = _usuarioRepositorio.BuscarPorId(id);

            return View(usuario);
        }

        public IActionResult ConfirmarExclusao(int id)
        {
            Usuario usuario = _usuarioRepositorio.BuscarPorId(id);

            return View(usuario);
        }

        public IActionResult ExibirDesenhos(int id)
        {
            List<Desenho> desenhos = _desenhoRepositorio.BuscarTodos();

            // Adicionar PartialViews depois aqui
            return View(desenhos);
        }

        // Métodos [POST]
        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.Adicionar(usuario);

                    TempData["MsgSucesso"] = "Usuário cadastrado com sucesso!";

                    return RedirectToAction("Cadastrar");
                }

                return View(usuario);
            }
            catch (Exception erro)
            {
                TempData["MsgErro"] = $"Não foi possível cadastrar o usuário! Detalhe do erro: {erro.Message}";

                return RedirectToAction("Cadastrar");
            }
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