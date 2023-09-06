using AutoMapper;
using FluentValidation;
using MediatR;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.DTOs.NguoiDaiDien;
using SurveyApplication.Application.DTOs.PhieuKhaoSat;
using SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Handlers.Queries
{
    public class GetThongTinChungRequestHandler : IRequestHandler<GetThongTinChungRequest, ThongTinChungDto>
    {
        private readonly IMapper _mapper;
        private readonly IDonViRepository _donViRepository;
        private readonly INguoiDaiDienRepository _nguoiDaiDienRepository;
        private readonly IBangKhaoSatRepository _bangKhaoSatRepository;

        public GetThongTinChungRequestHandler(IMapper mapper, IDonViRepository donViRepository, INguoiDaiDienRepository nguoiDaiDienRepository, IBangKhaoSatRepository bangKhaoSatRepository)
        {
            _mapper = mapper;
            _donViRepository = donViRepository;
            _nguoiDaiDienRepository = nguoiDaiDienRepository;
            _bangKhaoSatRepository = bangKhaoSatRepository;
        }

        public async Task<ThongTinChungDto> Handle(GetThongTinChungRequest request, CancellationToken cancellationToken)
        {
            var donVi = await _donViRepository.GetById(request.IdDonVi);
            var nguoiDaiDien = await _nguoiDaiDienRepository.GetByIdDonVi(request.IdDonVi);
            var bangKs = await _bangKhaoSatRepository.GetById(request.IdBangKhaoSat);
            return bangKs == null
                ? throw new ValidationException("Không tồn tại bảng khảo sát")
                : new ThongTinChungDto
                {
                    DonVi = _mapper.Map<DonViDto>(donVi),
                    NguoiDaiDien = _mapper.Map<NguoiDaiDienDto>(nguoiDaiDien),
                    BangKhaoSat = request.IdBangKhaoSat,
                    TrangThai = bangKs.TrangThai ?? 0
                };
        }
    }
}
