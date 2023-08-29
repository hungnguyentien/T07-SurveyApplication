using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.DTOs.NguoiDaiDien;
using SurveyApplication.Application.DTOs.PhieuKhaoSat;
using SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Queries;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Handlers.Queries
{
    public class GetThongTinChungRequestHandler : IRequestHandler<GetThongTinChungRequest, ThongTinChungDto>
    {
        private readonly IMapper _mapper;
        private readonly IDonViRepository _donViRepository;
        private readonly INguoiDaiDienRepository _nguoiDaiDienRepository;

        public GetThongTinChungRequestHandler(IMapper mapper, IDonViRepository donViRepository, INguoiDaiDienRepository nguoiDaiDienRepository)
        {
            _mapper = mapper;
            _donViRepository = donViRepository;
            _nguoiDaiDienRepository = nguoiDaiDienRepository;
        }

        public async Task<ThongTinChungDto> Handle(GetThongTinChungRequest request, CancellationToken cancellationToken)
        {
            var donVi = await _donViRepository.GetById(request.IdDonVi);
            var nguoiDaiDien = await _nguoiDaiDienRepository.GetByIdDonVi(request.IdDonVi);
            return new ThongTinChungDto
            {
                DonVi = _mapper.Map<DonViDto>(donVi),
                NguoiDaiDien = _mapper.Map<NguoiDaiDienDto>(nguoiDaiDien)
            };
        }
    }
}
