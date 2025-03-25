using AutoMapper;
using backend.Application.Commons.Bases;
using backend.Application.Dtos.Product.Response;
using backend.Application.Interfaces.Services;
using backend.Utilities.Static;
using MediatR;
using WatchDog;

namespace backend.Application.UseCases.Product.Queries.GetAllQuery;

public class GetAllProductHandler : IRequestHandler<GetAllProductQuery, BaseResponse<IEnumerable<ProductResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    //private readonly IOrderingQuery _ordering;

    public GetAllProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<IEnumerable<ProductResponseDto>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<ProductResponseDto>>();

        try
        {
            
            
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            WatchLogger.LogError(ex.Message);
        }

        return response;
    }
}