// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.Appointment.Response;
using backend.Application.UseCases.Appointment.Queries.GetAllQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Tests.Appointment.Queries;

[TestClass]
public class GetAllAppointmentQueryTest
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
    public async Task ShouldReturnAllAppointmentsWithoutFilters()
    {
        var response = await SendQuery(new GetAllAppointmentQuery());

        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
    }

    [TestMethod]
    public async Task ShouldFilterAppointmentsByPetName()
    {
        var response = await SendQuery(new GetAllAppointmentQuery
        {
            NumFilter = 1,
            TextFilter = "Michi"
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.Any(a => a.Pet!.Contains("Michi", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    public async Task ShouldFilterAppointmentsByMedicFirstName()
    {
        var response = await SendQuery(new GetAllAppointmentQuery
        {
            NumFilter = 2,
            TextFilter = "Carlos"
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.Any(a => a.Medic!.Contains("Carlos", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    public async Task ShouldFilterAppointmentsByMedicLastName()
    {
        var response = await SendQuery(new GetAllAppointmentQuery
        {
            NumFilter = 3,
            TextFilter = "Ramírez"
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.Any(a => a.Medic!.Contains("Ramírez", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    public async Task ShouldFilterAppointmentsByState()
    {
        var response = await SendQuery(new GetAllAppointmentQuery
        {
            StateFilter = 1
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data!.All(a => a.State == 1));
    }

    [TestMethod]
    public async Task ShouldFilterAppointmentsByDateRange()
    {
        var response = await SendQuery(new GetAllAppointmentQuery
        {
            StartDate = DateTime.UtcNow.AddDays(-30).ToString("yyyy-MM-dd"),
            EndDate = DateTime.UtcNow.ToString("yyyy-MM-dd")
        });

        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
    }

    private static async Task<BaseResponse<IEnumerable<AppointmentResponseDto>>> SendQuery(GetAllAppointmentQuery query)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        return await mediator.Send(query);
    }
}
