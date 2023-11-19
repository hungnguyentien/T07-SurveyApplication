using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.QuanHuyen;
using SurveyApplication.Application.Features.QuanHuyens.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.QuanHuyens.Handlers.Queries;

public class GetQuanHuyenDetailRequestHandler : BaseMasterFeatures,
    IRequestHandler<GetQuanHuyenDetailRequest, QuanHuyenDto>
{
    private readonly IMapper _mapper;

    public GetQuanHuyenDetailRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<QuanHuyenDto> Handle(GetQuanHuyenDetailRequest request, CancellationToken cancellationToken)
    {
        var QuanHuyenRepository = await _surveyRepo.QuanHuyen.GetById(request.Id);
        return _mapper.Map<QuanHuyenDto>(QuanHuyenRepository);
    }
}