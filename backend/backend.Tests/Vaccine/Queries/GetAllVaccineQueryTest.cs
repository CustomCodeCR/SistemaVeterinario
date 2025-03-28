// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.Vaccine.Response;
using backend.Application.UseCases.Vaccine.Queries.GetAllQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Tests.Vaccine.Queries;

[TestClass]
public class GetAllVaccineQueryTest
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
    public async Task ShouldReturnAllVaccinesWithoutFilters()
    {
        var response = await SendQuery(new GetAllVaccineQuery());

        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
    }

    [TestMethod]
    public async Task ShouldFilterVaccinesByName()
    {
        var response = await SendQuery(new GetAllVaccineQuery
        {
            NumFilter = 1,
            TextFilter = "Rabia"
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.Any(u => u.VaccineName!.Contains("Rabia", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    public async Task ShouldFilterVaccinesByType()
    {
        var response = await SendQuery(new GetAllVaccineQuery
        {
            NumFilter = 2,
            TextFilter = "Core"
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.All(u => u.Type!.Contains("Core")));
    }

    [TestMethod]
    public async Task ShouldFilterVaccinesByState()
    {
        var response = await SendQuery(new GetAllVaccineQuery
        {
            StateFilter = 1
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.All(u => u.State == 1));
    }

    [TestMethod]
    public async Task ShouldFilterVaccinesByDateRange()
    {
        var response = await SendQuery(new GetAllVaccineQuery
        {
            StartDate = DateTime.UtcNow.AddDays(-30).ToString("yyyy-MM-dd"),
            EndDate = DateTime.UtcNow.ToString("yyyy-MM-dd")
        });

        Assert.IsTrue(response.IsSuccess);
    }

    private static async Task<BaseResponse<IEnumerable<VaccineResponseDto>>> SendQuery(GetAllVaccineQuery query)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        return await mediator.Send(query);
    }
}