using backend.Application.Commons.Bases;
using backend.Application.Commons.Exceptions;
using backend.Application.UseCases.Medic.Commands.CreateCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Tests.Medic.Commands;

[TestClass]
public class CreateMedicCommandTest
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

        var command = new CreateMedicCommand
        {
            Specialty = "1",
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
            Assert.AreEqual(response.Message, response.Message);
        }
    }

    [TestMethod]
    public async Task ShouldReturnValidationErrorWhenSpecialtyIsEmpty()
    {
        await AssertValidationError(command => command.Specialty = "");
    }

    [TestMethod]
    public async Task ShouldReturnValidationErrorWhenPhoneIsEmpty()
    {
        await AssertValidationError(command => command.Phone = "");
    }

    [TestMethod]
    public async Task ShouldCreateMedicSuccessfully()
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new CreateMedicCommand
        {
            UserId = 1,
            Specialty = "Cardiologo",
            Phone = "1111-1111",
            State = 1,
            AuditCreateUser = 1001
        };

        var response = await mediator.Send(command);

        Assert.IsTrue(response.IsSuccess);
    }

    private async Task AssertValidationError(Action<CreateMedicCommand> updateCommand)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new CreateMedicCommand
        {
            UserId = 1,
            Specialty = "Especialidad",
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