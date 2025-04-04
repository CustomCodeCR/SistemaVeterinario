// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.ProductCategory.Response;
using backend.Application.UseCases.ProductCategory.Queries.GetAllQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Tests.ProductCategory.Queries;

[TestClass]
public class GetAllProductCategoryQueryTest
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
    public async Task ShouldReturnAllProductCategorysWithoutFilters()
    {
        var response = await SendQuery(new GetAllProductCategoryQuery());

        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
    }

    [TestMethod]
    public async Task ShouldFilterProductCategorysByName()
    {
        var response = await SendQuery(new GetAllProductCategoryQuery
        {
            NumFilter = 1,
            TextFilter = "Snack"
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.Any(u => u.CategoryName!.Contains("Snack", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    public async Task ShouldFilterProductCategorysByDescription()
    {
        var response = await SendQuery(new GetAllProductCategoryQuery
        {
            NumFilter = 2,
            TextFilter = "Bebidas"
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.All(u => u.Description!.Contains("Bebidas")));
    }

    [TestMethod]
    public async Task ShouldFilterProductCategorysByState()
    {
        var response = await SendQuery(new GetAllProductCategoryQuery
        {
            StateFilter = 1
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.All(u => u.State == 1));
    }

    [TestMethod]
    public async Task ShouldFilterProductCategorysByDateRange()
    {
        var response = await SendQuery(new GetAllProductCategoryQuery
        {
            StartDate = DateTime.UtcNow.AddDays(-30).ToString("yyyy-MM-dd"),
            EndDate = DateTime.UtcNow.ToString("yyyy-MM-dd")
        });

        Assert.IsTrue(response.IsSuccess);
    }

    private static async Task<BaseResponse<IEnumerable<ProductCategoryResponseDto>>> SendQuery(GetAllProductCategoryQuery query)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        return await mediator.Send(query);
    }
}