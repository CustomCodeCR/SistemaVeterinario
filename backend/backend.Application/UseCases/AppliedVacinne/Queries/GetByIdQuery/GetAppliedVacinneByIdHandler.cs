// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Commons.Bases;
using backend.Application.Dtos.AppliedVaccine.Response;
using backend.Application.Interfaces.Services;
using backend.Utilities.Static;
using MediatR;
using WatchDog;

namespace backend.Application.UseCases.AppliedVaccine.Queries.GetByIdQuery;

public class GetAppliedVaccineByIdHandler : IRequestHandler<GetAppliedVaccineByIdQuery, BaseResponse<AppliedVaccineByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAppliedVaccineByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<AppliedVaccineByIdResponseDto>> Handle(GetAppliedVaccineByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<AppliedVaccineByIdResponseDto>();

        try
        {
            var AppliedVaccine = await _unitOfWork.AppliedVaccine.GetByIdAsync(request.AppliedVaccineId);

            if (AppliedVaccine is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<AppliedVaccineByIdResponseDto>(AppliedVaccine);
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