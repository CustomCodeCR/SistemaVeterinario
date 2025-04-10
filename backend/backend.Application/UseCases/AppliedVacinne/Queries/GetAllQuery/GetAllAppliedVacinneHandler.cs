using AutoMapper;
using backend.Application.Commons.Bases;
using backend.Application.Dtos.AppliedVaccine.Response;
using backend.Application.Interfaces.Services;
using backend.Utilities.Static;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WatchDog;

namespace backend.Application.UseCases.AppliedVaccine.Queries.GetAllQuery;

public class GetAllAppliedVaccineHandler : IRequestHandler<GetAllAppliedVaccineQuery, BaseResponse<IEnumerable<AppliedVaccineResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

public GetAllAppliedVaccineHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
{
    _unitOfWork = unitOfWork;
    _mapper = mapper;
    _ordering = ordering;
}

public async Task<BaseResponse<IEnumerable<AppliedVaccineResponseDto>>> Handle(GetAllAppliedVaccineQuery request, CancellationToken cancellationToken)
{
    var response = new BaseResponse<IEnumerable<AppliedVaccineResponseDto>>();

    try
    {
        var appliedVaccines = _unitOfWork.AppliedVaccine.GetAllQueryable()
                .Include(x => x.Vaccine)
                .Include(x => x.Pet)
                .AsQueryable();

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
        {
            switch (request.NumFilter)
            {
                case 1:
                    appliedVaccines = appliedVaccines.Where(x => x.Vaccine.Vaccinename.Contains(request.TextFilter));
                    break;
                case 2:
                    appliedVaccines = appliedVaccines.Where(x => x.Pet.Name!.Contains(request.TextFilter));
                    break;
            }
        }

        if (request.StateFilter is not null)
        {
            appliedVaccines = appliedVaccines.Where(x => x.State == request.StateFilter);
        }

        if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
        {
            appliedVaccines = appliedVaccines.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate).ToUniversalTime() &&
                                     x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).ToUniversalTime().AddDays(1));
        }

        request.Sort ??= "Id";

        var items = await _ordering.Ordering(request, appliedVaccines)
            .ToListAsync(cancellationToken);

        response.IsSuccess = true;
        response.TotalRecords = await appliedVaccines.CountAsync(cancellationToken);
        response.Data = _mapper.Map<IEnumerable<AppliedVaccineResponseDto>>(items);
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