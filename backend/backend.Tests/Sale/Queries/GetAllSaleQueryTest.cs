// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.Sale.Response;
using backend.Application.UseCases.Sale.Queries.GetAllQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Tests.Sale.Queries;

[TestClass]
public class GetAllSaleQueryTest
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
    public async Task ShouldReturnAllSalesWithoutFilters()
    {
        var response = await SendQuery(new GetAllSaleQuery());

        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
    }

    [TestMethod]
    public async Task ShouldFilterSalesByClientFirstname()
    {
        var response = await SendQuery(new GetAllSaleQuery
        {
            NumFilter = 1,
            TextFilter = "Carlos"
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.Any(a => a.Client!.Contains("Carlos", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    public async Task ShouldFilterSalesByClientLastname()
    {
        var response = await SendQuery(new GetAllSaleQuery
        {
            NumFilter = 2,
            TextFilter = "Ramírez"
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.Any(a => a.Client!.Contains("Ramírez", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    public async Task ShouldFilterSalesByState()
    {
        var response = await SendQuery(new GetAllSaleQuery
        {
            StateFilter = 1
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.All(a => a.State == 1));
    }

    [TestMethod]
    public async Task ShouldFilterSalesByDateRange()
    {
        var response = await SendQuery(new GetAllSaleQuery
        {
            StartDate = DateTime.UtcNow.AddDays(-30).ToString("yyyy-MM-dd"),
            EndDate = DateTime.UtcNow.ToString("yyyy-MM-dd")
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
    }

    private static async Task<BaseResponse<IEnumerable<SaleResponseDto>>> SendQuery(GetAllSaleQuery query)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        return await mediator.Send(query);
    }
}
