﻿// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.UseCases.Payment.Commands.DeleteCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Tests.Payment.Commands;

[TestClass]
public class DeletePaymentCommandTest
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
    public async Task ShouldDeletePaymentSuccessfully()
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new DeletePaymentCommand
        {
            PaymentId = 4,
            AuditDeleteUser = 1003
        };

        var response = await mediator.Send(command);

        Assert.IsTrue(response.IsSuccess);
    }
}