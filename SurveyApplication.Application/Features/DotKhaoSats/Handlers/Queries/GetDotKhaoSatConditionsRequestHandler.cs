using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.DTOs.DotKhaoSat;
using SurveyApplication.Application.Features.DotKhaoSats.Requests.Queries;
using SurveyApplication.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.DotKhaoSats.Handlers.Queries
{
   
    public class GetDotKhaoSatConditionsRequestHandler : IRequestHandler<GetDotKhaoSatConditionsRequest, PageCommandResponse<DotKhaoSatDto>>
    {
        private readonly IDotKhaoSatRepository _dotKhaoSatRepository;
        private readonly IMapper _mapper;
        public GetDotKhaoSatConditionsRequestHandler(IDotKhaoSatRepository dotKhaoSatRepository, IMapper mapper)
        {
            _dotKhaoSatRepository = dotKhaoSatRepository;
            _mapper = mapper;
        }

        public async Task<PageCommandResponse<DotKhaoSatDto>> Handle(GetDotKhaoSatConditionsRequest request, CancellationToken cancellationToken)
        {
            var DotKhaoSats = await _dotKhaoSatRepository.GetByConditions(request.PageIndex, request.PageSize, x => string.IsNullOrEmpty(request.Keyword) || !string.IsNullOrEmpty(x.TenDotKhaoSat) && x.TenDotKhaoSat.Contains(request.Keyword), x => x.Created);
            return _mapper.Map<PageCommandResponse<DotKhaoSatDto>>(DotKhaoSats);
        }
    }
}
