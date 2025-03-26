// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.UseCases.PurchaseOrder.Commands.CreateCommand;
using backend.Application.UseCases.PurchaseOrder.Commands.DeleteCommand;
using backend.Application.UseCases.PurchaseOrder.Commands.UpdateCommand;
using backend.Application.UseCases.PurchaseOrder.Queries.GetAllQuery;
using backend.Application.UseCases.PurchaseOrder.Queries.GetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace backend.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PurchaseOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> PurchaseOrderList([FromQuery] GetAllPurchaseOrderQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> PurchaseOrderById(int userId)
        {
            var response = await _mediator.Send(new GetPurchaseOrderByIdQuery() { PurchaseOrderId = userId });
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> PurchaseOrderCreate([FromBody] CreatePurchaseOrderCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> PurchaseOrderUpdate([FromBody] UpdatePurchaseOrderCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("Delete/{userId:int}")]
        public async Task<IActionResult> PurchaseOrderDelete(int userId)
        {
            var response = await _mediator.Send(new DeletePurchaseOrderCommand() { PurchaseOrderId = userId });
            return Ok(response);
        }
    }
}