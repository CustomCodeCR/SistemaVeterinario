// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Commons.Exceptions;
using backend.Application.UseCases.PurchaseOrder.Commands.CreateCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Tests.PurchaseOrder.Commands;

[TestClass]
public class CreatePurchaseOrderCommandTest
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
    public async Task ShouldGetValidationErrorsWhenRequiredFieldsAreMissing()
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new CreatePurchaseOrderCommand
        {
            SupplierId = 0,
            OrderDate = DateTime.MinValue,
            Status = "",
            State = 1,
            AuditCreateUser = 1001,
            PurchaseOrderDetail = new List<CreatePurchaseOrderDetailCommand>
            {
                new()
                {
                    ProductId = 0,
                    Quantity = 0,
                    UnitPrice = 0
                }
            }
        };

        BaseResponse<bool> response = new();

        try
        {
            response = await mediator.Send(command);
            Assert.Fail("Expected ValidationException but got success.");
        }
        catch (ValidationException ex)
        {
            Assert.IsNotNull(ex.Errors);
            Assert.AreEqual(false, response.IsSuccess);
        }
    }

    [TestMethod]
    public async Task ShouldCreatePurchaseOrderSuccessfully()
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new CreatePurchaseOrderCommand
        {
            SupplierId = 1,
            OrderDate = DateTime.Now.AddDays(1),
            Status = "P",
            State = 1,
            AuditCreateUser = 1001,
            PurchaseOrderDetail = new List<CreatePurchaseOrderDetailCommand>
            {
                new()
                {
                    ProductId = 2,
                    Quantity = 5,
                    UnitPrice = 300
                }
            }
        };

        var response = await mediator.Send(command);

        Assert.IsTrue(response.IsSuccess);
    }

    [TestMethod]
    public async Task ShouldReturnValidationErrorWhenStatusIsEmpty()
    {
        await AssertValidationError(cmd => cmd.Status = "");
    }

    [TestMethod]
    public async Task ShouldReturnValidationErrorWhenProductIdIsZero()
    {
        await AssertValidationError(cmd => cmd.PurchaseOrderDetail.First().ProductId = 0);
    }

    private async Task AssertValidationError(Action<CreatePurchaseOrderCommand> updateCommand)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new CreatePurchaseOrderCommand
        {
            SupplierId = 1,
            OrderDate = DateTime.Now,
            Status = "Activo",
            State = 1,
            AuditCreateUser = 1001,
            PurchaseOrderDetail = new List<CreatePurchaseOrderDetailCommand>
            {
                new()
                {
                    ProductId = 1,
                    Quantity = 10,
                    UnitPrice = 500
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
