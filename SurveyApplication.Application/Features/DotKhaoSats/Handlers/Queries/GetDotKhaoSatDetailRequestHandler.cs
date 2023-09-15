using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.DotKhaoSat;
using SurveyApplication.Application.Features.DotKhaoSats.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.DotKhaoSats.Handlers.Queries;

public class GetDotKhaoSatDetailRequestHandler : BaseMasterFeatures,
    IRequestHandler<GetDotKhaoSatDetailRequest, DotKhaoSatDto>
{
    private readonly IMapper _mapper;

    public GetDotKhaoSatDetailRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<DotKhaoSatDto> Handle(GetDotKhaoSatDetailRequest request, CancellationToken cancellationToken)
    {
        var dotKhaoSatRepository = await _surveyRepo.DotKhaoSat.GetById(request.Id);
        return _mapper.Map<DotKhaoSatDto>(dotKhaoSatRepository);
    }
}