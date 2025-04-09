﻿// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.UseCases.Vaccine.Commands.CreateCommand;
using backend.Application.UseCases.Vaccine.Commands.DeleteCommand;
using backend.Application.UseCases.Vaccine.Commands.UpdateCommand;
using backend.Application.UseCases.Vaccine.Queries.GetAllQuery;
using backend.Application.UseCases.Vaccine.Queries.GetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace backend.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class VaccineController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IOutputCacheStore _cacheStore;

        public VaccineController(IMediator mediator, IOutputCacheStore cacheStore)
        {
            _mediator = mediator;
            _cacheStore = cacheStore;
        }

        [HttpGet]
        [OutputCache(PolicyName = "vaccine")]
        public async Task<IActionResult> VaccineList([FromQuery] GetAllVaccineQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> VaccineById(int userId)
        {
            var response = await _mediator.Send(new GetVaccineByIdQuery() { VaccineId = userId });
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> VaccineCreate([FromBody] CreateVaccineCommand command)
        {
            var response = await _mediator.Send(command);
            await _cacheStore.EvictByTagAsync("vaccine", default);
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> VaccineUpdate([FromBody] UpdateVaccineCommand command)
        {
            var response = await _mediator.Send(command);
            await _cacheStore.EvictByTagAsync("vaccine", default);
            return Ok(response);
        }

        [HttpDelete("Delete/{userId:int}")]
        public async Task<IActionResult> VaccineDelete(int userId)
        {
            var response = await _mediator.Send(new DeleteVaccineCommand() { VaccineId = userId });
            await _cacheStore.EvictByTagAsync("vaccine", default);
            return Ok(response);
        }
    }
}