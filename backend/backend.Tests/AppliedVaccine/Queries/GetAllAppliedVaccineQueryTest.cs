// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.AppliedVaccine.Response;
using backend.Application.UseCases.AppliedVaccine.Queries.GetAllQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Tests.AppliedVaccine.Queries;

[TestClass]
public class GetAllAppliedVaccineQueryTest
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
    public async Task ShouldReturnAllAppliedVaccinesWithoutFilters()
    {
        var response = await SendQuery(new GetAllAppliedVaccineQuery());

        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
    }

    [TestMethod]
    public async Task ShouldFilterAppliedVaccinesByVaccineName()
    {
        var response = await SendQuery(new GetAllAppliedVaccineQuery
        {
            NumFilter = 1,
            TextFilter = "Parvovirus"
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.Any(u => u.Vaccine!.Contains("Parvovirus", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    public async Task ShouldFilterAppliedVaccinesByPhone()
    {
        var response = await SendQuery(new GetAllAppliedVaccineQuery
        {
            NumFilter = 2,
            TextFilter = "Michi"
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.All(u => u.Pet!.Contains("Michi")));
    }

    [TestMethod]
    public async Task ShouldFilterAppliedVaccinesByState()
    {
        var response = await SendQuery(new GetAllAppliedVaccineQuery
        {
            StateFilter = 1
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.All(u => u.State == 1));
    }

    [TestMethod]
    public async Task ShouldFilterAppliedVaccinesByDateRange()
    {
        var response = await SendQuery(new GetAllAppliedVaccineQuery
        {
            StartDate = DateTime.UtcNow.AddDays(-30).ToString("yyyy-MM-dd"),
            EndDate = DateTime.UtcNow.ToString("yyyy-MM-dd")
        });

        Assert.IsTrue(response.IsSuccess);
    }

    private static async Task<BaseResponse<IEnumerable<AppliedVaccineResponseDto>>> SendQuery(GetAllAppliedVaccineQuery query)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        return await mediator.Send(query);
    }
}