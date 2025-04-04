// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;

namespace backend.Application.Interfaces.Persistence;

public interface IProductCategoryRelationRepository : IGenericRepository<Productcategoryrelation>
{
    Task<Productcategoryrelation> GetProductCategoryRelationByProductId(int productId, int categoryId);
    Task<IEnumerable<Productcategoryrelation>> GetProductCategoryRelationByCategory(int productId);
}