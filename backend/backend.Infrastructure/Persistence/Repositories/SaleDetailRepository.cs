// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Interfaces.Persistence;
using backend.Domain.Entities;
using backend.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Persistence.Repositories;

public class SaleDetailRepository : GenericRepository<Saledetail>, ISaleDetailRepository
{
    private readonly ApplicationDbContext _context;

    public SaleDetailRepository(ApplicationDbContext context) : base(context) 
    {
        _context = context;
    }

    public async Task<IEnumerable<Saledetail>> GetSaleDetailBySaleId(int id)
    {
        var response = await _context.Products
                .AsNoTracking()
                .Join(_context.Saledetails, p => p.Id, pd => pd.Productid, (p, pd) => new { Product = p, SaleDetail = pd })
                .Where(x => x.SaleDetail.Saleid == id)
        .Select(x => new Saledetail
        {
            Productid = x.Product.Id,
            Product = new Product
            {
                Name = x.Product.Name,
            },
            Quantity = x.SaleDetail.Quantity,
            Price = x.SaleDetail.Price,
        })
                .ToListAsync();

        return response!;
    }
}