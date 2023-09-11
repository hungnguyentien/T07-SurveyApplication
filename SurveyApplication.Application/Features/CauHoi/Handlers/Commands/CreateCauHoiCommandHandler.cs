using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.CauHoi.Validators;
using SurveyApplication.Application.Features.CauHoi.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.CauHoi.Handlers.Commands;

public class CreateCauHoiCommandHandler : BaseMasterFeatures, IRequestHandler<CreateCauHoiCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public CreateCauHoiCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(CreateCauHoiCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new CreateCauHoiDtoValidator(_surveyRepo);
        var validatorResult = await validator.ValidateAsync(request.CauHoiDto, cancellationToken);
        if (!validatorResult.IsValid)
        {
            response.Success = false;
            response.Message = "Tạo mới thất bại";
            response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
            return response;
        }

        var cauHoi = _mapper.Map<Domain.CauHoi>(request.CauHoiDto);
        await _surveyRepo.CauHoi.InsertAsync(cauHoi);
        await _surveyRepo.SaveAync();
        if (cauHoi != null)
        {
            if (request.CauHoiDto.LstCot != null)
            {
                var lstCot = _mapper.Map<List<Cot>>(request.CauHoiDto.LstCot);
                lstCot.ForEach(x => x.IdCauHoi = cauHoi.Id);
                await _surveyRepo.Cot.InsertAsync(lstCot);
                await _surveyRepo.SaveAync();
            }

            if (request.CauHoiDto.LstHang != null)
            {
                var lstHang = _mapper.Map<List<Hang>>(request.CauHoiDto.LstHang);
                lstHang.ForEach(x => x.IdCauHoi = cauHoi.Id);
                await _surveyRepo.Hang.InsertAsync(lstHang);
                await _surveyRepo.SaveAync();
            }
        }

        response.Success = true;
        response.Message = "Tạo mới thành công!";
        response.Id = cauHoi?.Id ?? 0;
        return response;
    }
}