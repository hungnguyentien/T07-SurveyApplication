using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.LinhVucHoatDong;
using SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.LinhVucHoatDong.Handlers.Queries
{
    public class GetLinhVucHoatDongListRequestHandler : BaseMasterFeatures, IRequestHandler<GetLinhVucHoatDongListRequest, List<LinhVucHoatDongDto>>
    {
        private readonly IMapper _mapper;

        public GetLinhVucHoatDongListRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<List<LinhVucHoatDongDto>> Handle(GetLinhVucHoatDongListRequest request, CancellationToken cancellationToken)
        {
            var data = await _surveyRepo.LinhVucHoatDong.GetAll();
            return _mapper.Map<List<LinhVucHoatDongDto>>(data);
        }
    }
}
