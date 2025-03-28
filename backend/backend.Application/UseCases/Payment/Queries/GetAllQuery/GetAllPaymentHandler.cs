using AutoMapper;
using backend.Application.Commons.Bases;
using backend.Application.Dtos.Payment.Response;
using backend.Application.Interfaces.Services;
using backend.Utilities.Static;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WatchDog;

namespace backend.Application.UseCases.Payment.Queries.GetAllQuery;

public class GetAllPaymentHandler : IRequestHandler<GetAllPaymentQuery, BaseResponse<IEnumerable<PaymentResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

public GetAllPaymentHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
{
    _unitOfWork = unitOfWork;
    _mapper = mapper;
    _ordering = ordering;
}

public async Task<BaseResponse<IEnumerable<PaymentResponseDto>>> Handle(GetAllPaymentQuery request, CancellationToken cancellationToken)
{
    var response = new BaseResponse<IEnumerable<PaymentResponseDto>>();

    try
    {
        var payments = _unitOfWork.Payment.GetAllQueryable()
                .Include(x => x.User)
                .AsQueryable();

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
        {
            switch (request.NumFilter)
            {
                case 1:
                    payments = payments.Where(x => x.Address.Contains(request.TextFilter));
                    break;
                case 2:
                    payments = payments.Where(x => x.Phone!.Contains(request.TextFilter));
                    break;
            }
        }

        if (request.StateFilter is not null)
        {
            payments = payments.Where(x => x.State == request.StateFilter);
        }

        if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
        {
            payments = payments.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate).ToUniversalTime() &&
                                     x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).ToUniversalTime().AddDays(1));
        }

        request.Sort ??= "Id";

        var items = await _ordering.Ordering(request, payments)
            .ToListAsync(cancellationToken);

        response.IsSuccess = true;
        response.TotalRecords = await payments.CountAsync(cancellationToken);
        response.Data = _mapper.Map<IEnumerable<PaymentResponseDto>>(items);
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