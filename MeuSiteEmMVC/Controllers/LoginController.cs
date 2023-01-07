using MeuSiteEmMVC.Models;
using MeuSiteEmMVC.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace MeuSiteEmMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public LoginController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            return View();
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
