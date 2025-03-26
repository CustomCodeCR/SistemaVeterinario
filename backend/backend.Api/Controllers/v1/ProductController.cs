// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.UseCases.Product.Commands.CreateCommand;
using backend.Application.UseCases.Product.Commands.DeleteCommand;
using backend.Application.UseCases.Product.Commands.UpdateCommand;
using backend.Application.UseCases.Product.Queries.GetAllQuery;
using backend.Application.UseCases.Product.Queries.GetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace backend.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ProductList([FromQuery] GetAllProductQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> ProductById(int userId)
        {
            var response = await _mediator.Send(new GetProductByIdQuery() { ProductId = userId });
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> ProductCreate([FromBody] CreateProductCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> ProductUpdate([FromBody] UpdateProductCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("Delete/{userId:int}")]
        public async Task<IActionResult> ProductDelete(int userId)
        {
            var response = await _mediator.Send(new DeleteProductCommand() { ProductId = userId });
            return Ok(response);
        }
    }
}