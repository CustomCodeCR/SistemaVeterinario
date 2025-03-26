// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.UseCases.Appointment.Commands.CreateCommand;
using backend.Application.UseCases.Appointment.Commands.DeleteCommand;
using backend.Application.UseCases.Appointment.Commands.UpdateCommand;
using backend.Application.UseCases.Appointment.Queries.GetAllQuery;
using backend.Application.UseCases.Appointment.Queries.GetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace backend.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> AppointmentList([FromQuery] GetAllAppointmentQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> AppointmentById(int userId)
        {
            var response = await _mediator.Send(new GetAppointmentByIdQuery() { AppointmentId = userId });
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> AppointmentCreate([FromBody] CreateAppointmentCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> AppointmentUpdate([FromBody] UpdateAppointmentCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("Delete/{userId:int}")]
        public async Task<IActionResult> AppointmentDelete(int userId)
        {
            var response = await _mediator.Send(new DeleteAppointmentCommand() { AppointmentId = userId });
            return Ok(response);
        }
    }
}