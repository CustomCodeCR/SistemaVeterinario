// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.UseCases.Pet.Commands.CreateCommand;
using backend.Application.UseCases.Pet.Commands.DeleteCommand;
using backend.Application.UseCases.Pet.Commands.UpdateCommand;
using backend.Application.UseCases.Pet.Queries.GetAllQuery;
using backend.Application.UseCases.Pet.Queries.GetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace backend.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> PetList([FromQuery] GetAllPetQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> PetById(int userId)
        {
            var response = await _mediator.Send(new GetPetByIdQuery() { PetId = userId });
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> PetCreate([FromBody] CreatePetCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> PetUpdate([FromBody] UpdatePetCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("Delete/{userId:int}")]
        public async Task<IActionResult> PetDelete(int userId)
        {
            var response = await _mediator.Send(new DeletePetCommand() { PetId = userId });
            return Ok(response);
        }
    }
}