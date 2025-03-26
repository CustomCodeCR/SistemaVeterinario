// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Commons.Select.Response;
using backend.Application.Dtos.Supplier.Response;
using backend.Application.UseCases.Supplier.Commands.CreateCommand;
using backend.Application.UseCases.Supplier.Commands.UpdateCommand;
using backend.Domain.Entities;
using backend.Utilities.Static;

namespace backend.Application.Mappings;

public class SupplierMapping : Profile
{
    public SupplierMapping()
    {
        CreateMap<Supplier, SupplierResponseDto>()
            .ForMember(x => x.SupplierId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.StateSupplier, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
        .ReverseMap();

        CreateMap<Supplier, SupplierByIdResponseDto>()
            .ForMember(x => x.SupplierId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        CreateMap<CreateSupplierCommand, Supplier>();

        CreateMap<UpdateSupplierCommand, Supplier>();
    }
}