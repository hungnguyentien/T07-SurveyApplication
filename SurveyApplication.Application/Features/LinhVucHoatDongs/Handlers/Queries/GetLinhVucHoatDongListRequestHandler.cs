using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.LinhVucHoatDong;
using SurveyApplication.Application.Features.LinhVucHoatDongs.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.LinhVucHoatDongs.Handlers.Queries
{
    
    public class GetLinhVucHoatDongListRequestHandler : IRequestHandler<GetLinhVucHoatDongListRequest, List<LinhVucHoatDongDto>>
    {
        private readonly ILinhVucHoatDongRepository _LinhVucHoatDongRepository;
        private readonly IMapper _mapper;

        public GetLinhVucHoatDongListRequestHandler(ILinhVucHoatDongRepository LinhVucHoatDongRepository, IMapper mapper)
        {
            _LinhVucHoatDongRepository = LinhVucHoatDongRepository;
            _mapper = mapper;
        }

        public async Task<List<LinhVucHoatDongDto>> Handle(GetLinhVucHoatDongListRequest request, CancellationToken cancellationToken)
        {
            var LinhVucHoatDongs = await _LinhVucHoatDongRepository.GetAll();
            return _mapper.Map<List<LinhVucHoatDongDto>>(LinhVucHoatDongs);
        }
    }
}
