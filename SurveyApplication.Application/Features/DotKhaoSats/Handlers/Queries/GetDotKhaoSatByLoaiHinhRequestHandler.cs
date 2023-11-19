using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.DotKhaoSat;
using SurveyApplication.Application.Features.DotKhaoSats.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.DotKhaoSats.Handlers.Queries;

public class GetDotKhaoSatByLoaiHinhRequestHandler : BaseMasterFeatures,
    IRequestHandler<GetDotKhaoSatByLoaiHinhRequest, List<DotKhaoSatDto>>
{
    private readonly IMapper _mapper;

    public GetDotKhaoSatByLoaiHinhRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<List<DotKhaoSatDto>> Handle(GetDotKhaoSatByLoaiHinhRequest request,
        CancellationToken cancellationToken)
    {
        var dotKhaoSats = request.Id == null || request.Id < 1
            ? await _surveyRepo.DotKhaoSat.GetAllListAsync()
            : await _surveyRepo.DotKhaoSat.GetAllListAsync(x => x.IdLoaiHinh == request.Id);
        return _mapper.Map<List<DotKhaoSatDto>>(dotKhaoSats);
    }
}