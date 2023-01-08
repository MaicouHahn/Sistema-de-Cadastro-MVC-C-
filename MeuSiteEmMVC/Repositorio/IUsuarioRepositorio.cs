using MeuSiteEmMVC.Models;

namespace MeuSiteEmMVC.Repositorio
{
    public interface IUsuarioRepositorio
    {
        UsuarioModel BuscarPorLogin(string login);
        UsuarioModel BuscarPorEmailELogin(string email,string login);
        UsuarioModel ListarPorId(int Id);
        List<UsuarioModel> BuscarTodos();
        UsuarioModel Adicionar(UsuarioModel usuario);

        UsuarioModel Atualizar(UsuarioModel usuario);

        UsuarioModel AlterarSenha(AlterarSenhaModel usuario);
        bool Apagar(int id);

    }
}
