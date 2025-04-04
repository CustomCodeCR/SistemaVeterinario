using backend.Application.Interfaces.Persistence;
using backend.Domain.Entities;
using backend.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Persistence.Repositories;

public class ProductCategoryRelationRepository : GenericRepository<Productcategoryrelation>, IProductCategoryRelationRepository
{
    private readonly ApplicationDbContext _context;

    public ProductCategoryRelationRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Productcategoryrelation>> GetProductCategoryRelationByCategory(int productId)
    {
        return await _context.Productcategoryrelations
            .AsNoTracking()
            .Join(
                _context.Productcategories,
                relation => relation.Categoryid,
                category => category.Id,
                (relation, category) => new { Relation = relation, Category = category }
            )
            .Where(x => x.Relation.Productid == productId)
            .OrderBy(x => x.Category.Id)
            .Select(x => new Productcategoryrelation
            {
                Productid = x.Relation.Productid,
                Categoryid = x.Relation.Categoryid,
                State = x.Relation.State,
                AuditCreateDate = x.Relation.AuditCreateDate,
                AuditCreateUser = x.Relation.AuditCreateUser,
                AuditUpdateDate = x.Relation.AuditUpdateDate,
                AuditUpdateUser = x.Relation.AuditUpdateUser,
                Category = x.Category
            })
            .ToListAsync();
    }

    public async Task<Productcategoryrelation> GetProductCategoryRelationByProductId(int productId, int categoryId)
    {
        var relations = await _context.Productcategoryrelations
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Productid == productId && x.Categoryid == categoryId);

        return relations!;
    }
}