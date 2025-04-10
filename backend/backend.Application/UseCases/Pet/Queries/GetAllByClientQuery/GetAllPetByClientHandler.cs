// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Commons.Bases;
using backend.Application.Dtos.Pet.Response;
using backend.Application.Interfaces.Services;
using backend.Utilities.Static;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WatchDog;

namespace backend.Application.UseCases.Pet.Queries.GetAllByClientQuery;

public class GetAllPetByClientHandler : IRequestHandler<GetAllPetByClientQuery, BaseResponse<IEnumerable<PetResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

    public GetAllPetByClientHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ordering = ordering;
    }

    public async Task<BaseResponse<IEnumerable<PetResponseDto>>> Handle(GetAllPetByClientQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<PetResponseDto>>();

        try
        {
            var pets = _unitOfWork.Pet.GetAllQueryable()
                    .Include(x => x.Client)
                    .Include(x => x.Client.User)
                    .AsQueryable(); ;

            pets = pets.Where(x => x.Clientid.Equals(request.ClientId));

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        pets = pets.Where(x => x.Name.Contains(request.TextFilter));
                        break;
                    case 2:
                        pets = pets.Where(x => x.Type!.Contains(request.TextFilter));
                        break;
                    case 3:
                        pets = pets.Where(x => x.Breed!.Contains(request.TextFilter));
                        break;
                }
            }

            if (request.StateFilter is not null)
            {
                pets = pets.Where(x => x.State == request.StateFilter);
            }

            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                pets = pets.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate).ToUniversalTime() &&
                                         x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).ToUniversalTime().AddDays(1));
            }

            request.Sort ??= "Id";

            var items = await _ordering.Ordering(request, pets)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await pets.CountAsync(cancellationToken);
            response.Data = _mapper.Map<IEnumerable<PetResponseDto>>(items);
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