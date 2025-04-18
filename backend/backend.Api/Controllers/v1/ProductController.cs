﻿// -----------------------------------------------------------------------------
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
using Microsoft.AspNetCore.OutputCaching;

namespace backend.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IOutputCacheStore _cacheStore;

        public ProductController(IMediator mediator, IOutputCacheStore cacheStore)
        {
            _mediator = mediator;
            _cacheStore = cacheStore;
        }

        [HttpGet]
        [OutputCache(PolicyName = "product")]
        public async Task<IActionResult> ProductList([FromQuery] GetAllProductQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{productId:int}")]
        public async Task<IActionResult> ProductById(int productId)
        {
            var response = await _mediator.Send(new GetProductByIdQuery() { ProductId = productId });
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> ProductCreate([FromForm] CreateProductCommand command)
        {
            var response = await _mediator.Send(command);
            await _cacheStore.EvictByTagAsync("product", default);
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> ProductUpdate([FromForm] UpdateProductCommand command)
        {
            var response = await _mediator.Send(command);
            await _cacheStore.EvictByTagAsync("product", default);
            return Ok(response);
        }

        [HttpDelete("Delete/{productId:int}")]
        public async Task<IActionResult> ProductDelete(int productId)
        {
            var response = await _mediator.Send(new DeleteProductCommand() { ProductId = productId });
            await _cacheStore.EvictByTagAsync("product", default);
            return Ok(response);
        }
    }
}