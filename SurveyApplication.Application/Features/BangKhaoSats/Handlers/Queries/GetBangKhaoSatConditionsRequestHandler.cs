using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

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
            var BangKhaoSats = await _bangKhaoSatRepository.GetByConditions(request.PageIndex, request.PageSize, x => string.IsNullOrEmpty(request.Keyword) || !string.IsNullOrEmpty(x.TenBangKhaoSat) && x.TenBangKhaoSat.Contains(request.Keyword), x => x.MoTa);
            return _mapper.Map<PageCommandResponse<BangKhaoSatDto>>(BangKhaoSats);
        }
    }

}
