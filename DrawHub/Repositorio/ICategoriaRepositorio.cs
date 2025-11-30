using DrawHub.Models;

namespace DrawHub.Repositorio
{
    public interface ICategoriaRepositorio
    {
        // Adiciona uma nova categoria no banco de dados
        Categoria Adicionar(Categoria categoria);

        // Atualiza todos os dados de uma categoria
        Categoria Atualizar(Categoria categoria);

        // Busca todas as categorias do banco de dados
        List<Categoria> BuscarTodos();

        // Busca uma única categoria por Id no banco de dados
        Categoria BuscarPorId(int id);

        // Conta quantos desenhos estão atribuídos em uma categoria
        public int ContarDesenhos(int categoria);

        // Exclui uma categoria
        bool Excluir(int id);
    }
}
