// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Exceptions;
using backend.Application.UseCases.Sale.Commands.UpdateCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Tests.Sale.Commands;

[TestClass]
public class UpdateSaleCommandTest
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
    public async Task ShouldUpdateSaleSuccessfully()
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new UpdateSaleCommand
        {
            SaleId = 4,
            ClientId = 1,
            SaleDate = DateTime.Now.AddDays(2),
            State = 1,
            AuditUpdateUser = 1002,
            SaleDetail = new List<UpdateSaleDetailCommand>
            {
                new()
                {
                    ProductId = 2,
                    Quantity = 12,
                    Price = 360
                }
            }
        };

        var response = await mediator.Send(command);

        Assert.IsTrue(response.IsSuccess);
    }

    private async Task AssertValidationError(Action<UpdateSaleCommand> updateCommand)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new UpdateSaleCommand
        {
            SaleId = 4,
            ClientId = 2,
            SaleDate = DateTime.Now,
            State = 1,
            AuditUpdateUser = 1002,
            SaleDetail = new List<UpdateSaleDetailCommand>
            {
                new()
                {
                    ProductId = 2,
                    Quantity = 5,
                    Price = 300
                }
            }
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