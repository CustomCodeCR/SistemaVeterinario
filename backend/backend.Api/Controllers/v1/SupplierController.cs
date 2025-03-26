// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.UseCases.Supplier.Commands.CreateCommand;
using backend.Application.UseCases.Supplier.Commands.DeleteCommand;
using backend.Application.UseCases.Supplier.Commands.UpdateCommand;
using backend.Application.UseCases.Supplier.Queries.GetAllQuery;
using backend.Application.UseCases.Supplier.Queries.GetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace backend.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SupplierController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> SupplierList([FromQuery] GetAllSupplierQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> SupplierById(int userId)
        {
            var response = await _mediator.Send(new GetSupplierByIdQuery() { SupplierId = userId });
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> SupplierCreate([FromBody] CreateSupplierCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> SupplierUpdate([FromBody] UpdateSupplierCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("Delete/{userId:int}")]
        public async Task<IActionResult> SupplierDelete(int userId)
        {
            var response = await _mediator.Send(new DeleteSupplierCommand() { SupplierId = userId });
            return Ok(response);
        }
    }
}