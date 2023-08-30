using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Application.Enums;
using SurveyApplication.Application.Features.CauHoi.Requests.Queries;
using SurveyApplication.Application.Responses;
using SurveyApplication.Application.Utils;

namespace SurveyApplication.Application.Features.CauHoi.Handlers.Queries
{
    public class GetCauHoiConditionsRequestHandler : IRequestHandler<GetCauHoiConditionsRequest, BaseQuerieResponse<CauHoiDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICauHoiRepository _cauHoiRepository;
        public GetCauHoiConditionsRequestHandler(IMapper mapper, ICauHoiRepository cauHoiRepository)
        {
            _mapper = mapper;
            _cauHoiRepository = cauHoiRepository;
        }

        public async Task<BaseQuerieResponse<CauHoiDto>> Handle(GetCauHoiConditionsRequest request, CancellationToken cancellationToken)
        {
            var lstCauHoi = await _cauHoiRepository.GetByConditionsQuerieResponse(request.PageIndex, request.PageSize, x => (string.IsNullOrEmpty(request.Keyword) || x.TieuDe.Contains(request.Keyword)) && x.ActiveFlag == (int)EnumCommon.ActiveFlag.Active, x => x.CreatedBy);
            var result = _mapper.Map<List<CauHoiDto>>(lstCauHoi);
            result.ForEach(x => x.LoaiCauHoiText = EnumUltils.GetDescription<EnumCauHoi.Type>().First(t => (int)t.Key == x.LoaiCauHoi).Value);
            return new BaseQuerieResponse<CauHoiDto>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Keyword = request.Keyword,
                TotalFilter = lstCauHoi.TotalFilter,
                Data = result
            };
        }
    }
}
