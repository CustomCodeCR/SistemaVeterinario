// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.Product.Response;
using backend.Application.UseCases.Product.Queries.GetAllQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Tests.Product.Queries;

[TestClass]
public class GetAllProductQueryTest
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
    public async Task ShouldReturnAllProductsWithoutFilters()
    {
        var response = await SendQuery(new GetAllProductQuery());

        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
    }

    [TestMethod]
    public async Task ShouldFilterProductsByName()
    {
        var response = await SendQuery(new GetAllProductQuery
        {
            NumFilter = 1,
            TextFilter = "7Up"
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.Any(u => u.Name!.Contains("7Up", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    public async Task ShouldFilterProductsByDescription()
    {
        var response = await SendQuery(new GetAllProductQuery
        {
            NumFilter = 2,
            TextFilter = "Bebida"
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.All(u => u.Description!.Contains("Bebida")));
    }

    [TestMethod]
    public async Task ShouldFilterProductsByState()
    {
        var response = await SendQuery(new GetAllProductQuery
        {
            StateFilter = 1
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.All(u => u.State == 1));
    }

    [TestMethod]
    public async Task ShouldFilterProductsByDateRange()
    {
        var response = await SendQuery(new GetAllProductQuery
        {
            StartDate = DateTime.UtcNow.AddDays(-30).ToString("yyyy-MM-dd"),
            EndDate = DateTime.UtcNow.ToString("yyyy-MM-dd")
        });

        Assert.IsTrue(response.IsSuccess);
    }

    private static async Task<BaseResponse<IEnumerable<ProductResponseDto>>> SendQuery(GetAllProductQuery query)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        return await mediator.Send(query);
    }
}