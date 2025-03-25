using backend.Application.Commons.Bases;
using backend.Application.Commons.Exceptions;
using backend.Application.UseCases.Client.Commands.CreateCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Tests.Client.Commands;

[TestClass]
public class CreateClientCommandTest
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

        var command = new CreateClientCommand
        {
            Address = "",
            Phone = "",
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
    public async Task ShouldReturnValidationErrorWhenAdressIsEmpty()
    {
        await AssertValidationError(command => command.Address = "");
    }

    [TestMethod]
    public async Task ShouldReturnValidationErrorWhenPhoneIsEmpty()
    {
        await AssertValidationError(command => command.Phone = "");
    }

    [TestMethod]
    public async Task ShouldCreateClientSuccessfully()
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new CreateClientCommand
        {
            UserId = 1,
            Address = "San Jose, Carmen, Carmen, 10101",
            Phone = "1111-1111",
            State = 1,
            AuditCreateUser = 1001
        };

        var response = await mediator.Send(command);

        Assert.IsTrue(response.IsSuccess);
    }

    private async Task AssertValidationError(Action<CreateClientCommand> updateCommand)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new CreateClientCommand
        {
            UserId = 1,
            Address = "Direccion",
            Phone = "1111-1111",
            State = 1,
            AuditCreateUser = 1001
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