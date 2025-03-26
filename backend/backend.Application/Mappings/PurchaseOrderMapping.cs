// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Dtos.PurchaseOrder.Response;
using backend.Application.UseCases.PurchaseOrder.Commands.CreateCommand;
using backend.Application.UseCases.PurchaseOrder.Commands.UpdateCommand;
using backend.Domain.Entities;
using backend.Utilities.Static;

namespace backend.Application.Mappings;

public class PurchaseOrderMapping : Profile
{
    public PurchaseOrderMapping()
    {
        CreateMap<Purchaseorder, PurchaseOrderResponseDto>()
            .ForMember(x => x.PurchaseOrderId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Supplier, x => x.MapFrom(y => y.Supplier.Name))
            .ForMember(x => x.StatePurchaseOrder, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
        .ReverseMap();

        CreateMap<Purchaseorder, PurchaseOrderByIdResponseDto>()
            .ForMember(x => x.PurchaseOrderId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.SupplierId, x => x.MapFrom(y => y.Supplierid))
            .ReverseMap();

        CreateMap<Purchaseorderdetail, PurchaseOrderDetailByIdResponseDto>()
            .ForMember(x => x.ProductId, x => x.MapFrom(y => y.Productid))
            .ForMember(x => x.Name, x => x.MapFrom(y => y.Product.Name))
            .ReverseMap();

        CreateMap<CreatePurchaseOrderCommand, Purchaseorder>();

        CreateMap<UpdatePurchaseOrderCommand, Purchaseorder>();
    }
}