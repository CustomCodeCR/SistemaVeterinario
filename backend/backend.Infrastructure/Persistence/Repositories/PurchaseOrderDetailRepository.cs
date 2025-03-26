// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Interfaces.Persistence;
using backend.Domain.Entities;
using backend.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Persistence.Repositories;

public class PurchaseOrderDetailRepository : GenericRepository<Purchaseorderdetail>, IPurchaseOrderDetailRepository
{
    private readonly ApplicationDbContext _context;

    public PurchaseOrderDetailRepository(ApplicationDbContext context) : base(context) 
    {
        _context = context;
    }

    public async Task<IEnumerable<Purchaseorderdetail>> GetPurchaseOrderDetailByPurchaseOrderId(int id)
    {
        var response = await _context.Products
                .AsNoTracking()
                .Join(_context.Purchaseorderdetails, p => p.Id, pd => pd.Productid, (p, pd) => new { Product = p, PurchaseDetail = pd })
                .Where(x => x.PurchaseDetail.Purchaseorderid == id)
        .Select(x => new Purchaseorderdetail
        {
            Productid = x.Product.Id,
            Product = new Product
            {
                Name = x.Product.Name,
            },
            Quantity = x.PurchaseDetail.Quantity,
            Unitprice = x.PurchaseDetail.Unitprice,
        })
                .ToListAsync();

        return response!;
    }
}