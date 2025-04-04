// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.UseCases.ProductCategory.Commands.CreateCommand;
using backend.Application.UseCases.ProductCategory.Commands.DeleteCommand;
using backend.Application.UseCases.ProductCategory.Commands.UpdateCommand;
using backend.Application.UseCases.ProductCategory.Queries.GetAllQuery;
using backend.Application.UseCases.ProductCategory.Queries.GetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace backend.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ProductCategoryList([FromQuery] GetAllProductCategoryQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> ProductCategoryById(int userId)
        {
            var response = await _mediator.Send(new GetProductCategoryByIdQuery() { CategoryId = userId });
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> ProductCategoryCreate([FromBody] CreateProductCategoryCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> ProductCategoryUpdate([FromBody] UpdateProductCategoryCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("Delete/{userId:int}")]
        public async Task<IActionResult> ProductCategoryDelete(int userId)
        {
            var response = await _mediator.Send(new DeleteProductCategoryCommand() { ProductCategoryId = userId });
            return Ok(response);
        }
    }
}