﻿// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Exceptions;
using backend.Application.UseCases.Medic.Commands.UpdateCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Tests.Medic.Commands;

[TestClass]
public class UpdateMedicCommandTest
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
    public async Task ShouldUpdateMedicSuccessfully()
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new UpdateMedicCommand
        {
            MedicId = 21,
            UserId = 1,
            Specialty = "Dermatologo",
            Phone = "2222-2222",
            State = 1,
            AuditUpdateUser = 1002
        };

        var response = await mediator.Send(command);

        Assert.IsTrue(response.IsSuccess);
    }

    private async Task AssertValidationError(Action<UpdateMedicCommand> updateCommand)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new UpdateMedicCommand
        {
            MedicId = 5,
            UserId = 1,
            Specialty = "Dermatologo",
            Phone = "2222-2222",
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