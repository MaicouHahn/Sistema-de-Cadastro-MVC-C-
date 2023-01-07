using MeuSiteEmMVC.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MeuSiteEmMVC.Helper
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _httpContext; 
        
        
        public Sessao(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext= httpContextAccessor;
        }
        
        public UsuarioModel BuscarSessaoDoUsuario()
        {
            string sessaoUsuario = _httpContext.HttpContext.Session.GetString("SessaoUsuarioLogado");
            if (string.IsNullOrEmpty(sessaoUsuario)) return null;

            return JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);//aqui ele faz o inverso, pega de uma string JSON e converte novamente
            //em um objeto da classe UsuarioModel
        }

        
        public void CriarSessaoDoUsuario(UsuarioModel usuario)
        {
            string valor = JsonConvert.SerializeObject(usuario);//transforma o objeto em JSON que é uma string que será passada como valor no metodo abaixo
            _httpContext.HttpContext.Session.SetString("SessaoUsuarioLogado",valor);
        }

        public void RemoverSessaoDoUsuario()
        {
            _httpContext.HttpContext.Session.Remove("SessaoUsuarioLogado");//aqui foi passado uma chave
        }
    }
}
