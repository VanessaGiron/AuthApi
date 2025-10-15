using AuthApi.DTOs.UsuarioDTOs;
using AuthApi.Entidades;
using AuthApi.Interfaces;
using AuthApi.Repositorios;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApi.TestXunit
{
    public class AuthRepositoryTest
    {
        private IConfiguration GetTestContiguration()
        {
            var inMemoryConfig = new Dictionary<string, string>
            {
                {"Jwt:key","ClaveSuperSecretaMuyLargatest567890!" },
                {"Jwt:Issuer","AuthApiTest"},
                {"Jwt:Audience","AuthApiClients"  },
            };

            return new ConfigurationBuilder().AddInMemoryCollection(inMemoryConfig).Build();
        }

        [Fact]
        public async Task RegistrarAsync_RetorneUsuarioConToken()
        {
            //Arrange
            var mockRepo = new Mock<IUsuarioRepository>();
            var config = GetTestContiguration();
            var usuario = new Usuario
            {
                Id = 1,
                Nombre = "vane",
                Email = "vane@test.com",
                PasswordHash = "hash",
                Rol = new Rol { Id = 2, Nombre = "usuario" }
            };

            mockRepo.Setup(r => r.AddAsync(It.IsAny<Usuario>())).ReturnsAsync(usuario);

            mockRepo.Setup(r => r.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(usuario);

            var service = new AuthRepository(mockRepo.Object, config);

            var registroDTO = new UsuarioRegistroDTO
            {
                Nombre = "vane",
                Email = "vane@test.com",
                Password = "1130"
            };

            //Act
            var result = await service.RegistrarAsync(registroDTO);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("vane", result.Nombre);
            Assert.Equal("vane@test.com", result.Email);
            Assert.False(string.IsNullOrEmpty(result.Token));
        }

        [Fact]
        public async Task LoginAsync_RetornalNullSiUsuarioNoExiste()
        {
            //Arrange
            var mockRepo = new Mock<IUsuarioRepository>();
            var config = GetTestContiguration();

            mockRepo.Setup(r => r.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync((Usuario?)null);

            var service = new AuthRepository(mockRepo.Object, config);

            var loginDTO = new UsuarioLoginDTO
            {
                Email = "vaneNoEsxite@test.com",
                Password = "123",
            };

            //Act
            var result = service.LoginAsync(loginDTO);

            //Assert
            Assert.NotNull(result);
        }
    }
}  

