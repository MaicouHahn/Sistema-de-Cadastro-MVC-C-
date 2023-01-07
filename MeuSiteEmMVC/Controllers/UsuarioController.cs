using MeuSiteEmMVC.Filters;
using MeuSiteEmMVC.Models;
using MeuSiteEmMVC.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace MeuSiteEmMVC.Controllers
{
    [PaginaRestritaSomenteAdin]

    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {

            _usuarioRepositorio = usuarioRepositorio;

        }
        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();

            return View(usuarios);
        }
        public IActionResult Criar()
        {

            return View();
        }

        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            return View(usuario);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            return View(usuario);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _usuarioRepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Usuario apagado com Sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = $"Ops, não foi possivel apagar o seu Usuario";
                }
                return RedirectToAction("Index");

            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possivel apagar o seu Usuario.Detalhes do erro : {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost] //cria no banco  o objeto usuario que for passado no parametro
        public IActionResult Criar(UsuarioModel usuario)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    usuario=_usuarioRepositorio.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "Usuario Cadastrado com Sucesso";
                    return RedirectToAction("Index");
                }

                return View(usuario);

            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possivel cadastrar o seu Usuario, Tente Novamente.Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public IActionResult Editar(UsuarioSemSenhaModel usuarioSemSenhaModel)
        {
            try
            {
                UsuarioModel usuario=null;

                if (ModelState.IsValid)
                {
                    usuario = new UsuarioModel()
                    {
                        Id= usuarioSemSenhaModel.Id,
                        Nome=usuarioSemSenhaModel.Nome,
                        Login=usuarioSemSenhaModel.Login,
                        Email=usuarioSemSenhaModel.Email,
                        Perfil=usuarioSemSenhaModel.Perfil

                    };
                    usuario = _usuarioRepositorio.Atualizar(usuario);
                    TempData["MensagemSucesso"] = "usuario alterado com Sucesso";
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possivel alterar o seu Usuario, Tente Novamente.Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

    }
}
