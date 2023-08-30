using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Application.Features.CauHoi.Requests.Queries;

namespace SurveyApplication.Application.Features.CauHoi.Handlers.Queries
{
    public class GetCauHoiDetailRequestHandler : IRequestHandler<GetCauHoiDetailRequest, CauHoiDto>
    {
        private readonly IMapper _mapper;
        private readonly ICauHoiRepository _cauHoiRepository;
        public GetCauHoiDetailRequestHandler(IMapper mapper, ICauHoiRepository cauHoiRepository)
        {
            _mapper = mapper;
            _cauHoiRepository = cauHoiRepository;
        }

        public async Task<CauHoiDto> Handle(GetCauHoiDetailRequest request, CancellationToken cancellationToken)
        {
            var cauHoi = await _cauHoiRepository.GetById(request.Id);
            if (cauHoi == null) return new CauHoiDto();
            var lstCot = await _cauHoiRepository.GetCotByCauHoi(new List<int> { cauHoi.Id });
            var lstHang = await _cauHoiRepository.GetHangByCauHoi(new List<int> { cauHoi.Id });
            var result = _mapper.Map<CauHoiDto>(cauHoi);
            result.LstCot = _mapper.Map<List<CotDto>>(lstCot.Where(c => c.IdCauHoi == cauHoi.Id).Select(c => c));
            result.LstHang = _mapper.Map<List<HangDto>>(lstHang.Where(h => h.IdCauHoi == cauHoi.Id).Select(h => h));
            return result;

        }
    }
}
