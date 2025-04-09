// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.UseCases.Sale.Commands.CreateCommand;
using backend.Application.UseCases.Sale.Commands.DeleteCommand;
using backend.Application.UseCases.Sale.Commands.UpdateCommand;
using backend.Application.UseCases.Sale.Queries.GetAllQuery;
using backend.Application.UseCases.Sale.Queries.GetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace backend.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IOutputCacheStore _cacheStore;

        public SaleController(IMediator mediator, IOutputCacheStore cacheStore)
        {
            _mediator = mediator;
            _cacheStore = cacheStore;
        }

        [HttpGet]
        [OutputCache(PolicyName = "sale")]
        public async Task<IActionResult> SaleList([FromQuery] GetAllSaleQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> SaleById(int userId)
        {
            var response = await _mediator.Send(new GetSaleByIdQuery() { SaleId = userId });
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> SaleCreate([FromBody] CreateSaleCommand command)
        {
            var response = await _mediator.Send(command);
            await _cacheStore.EvictByTagAsync("sale", default);
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> SaleUpdate([FromBody] UpdateSaleCommand command)
        {
            var response = await _mediator.Send(command);
            await _cacheStore.EvictByTagAsync("sale", default);
            return Ok(response);
        }

        [HttpDelete("Delete/{userId:int}")]
        public async Task<IActionResult> SaleDelete(int userId)
        {
            var response = await _mediator.Send(new DeleteSaleCommand() { SaleId = userId });
            await _cacheStore.EvictByTagAsync("sale", default);
            return Ok(response);
        }
    }
}