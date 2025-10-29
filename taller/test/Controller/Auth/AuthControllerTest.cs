using Business.Interfaces.IBusinessImplements.Auth;
using Entity.Domain.Config;
using Entity.DTOs.Auth;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ModelSecurity.Controllers.Implements.Auth;
using ModelSecurity.Infrastructure.Cookies.Implements;
using Moq;

namespace test.Controller.Auth
{
    public class AuthControllerTest
    {
        private readonly Mock<ILogger<AuthController>> _mockLogger;
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly AuthController _controller;

        public AuthControllerTest()
        {
            _mockLogger = new Mock<ILogger<AuthController>>();
            _mockAuthService = new Mock<IAuthService>();

            // Se crean valores vacíos solo para cumplir el constructor, no afectan el test
            var dummyToken = new Mock<IToken>();
            var dummyCookieFactory = new Mock<IAuthCookieFactory>();
            var jwtOptions = Options.Create(new JwtSettings());
            var cookieOptions = Options.Create(new CookieSettings());

            _controller = new AuthController(
                _mockLogger.Object,
                dummyToken.Object,
                _mockAuthService.Object,
                jwtOptions,
                cookieOptions,
                dummyCookieFactory.Object
            );
        }

        [Fact]
        public async Task Register_DeberiaRetornar200_SiElUsuarioSeCreaCorrectamente()
        {
            // Arrange
            var dto = new RegisterUserDto
            {
                Email = "usuario@test.com",
                Password = "ClaveSegura123*",
                Name = "Usuario Prueba"
            };

            _mockAuthService
                .Setup(s => s.RegisterAsync(dto))
                .ReturnsAsync(true); // simula que se creó correctamente

            // Act
            var result = await _controller.Registrarse(dto);

            // Assert
            var objectResult = result as ObjectResult;
            objectResult.Should().NotBeNull();
            objectResult!.StatusCode.Should().Be(StatusCodes.Status200OK);
            objectResult.Value.Should().BeEquivalentTo(new { isSuccess = true });

            _mockAuthService.Verify(s => s.RegisterAsync(dto), Times.Once);
        }

        [Fact]
        public async Task Register_DeberiaRetornar400_SiElServicioLanzaExcepcion()
        {
            // Arrange
            var dto = new RegisterUserDto
            {
                Email = "repetido@test.com",
                Password = "Clave123*",
                FirstName = "Duplicado"
            };

            _mockAuthService
                .Setup(s => s.RegisterAsync(dto))
                .ThrowsAsync(new Exception("El correo ya existe"));

            // Act
            var result = await _controller.Registrarse(dto);

            // Assert
            var objectResult = result as ObjectResult;
            objectResult.Should().NotBeNull();
            objectResult!.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            objectResult.Value.Should().BeEquivalentTo(new
            {
                isSuccess = false,
                message = "El correo ya existe"
            });

            _mockAuthService.Verify(s => s.RegisterAsync(dto), Times.Once);
        }
    }
}