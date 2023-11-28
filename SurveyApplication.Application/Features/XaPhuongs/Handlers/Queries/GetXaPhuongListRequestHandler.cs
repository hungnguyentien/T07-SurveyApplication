using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.XaPhuong;
using SurveyApplication.Application.Features.XaPhuongs.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.XaPhuongs.Handlers.Queries
{
   
    public class GetXaPhuongListRequestHandler : BaseMasterFeatures, IRequestHandler<GetXaPhuongListRequest, List<XaPhuongDto>>
    {
        private readonly IMapper _mapper;

        public GetXaPhuongListRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<List<XaPhuongDto>> Handle(GetXaPhuongListRequest request, CancellationToken cancellationToken)
        {
            var XaPhuongs = await _surveyRepo.XaPhuong.GetAllListAsync(x => !x.Deleted);
            return _mapper.Map<List<XaPhuongDto>>(XaPhuongs);
        }
    }
}
