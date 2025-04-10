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
using Microsoft.AspNetCore.OutputCaching;

namespace backend.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IOutputCacheStore _cacheStore;

        public ProductCategoryController(IMediator mediator, IOutputCacheStore cacheStore)
        {
            _mediator = mediator;
            _cacheStore = cacheStore;
        }

        [HttpGet]
        [OutputCache(PolicyName = "product-category")]
        public async Task<IActionResult> ProductCategoryList([FromQuery] GetAllProductCategoryQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{productCategoryId:int}")]
        public async Task<IActionResult> ProductCategoryById(int productCategoryId)
        {
            var response = await _mediator.Send(new GetProductCategoryByIdQuery() { CategoryId = productCategoryId });
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> ProductCategoryCreate([FromBody] CreateProductCategoryCommand command)
        {
            var response = await _mediator.Send(command);
            await _cacheStore.EvictByTagAsync("product-category", default);
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> ProductCategoryUpdate([FromBody] UpdateProductCategoryCommand command)
        {
            var response = await _mediator.Send(command);
            await _cacheStore.EvictByTagAsync("product-category", default);
            return Ok(response);
        }

        [HttpDelete("Delete/{productCategoryId:int}")]
        public async Task<IActionResult> ProductCategoryDelete(int productCategoryId)
        {
            var response = await _mediator.Send(new DeleteProductCategoryCommand() { ProductCategoryId = productCategoryId });
            await _cacheStore.EvictByTagAsync("product-category", default);
            return Ok(response);
        }
    }
}