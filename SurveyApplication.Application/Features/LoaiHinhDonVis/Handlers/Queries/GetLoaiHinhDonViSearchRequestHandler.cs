using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Handlers.Queries
{
    public class GetLoaiHinhDonViSearchRequestHandler : IRequestHandler<GetLoaiHinhDonViSearchRequest, List<LoaiHinhDonViDto>>
    {
        private readonly ILoaiHinhDonViRepository _loaiHinhDonViRepository;
        private readonly IMapper _mapper;

        public GetLoaiHinhDonViSearchRequestHandler(ILoaiHinhDonViRepository loaiHinhDonViRepository, IMapper mapper)
        {
            _loaiHinhDonViRepository = loaiHinhDonViRepository;
            _mapper = mapper;
        }

        public async Task<List<LoaiHinhDonViDto>> Handle(GetLoaiHinhDonViSearchRequest request, CancellationToken cancellationToken)
        {
            var loaiHinhDonVis = await _loaiHinhDonViRepository.GetByConditions(request.PageIndex, request.PageSize, x => string.IsNullOrEmpty(request.Keyword) || !string.IsNullOrEmpty(x.TenLoaiHinh) && x.TenLoaiHinh.Contains(request.Keyword), x => x.Created);
            return _mapper.Map<List<LoaiHinhDonViDto>>(loaiHinhDonVis);
        }
    }
}
