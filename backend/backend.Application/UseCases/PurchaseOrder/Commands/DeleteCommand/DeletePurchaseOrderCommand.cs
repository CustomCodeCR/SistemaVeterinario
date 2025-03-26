using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.PurchaseOrder.Commands.DeleteCommand;

public class DeletePurchaseOrderCommand : IRequest<BaseResponse<bool>>
{
    public int PurchaseOrderId { get; set; }
    public int AuditDeleteUser { get; set; }
}