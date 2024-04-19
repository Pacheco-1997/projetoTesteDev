using Application.Usuarios.Commands.Requests;
using Application.Usuarios.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TesteDevApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsuariosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsuario(CreateUsuarioRequest request)
        {
            var usuario = await _mediator.Send(request);

            if (usuario != null)
            {
                return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
            }
            else
            {
                return BadRequest("Erro ao criar o usuário.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUsuario(UpdateUsuarioRequest request)
        {
            var usuario = await _mediator.Send(request);

            return Ok(usuario);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUsuario(DeleteUsuarioRequest request)
        {
            await _mediator.Send(request);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsuarios()
        {
            var query = new GetUsuariosRequest();
            var usuarios = await _mediator.Send(query);

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            var query = new GetUsuarioByIdRequest { Id = id };
            var usuario = await _mediator.Send(query);

            return usuario != null ? Ok(usuario) : NotFound("Usuario não encontrado");
        }
    }
}
