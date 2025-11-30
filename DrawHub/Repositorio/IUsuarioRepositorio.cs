using DrawHub.Models;

namespace DrawHub.Repositorio
{
    public interface IUsuarioRepositorio
    {
        // Adiciona um novo usuário no banco de dados
        Usuario Adicionar(Usuario usuario);

        // Atualiza todos os dados de um usuário
        Usuario Atualizar(Usuario usuario);

        // Busca todos os usuários do banco de dados
        List<Usuario> BuscarTodos();

        // Busca um único usuário do banco no banco de dados
        Usuario BuscarPorId(Guid id);

        // Busca um usuário pelo e-mail de Login
        Usuario BuscarPorEmail(string email);

        // Exclui um usuário
        bool Excluir(Guid id);
    }
}
