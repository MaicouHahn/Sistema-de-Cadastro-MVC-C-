using MeuSiteEmMVC.Helper;
using MeuSiteEmMVC.Models;
using MeuSiteEmMVC.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace MeuSiteEmMVC.Controllers
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

        public IActionResult Index()
        {
            //se o usuario estiver logado redirecionar para pagina Home
            if (_sessao.BuscarSessaoDoUsuario()!=null) return RedirectToAction("Index","Home");
     
            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoDoUsuario();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   UsuarioModel usuario= _usuarioRepositorio.BuscarPorLogin(loginmodel.Login);


                    if (usuario != null )
                    {
                        if (usuario.SenhaValida(loginmodel.Senha))
                        {
                            _sessao.CriarSessaoDoUsuario(usuario);
                            return RedirectToAction("Index","Home");
                        }
                        else
                        {
                            TempData["MensagemErro"] = $"Senha Invalida.Tente Novamente";
                        }
                    }
                    else
                    {
                        TempData["MensagemErro"] = $"Usuario e/ou Senha Invalido(s).Tente Novamente";
                    }

                }
                return View("Index");
            }
            catch (System.Exception erro) {

                TempData["MensagemErro"] = $"Não foi possivel efetuar Login. Detalhes do erro : {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
