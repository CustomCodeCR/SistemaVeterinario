﻿using AutoMapper;
using backend.Application.Commons.Bases;
using backend.Application.Dtos.Client.Response;
using backend.Application.Interfaces.Services;
using backend.Utilities.Static;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WatchDog;

namespace backend.Application.UseCases.Client.Queries.GetAllQuery;

public class GetAllClientHandler : IRequestHandler<GetAllClientQuery, BaseResponse<IEnumerable<ClientResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

public GetAllClientHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
{
    _unitOfWork = unitOfWork;
    _mapper = mapper;
    _ordering = ordering;
}

public async Task<BaseResponse<IEnumerable<ClientResponseDto>>> Handle(GetAllClientQuery request, CancellationToken cancellationToken)
{
    var response = new BaseResponse<IEnumerable<ClientResponseDto>>();

    try
    {
        var clients = _unitOfWork.Client.GetAllQueryable()
                .Include(x => x.User)
                .AsQueryable();

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
        {
            switch (request.NumFilter)
            {
                case 1:
                    clients = clients.Where(x => x.Address.Contains(request.TextFilter));
                    break;
                case 2:
                    clients = clients.Where(x => x.Phone!.Contains(request.TextFilter));
                    break;
            }
        }

        if (request.StateFilter is not null)
        {
            clients = clients.Where(x => x.State == request.StateFilter);
        }

        if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
        {
            clients = clients.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate).ToUniversalTime() &&
                                     x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).ToUniversalTime().AddDays(1));
        }

        request.Sort ??= "Id";

        var items = await _ordering.Ordering(request, clients)
            .ToListAsync(cancellationToken);

        response.IsSuccess = true;
        response.TotalRecords = await clients.CountAsync(cancellationToken);
        response.Data = _mapper.Map<IEnumerable<ClientResponseDto>>(items);
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