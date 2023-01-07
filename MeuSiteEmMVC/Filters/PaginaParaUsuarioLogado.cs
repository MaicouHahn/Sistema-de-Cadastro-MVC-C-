using MeuSiteEmMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Routing;

namespace MeuSiteEmMVC.Filters
{
    public class PaginaParaUsuarioLogado : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)//executa esse codigo antes de interagir com os controllers
        {

            string sessaoUsuario = context.HttpContext.Session.GetString("SessaoUsuarioLogado");

            if(string.IsNullOrEmpty(sessaoUsuario))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary{ {"controller", "Login" }, {"action", "Index" } });
            }
            else
            {
                UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
                if(usuario == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
