using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usuarios.Commands.Requests
{
    public class CreateUsuarioRequest : IRequest<Usuario>
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
    }
}
