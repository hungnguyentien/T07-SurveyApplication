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
   
    public class GetNguoiDaiDienConditionsRequestHandler : IRequestHandler<GetNguoiDaiDienConditionsRequest, List<NguoiDaiDienDto>>
    {
        private readonly INguoiDaiDienRepository _nguoiDaiDienRepository;
        private readonly IMapper _mapper;
        public GetNguoiDaiDienConditionsRequestHandler(INguoiDaiDienRepository nguoiDaiDienRepository, IMapper mapper)
        {
            _nguoiDaiDienRepository = nguoiDaiDienRepository;
            _mapper = mapper;
        }

        public async Task<List<NguoiDaiDienDto>> Handle(GetNguoiDaiDienConditionsRequest request, CancellationToken cancellationToken)
        {
            var NguoiDaiDiens = await _nguoiDaiDienRepository.GetByConditions(request.PageIndex, request.PageSize, x => string.IsNullOrEmpty(request.Keyword) || !string.IsNullOrEmpty(x.HoTen) && x.HoTen.Contains(request.Keyword));
            return _mapper.Map<List<NguoiDaiDienDto>>(NguoiDaiDiens);
        }
    }

}
