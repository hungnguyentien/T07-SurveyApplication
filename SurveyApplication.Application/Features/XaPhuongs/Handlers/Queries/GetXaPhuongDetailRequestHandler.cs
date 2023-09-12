using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.XaPhuong;
using SurveyApplication.Application.Features.XaPhuongs.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.XaPhuongs.Handlers.Queries
{
    public class GetXaPhuongDetailRequestHandler : BaseMasterFeatures, IRequestHandler<GetXaPhuongDetailRequest, XaPhuongDto>
    {
        private readonly IMapper _mapper;

        public GetXaPhuongDetailRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<XaPhuongDto> Handle(GetXaPhuongDetailRequest request, CancellationToken cancellationToken)
        {
            var XaPhuongRepository = await _surveyRepo.XaPhuong.GetById(request.Id);
            return _mapper.Map<XaPhuongDto>(XaPhuongRepository);
        }
    }
    
}
