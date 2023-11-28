using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.QuanHuyen;
using SurveyApplication.Application.Features.QuanHuyens.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.QuanHuyens.Handlers.Queries
{
   
    public class GetQuanHuyenListRequestHandler : BaseMasterFeatures, IRequestHandler<GetQuanHuyenListRequest, List<QuanHuyenDto>>
    {
        private readonly IMapper _mapper;

        public GetQuanHuyenListRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<List<QuanHuyenDto>> Handle(GetQuanHuyenListRequest request, CancellationToken cancellationToken)
        {
            var QuanHuyens = await _surveyRepo.QuanHuyen.GetAllListAsync(x => !x.Deleted);
            return _mapper.Map<List<QuanHuyenDto>>(QuanHuyens);
        }
    }
}
