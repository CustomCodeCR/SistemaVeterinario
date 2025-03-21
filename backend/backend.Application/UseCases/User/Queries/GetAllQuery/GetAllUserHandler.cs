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
using Microsoft.EntityFrameworkCore;
using WatchDog;

namespace backend.Application.UseCases.User.Queries.GetAllQuery;

public class GetAllUserHandler : IRequestHandler<GetAllUserQuery, BaseResponse<IEnumerable<UserResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

    public GetAllUserHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ordering = ordering;
    }

    public async Task<BaseResponse<IEnumerable<UserResponseDto>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<UserResponseDto>>();

        try
        {
            var users = _unitOfWork.User.GetAllQueryable();

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        users = users.Where(x => x.Username.Contains(request.TextFilter));
                        break;
                    case 2:
                        users = users.Where(x => x.Email!.Contains(request.TextFilter));
                        break;
                }
            }

            if (request.StateFilter is not null)
            {
                users = users.Where(x => x.State == request.StateFilter);
            }

            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                users = users.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate).ToUniversalTime() &&
                                         x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).ToUniversalTime().AddDays(1));
            }

            request.Sort ??= "Id";

            var items = await _ordering.Ordering(request, users)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await users.CountAsync(cancellationToken);
            response.Data = _mapper.Map<IEnumerable<UserResponseDto>>(items);
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