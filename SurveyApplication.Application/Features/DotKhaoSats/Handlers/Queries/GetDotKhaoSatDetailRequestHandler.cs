using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.DotKhaoSat;
using SurveyApplication.Application.Features.DotKhaoSats.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.DotKhaoSats.Handlers.Queries
{
    
    public class GetDotKhaoSatDetailRequestHandler : IRequestHandler<GetDotKhaoSatDetailRequest, DotKhaoSatDto>
    {
        private readonly IDotKhaoSatRepository _dotKhaoSatRepository;
        private readonly IMapper _mapper;

        public GetDotKhaoSatDetailRequestHandler(IDotKhaoSatRepository dotKhaoSatRepository, IMapper mapper)
        {
            _dotKhaoSatRepository = dotKhaoSatRepository;
            _mapper = mapper;
        }

        public async Task<DotKhaoSatDto> Handle(GetDotKhaoSatDetailRequest request, CancellationToken cancellationToken)
        {
            var dotKhaoSatRepository = await _dotKhaoSatRepository.GetById(request.Id);
            return _mapper.Map<DotKhaoSatDto>(dotKhaoSatRepository);
        }
    }
}
