// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Exceptions;
using backend.Application.UseCases.Vaccine.Commands.UpdateCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Tests.Vaccine.Commands;

[TestClass]
public class UpdateVaccineCommandTest
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
    public async Task ShouldUpdateVaccineSuccessfully()
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new UpdateVaccineCommand
        {
            VaccineId = 1,
            VaccineName = "Parvovirus",
            Description = "Previene el Virus intestinal severo",
            Type = "Core",
            State = 1,
            AuditUpdateUser = 1002
        };

        var response = await mediator.Send(command);

        Assert.IsTrue(response.IsSuccess);
    }

    private async Task AssertValidationError(Action<UpdateVaccineCommand> updateCommand)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new UpdateVaccineCommand
        {
            VaccineId = 1,
            VaccineName = "",
            Description = "",
            Type = "",
            State = 1,
            AuditUpdateUser = 1002
        };

        updateCommand(command);

        try
        {
            var response = await mediator.Send(command);
            Assert.Fail("Expected validation exception but got success.");
        }
        catch (ValidationException ex)
        {
            Assert.IsNotNull(ex.Errors);
        }
    }
}