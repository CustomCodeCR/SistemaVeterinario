// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Commons.Bases;
using backend.Application.Dtos.Sale.Response;
using backend.Application.Interfaces.Services;
using backend.Application.UseCases.Sale.Queries.GetByIdQuery;
using backend.Utilities.Static;
using MediatR;
using WatchDog;

public class GetSaleByIdHandler : IRequestHandler<GetSaleByIdQuery, BaseResponse<SaleByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSaleByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<SaleByIdResponseDto>> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<SaleByIdResponseDto>();

        try
        {
            var appointment = await _unitOfWork.Sale.GetByIdAsync(request.SaleId);

            if (appointment == null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var appointmentDetails = await _unitOfWork.SaleDetail.GetSaleDetailBySaleId(request.SaleId);
            appointment.Saledetails = appointmentDetails.ToList();

            appointment.AuditCreateDate = appointment.AuditCreateDate.Date;

            response.IsSuccess = true;
            response.Data = _mapper.Map<SaleByIdResponseDto>(appointment);
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