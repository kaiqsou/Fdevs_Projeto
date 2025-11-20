using DrawHub.Data;
using DrawHub.Models;
using Microsoft.EntityFrameworkCore;

namespace DrawHub.Repositorio
{
    public class CategoriaRepositorio : ICategoriaRepositorio
    {
        private readonly BancoContext _context;
        public CategoriaRepositorio(BancoContext bancoContext)
        {
            _context = bancoContext;
        }

        public Categoria Adicionar(Categoria categoria)
        {
            categoria.DataCriacao = DateTime.Now;

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return categoria;
        }

        public Categoria Atualizar(Categoria categoria)
        {
            Categoria categoriaDb = BuscarPorId(categoria.Id);

            if (categoriaDb == null) throw new Exception("Houve um erro ao atualizar a categoria!");

            categoriaDb.Nome = categoria.Nome;

            _context.Categorias.Update(categoriaDb);
            _context.SaveChanges();

            return categoriaDb;
        }

        public Categoria BuscarPorId(int id)
        {
            return _context.Categorias.Include(u => u.Desenhos).FirstOrDefault(x => x.Id == id);
        }

        public List<Categoria> BuscarTodos()
        {
            return _context.Categorias.ToList();
        }

        public bool Excluir(int id)
        {
            throw new NotImplementedException();
        }
    }
}
