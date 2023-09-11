using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Handlers.Queries;

public class GetLoaiHinhDonViDetailRequestHandler : BaseMasterFeatures,
    IRequestHandler<GetLoaiHinhDonViDetailRequest, LoaiHinhDonViDto>
{
    private readonly IMapper _mapper;

    public GetLoaiHinhDonViDetailRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<LoaiHinhDonViDto> Handle(GetLoaiHinhDonViDetailRequest request,
        CancellationToken cancellationToken)
    {
        var LoaiHinhDonViRepository = await _surveyRepo.LoaiHinhDonVi.GetById(request.Id);
        return _mapper.Map<LoaiHinhDonViDto>(LoaiHinhDonViRepository);
    }
}