using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.XaPhuong;
using SurveyApplication.Application.Features.XaPhuongs.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.XaPhuongs.Handlers.Queries;

public class GetXaPhuonByQuanHuyenRequestHandler : BaseMasterFeatures,
    IRequestHandler<GetXaPhuonByQuanHuyenRequest, List<XaPhuongDto>>
{
    private readonly IMapper _mapper;

    public GetXaPhuonByQuanHuyenRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<List<XaPhuongDto>> Handle(GetXaPhuonByQuanHuyenRequest request,
        CancellationToken cancellationToken)
    {
        var QuanHuyens = await _surveyRepo.XaPhuong.GetAllListAsync(x => x.ParentCode == request.Id);
        return _mapper.Map<List<XaPhuongDto>>(QuanHuyens);
    }
}