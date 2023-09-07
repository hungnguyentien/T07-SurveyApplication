using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.NguoiDaiDien;
using SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.NguoiDaiDiens.Handlers.Queries
{
    public class GetNguoiDaiDienDetailRequestHandler : BaseMasterFeatures, IRequestHandler<GetNguoiDaiDienDetailRequest, NguoiDaiDienDto>
    {
        private readonly IMapper _mapper;

        public GetNguoiDaiDienDetailRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<NguoiDaiDienDto> Handle(GetNguoiDaiDienDetailRequest request, CancellationToken cancellationToken)
        {
            var NguoiDaiDienRepository = await _surveyRepo.NguoiDaiDien.GetById(request.Id);
            return _mapper.Map<NguoiDaiDienDto>(NguoiDaiDienRepository);
        }
    }
    
}
