// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.UseCases.Client.Commands.CreateCommand;
using backend.Application.UseCases.Client.Commands.DeleteCommand;
using backend.Application.UseCases.Client.Commands.UpdateCommand;
using backend.Application.UseCases.Client.Queries.GetAllQuery;
using backend.Application.UseCases.Client.Queries.GetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace backend.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ClientList([FromQuery] GetAllClientQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> ClientById(int userId)
        {
            var response = await _mediator.Send(new GetClientByIdQuery() { ClientId = userId });
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> ClientCreate([FromBody] CreateClientCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> ClientUpdate([FromBody] UpdateClientCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("Delete/{userId:int}")]
        public async Task<IActionResult> ClientDelete(int userId)
        {
            var response = await _mediator.Send(new DeleteClientCommand() { ClientId = userId });
            return Ok(response);
        }
    }
}