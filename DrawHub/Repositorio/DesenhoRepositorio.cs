using DrawHub.Data;
using DrawHub.Models;
using Microsoft.EntityFrameworkCore;

namespace DrawHub.Repositorio
{
    public class DesenhoRepositorio : IDesenhoRepositorio
    {
        private readonly BancoContext _context;
        public DesenhoRepositorio(BancoContext bancoContext)
        {
            _context = bancoContext;
        }

        public Desenho Adicionar(Desenho desenho)
        {
            desenho.DataEnvio = DateTime.Now;

            _context.Desenhos.Add(desenho);
            _context.SaveChanges();

            return desenho;
        }

        public Desenho Atualizar(Desenho desenho)
        {
            Console.WriteLine($"[REPOSITORIO] Atualizando desenho ID: {desenho.Id}");

            Desenho desenhoDb = BuscarPorId(desenho.Id);

            if (desenhoDb == null) throw new Exception("[NULL] Houve um erro ao atualizar o desenho!");

            desenhoDb.Titulo = desenho.Titulo;
            desenhoDb.Descricao = desenho.Descricao;
            desenhoDb.Privacidade = desenho.Privacidade;
            desenhoDb.Categoria = desenho.Categoria;
            desenhoDb.DataAtualizacao = DateTime.Now;

            _context.Desenhos.Update(desenhoDb);
            _context.SaveChanges();

            return desenhoDb;
        }

        public Desenho BuscarPorId(int id)
        {
            return _context.Desenhos.Include(u => u.Usuario).Include(c => c.Categoria).FirstOrDefault(d => d.Id == id);
        }

        public List<Desenho> BuscarTodos()
        {
            return _context.Desenhos.Include(u => u.Usuario).Include(c => c.Categoria).Where(p => p.Privacidade != true).ToList();
        }

        public List<Desenho> BuscarTodosPorUser(int userId)
        {
            return _context.Desenhos.Include(d => d.Categoria).Where(u => u.UsuarioId == userId).ToList();
        }

        /*
        public List<Desenho> BuscarTodosPorCategoria(int categoriaId)
        {
            return _context.Desenhos.Include(c => c.Categorias).Where(d => d.Categorias.Any(c => c.Id == categoriaId)).ToList();
        }
        */

        public bool Excluir(int id)
        {
            Desenho desenho = BuscarPorId(id);

            if (desenho == null) throw new Exception("Houve um erro ao excluir o desenho!");

            _context.Desenhos.Remove(desenho);
            _context.SaveChanges();

            return true;
        }
    }
}
