// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.UseCases.Medic.Queries.GetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Tests.Medic.Queries;

[TestClass]
public class GetMedicByIdQueryTest
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
    public async Task ShouldReturnMedicByIdWhenExists()
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var query = new GetMedicByIdQuery
        {
            MedicId = 9
        };

        var response = await mediator.Send(query);

        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
        Assert.AreEqual(query.MedicId, response.Data.MedicId);
    }

    [TestMethod]
    public async Task ShouldReturnNullWhenMedicDoesNotExist()
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var query = new GetMedicByIdQuery
        {
            MedicId = 9999
        };

        var response = await mediator.Send(query);

        Assert.IsFalse(response.IsSuccess);
        Assert.IsNull(response.Data);
    }
}