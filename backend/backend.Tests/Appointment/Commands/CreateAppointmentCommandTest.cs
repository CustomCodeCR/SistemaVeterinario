// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Commons.Exceptions;
using backend.Application.UseCases.Appointment.Commands.CreateCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Tests.Appointment.Commands;

[TestClass]
public class CreateAppointmentCommandTest
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
    public async Task ShouldGetValidationErrorsWhenRequiredFieldsAreMissing()
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new CreateAppointmentCommand
        {
            AppointmentDate = DateTime.MinValue, // Invalid date
            Reason = "",
            PetId = 0,
            MedicId = 0,
            State = 1,
            AuditCreateUser = 1,
            AppointmentDetail = new List<CreateAppointmentDetailCommand>
            {
                new()
                {
                    Diagnosis = "",
                    Treatment = "",
                    Observations = ""
                }
            }
        };

        BaseResponse<bool> response = new();

        try
        {
            response = await mediator.Send(command);
            Assert.Fail("Expected ValidationException but got success.");
        }
        catch (ValidationException ex)
        {
            Assert.IsNotNull(ex.Errors);
            Assert.AreEqual(false, response.IsSuccess);
        }
    }

    [TestMethod]
    public async Task ShouldCreateAppointmentSuccessfully()
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new CreateAppointmentCommand
        {
            AppointmentDate = DateTime.Now.AddDays(1),
            Reason = "Consulta general",
            PetId = 3,
            MedicId = 1,
            State = 1,
            AuditCreateUser = 1001,
            AppointmentDetail = new List<CreateAppointmentDetailCommand>
            {
                new()
                {
                    Diagnosis = "Dolor abdominal",
                    Treatment = "Desparasitante",
                    Observations = "Presenta mejora"
                }
            }
        };

        var response = await mediator.Send(command);

        Assert.IsTrue(response.IsSuccess);
    }

    [TestMethod]
    public async Task ShouldReturnValidationErrorWhenReasonIsEmpty()
    {
        await AssertValidationError(cmd => cmd.Reason = "");
    }

    [TestMethod]
    public async Task ShouldReturnValidationErrorWhenDiagnosisIsEmpty()
    {
        await AssertValidationError(cmd =>
        {
            cmd.AppointmentDetail.First().Diagnosis = "";
        });
    }

    private async Task AssertValidationError(Action<CreateAppointmentCommand> updateCommand)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new CreateAppointmentCommand
        {
            AppointmentDate = DateTime.Now,
            Reason = "Control",
            PetId = 1,
            MedicId = 1,
            State = 1,
            AuditCreateUser = 1001,
            AppointmentDetail = new List<CreateAppointmentDetailCommand>
            {
                new()
                {
                    Diagnosis = "Normal",
                    Treatment = "Ninguno",
                    Observations = "Sin cambios"
                }
            }
        };

        updateCommand(command);

        try
        {
            var response = await mediator.Send(command);
            Assert.Fail("Expected ValidationException but got success.");
        }
        catch (ValidationException ex)
        {
            Assert.IsNotNull(ex.Errors);
        }
    }
}