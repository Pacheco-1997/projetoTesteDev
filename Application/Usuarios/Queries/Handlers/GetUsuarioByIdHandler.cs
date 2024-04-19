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
    public class GetUsuarioByIdHandler : IRequestHandler<GetUsuarioByIdRequest, Usuario>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUsuarioByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Usuario> Handle(GetUsuarioByIdRequest request, CancellationToken cancellationToken)
        {
            var usuario = await _unitOfWork.UsuarioRepositoty.GetUserById(request.Id);
            return usuario;
        }
    }
}
