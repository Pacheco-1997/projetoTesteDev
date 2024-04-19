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
    public class DeleteUsuarioHandler : IRequestHandler<DeleteUsuarioRequest, Usuario>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUsuarioHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Usuario> Handle(DeleteUsuarioRequest request, CancellationToken cancellationToken)
        {
            var usuarioExistente = await _unitOfWork.UsuarioRepositoty.GetUserById(request.Id);

            if (usuarioExistente == null)
                throw new InvalidOperationException("Usuario não encontrado");

            await _unitOfWork.UsuarioRepositoty.DeleteUser(usuarioExistente.Id);
            await _unitOfWork.CommitAsync();

            return usuarioExistente;
        }
    }
}
