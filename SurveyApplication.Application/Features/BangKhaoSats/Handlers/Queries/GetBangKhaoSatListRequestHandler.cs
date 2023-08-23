using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Queries;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.BangKhaoSats.Handlers.Queries
{
   
    public class GetBangKhaoSatListRequestHandler : IRequestHandler<GetBangKhaoSatListRequest, List<BangKhaoSatDto>>
    {
        private readonly IBangKhaoSatRepository _bangKhaoSatRepository;
        private readonly IMapper _mapper;

        public GetBangKhaoSatListRequestHandler(IBangKhaoSatRepository bangKhaoSatRepository, IMapper mapper)
        {
            _bangKhaoSatRepository = bangKhaoSatRepository;
            _mapper = mapper;
        }

        public async Task<List<BangKhaoSatDto>> Handle(GetBangKhaoSatListRequest request, CancellationToken cancellationToken)
        {
            var bangKhaoSats = await _bangKhaoSatRepository.GetAll();
            return _mapper.Map<List<BangKhaoSatDto>>(bangKhaoSats);
        }
    }
}
