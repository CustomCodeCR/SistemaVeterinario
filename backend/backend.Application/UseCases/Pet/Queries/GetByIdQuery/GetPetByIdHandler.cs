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
using WatchDog;

namespace backend.Application.UseCases.Pet.Queries.GetByIdQuery;

public class GetPetByIdHandler : IRequestHandler<GetPetByIdQuery, BaseResponse<PetByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPetByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<PetByIdResponseDto>> Handle(GetPetByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<PetByIdResponseDto>();

        try
        {
            var pet = await _unitOfWork.Pet.GetByIdAsync(request.PetId);

            if (pet is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<PetByIdResponseDto>(pet);
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