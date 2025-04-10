// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.UseCases.Inventory.Commands.CreateCommand;
using backend.Application.UseCases.Inventory.Commands.DeleteCommand;
using backend.Application.UseCases.Inventory.Commands.UpdateCommand;
using backend.Application.UseCases.Inventory.Queries.GetAllQuery;
using backend.Application.UseCases.Inventory.Queries.GetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace backend.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IOutputCacheStore _cacheStore;

        public InventoryController(IMediator mediator, IOutputCacheStore cacheStore)
        {
            _mediator = mediator;
            _cacheStore = cacheStore;
        }

        [HttpGet]
        [OutputCache(PolicyName = "inventory")]
        public async Task<IActionResult> InventoryList([FromQuery] GetAllInventoryQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{inventoryId:int}")]
        public async Task<IActionResult> InventoryById(int inventoryId)
        {
            var response = await _mediator.Send(new GetInventoryByIdQuery() { InventoryId = inventoryId });
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> InventoryCreate([FromBody] CreateInventoryCommand command)
        {
            var response = await _mediator.Send(command);
            await _cacheStore.EvictByTagAsync("inventory", default);
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> InventoryUpdate([FromBody] UpdateInventoryCommand command)
        {
            var response = await _mediator.Send(command);
            await _cacheStore.EvictByTagAsync("inventory", default);
            return Ok(response);
        }

        [HttpDelete("Delete/{inventoryId:int}")]
        public async Task<IActionResult> InventoryDelete(int inventoryId)
        {
            var response = await _mediator.Send(new DeleteInventoryCommand() { InventoryId = inventoryId });
            await _cacheStore.EvictByTagAsync("inventory", default);
            return Ok(response);
        }
    }
}