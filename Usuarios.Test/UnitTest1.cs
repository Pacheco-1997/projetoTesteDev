using Application.Usuarios.Commands.Requests;
using Application.Usuarios.Queries.Requests;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TesteDevApi.Controllers;

namespace Usuarios.Test
{
    public class ApiUsuariosControllerTests
    {
        [Fact]
        public async Task CreateUsuario_WithValidRequest_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var usuarioCriado = new Usuario("Usuário Teste","teste@teste.com"); // Simule o usuário criado
            mediatorMock.Setup(m => m.Send(It.IsAny<CreateUsuarioRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(usuarioCriado);

            var controller = new UsuariosController(mediatorMock.Object);

            // Act
            var result = await controller.CreateUsuario(new CreateUsuarioRequest());

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetUsuario", createdAtActionResult.ActionName);
            Assert.Equal(usuarioCriado.Id, createdAtActionResult.RouteValues["id"]);
            Assert.Equal(usuarioCriado, createdAtActionResult.Value);
        }

        [Fact]
        public async Task UpdateUsuario_WithValidRequest_ReturnsOkObjectResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var usuarioAtualizado = new Usuario(1, "Usuário Teste Atualizado", "teste@teste.com"); // Simule o usuário atualizado
            mediatorMock.Setup(m => m.Send(It.IsAny<UpdateUsuarioRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(usuarioAtualizado);

            var controller = new UsuariosController(mediatorMock.Object);

            // Act
            var result = await controller.UpdateUsuario(new UpdateUsuarioRequest());

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(usuarioAtualizado, okObjectResult.Value);
        }

        [Fact]
        public async Task DeleteUsuario_WithValidRequest_ReturnsNoContentResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();

            var controller = new UsuariosController(mediatorMock.Object);

            // Act
            var result = await controller.DeleteUsuario(new DeleteUsuarioRequest());

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PegaTodosOsUsuarios_RetornaOkObjectResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var usuarios = new List<Usuario>(); // Simule uma lista de usuários
            mediatorMock.Setup(m => m.Send(It.IsAny<GetUsuariosRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(usuarios);

            var controller = new UsuariosController(mediatorMock.Object);

            // Act
            var result = await controller.GetAllUsuarios();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(usuarios, okResult.Value);
        }

        [Fact]
        public async Task PegaUsuarioPorIdRetornaOkComUsuario()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var usuario = new Usuario(1,"Usuário Teste", "teste@teste.com"); // Réplica de um usuário real do banco de dados
            mediatorMock.Setup(m => m.Send(It.IsAny<GetUsuarioByIdRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(usuario);

            var controller = new UsuariosController(mediatorMock.Object);

            // Act
            var result = await controller.GetUsuario(2); // ID válido de um usuário existente

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var retornoUsuario = Assert.IsType<Usuario>(okObjectResult.Value);
            Assert.Equal(usuario.Id, retornoUsuario.Id);
            Assert.Equal(usuario.Name, retornoUsuario.Name);
            Assert.Equal(usuario.Email, retornoUsuario.Email);
            // Verifique outras propriedades conforme necessário
        }

        [Fact]
        public async Task PegaUsuario_ComUsuarioNaoExistente_RetornaObejetoNaoEncontradoResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<GetUsuarioByIdRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync((Usuario)null); // Simule o retorno nulo

            var controller = new UsuariosController(mediatorMock.Object);

            // Act
            var result = await controller.GetUsuario(800); // ID que não existe

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

    }
}