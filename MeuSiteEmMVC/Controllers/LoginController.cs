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
        private readonly IEmail _email;
        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IEmail email)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email = email;
        }

        public IActionResult Index()
        {
            //se o usuario estiver logado redirecionar para pagina Home
            if (_sessao.BuscarSessaoDoUsuario()!=null) return RedirectToAction("Index","Home");
     
            return View();
        }

        public IActionResult RedefinirSenha()
        {
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

        [HttpPost] 
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailELogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);


                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();
                        string mensagem = $"Sua nova Senha é : {novaSenha}";                  
                       bool emailEnviado = _email.Enviar(usuario.Email,"Sistema de Contatos-Nova Senha",mensagem);
                        if (emailEnviado)
                        {
                            _usuarioRepositorio.Atualizar(usuario);
                            TempData["MensagemSucesso"] = $"Enviamos uma nova Senha para seu E-Mail cadastrado";
                        }
                        else
                        {
                            TempData["MensagemErro"] = $"Não foi possivel Enviar o e-mail. Verifique os dados informados";
                        }
                    
                        return RedirectToAction("Index","Login");
                    }
                    else
                    {
                        TempData["MensagemErro"] = $"Não foi possivel redefinir a senha. Verifique os dados informados";
                    }

                }
                return View("Index");
            }
            catch (System.Exception erro)
            {

                TempData["MensagemErro"] = $"Não foi possivel redefinir a Senha. Detalhes do erro : {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
