using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Handlers.Queries
{
   
    public class GetLoaiHinhDonViListRequestHandler : BaseMasterFeatures, IRequestHandler<GetLoaiHinhDonViListRequest, List<LoaiHinhDonViDto>>
    {
        private readonly IMapper _mapper;

        public GetLoaiHinhDonViListRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<List<LoaiHinhDonViDto>> Handle(GetLoaiHinhDonViListRequest request, CancellationToken cancellationToken)
        {
            var LoaiHinhDonVis = await _surveyRepo.LoaiHinhDonVi.GetAll();
            return _mapper.Map<List<LoaiHinhDonViDto>>(LoaiHinhDonVis);
        }
    }
}
