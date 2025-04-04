using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.Sale.Commands.DeleteCommand;

public class DeleteSaleCommand : IRequest<BaseResponse<bool>>
{
    public int SaleId { get; set; }
    public int AuditDeleteUser { get; set; }
}