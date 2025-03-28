// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.UseCases.Payment.Commands.CreateCommand;
using backend.Application.UseCases.Payment.Commands.DeleteCommand;
using backend.Application.UseCases.Payment.Commands.UpdateCommand;
using backend.Application.UseCases.Payment.Queries.GetAllQuery;
using backend.Application.UseCases.Payment.Queries.GetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace backend.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> PaymentList([FromQuery] GetAllPaymentQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> PaymentById(int userId)
        {
            var response = await _mediator.Send(new GetPaymentByIdQuery() { PaymentId = userId });
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> PaymentCreate([FromBody] CreatePaymentCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> PaymentUpdate([FromBody] UpdatePaymentCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("Delete/{userId:int}")]
        public async Task<IActionResult> PaymentDelete(int userId)
        {
            var response = await _mediator.Send(new DeletePaymentCommand() { PaymentId = userId });
            return Ok(response);
        }
    }
}