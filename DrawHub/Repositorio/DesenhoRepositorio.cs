using DrawHub.Data;
using DrawHub.Models;

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
            Desenho desenhoDb = BuscarPorId(desenho.Id);

            if (desenhoDb == null) throw new Exception("Houve um erro ao atualizar o desenho!");

            desenhoDb.Titulo = desenho.Titulo;
            desenhoDb.Descricao = desenho.Descricao;
            desenhoDb.Imagem = desenho.Imagem;
            desenhoDb.Privacidade = desenho.Privacidade;
            desenhoDb.Categorias = desenho.Categorias;
            desenho.DataAtualizacao = DateTime.Now;

            _context.Desenhos.Update(desenhoDb);
            return desenhoDb;
        }

        public Desenho BuscarPorId(int id)
        {
            return _context.Desenhos.FirstOrDefault(x => x.Id == id);
        }

        public List<Desenho> BuscarTodos()
        {
            return _context.Desenhos.Where(x => x.Privacidade != true).ToList();
        }

        public List<Desenho> BuscarTodosPorUser(int userId)
        {
            return _context.Desenhos.Where(x => x.UsuarioId == userId).ToList();
        }

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
