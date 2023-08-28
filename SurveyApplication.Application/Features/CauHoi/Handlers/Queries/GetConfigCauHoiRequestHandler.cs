using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Application.Enums;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Queries;
using SurveyApplication.Application.Features.CauHoi.Requests.Queries;
using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.CauHoi.Handlers.Queries
{
    public class GetConfigCauHoiRequestHandler : IRequestHandler<GetConfigCauHoiRequest, List<CauHoiDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICauHoiRepository _cauHoiRepository;
        public GetConfigCauHoiRequestHandler(IMapper mapper, ICauHoiRepository cauHoiRepository)
        {
            _mapper = mapper;
            _cauHoiRepository = cauHoiRepository;
        }

        public async Task<List<CauHoiDto>> Handle(GetConfigCauHoiRequest request, CancellationToken cancellationToken)
        {
            //TODO phải lấy theo IdBangKhaoSat
            var lstCauHoi = await _cauHoiRepository.GetByConditions(1, 1000, x => x.ActiveFlag == (int)EnumCommon.ActiveFlag.Active, x => x.TieuDe);
            var lstId = lstCauHoi.Select(x => x.Id).ToList();
            var lstCot = await _cauHoiRepository.GetCotByCauHoi(lstId);
            var lstHang = await _cauHoiRepository.GetHangByCauHoi(lstId);
            var result = _mapper.Map<List<CauHoiDto>>(lstCauHoi.OrderBy(x => x.Priority));
            result.ForEach(x =>
            {
                x.LstCot = _mapper.Map<List<CotDto>>(lstCot.Where(c => c.IdCauHoi == x.Id).Select(c => c));
                x.LstHang = _mapper.Map<List<HangDto>>(lstHang.Where(h => h.IdCauHoi == x.Id).Select(h => h));
            });
            return result;
        }
    }
}
