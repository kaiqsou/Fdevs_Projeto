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
            return _context.Categorias.FirstOrDefault(c => c.Id == id);
        }

        public List<Categoria> BuscarTodos()
        {
            return _context.Categorias.ToList();
        }

        public int ContarDesenhos(int categoriaId)
        {
            return _context.Desenhos.Count(d => d.CategoriaId == categoriaId);
        }

        public bool Excluir(int id)
        {
            Categoria categoria = BuscarPorId(id);

            if (categoria == null) throw new Exception("Houve um erro ao excluir a categoria!");

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return true;
        }
    }
}
