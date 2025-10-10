﻿using DrawHub.Data;
using DrawHub.Enums;
using DrawHub.Models;
using Microsoft.EntityFrameworkCore;

namespace DrawHub.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _context;
        public UsuarioRepositorio(BancoContext bancoContext)
        {
            _context = bancoContext;
        }

        public Usuario Adicionar(Usuario usuario)
        {
            usuario.DataCadastro = DateTime.Now;
            usuario.Tipo = RoleEnum.Padrão;

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return usuario;
        }

        public Usuario Atualizar(Usuario usuario)
        {
            Usuario usuarioDb = BuscarPorId(usuario.Id);

            if (usuarioDb == null) throw new Exception("Houve um erro na atualização do usuário!");

            usuarioDb.Nome = usuario.Nome;
            usuarioDb.Email = usuario.Email;
            usuarioDb.Telefone = usuario.Telefone;
            usuarioDb.Tipo = usuario.Tipo;
            usuarioDb.DataAtualizacao = DateTime.Now;

            _context.Usuarios.Update(usuarioDb);
            _context.SaveChanges();

            return usuarioDb;
        }

        public Usuario BuscarPorEmail(string email)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper());
        }

        public Usuario BuscarPorId(int id)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public List<Usuario> BuscarTodos()
        {
            return _context.Usuarios.Include(x => x.Desenhos).ToList();
        }

        public bool Excluir(int id)
        {
            Usuario usuario = BuscarPorId(id);

            if (usuario == null) throw new Exception("Houve um erro na exclusão do usuário!");

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();

            return true;
        }
    }
}
