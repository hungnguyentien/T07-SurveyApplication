using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.LinhVucHoatDong;
using SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.LinhVucHoatDong.Handlers.Queries
{
    public class GetLinhVucHoatDongDetailRequestHandler : BaseMasterFeatures, IRequestHandler<GetLinhVucHoatDongDetailRequest, LinhVucHoatDongDto>
    {
        private readonly IMapper _mapper;

        public GetLinhVucHoatDongDetailRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<LinhVucHoatDongDto> Handle(GetLinhVucHoatDongDetailRequest request, CancellationToken cancellationToken)
        {
            var LinhVucHoatDongRepository = await _surveyRepo.LinhVucHoatDong.GetById(request.Id);
            return _mapper.Map<LinhVucHoatDongDto>(LinhVucHoatDongRepository);
        }
    }
}
