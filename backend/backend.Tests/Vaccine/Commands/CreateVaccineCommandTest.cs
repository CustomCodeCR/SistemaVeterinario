using backend.Application.Commons.Bases;
using backend.Application.Commons.Exceptions;
using backend.Application.UseCases.Vaccine.Commands.CreateCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Tests.Vaccine.Commands;

[TestClass]
public class CreateVaccineCommandTest
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

        var command = new CreateVaccineCommand
        {
            VaccineName = "",
            Description = "",
            Type = "",
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
    public async Task ShouldReturnValidationErrorWhenNameIsEmpty()
    {
        await AssertValidationError(command => command.VaccineName = "");
    }

    [TestMethod]
    public async Task ShouldReturnValidationErrorWhenTypeIsEmpty()
    {
        await AssertValidationError(command => command.Type = "");
    }

    [TestMethod]
    public async Task ShouldCreateVaccineSuccessfully()
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new CreateVaccineCommand
        {
            VaccineName = "Rabia",
            Description = "Previene la Rabia (zoonótica, mortal)",
            Type = "Core",
            State = 1,
            AuditCreateUser = 1001
        };

        var response = await mediator.Send(command);

        Assert.IsTrue(response.IsSuccess);
    }

    private async Task AssertValidationError(Action<CreateVaccineCommand> updateCommand)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new CreateVaccineCommand
        {
            VaccineName = "",
            Description = "",
            Type = "",
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