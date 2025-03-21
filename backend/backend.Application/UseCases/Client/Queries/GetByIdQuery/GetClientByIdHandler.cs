// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Commons.Bases;
using backend.Application.Dtos.Client.Response;
using backend.Application.Interfaces.Services;
using backend.Utilities.Static;
using MediatR;
using WatchDog;

namespace backend.Application.UseCases.Client.Queries.GetByIdQuery;

public class GetClientByIdHandler : IRequestHandler<GetClientByIdQuery, BaseResponse<ClientByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetClientByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<ClientByIdResponseDto>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<ClientByIdResponseDto>();

        try
        {
            var client = await _unitOfWork.Client.GetByIdAsync(request.ClientId);

            if (client is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<ClientByIdResponseDto>(client);
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