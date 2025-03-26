// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.Pet.Response;
using backend.Application.UseCases.Pet.Queries.GetAllQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Tests.Pet.Queries;

[TestClass]
public class GetAllPetQueryTest
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
    public async Task ShouldReturnAllPetsWithoutFilters()
    {
        var response = await SendQuery(new GetAllPetQuery());

        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
    }

    [TestMethod]
    public async Task ShouldFilterPetsByName()
    {
        var response = await SendQuery(new GetAllPetQuery
        {
            NumFilter = 1,
            TextFilter = "Michi"
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.Any(u => u.Name!.Contains("Michi", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    public async Task ShouldFilterPetsByType()
    {
        var response = await SendQuery(new GetAllPetQuery
        {
            NumFilter = 2,
            TextFilter = "Gato"
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.All(u => u.Type!.Contains("Gato")));
    }

    [TestMethod]
    public async Task ShouldFilterPetsByBreed()
    {
        var response = await SendQuery(new GetAllPetQuery
        {
            NumFilter = 3,
            TextFilter = "Naranja"
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.All(u => u.Breed!.Contains("Naranja")));
    }

    [TestMethod]
    public async Task ShouldFilterPetsByState()
    {
        var response = await SendQuery(new GetAllPetQuery
        {
            StateFilter = 1
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.All(u => u.State == 1));
    }

    [TestMethod]
    public async Task ShouldFilterPetsByDateRange()
    {
        var response = await SendQuery(new GetAllPetQuery
        {
            StartDate = DateTime.UtcNow.AddDays(-30).ToString("yyyy-MM-dd"),
            EndDate = DateTime.UtcNow.ToString("yyyy-MM-dd")
        });

        Assert.IsTrue(response.IsSuccess);
    }

    private static async Task<BaseResponse<IEnumerable<PetResponseDto>>> SendQuery(GetAllPetQuery query)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        return await mediator.Send(query);
    }
}