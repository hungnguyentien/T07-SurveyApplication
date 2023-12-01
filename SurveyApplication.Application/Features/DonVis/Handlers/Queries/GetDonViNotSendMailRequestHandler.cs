using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.Features.DonVis.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.DonVis.Handlers.Queries;

public class GetDonViNotSendMailRequestHandler : BaseMasterFeatures, IRequestHandler<GetDonViNotSendMailRequest, List<DonViDto>>
{
    private readonly IMapper _mapper;

    public GetDonViNotSendMailRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<List<DonViDto>> Handle(GetDonViNotSendMailRequest request, CancellationToken cancellationToken)
    {
        var lstDvSendMail = await _surveyRepo.GuiEmail.GetAllQueryable().Where(x => !x.Deleted).Select(x => x.IdDonVi).ToListAsync(cancellationToken: cancellationToken);
        var donVis = await _surveyRepo.DonVi.GetAllListAsync(x => !x.Deleted && !lstDvSendMail.Contains(x.Id));
        return _mapper.Map<List<DonViDto>>(donVis);
    }
}