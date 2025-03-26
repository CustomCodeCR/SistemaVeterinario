// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Commons.Bases;
using backend.Application.Dtos.Supplier.Response;
using backend.Application.Interfaces.Services;
using backend.Utilities.Static;
using MediatR;
using WatchDog;

namespace backend.Application.UseCases.Supplier.Queries.GetByIdQuery;

public class GetSupplierByIdHandler : IRequestHandler<GetSupplierByIdQuery, BaseResponse<SupplierByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSupplierByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<SupplierByIdResponseDto>> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<SupplierByIdResponseDto>();

        try
        {
            var supplier = await _unitOfWork.Supplier.GetByIdAsync(request.SupplierId);

            if (supplier is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<SupplierByIdResponseDto>(supplier);
            response.Message = ReplyMessage.MESSAGE_QUERY;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            WatchLogger.LogError(ex.Message);
        }

        return response;
    }
}