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
    public class UpdateUsuarioHandler : IRequestHandler<UpdateUsuarioRequest, Usuario>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUsuarioHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Usuario> Handle(UpdateUsuarioRequest request, CancellationToken cancellationToken)
        {
            var usuarioExistente = await _unitOfWork.UsuarioRepositoty.GetUserById(request.Id);

            if (usuarioExistente == null)
                throw new InvalidOperationException("Usuario não encontrado");

            usuarioExistente.Update(request.Nome, request.Email);

            _unitOfWork.UsuarioRepositoty.UpdateUser(usuarioExistente);
            await _unitOfWork.CommitAsync();

            return usuarioExistente;
        }
    }
}
