// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Commons.Exceptions;
using backend.Application.UseCases.Sale.Commands.CreateCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Tests.Sale.Commands;

[TestClass]
public class CreateSaleCommandTest
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

        var command = new CreateSaleCommand
        {
            ClientId = 0,
            SaleDate = DateTime.MinValue,
            State = 1,
            AuditCreateUser = 1001,
            SaleDetail = new List<CreateSaleDetailCommand>
            {
                new()
                {
                    ProductId = 0,
                    Quantity = 0,
                    Price = 0
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
    public async Task ShouldCreateSaleSuccessfully()
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new CreateSaleCommand
        {
            ClientId = 1,
            SaleDate = DateTime.Now.AddDays(1),
            State = 1,
            AuditCreateUser = 1001,
            SaleDetail = new List<CreateSaleDetailCommand>
            {
                new()
                {
                    ProductId = 2,
                    Quantity = 5,
                    Price = 300
                }
            }
        };

        var response = await mediator.Send(command);

        Assert.IsTrue(response.IsSuccess);
    }

    [TestMethod]
    public async Task ShouldReturnValidationErrorWhenProductIdIsZero()
    {
        await AssertValidationError(cmd => cmd.SaleDetail.First().ProductId = 0);
    }

    private async Task AssertValidationError(Action<CreateSaleCommand> updateCommand)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new CreateSaleCommand
        {
            ClientId = 1,
            SaleDate = DateTime.Now,
            State = 1,
            AuditCreateUser = 1001,
            SaleDetail = new List<CreateSaleDetailCommand>
            {
                new()
                {
                    ProductId = 1,
                    Quantity = 10,
                    Price = 500
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
