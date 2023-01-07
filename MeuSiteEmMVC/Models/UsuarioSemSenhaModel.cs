using MeuSiteEmMVC.Enums;
using System.ComponentModel.DataAnnotations;

namespace MeuSiteEmMVC.Models
{
    public class UsuarioSemSenhaModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome do Usuario")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite o e-mail do Usuario")]
        [EmailAddress(ErrorMessage = "O email informado não é valido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Digite o Login do Usuario")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Digite o Perfil do Usuario")]
        public PerfilEnum? Perfil { get; set; }

    }
}
