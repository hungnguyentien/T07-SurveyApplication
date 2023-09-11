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
    public class GetThongTinChungRequestHandler : BaseMasterFeatures, IRequestHandler<GetThongTinChungRequest, ThongTinChungDto>
    {
        private readonly IMapper _mapper;

        public GetThongTinChungRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<ThongTinChungDto> Handle(GetThongTinChungRequest request, CancellationToken cancellationToken)
        {
            var donVi = await _surveyRepo.DonVi.GetById(request.IdDonVi);
            var nguoiDaiDien = await _surveyRepo.NguoiDaiDien.FirstOrDefaultAsync(x => !x.Deleted && x.IdDonVi == request.IdDonVi);
            var bangKs = await _surveyRepo.BangKhaoSat.GetById(request.IdBangKhaoSat);
            return bangKs == null
                ? throw new ValidationException("Không tồn tại bảng khảo sát")
                : nguoiDaiDien == null
                ? throw new ValidationException("Không tồn người đại diện")
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
