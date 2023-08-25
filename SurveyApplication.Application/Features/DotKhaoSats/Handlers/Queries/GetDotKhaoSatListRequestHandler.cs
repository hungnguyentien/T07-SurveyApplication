using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.DotKhaoSat;
using SurveyApplication.Application.Features.DotKhaoSats.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.DotKhaoSats.Handlers.Queries
{
    
    public class GetGuiEmailListRequestHandler : IRequestHandler<GetGuiEmailListRequest, List<DotKhaoSatDto>>
    {
        private readonly IDotKhaoSatRepository _dotKhaoSatRepository;
        private readonly IMapper _mapper;

        public GetGuiEmailListRequestHandler(IDotKhaoSatRepository dotKhaoSatRepository, IMapper mapper)
        {
            _dotKhaoSatRepository = dotKhaoSatRepository;
            _mapper = mapper;
        }

        public async Task<List<DotKhaoSatDto>> Handle(GetGuiEmailListRequest request, CancellationToken cancellationToken)
        {
            var dotKhaoSats = await _dotKhaoSatRepository.GetAll();
            return _mapper.Map<List<DotKhaoSatDto>>(dotKhaoSats);
        }
    }
}
