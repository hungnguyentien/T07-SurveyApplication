using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.Features.DonVis.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.DonVis.Handlers.Queries;

public class GetDonViListRequestHandler : BaseMasterFeatures, IRequestHandler<GetDonViListRequest, List<DonViDto>>
{
    private readonly IMapper _mapper;

    public GetDonViListRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<List<DonViDto>> Handle(GetDonViListRequest request, CancellationToken cancellationToken)
    {
        var DonVis = await _surveyRepo.DonVi.GetAll();
        return _mapper.Map<List<DonViDto>>(DonVis);
    }
}