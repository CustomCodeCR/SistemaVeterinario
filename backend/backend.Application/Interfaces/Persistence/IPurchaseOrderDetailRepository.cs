// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;

namespace backend.Application.Interfaces.Persistence;

public interface IPurchaseOrderDetailRepository : IGenericRepository<Purchaseorderdetail>
{
    Task<IEnumerable<Purchaseorderdetail>> GetPurchaseOrderDetailByPurchaseOrderId(int id);
}