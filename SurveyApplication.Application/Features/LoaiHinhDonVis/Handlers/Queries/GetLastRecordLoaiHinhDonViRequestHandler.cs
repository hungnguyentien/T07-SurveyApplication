using AutoMapper;
using MediatR;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Handlers.Queries
{
    public class GetLastRecordLoaiHinhDonViRequestHandler : BaseMasterFeatures, IRequestHandler<GetLastRecordLoaiHinhDonViRequest, string>
    {
        private readonly IMapper _mapper;

        public GetLastRecordLoaiHinhDonViRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<string> Handle(GetLastRecordLoaiHinhDonViRequest request, CancellationToken cancellationToken)
        {
            var LoaiHinhDonVis = await _surveyRepo.LoaiHinhDonVi.GetLastRecordByMaLoaiHinh();
            return LoaiHinhDonVis;
        }
    }
}
