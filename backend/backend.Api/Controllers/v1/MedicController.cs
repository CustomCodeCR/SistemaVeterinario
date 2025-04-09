// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.UseCases.Medic.Commands.CreateCommand;
using backend.Application.UseCases.Medic.Commands.DeleteCommand;
using backend.Application.UseCases.Medic.Commands.UpdateCommand;
using backend.Application.UseCases.Medic.Queries.GetAllQuery;
using backend.Application.UseCases.Medic.Queries.GetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace backend.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class MedicController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IOutputCacheStore _cacheStore;

        public MedicController(IMediator mediator, IOutputCacheStore cacheStore)
        {
            _mediator = mediator;
            _cacheStore = cacheStore;
        }

        [HttpGet]
        [OutputCache(PolicyName = "medic")]
        public async Task<IActionResult> MedicList([FromQuery] GetAllMedicQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> MedicById(int userId)
        {
            var response = await _mediator.Send(new GetMedicByIdQuery() { MedicId = userId });
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> MedicCreate([FromBody] CreateMedicCommand command)
        {
            var response = await _mediator.Send(command);
            await _cacheStore.EvictByTagAsync("medic", default);
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> MedicUpdate([FromBody] UpdateMedicCommand command)
        {
            var response = await _mediator.Send(command);
            await _cacheStore.EvictByTagAsync("medic", default);
            return Ok(response);
        }

        [HttpDelete("Delete/{userId:int}")]
        public async Task<IActionResult> MedicDelete(int userId)
        {
            var response = await _mediator.Send(new DeleteMedicCommand() { MedicId = userId });
            await _cacheStore.EvictByTagAsync("medic", default);
            return Ok(response);
        }
    }
}