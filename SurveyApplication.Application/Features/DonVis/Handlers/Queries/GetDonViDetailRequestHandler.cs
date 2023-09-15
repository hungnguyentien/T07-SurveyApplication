using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.DTOs.DonViAndNguoiDaiDien;
using SurveyApplication.Application.DTOs.NguoiDaiDien;
using SurveyApplication.Application.Features.DonVis.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.DonVis.Handlers.Queries;

public class GetDonViDetailRequestHandler : BaseMasterFeatures,
    IRequestHandler<GetDonViDetailRequest, CreateDonViAndNguoiDaiDienDto>
{
    private readonly IMapper _mapper;

    public GetDonViDetailRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<CreateDonViAndNguoiDaiDienDto> Handle(GetDonViDetailRequest request,
        CancellationToken cancellationToken)
    {
        var donViRepository = await _surveyRepo.DonVi.GetById(request.Id);
        var nguoiDaiDien = await _surveyRepo.NguoiDaiDien.FirstOrDefaultAsync(x => x.IdDonVi == donViRepository.Id);
        return new CreateDonViAndNguoiDaiDienDto
        {
            DonViDto = _mapper.Map<CreateDonViDto>(donViRepository),
            NguoiDaiDienDto = _mapper.Map<CreateNguoiDaiDienDto>(nguoiDaiDien)
        };
    }
}