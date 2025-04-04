// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Dtos.Payment.Response;
using backend.Application.UseCases.Payment.Commands.CreateCommand;
using backend.Application.UseCases.Payment.Commands.UpdateCommand;
using backend.Domain.Entities;
using backend.Utilities.Static;

namespace backend.Application.Mappings;

public class PaymentMapping : Profile
{
    public PaymentMapping()
    {
        CreateMap<Payment, PaymentResponseDto>()
            .ForMember(x => x.PaymentId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.SaleId, x => x.MapFrom(y => y.Saleid))
            .ForMember(x => x.StatePayment, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
        .ReverseMap();

        CreateMap<Payment, PaymentByIdResponseDto>()
            .ForMember(x => x.PaymentId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.SaleId, x => x.MapFrom(y => y.Saleid))
            .ReverseMap();

        CreateMap<CreatePaymentCommand, Payment>();

        CreateMap<UpdatePaymentCommand, Payment>();
    }
}