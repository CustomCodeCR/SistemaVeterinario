// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.PurchaseOrder.Response;
using backend.Application.UseCases.PurchaseOrder.Queries.GetAllQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Tests.PurchaseOrder.Queries;

[TestClass]
public class GetAllPurchaseOrderQueryTest
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
    public async Task ShouldReturnAllPurchaseOrdersWithoutFilters()
    {
        var response = await SendQuery(new GetAllPurchaseOrderQuery());

        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
    }

    [TestMethod]
    public async Task ShouldFilterPurchaseOrdersBySupplier()
    {
        var response = await SendQuery(new GetAllPurchaseOrderQuery
        {
            NumFilter = 1,
            TextFilter = "Productos S.A."
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.Any(a => a.Supplier!.Contains("Productos S.A.", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    public async Task ShouldFilterPurchaseOrdersByStatus()
    {
        var response = await SendQuery(new GetAllPurchaseOrderQuery
        {
            NumFilter = 2,
            TextFilter = "Pending"
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.Any(a => a.Status!.Contains("Pending", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    public async Task ShouldFilterPurchaseOrdersByState()
    {
        var response = await SendQuery(new GetAllPurchaseOrderQuery
        {
            StateFilter = 1
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.All(a => a.State == 1));
    }

    [TestMethod]
    public async Task ShouldFilterPurchaseOrdersByDateRange()
    {
        var response = await SendQuery(new GetAllPurchaseOrderQuery
        {
            StartDate = DateTime.UtcNow.AddDays(-30).ToString("yyyy-MM-dd"),
            EndDate = DateTime.UtcNow.ToString("yyyy-MM-dd")
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
    }

    private static async Task<BaseResponse<IEnumerable<PurchaseOrderResponseDto>>> SendQuery(GetAllPurchaseOrderQuery query)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        return await mediator.Send(query);
    }
}
