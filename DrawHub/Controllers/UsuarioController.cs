using DrawHub.Filters;
using DrawHub.Helpers;
using DrawHub.Models;
using DrawHub.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace DrawHub.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }

        // Métodos [GET]
        public IActionResult Index()
        {
            List<Usuario> usuarios = _usuarioRepositorio.BuscarTodos();

            return View(usuarios);
        }

        public IActionResult Cadastrar()
        {
            if (_sessao.BuscarSessao() != null) return RedirectToAction("MeusDesenhos", "Desenho");

            return View();
        }

        public IActionResult Editar(Guid id)
        {
            Usuario usuario = _usuarioRepositorio.BuscarPorId(id);

            return View(usuario);
        }

        // Métodos [POST]
        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _usuarioRepositorio.BuscarPorEmail(usuario.Email);

                    if (user != null)
                    {
                        TempData["MsgErro"] = "Usuário já cadastrado!";
                        return View();
                    }

                    usuario.SetSenhaHash();
                    _usuarioRepositorio.Adicionar(usuario);

                    TempData["MsgSucesso"] = "Usuário cadastrado com sucesso!";
                    return View();
                }

                return View(usuario);
            }
            catch (Exception erro)
            {
                Console.WriteLine($"Não foi possível cadastrar o usuário! Detalhe do erro: {erro.Message}");
                return View();
            }
        }
    }
}