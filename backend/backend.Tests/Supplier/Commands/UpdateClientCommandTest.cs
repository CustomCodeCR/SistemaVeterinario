// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Exceptions;
using backend.Application.UseCases.Supplier.Commands.UpdateCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Tests.Supplier.Commands;

[TestClass]
public class UpdateSupplierCommandTest
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
    public async Task ShouldUpdateSupplierSuccessfully()
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new UpdateSupplierCommand
        {
            SupplierId = 4,
            Name = "Animalitos, S.A.",
            Contact = "Jennifer Gonzalez",
            Address = "Heredia, Heredia, Heredia, 40101",
            Phone = "2222-2222",
            State = 1,
            AuditUpdateUser = 1002
        };

        var response = await mediator.Send(command);

        Assert.IsTrue(response.IsSuccess);
    }

    private async Task AssertValidationError(Action<UpdateSupplierCommand> updateCommand)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new UpdateSupplierCommand
        {
            SupplierId = 1,
            Name = "Animalitos, S.A.",
            Contact = "Jennifer Gonzalez",
            Address = "Heredia, Heredia, Heredia, 40101",
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