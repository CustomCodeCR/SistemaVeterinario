// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.UseCases.Appointment.Queries.GetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Tests.Appointment.Queries;

[TestClass]
public class GetAppointmentByIdQueryTest
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
    public async Task ShouldReturnAppointmentByIdWhenExists()
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var query = new GetAppointmentByIdQuery
        {
            AppointmentId = 1
        };

        var response = await mediator.Send(query);

        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
        Assert.AreEqual(query.AppointmentId, response.Data.AppointmentId);
    }

    [TestMethod]
    public async Task ShouldReturnNullWhenAppointmentDoesNotExist()
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var query = new GetAppointmentByIdQuery
        {
            AppointmentId = 9999
        };

        var response = await mediator.Send(query);

        Assert.IsFalse(response.IsSuccess);
        Assert.IsNull(response.Data);
    }
}