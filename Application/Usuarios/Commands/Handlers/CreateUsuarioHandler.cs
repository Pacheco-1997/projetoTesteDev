using Application.Usuarios.Commands.Requests;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usuarios.Commands.Handlers
{
    public class CreateUsuarioHandler : IRequestHandler<CreateUsuarioRequest, Usuario>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateUsuarioHandler(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Usuario> Handle(CreateUsuarioRequest request, CancellationToken cancellationToken)
        {
            Usuario usuario = new Usuario(request.Nome, request.Email);

            await _unitOfWork.UsuarioRepositoty.AddUser(usuario);
            await _unitOfWork.CommitAsync();

            return usuario;
        }
    }
}
