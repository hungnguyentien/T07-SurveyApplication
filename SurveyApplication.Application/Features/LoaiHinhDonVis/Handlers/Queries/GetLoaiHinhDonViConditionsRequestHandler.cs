using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Queries;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Handlers.Queries
{
   
    public class GetLoaiHinhDonViConditionsRequestHandler : IRequestHandler<GetLoaiHinhDonViConditionsRequest, PageCommandResponse<LoaiHinhDonViDto>>
    {
        private readonly ILoaiHinhDonViRepository _LoaiHinhDonViRepository;
        private readonly IMapper _mapper;
        public GetLoaiHinhDonViConditionsRequestHandler(ILoaiHinhDonViRepository LoaiHinhDonViRepository, IMapper mapper)
        {
            _LoaiHinhDonViRepository = LoaiHinhDonViRepository;
            _mapper = mapper;
        }

        public async Task<PageCommandResponse<LoaiHinhDonViDto>> Handle(GetLoaiHinhDonViConditionsRequest request, CancellationToken cancellationToken)
        {
            var LoaiHinhDonVis = await _LoaiHinhDonViRepository.GetByConditions(request.PageIndex, request.PageSize, x => string.IsNullOrEmpty(request.Keyword) || !string.IsNullOrEmpty(x.TenLoaiHinh) && x.TenLoaiHinh.Contains(request.Keyword), x => x.Created);
            return _mapper.Map<PageCommandResponse<LoaiHinhDonViDto>>(LoaiHinhDonVis);
        }
    }

}
