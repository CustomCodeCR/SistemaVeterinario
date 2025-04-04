using backend.Application.Commons.Bases;
using backend.Application.Commons.Exceptions;
using backend.Application.UseCases.User.Commands.CreateCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Tests.User.Commands;

[TestClass]
public class CreateUserCommandTest
{
    private static WebApplicationFactory<Program> _factory = null!;
    private static IServiceScopeFactory _scopeFactory = null!;

    [ClassInitialize]
    public static void ClassInitialize(TestContext _)
    {
        _factory = new CustomWebApplicationFactory();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
    }

    [TestMethod]
    public async Task ShouldGetValidationErrorsWhenAllFieldsAreEmpty()
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new CreateUserCommand
        {
            FirstName = "",
            LastName = "",
            Username = "",
            Password = "",
            Email = "",
            UserType = "Admin",
            State = 1,
            AuditCreateUser = 1
        };

        BaseResponse<bool> response = new();

        try
        {
            response = await mediator.Send(command);
            Assert.Fail(response.Message);
        }
        catch (ValidationException ex)
        {
            Assert.IsNotNull(ex.Errors);
            Assert.AreEqual(false, response.IsSuccess);
        }
    }

    [TestMethod]
    public async Task ShouldReturnValidationErrorWhenFirstNameIsEmpty()
    {
        await AssertValidationError(command => command.FirstName = "");
    }

    [TestMethod]
    public async Task ShouldReturnValidationErrorWhenLastNameIsEmpty()
    {
        await AssertValidationError(command => command.LastName = "");
    }

    [TestMethod]
    public async Task ShouldReturnValidationErrorWhenUsernameIsEmpty()
    {
        await AssertValidationError(command => command.Username = "");
    }

    [TestMethod]
    public async Task ShouldReturnValidationErrorWhenPasswordIsEmpty()
    {
        await AssertValidationError(command => command.Password = "");
    }

    [TestMethod]
    public async Task ShouldReturnValidationErrorWhenEmailIsEmpty()
    {
        await AssertValidationError(command => command.Email = "");
    }

    [TestMethod]
    public async Task ShouldCreateUserSuccessfully()
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new CreateUserCommand
        {
            FirstName = "Juan",
            LastName = "Pérez",
            Username = "juanperez12",
            Password = "password123",
            Email = "juan@example.com",
            UserType = "Admin",
            State = 1,
            AuditCreateUser = 1001
        };

        var response = await mediator.Send(command);

        Assert.IsTrue(response.IsSuccess);
    }

    private async Task AssertValidationError(Action<CreateUserCommand> updateCommand)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new CreateUserCommand
        {
            FirstName = "Nombre",
            LastName = "Apellido",
            Username = "usuario",
            Password = "contraseña",
            Email = "email@ejemplo.com",
            UserType = "Admin",
            State = 1,
            AuditCreateUser = 1
        };

        updateCommand(command);

        try
        {
            var response = await mediator.Send(command);
            Assert.Fail("Expected ValidationException but got success.");
        }
        catch (ValidationException ex)
        {
            Assert.IsNotNull(ex.Errors);
        }
    }
}