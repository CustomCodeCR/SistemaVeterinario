// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.UseCases.Pet.Commands.CreateCommand;
using backend.Application.UseCases.Pet.Commands.DeleteCommand;
using backend.Application.UseCases.Pet.Commands.UpdateCommand;
using backend.Application.UseCases.Pet.Queries.GetAllByClientQuery;
using backend.Application.UseCases.Pet.Queries.GetAllQuery;
using backend.Application.UseCases.Pet.Queries.GetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace backend.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IOutputCacheStore _cacheStore;

        public PetController(IMediator mediator, IOutputCacheStore cacheStore)
        {
            _mediator = mediator;
            _cacheStore = cacheStore;
        }

        [HttpGet]
        [OutputCache(PolicyName = "pet")]
        public async Task<IActionResult> PetList([FromQuery] GetAllPetQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("Client/{clientId:int}")]
        public async Task<IActionResult> PetListByClient([FromQuery] GetAllPetByClientQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }


        [HttpGet("{petId:int}")]
        public async Task<IActionResult> PetById(int petId)
        {
            var response = await _mediator.Send(new GetPetByIdQuery() { PetId = petId });
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> PetCreate([FromBody] CreatePetCommand command)
        {
            var response = await _mediator.Send(command);
            await _cacheStore.EvictByTagAsync("pet", default);
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> PetUpdate([FromBody] UpdatePetCommand command)
        {
            var response = await _mediator.Send(command);
            await _cacheStore.EvictByTagAsync("pet", default);
            return Ok(response);
        }

        [HttpDelete("Delete/{petId:int}")]
        public async Task<IActionResult> PetDelete(int petId)
        {
            var response = await _mediator.Send(new DeletePetCommand() { PetId = petId });
            await _cacheStore.EvictByTagAsync("pet", default);
            return Ok(response);
        }
    }
}