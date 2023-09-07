using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.GuiEmail;
using SurveyApplication.Application.Features.GuiEmails.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.GuiEmails.Handlers.Queries
{
    
    public class GetGuiEmailListRequestHandler : BaseMasterFeatures, IRequestHandler<GetGuiEmailListRequest, List<GuiEmailDto>>
    {
        private readonly IMapper _mapper;

        public GetGuiEmailListRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<List<GuiEmailDto>> Handle(GetGuiEmailListRequest request, CancellationToken cancellationToken)
        {
            var guiEmails = await _surveyRepo.GuiEmail.GetAll();
            return _mapper.Map<List<GuiEmailDto>>(guiEmails);
        }
    }
}
