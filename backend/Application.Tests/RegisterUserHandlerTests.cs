using Microsoft.EntityFrameworkCore;
using FluentAssertions; 
using System;
using System.Threading.Tasks;
using Xunit;
using Domain;

public class RegisterUserHandlerTests
{
    // Método cria um clone do appdbcontext em memória para cada teste.
    private AppDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var dbContext = new AppDbContext(options);
        dbContext.Database.EnsureCreated();
        return dbContext;
    }

    [Fact]
    public async Task Handle_Should_ThrowException_WhenEmailAlreadyExists()
    {
        var dbContext = CreateInMemoryDbContext();

        // para simular a condição de e-mail duplicado.
        var existingUser = new User
        {
            Id = Guid.NewGuid(),
            Name = "Usuario já existe",
            Email = "testusuarioexistente@usuario.com",
            Passkey = "123456789"
        };
        dbContext.Users.Add(existingUser);
        await dbContext.SaveChangesAsync();

        var handler = new RegisterUserHandler(dbContext);

        var command = new RegisterUser
        {
            Name = "Novo usuario",
            Email = "testusuarioexistente@usuario.com", //para e-mail duplicado
            Passkey = "NovaSenha"
        };

        var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(command));

        exception.Message.Should().Be("E-mail já cadastrado.");
    }

    [Fact]
    public async Task Handle_Should_CreateAndReturnUser_WhenEmailIsUnique()
    {
        var dbContext = CreateInMemoryDbContext();
        var handler = new RegisterUserHandler(dbContext);

        var command = new RegisterUser
        {
            Name = "Diogo vieira", // novo usuário unico
            Email = "diogo.vieira@gmail.com",
            Passkey = "123456"
        };

        var resultUser = await handler.Handle(command);

        resultUser.Should().NotBeNull();
        resultUser.Name.Should().Be(command.Name);
        resultUser.Email.Should().Be(command.Email);
        resultUser.Id.Should().NotBe(Guid.Empty); 

        var userInDb = await dbContext.Users.FindAsync(resultUser.Id);
        userInDb.Should().NotBeNull();
        userInDb.Email.Should().Be("diogo.vieira@gmail.com");
    }
}