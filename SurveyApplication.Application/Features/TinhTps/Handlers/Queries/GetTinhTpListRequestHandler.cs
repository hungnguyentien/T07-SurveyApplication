using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.TinhTp;
using SurveyApplication.Application.Features.TinhTps.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.TinhTps.Handlers.Queries
{
   
    public class GetTinhTpListRequestHandler : BaseMasterFeatures, IRequestHandler<GetTinhTpListRequest, List<TinhTpDto>>
    {
        private readonly IMapper _mapper;

        public GetTinhTpListRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<List<TinhTpDto>> Handle(GetTinhTpListRequest request, CancellationToken cancellationToken)
        {
            var TinhTps = await _surveyRepo.TinhTp.GetAll();
            return _mapper.Map<List<TinhTpDto>>(TinhTps);
        }
    }
}
