// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Commons.Bases;
using backend.Application.Dtos.Vaccine.Response;
using backend.Application.Interfaces.Services;
using backend.Utilities.Static;
using MediatR;
using WatchDog;

namespace backend.Application.UseCases.Vaccine.Queries.GetByIdQuery;

public class GetVaccineByIdHandler : IRequestHandler<GetVaccineByIdQuery, BaseResponse<VaccineByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetVaccineByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<VaccineByIdResponseDto>> Handle(GetVaccineByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<VaccineByIdResponseDto>();

        try
        {
            var client = await _unitOfWork.Vaccine.GetByIdAsync(request.VaccineId);

            if (client is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<VaccineByIdResponseDto>(client);
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