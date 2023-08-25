using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.NguoiDaiDien;
using SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.NguoiDaiDiens.Handlers.Queries
{
    public class GetNguoiDaiDienDetailRequestHandler : IRequestHandler<GetNguoiDaiDienDetailRequest, NguoiDaiDienDto>
    {
        private readonly INguoiDaiDienRepository _nguoiDaiDienRepository;
        private readonly IMapper _mapper;

        public GetNguoiDaiDienDetailRequestHandler(INguoiDaiDienRepository nguoiDaiDienRepository, IMapper mapper)
        {
            _nguoiDaiDienRepository = nguoiDaiDienRepository;
            _mapper = mapper;
        }

        public async Task<NguoiDaiDienDto> Handle(GetNguoiDaiDienDetailRequest request, CancellationToken cancellationToken)
        {
            var NguoiDaiDienRepository = await _nguoiDaiDienRepository.GetById(request.Id);
            return _mapper.Map<NguoiDaiDienDto>(NguoiDaiDienRepository);
        }
    }
    
}
