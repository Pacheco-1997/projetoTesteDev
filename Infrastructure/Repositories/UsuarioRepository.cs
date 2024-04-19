using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepositoty
    {
        protected readonly AppDbContext _db;

        public UsuarioRepository(AppDbContext db)
        {
            this._db = db;
        }

        public async Task AddUser(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            await _db.Usuarios.AddAsync(usuario);
        }

        public async Task DeleteUser(int id)
        {
            var usuario = await GetUserById(id);

            if (usuario == null)
                throw new InvalidOperationException("Usuario Não encontrado");

            _db.Usuarios.Remove(usuario);
        }

        public async Task<IEnumerable<Usuario>> GetAllUsers()
        {
            var listaDeUsuarios = await _db.Usuarios.ToListAsync();
            return listaDeUsuarios ?? Enumerable.Empty<Usuario>();
        }

        public async Task<Usuario> GetUserById(int id)
        {
            var usuario = await _db.Usuarios.FindAsync(id);

            if (usuario == null)
                throw new InvalidOperationException("Usuario Não encontrado");

            return usuario;
        }

        public void UpdateUser(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            _db.Usuarios.Update(usuario);
        }
    }
}
