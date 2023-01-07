﻿using MeuSiteEmMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MeuSiteEmMVC.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessaoUsuario = HttpContext.Session.GetString("SessaoUsuarioLogado");
            if (string.IsNullOrEmpty(sessaoUsuario)) return null;
          
            UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
            
            return View(usuario);
        }

    }
}
