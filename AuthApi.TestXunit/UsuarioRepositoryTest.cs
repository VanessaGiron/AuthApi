using AuthApi.Entidades;
using AuthApi.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApi.TestXunit
{
    public class UsuarioRepositoryTest
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDB_{System.Guid.NewGuid()}").Options;

            var context = new AppDbContext(options);

            //Datos iniciales
            context.Roles.Add(
                new Rol { Id = 1, Nombre = "Admin" }
            );
            context.Usuarios.Add(
                new Usuario
                {
                    Id = 2,
                    Nombre = "vanessa",
                    Email = "vanessagiron@test.com",
                    PasswordHash = "1130",
                    RolId = 1
                });
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task GetByEmailAsync_RetortanarUsuarioExiatente()
        {
            //Arrange
            var context = GetInMemoryDbContext();
            var repo = new UsuarioRepository(context);

            //Act
            var usuario = await repo.GetByEmailAsync("vanessagiron@test.com");

            //Assert
            Assert.NotNull(usuario);
            Assert.Equal("vanessa", usuario.Nombre);
            Assert.Equal("Admin", usuario.Rol.Nombre);
        }

        [Fact]
        public async Task AddAsync_AgregarUsuario( )
        {
            //Arrange
            var context = GetInMemoryDbContext();
            var repo = new UsuarioRepository(context);

            var nuevoUsuario = new Usuario()
            {
                Nombre = "Daniela",
                Email = "daniela@test.com",
                PasswordHash = "123",
                RolId = 1
            };

            //Act
            await repo.AddAsync(nuevoUsuario);

            //Assert
            var usuarioGuardado = await context.Usuarios.FirstOrDefaultAsync(u => u.Email == "daniela@test.com");

            Assert.NotNull(usuarioGuardado);
            Assert.Equal("Daniela", usuarioGuardado.Nombre);
        }

        [Fact]
        public async Task GetAllUsuariosAsync_RetornaListaUsuarios()
        {
            //Arrange
            var context = GetInMemoryDbContext();
            var repo = new UsuarioRepository(context);

            //Act
            var lista = await repo.GetAllUsuariosAsync();

            //Assert
            Assert.NotEmpty(lista);
            Assert.Contains(lista, u => u.Email == "vanessagiron@test.com");
        }
    }
}
