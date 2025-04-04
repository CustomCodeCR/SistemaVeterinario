// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.UseCases.ProductCategoryRelation.Commands.CreateCommand;
using backend.Application.UseCases.ProductCategoryRelation.Commands.DeleteCommand;
using backend.Application.UseCases.ProductCategoryRelation.Commands.UpdateCommand;
using backend.Application.UseCases.ProductCategoryRelation.Queries.GetAllQuery;
using backend.Application.UseCases.ProductCategoryRelation.Queries.GetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace backend.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductCategoryRelationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductCategoryRelationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ProductCategoryRelationList([FromQuery] GetAllProductCategoryRelationQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{productId:int}/{categoryId:int}")]
        public async Task<IActionResult> ProductCategoryRelationById(int productId, int categoryId)
        {
            var response = await _mediator.Send(new GetProductCategoryRelationByIdQuery() { ProductId = productId, CategoryId = categoryId });
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> ProductCategoryRelationCreate([FromBody] CreateProductCategoryRelationCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> ProductCategoryRelationUpdate([FromBody] UpdateProductCategoryRelationCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("Delete/{productId:int}/{categoryId:int}")]
        public async Task<IActionResult> ProductCategoryRelationDelete(int productId, int categoryId)
        {
            var response = await _mediator.Send(new DeleteProductCategoryRelationCommand() { ProductId = productId, CategoryId = categoryId });
            return Ok(response);
        }
    }
}