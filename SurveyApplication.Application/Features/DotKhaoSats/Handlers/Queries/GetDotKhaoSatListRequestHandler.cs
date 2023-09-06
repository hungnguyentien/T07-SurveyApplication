using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.DotKhaoSat;
using SurveyApplication.Application.Features.DotKhaoSats.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.DotKhaoSats.Handlers.Queries
{
    
    public class GetDotKhaoSatListRequestHandler : IRequestHandler<GetDotKhaoSatListRequest, List<DotKhaoSatDto>>
    {
        private readonly IDotKhaoSatRepository _dotKhaoSatRepository;
        private readonly IMapper _mapper;

        public GetDotKhaoSatListRequestHandler(IDotKhaoSatRepository dotKhaoSatRepository, IMapper mapper)
        {
            _dotKhaoSatRepository = dotKhaoSatRepository;
            _mapper = mapper;
        }

        public async Task<List<DotKhaoSatDto>> Handle(GetDotKhaoSatListRequest request, CancellationToken cancellationToken)
        {
            var dotKhaoSats = await _dotKhaoSatRepository.GetAll();
            return _mapper.Map<List<DotKhaoSatDto>>(dotKhaoSats);
        }
    }
}
