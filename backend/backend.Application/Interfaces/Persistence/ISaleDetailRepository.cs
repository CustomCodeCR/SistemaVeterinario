// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;

namespace backend.Application.Interfaces.Persistence;

public interface ISaleDetailRepository : IGenericRepository<Saledetail>
{
    Task<IEnumerable<Saledetail>> GetSaleDetailBySaleId(int id);
}