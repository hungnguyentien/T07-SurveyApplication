using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.DTOs.TinhTp;
using SurveyApplication.Application.Enums;
using SurveyApplication.Application.Features.TinhTps.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.TinhTps.Handlers.Queries
{
    public class GetTinhTpDetailRequestHandler : BaseMasterFeatures, IRequestHandler<GetTinhTpDetailRequest, TinhTpDto>
    {
        private readonly IMapper _mapper;
        public GetTinhTpDetailRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<TinhTpDto> Handle(GetTinhTpDetailRequest request, CancellationToken cancellationToken)
        {
            var bks = await _surveyRepo.TinhTp.GetById(request.Id);
            var rs = _mapper.Map<TinhTpDto>(bks);
           
            return rs;
        }
    }

}
