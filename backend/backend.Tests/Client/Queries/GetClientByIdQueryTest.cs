// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.UseCases.Client.Queries.GetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Tests.Client.Queries;

[TestClass]
public class GetClientByIdQueryTest
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
    public async Task ShouldReturnClientByIdWhenExists()
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var query = new GetClientByIdQuery
        {
            ClientId = 1
        };

        var response = await mediator.Send(query);

        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
        Assert.AreEqual(query.ClientId, response.Data.ClientId);
    }

    [TestMethod]
    public async Task ShouldReturnNullWhenClientDoesNotExist()
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var query = new GetClientByIdQuery
        {
            ClientId = 9999
        };

        var response = await mediator.Send(query);

        Assert.IsFalse(response.IsSuccess);
        Assert.IsNull(response.Data);
    }
}