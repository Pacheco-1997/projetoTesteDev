﻿using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usuarios.Commands.Requests
{
    public class DeleteUsuarioRequest : IRequest<Usuario>
    {
        public int Id { get; set; }
    }
}
