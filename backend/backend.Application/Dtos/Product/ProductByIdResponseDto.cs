namespace backend.Application.Dtos.Product.Response;

public class ProductByIdResponseDto
{
    public int ProductId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public int State { get; set; }
    public int AuditCreateUser { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public int? AuditUpdateUser { get; set; }
    public DateTime? AuditUpdateDate { get; set; }
    public int? AuditDeleteUser { get; set; }
    public DateTime? AuditDeleteDate { get; set; }
}