using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.DotKhaoSat;
using SurveyApplication.Application.Features.DotKhaoSats.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.DotKhaoSats.Handlers.Queries;

public class GetDotKhaoSatListRequestHandler : BaseMasterFeatures,
    IRequestHandler<GetDotKhaoSatListRequest, List<DotKhaoSatDto>>
{
    private readonly IMapper _mapper;

    public GetDotKhaoSatListRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<List<DotKhaoSatDto>> Handle(GetDotKhaoSatListRequest request, CancellationToken cancellationToken)
    {
        var dotKhaoSats = await _surveyRepo.DotKhaoSat.GetAll();
        return _mapper.Map<List<DotKhaoSatDto>>(dotKhaoSats);
    }
}