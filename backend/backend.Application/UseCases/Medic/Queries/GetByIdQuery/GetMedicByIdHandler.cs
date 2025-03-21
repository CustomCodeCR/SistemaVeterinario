// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Commons.Bases;
using backend.Application.Dtos.Medic.Response;
using backend.Application.Interfaces.Services;
using backend.Utilities.Static;
using MediatR;
using WatchDog;

namespace backend.Application.UseCases.Medic.Queries.GetByIdQuery;

public class GetMedicByIdHandler : IRequestHandler<GetMedicByIdQuery, BaseResponse<MedicByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetMedicByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<MedicByIdResponseDto>> Handle(GetMedicByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<MedicByIdResponseDto>();

        try
        {
            var client = await _unitOfWork.Medic.GetByIdAsync(request.MedicId);

            if (client is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<MedicByIdResponseDto>(client);
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