// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Exceptions;
using backend.Application.UseCases.PurchaseOrder.Commands.UpdateCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Tests.PurchaseOrder.Commands;

[TestClass]
public class UpdatePurchaseOrderCommandTest
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
    public async Task ShouldUpdatePurchaseOrderSuccessfully()
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new UpdatePurchaseOrderCommand
        {
            PurchaseOrderId = 6,
            SupplierId = 1,
            OrderDate = DateTime.Now.AddDays(2),
            Status = "D",
            State = 1,
            AuditUpdateUser = 1002,
            PurchaseOrderDetail = new List<UpdatePurchaseOrderDetailCommand>
            {
                new()
                {
                    ProductId = 2,
                    Quantity = 12,
                    UnitPrice = 360
                }
            }
        };

        var response = await mediator.Send(command);

        Assert.IsTrue(response.IsSuccess);
    }

    private async Task AssertValidationError(Action<UpdatePurchaseOrderCommand> updateCommand)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new UpdatePurchaseOrderCommand
        {
            PurchaseOrderId = 4,
            SupplierId = 2,
            OrderDate = DateTime.Now,
            Status = "P",
            State = 1,
            AuditUpdateUser = 1002,
            PurchaseOrderDetail = new List<UpdatePurchaseOrderDetailCommand>
            {
                new()
                {
                    ProductId = 2,
                    Quantity = 5,
                    UnitPrice = 300
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