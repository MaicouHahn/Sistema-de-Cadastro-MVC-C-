using System.ComponentModel.DataAnnotations;

namespace MeuSiteEmMVC.Models
{
    public class AlterarSenhaModel
    {
        public int Id { get; set; }
        
        
        [Required(ErrorMessage ="Digite a Senha Atual do Usuario")]
        public string SenhaAtual { get; set; }
        
        
        [Required(ErrorMessage = "Digite a nova Senha do Usuario")]
        public string NovaSenha { get; set; }

        
        
        [Required(ErrorMessage = "Confirme a nova Senha")]
        [Compare("NovaSenha",ErrorMessage ="As senhas não são iguais")]
        public string ConfirmarNovaSenha { get; set; }



    }
}
