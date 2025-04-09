// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.UseCases.AppliedVaccine.Commands.CreateCommand;
using backend.Application.UseCases.AppliedVaccine.Commands.DeleteCommand;
using backend.Application.UseCases.AppliedVaccine.Commands.UpdateCommand;
using backend.Application.UseCases.AppliedVaccine.Queries.GetAllQuery;
using backend.Application.UseCases.AppliedVaccine.Queries.GetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace backend.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AppliedVaccineController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IOutputCacheStore _cacheStore;

        public AppliedVaccineController(IMediator mediator, IOutputCacheStore cacheStore)
        {
            _mediator = mediator;
            _cacheStore = cacheStore;
        }

        [HttpGet]
        [OutputCache(PolicyName = "applied-vaccine")]
        public async Task<IActionResult> AppliedVaccineList([FromQuery] GetAllAppliedVaccineQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> AppliedVaccineById(int userId)
        {
            var response = await _mediator.Send(new GetAppliedVaccineByIdQuery() { AppliedVaccineId = userId });
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> AppliedVaccineCreate([FromBody] CreateAppliedVaccineCommand command)
        {
            var response = await _mediator.Send(command);
            await _cacheStore.EvictByTagAsync("applied-vaccine", default);
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> AppliedVaccineUpdate([FromBody] UpdateAppliedVaccineCommand command)
        {
            var response = await _mediator.Send(command);
            await _cacheStore.EvictByTagAsync("applied-vaccine", default);
            return Ok(response);
        }

        [HttpDelete("Delete/{userId:int}")]
        public async Task<IActionResult> AppliedVaccineDelete(int userId)
        {
            var response = await _mediator.Send(new DeleteAppliedVaccineCommand() { AppliedVaccineId = userId });
            await _cacheStore.EvictByTagAsync("applied-vaccine", default);
            return Ok(response);
        }
    }
}