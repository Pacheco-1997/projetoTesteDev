using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usuarios.Queries.Requests
{
    public class GetUsuariosRequest : IRequest<IEnumerable<Usuario>>
    {

    }
}
