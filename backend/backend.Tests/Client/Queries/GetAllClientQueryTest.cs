// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.Client.Response;
using backend.Application.UseCases.Client.Queries.GetAllQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Tests.Client.Queries;

[TestClass]
public class GetAllClientQueryTest
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
    public async Task ShouldReturnAllClientsWithoutFilters()
    {
        var response = await SendQuery(new GetAllClientQuery());

        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
    }

    [TestMethod]
    public async Task ShouldFilterClientsByAdress()
    {
        var response = await SendQuery(new GetAllClientQuery
        {
            NumFilter = 1,
            TextFilter = "Cartago"
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.Any(u => u.Address!.Contains("Cartago", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    public async Task ShouldFilterClientsByPhone()
    {
        var response = await SendQuery(new GetAllClientQuery
        {
            NumFilter = 2,
            TextFilter = "1111-1111"
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.All(u => u.Phone!.Contains("1111-1111")));
    }

    [TestMethod]
    public async Task ShouldFilterClientsByState()
    {
        var response = await SendQuery(new GetAllClientQuery
        {
            StateFilter = 1
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.All(u => u.State == 1));
    }

    [TestMethod]
    public async Task ShouldFilterClientsByDateRange()
    {
        var response = await SendQuery(new GetAllClientQuery
        {
            StartDate = DateTime.UtcNow.AddDays(-30).ToString("yyyy-MM-dd"),
            EndDate = DateTime.UtcNow.ToString("yyyy-MM-dd")
        });

        Assert.IsTrue(response.IsSuccess);
    }

    private static async Task<BaseResponse<IEnumerable<ClientResponseDto>>> SendQuery(GetAllClientQuery query)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        return await mediator.Send(query);
    }
}