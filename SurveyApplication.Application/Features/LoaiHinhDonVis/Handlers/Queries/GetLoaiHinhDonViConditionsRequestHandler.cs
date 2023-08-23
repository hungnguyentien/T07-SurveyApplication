using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Queries;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Handlers.Queries
{
    public class GetLoaiHinhDonViConditionsRequestHandler : IRequestHandler<GetLoaiHinhDonViConditionsRequest, List<LoaiHinhDonViDto>>
    {
        private readonly ILoaiHinhDonViRepository _loaiHinhDonViRepository;
        private readonly IMapper _mapper;
        public GetLoaiHinhDonViConditionsRequestHandler(ILoaiHinhDonViRepository loaiHinhDonViRepository, IMapper mapper)
        {
            _loaiHinhDonViRepository = loaiHinhDonViRepository;
            _mapper = mapper;
        }

        public async Task<List<LoaiHinhDonViDto>> Handle(GetLoaiHinhDonViConditionsRequest request, CancellationToken cancellationToken)
        {
            var loaiHinhDonVis = await _loaiHinhDonViRepository.GetByConditions(request.PageIndex, request.PageSize, x => string.IsNullOrEmpty(request.Keyword) || !string.IsNullOrEmpty(x.TenLoaiHinh) && x.TenLoaiHinh.Contains(request.Keyword));
            return _mapper.Map<List<LoaiHinhDonViDto>>(loaiHinhDonVis);
        }
    }
}
