using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Handlers.Queries
{
    public class GetLoaiHinhDonViDetailRequestHandler : IRequestHandler<GetLoaiHinhDonViDetailRequest, LoaiHinhDonViDto>
    {
        private readonly ILoaiHinhDonViRepository _LoaiHinhDonViRepository;
        private readonly IMapper _mapper;

        public GetLoaiHinhDonViDetailRequestHandler(ILoaiHinhDonViRepository LoaiHinhDonViRepository, IMapper mapper)
        {
            _LoaiHinhDonViRepository = LoaiHinhDonViRepository;
            _mapper = mapper;
        }

        public async Task<LoaiHinhDonViDto> Handle(GetLoaiHinhDonViDetailRequest request, CancellationToken cancellationToken)
        {
            var LoaiHinhDonViRepository = await _LoaiHinhDonViRepository.GetById(request.Id);
            return _mapper.Map<LoaiHinhDonViDto>(LoaiHinhDonViRepository);
        }
    }
    
}
