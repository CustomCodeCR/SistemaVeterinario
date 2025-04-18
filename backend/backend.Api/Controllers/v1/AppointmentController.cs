﻿// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.UseCases.Appointment.Commands.CreateCommand;
using backend.Application.UseCases.Appointment.Commands.DeleteCommand;
using backend.Application.UseCases.Appointment.Commands.UpdateCommand;
using backend.Application.UseCases.Appointment.Queries.GetAllByMedicQuery;
using backend.Application.UseCases.Appointment.Queries.GetAllByPetQuery;
using backend.Application.UseCases.Appointment.Queries.GetAllQuery;
using backend.Application.UseCases.Appointment.Queries.GetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace backend.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IOutputCacheStore _cacheStore;

        public AppointmentController(IMediator mediator, IOutputCacheStore cacheStore)
        {
            _mediator = mediator;
            _cacheStore = cacheStore;
        }

        [HttpGet]
        [OutputCache(PolicyName = "appointment")]
        public async Task<IActionResult> AppointmentList([FromQuery] GetAllAppointmentQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("Pet/")]
        public async Task<IActionResult> AppointmentListByPet([FromQuery] GetAllAppointmentByPetQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("Medic/")]
        public async Task<IActionResult> AppointmentListByMedic([FromQuery] GetAllAppointmentByMedicQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{appointmentId:int}")]
        public async Task<IActionResult> AppointmentById(int appointmentId)
        {
            var response = await _mediator.Send(new GetAppointmentByIdQuery() { AppointmentId = appointmentId });
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> AppointmentCreate([FromBody] CreateAppointmentCommand command)
        {
            var response = await _mediator.Send(command);
            await _cacheStore.EvictByTagAsync("appointment", default);
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> AppointmentUpdate([FromBody] UpdateAppointmentCommand command)
        {
            var response = await _mediator.Send(command);
            await _cacheStore.EvictByTagAsync("appointment", default);
            return Ok(response);
        }

        [HttpDelete("Delete/{appointmentId:int}")]
        public async Task<IActionResult> AppointmentDelete(int appointmentId)
        {
            var response = await _mediator.Send(new DeleteAppointmentCommand() { AppointmentId = appointmentId });
            await _cacheStore.EvictByTagAsync("appointment", default);
            return Ok(response);
        }
    }
}