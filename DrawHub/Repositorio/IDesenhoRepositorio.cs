using DrawHub.Models;

namespace DrawHub.Repositorio
{
    public interface IDesenhoRepositorio
    {
        // Adiciona um novo desenho
        Desenho Adicionar(Desenho desenho);

        // Atualiza um desenho
        Desenho Atualizar(Desenho desenho);

        // Busca um desenho específico pelo Id
        Desenho BuscarPorId(int id);

        // Busca todos os desenhos do banco de dados, desde que a privacidade não seja 'true'
        List<Desenho> BuscarTodos();

        // Busca todos os desenhos de um usuário específico
        List<Desenho> BuscarTodosPorUser(Guid id);

        // Exclui um desenho
        bool Excluir(int id);
    }
}
