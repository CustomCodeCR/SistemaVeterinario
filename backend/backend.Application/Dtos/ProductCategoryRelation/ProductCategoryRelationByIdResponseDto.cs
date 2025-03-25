namespace backend.Application.Dtos.ProductCategoryRelation.Response;

public class ProductCategoryRelationByIdResponseDto
{
    public int RelationId { get; set; }
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public int State { get; set; }
}