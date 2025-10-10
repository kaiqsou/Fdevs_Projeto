using DrawHub.Helpers;
using DrawHub.Models;
using DrawHub.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace DrawHub.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }

        // Métodos [GET]
        public IActionResult Index()
        {
            if (_sessao.BuscarSessao() != null) return RedirectToAction("Index", "Home");

            return View();
        }

        public IActionResult Logout()
        {
            _sessao.EncerrarSessao();

            return RedirectToAction("Index", "Login");
        }

        // Métodos [POST]
        public IActionResult Entrar(LoginModel login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario usuario = _usuarioRepositorio.BuscarPorEmail(login.Email);

                    if (usuario != null && usuario.SenhaValida(login.Senha))
                    {
                        _sessao.CriarSessao(usuario);
                        ViewData["MsgSucesso"] = "Logado com sucesso!";

                        return RedirectToAction("Index", "Home");
                    }

                    ViewData["MsgErro"] = "Usuário e/ou senha inválido(s). Por favor, tente novamente";
                }
                return View("Index");
            }
            catch (Exception erro)
            {
                ViewData["MsgErro"] = $"Erro ao entrar! Detalhe do erro: {erro.Message}";

                return RedirectToAction("Index");
            }
        }
    }
}
