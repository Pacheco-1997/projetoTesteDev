using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public interface IUsuarioRepositoty
    {
        Task<IEnumerable<Usuario>> GetAllUsers();
        Task<Usuario> GetUserById(int id);
        Task AddUser(Usuario usuario);
        void UpdateUser(Usuario usuario);
        Task DeleteUser(int id);

    }
}
