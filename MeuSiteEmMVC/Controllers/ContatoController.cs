using MeuSiteEmMVC.Models;
using MeuSiteEmMVC.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace MeuSiteEmMVC.Controllers
{
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _ContatoRepositorio;

        public ContatoController(IContatoRepositorio contatoRepositorio) {

            _ContatoRepositorio= contatoRepositorio;
        }
        //esses metodos basicos sao metodos Get apenas para consulta
        public IActionResult Index()
        {
            List<ContatoModel> contatos=_ContatoRepositorio.BuscarTodos();

            return View(contatos);
        }
        public IActionResult Criar()
        {

            return View();
        }
        public IActionResult Editar(int Id)//recebe por parametro o ID 
        {
            ContatoModel contato = _ContatoRepositorio.ListarPorId(Id);
            return View(contato);
        }


        public IActionResult ApagarConfirmacao(int id)
        {
            ContatoModel contato = _ContatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado=_ContatoRepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Contato apagado com Sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = $"Ops, não foi possivel apagar o seu contato";
                }
                return RedirectToAction("Index");

            }catch(System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possivel apagar o seu contato.Detalhes do erro : {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {

            try
            {
                if (ModelState.IsValid)
                {
                   contato= _ContatoRepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Contato Cadastrado com Sucesso";
                    return RedirectToAction("Index");
                }

                return View(contato);

            }
            catch(System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possivel cadastrar o seu contato, Tente Novamente.Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
          
        }

        [HttpPost] 
        public IActionResult Editar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    contato=_ContatoRepositorio.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Contato alterado com Sucesso";
                    return RedirectToAction("Index");
                }
                return View(contato);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possivel alterar o seu contato, Tente Novamente.Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
