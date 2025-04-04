// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Exceptions;
using backend.Application.UseCases.Appointment.Commands.UpdateCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Tests.Appointment.Commands;

[TestClass]
public class UpdateAppointmentCommandTest
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
    public async Task ShouldUpdateAppointmentSuccessfully()
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new UpdateAppointmentCommand
        {
            AppointmentId = 41,
            AppointmentDate = DateTime.Now.AddDays(2),
            Reason = "Revisión general",
            PetId = 3,
            MedicId = 1,
            State = 1,
            AuditUpdateUser = 1002,
            AppointmentDetail = new List<UpdateAppointmentDetailCommand>
            {
                new()
                {
                    AppointmentDetailId = 41,
                    Diagnosis = "Mejorando",
                    Treatment = "Continuar tratamiento",
                    Observations = "Reacción positiva"
                }
            }
        };

        var response = await mediator.Send(command);

        Assert.IsTrue(response.IsSuccess);
    }

    private async Task AssertValidationError(Action<UpdateAppointmentCommand> updateCommand)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new UpdateAppointmentCommand
        {
            AppointmentId = 22,
            AppointmentDate = DateTime.Now,
            Reason = "Control",
            PetId = 3,
            MedicId = 1,
            State = 1,
            AuditUpdateUser = 1002,
            AppointmentDetail = new List<UpdateAppointmentDetailCommand>
            {
                new()
                {
                    AppointmentDetailId = 21,
                    Diagnosis = "Todo bien",
                    Treatment = "Ninguno",
                    Observations = "Sin cambios"
                }
            }
        };

        updateCommand(command);

        try
        {
            var response = await mediator.Send(command);
            Assert.Fail("Expected validation exception but got success.");
        }
        catch (ValidationException ex)
        {
            Assert.IsNotNull(ex.Errors);
        }
    }
}
