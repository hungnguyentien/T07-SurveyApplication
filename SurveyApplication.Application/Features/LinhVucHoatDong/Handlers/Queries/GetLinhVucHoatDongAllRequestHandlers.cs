using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.LinhVucHoatDong;
using SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.LinhVucHoatDong.Handlers.Queries
{
    public class GetLinhVucHoatDongAllRequestHandlers : IRequestHandler<GetLinhVucHoatDongAllRequest, List<LinhVucHoatDongDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILinhVucHoatDongRepository _linhVucHoatDongRepository;

        public GetLinhVucHoatDongAllRequestHandlers(IMapper mapper, ILinhVucHoatDongRepository linhVucHoatDongRepository)
        {
            _mapper = mapper;
            _linhVucHoatDongRepository = linhVucHoatDongRepository;
        }

        public async Task<List<LinhVucHoatDongDto>> Handle(GetLinhVucHoatDongAllRequest request, CancellationToken cancellationToken)
        {
            var data = await _linhVucHoatDongRepository.GetAll();
            return _mapper.Map<List<LinhVucHoatDongDto>>(data);
        }
    }
}
