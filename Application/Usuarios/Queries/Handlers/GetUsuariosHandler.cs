using Application.Usuarios.Queries.Requests;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usuarios.Queries.Handlers
{
    public class GetUsuariosHandler : IRequestHandler<GetUsuariosRequest, IEnumerable<Usuario>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUsuariosHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Usuario>> Handle(GetUsuariosRequest request, CancellationToken cancellationToken)
        {
            var usuarios = await _unitOfWork.UsuarioRepositoty.GetAllUsers();
            return usuarios;
        }
    }
}
