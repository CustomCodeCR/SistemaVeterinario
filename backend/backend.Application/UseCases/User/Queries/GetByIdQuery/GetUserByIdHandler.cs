// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Commons.Bases;
using backend.Application.Dtos.User.Response;
using backend.Application.Interfaces.Services;
using backend.Utilities.Static;
using MediatR;
using WatchDog;

namespace backend.Application.UseCases.User.Queries.GetByIdQuery;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, BaseResponse<UserByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<UserByIdResponseDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<UserByIdResponseDto>();

        try
        {
            var user = await _unitOfWork.User.GetByIdAsync(request.UserId);

            if (user is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<UserByIdResponseDto>(user);
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