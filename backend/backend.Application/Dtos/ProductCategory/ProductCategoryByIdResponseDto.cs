namespace backend.Application.Dtos.ProductCategory.Response;

public class ProductCategoryByIdResponseDto
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int State { get; set; }
    public int AuditCreateUser { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public int? AuditUpdateUser { get; set; }
    public DateTime? AuditUpdateDate { get; set; }
    public int? AuditDeleteUser { get; set; }
    public DateTime? AuditDeleteDate { get; set; }
}