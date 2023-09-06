using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.NguoiDaiDien;
using SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

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
            var NguoiDaiDiens = await _nguoiDaiDienRepository.GetByConditions(request.PageIndex, request.PageSize, x => string.IsNullOrEmpty(request.Keyword) || !string.IsNullOrEmpty(x.HoTen) && x.HoTen.Contains(request.Keyword), x => x.Created);
            return _mapper.Map<List<NguoiDaiDienDto>>(NguoiDaiDiens);
        }
    }

}
