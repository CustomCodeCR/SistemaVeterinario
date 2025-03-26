// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.Supplier.Response;
using backend.Application.UseCases.Supplier.Queries.GetAllQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Tests.Supplier.Queries;

[TestClass]
public class GetAllSupplierQueryTest
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
    public async Task ShouldReturnAllSuppliersWithoutFilters()
    {
        var response = await SendQuery(new GetAllSupplierQuery());

        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
    }

    [TestMethod]
    public async Task ShouldFilterSuppliersByName()
    {
        var response = await SendQuery(new GetAllSupplierQuery
        {
            NumFilter = 1,
            TextFilter = "Productos"
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.Any(u => u.Name!.Contains("Productos", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    public async Task ShouldFilterSuppliersByContact()
    {
        var response = await SendQuery(new GetAllSupplierQuery
        {
            NumFilter = 2,
            TextFilter = "Jose"
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.All(u => u.Contact!.Contains("Jose")));
    }

    [TestMethod]
    public async Task ShouldFilterSuppliersByState()
    {
        var response = await SendQuery(new GetAllSupplierQuery
        {
            StateFilter = 1
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.All(u => u.State == 1));
    }

    [TestMethod]
    public async Task ShouldFilterSuppliersByDateRange()
    {
        var response = await SendQuery(new GetAllSupplierQuery
        {
            StartDate = DateTime.UtcNow.AddDays(-30).ToString("yyyy-MM-dd"),
            EndDate = DateTime.UtcNow.ToString("yyyy-MM-dd")
        });

        Assert.IsTrue(response.IsSuccess);
    }

    private static async Task<BaseResponse<IEnumerable<SupplierResponseDto>>> SendQuery(GetAllSupplierQuery query)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        return await mediator.Send(query);
    }
}