using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Queries;
using SurveyApplication.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.BangKhaoSats.Handlers.Queries
{
   
    public class GetBangKhaoSatConditionsRequestHandler : IRequestHandler<GetBangKhaoSatConditionsRequest, PageCommandResponse<BangKhaoSatDto>>
    {
        private readonly IBangKhaoSatRepository _bangKhaoSatRepository;
        private readonly IMapper _mapper;
        public GetBangKhaoSatConditionsRequestHandler(IBangKhaoSatRepository bangKhaoSatRepository, IMapper mapper)
        {
            _bangKhaoSatRepository = bangKhaoSatRepository;
            _mapper = mapper;
        }

        public async Task<PageCommandResponse<BangKhaoSatDto>> Handle(GetBangKhaoSatConditionsRequest request, CancellationToken cancellationToken)
        {
            var BangKhaoSats = await _bangKhaoSatRepository.GetByCondition(request.PageIndex, request.PageSize, request.Keyword, x => string.IsNullOrEmpty(request.Keyword) || !string.IsNullOrEmpty(x.TenBangKhaoSat) && x.TenBangKhaoSat.Contains(request.Keyword), x => x.MoTa);
            return _mapper.Map<PageCommandResponse<BangKhaoSatDto>>(BangKhaoSats);
        }
    }

}
