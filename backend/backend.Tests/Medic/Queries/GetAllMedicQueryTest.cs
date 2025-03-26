// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.Medic.Response;
using backend.Application.UseCases.Medic.Queries.GetAllQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Tests.Medic.Queries;

[TestClass]
public class GetAllMedicQueryTest
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
    public async Task ShouldReturnAllMedicsWithoutFilters()
    {
        var response = await SendQuery(new GetAllMedicQuery());

        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
    }

    [TestMethod]
    public async Task ShouldFilterMedicsBySpecialty()
    {
        var response = await SendQuery(new GetAllMedicQuery
        {
            NumFilter = 1,
            TextFilter = "Cardiologo"
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.Any(u => u.Specialty!.Contains("Cardiologo", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    public async Task ShouldFilterMedicsByPhone()
    {
        var response = await SendQuery(new GetAllMedicQuery
        {
            NumFilter = 2,
            TextFilter = "1111-1111"
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.All(u => u.Phone!.Contains("1111-1111")));
    }

    [TestMethod]
    public async Task ShouldFilterMedicsByState()
    {
        var response = await SendQuery(new GetAllMedicQuery
        {
            StateFilter = 1
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.All(u => u.State == 1));
    }

    [TestMethod]
    public async Task ShouldFilterMedicsByDateRange()
    {
        var response = await SendQuery(new GetAllMedicQuery
        {
            StartDate = DateTime.UtcNow.AddDays(-30).ToString("yyyy-MM-dd"),
            EndDate = DateTime.UtcNow.ToString("yyyy-MM-dd")
        });

        Assert.IsTrue(response.IsSuccess);
    }

    private static async Task<BaseResponse<IEnumerable<MedicResponseDto>>> SendQuery(GetAllMedicQuery query)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        return await mediator.Send(query);
    }
}