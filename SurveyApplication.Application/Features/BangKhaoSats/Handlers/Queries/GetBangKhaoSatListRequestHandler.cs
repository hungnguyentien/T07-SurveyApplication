using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.BangKhaoSats.Handlers.Queries
{
   
    public class GetBangKhaoSatListRequestHandler : BaseMasterFeatures, IRequestHandler<GetBangKhaoSatListRequest, List<BangKhaoSatDto>>
    {
        private readonly IMapper _mapper;

        public GetBangKhaoSatListRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<List<BangKhaoSatDto>> Handle(GetBangKhaoSatListRequest request, CancellationToken cancellationToken)
        {
            var bangKhaoSats = await _surveyRepo.BangKhaoSat.GetAll();
            return _mapper.Map<List<BangKhaoSatDto>>(bangKhaoSats);
        }
    }
}
