// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Commons.Bases;
using backend.Application.Dtos.PurchaseOrder.Response;
using backend.Application.Interfaces.Services;
using backend.Application.UseCases.PurchaseOrder.Queries.GetByIdQuery;
using backend.Utilities.Static;
using MediatR;
using WatchDog;

public class GetPurchaseOrderByIdHandler : IRequestHandler<GetPurchaseOrderByIdQuery, BaseResponse<PurchaseOrderByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPurchaseOrderByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<PurchaseOrderByIdResponseDto>> Handle(GetPurchaseOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<PurchaseOrderByIdResponseDto>();

        try
        {
            var appointment = await _unitOfWork.PurchaseOrder.GetByIdAsync(request.PurchaseOrderId);

            if (appointment == null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var appointmentDetails = await _unitOfWork.PurchaseOrderDetail.GetPurchaseOrderDetailByPurchaseOrderId(request.PurchaseOrderId);
            appointment.Purchaseorderdetails = appointmentDetails.ToList();

            appointment.AuditCreateDate = appointment.AuditCreateDate.Date;

            response.IsSuccess = true;
            response.Data = _mapper.Map<PurchaseOrderByIdResponseDto>(appointment);
            response.Message = ReplyMessage.MESSAGE_QUERY;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
            WatchLogger.LogError(ex.Message);
        }

        return response;
    }
}