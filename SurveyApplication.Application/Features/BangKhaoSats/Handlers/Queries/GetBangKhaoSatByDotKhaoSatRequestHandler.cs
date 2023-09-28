using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.BangKhaoSats.Handlers.Queries
{
    public class GetBangKhaoSatByDotKhaoSatRequestHandler : BaseMasterFeatures, IRequestHandler<GetBangKhaoSatByDotKhaoSatRequest, List<BangKhaoSatDto>>
    {
        private readonly IMapper _mapper;

        public GetBangKhaoSatByDotKhaoSatRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
            surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<List<BangKhaoSatDto>> Handle(GetBangKhaoSatByDotKhaoSatRequest request,
            CancellationToken cancellationToken)
        {
            var bangKhaoSats = request.Id == null || request.Id < 1 ? await _surveyRepo.BangKhaoSat.GetAllListAsync() : await _surveyRepo.BangKhaoSat.GetAllListAsync(x => x.IdDotKhaoSat == request.Id);
            return _mapper.Map<List<BangKhaoSatDto>>(bangKhaoSats);
        }
    }
}
