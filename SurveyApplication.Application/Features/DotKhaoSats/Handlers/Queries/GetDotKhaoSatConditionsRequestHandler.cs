using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.DTOs.DotKhaoSat;
using SurveyApplication.Application.Features.DotKhaoSats.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.DotKhaoSats.Handlers.Queries
{
   
    public class GetGuiEmailConditionsRequestHandler : IRequestHandler<GetGuiEmailConditionsRequest, List<DotKhaoSatDto>>
    {
        private readonly IDotKhaoSatRepository _dotKhaoSatRepository;
        private readonly IMapper _mapper;
        public GetGuiEmailConditionsRequestHandler(IDotKhaoSatRepository dotKhaoSatRepository, IMapper mapper)
        {
            _dotKhaoSatRepository = dotKhaoSatRepository;
            _mapper = mapper;
        }

        public async Task<List<DotKhaoSatDto>> Handle(GetGuiEmailConditionsRequest request, CancellationToken cancellationToken)
        {
            var DotKhaoSats = await _dotKhaoSatRepository.GetByConditions(request.PageIndex, request.PageSize, x => string.IsNullOrEmpty(request.Keyword) || !string.IsNullOrEmpty(x.TenDotKhaoSat) && x.TenDotKhaoSat.Contains(request.Keyword));
            return _mapper.Map<List<DotKhaoSatDto>>(DotKhaoSats);
        }
    }
}
