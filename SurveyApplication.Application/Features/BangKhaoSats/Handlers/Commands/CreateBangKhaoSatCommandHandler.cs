using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.BangKhaoSat.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.BangKhaoSats.Handlers.Commands;

public class CreateBangKhaoSatCommandHandler : BaseMasterFeatures,
    IRequestHandler<CreateBangKhaoSatCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public CreateBangKhaoSatCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(CreateBangKhaoSatCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new CreateBangKhaoSatDtoValidator(_surveyRepo.BangKhaoSat);
        if (request.BangKhaoSatDto != null)
        {
            var validatorResult = await validator.ValidateAsync(request.BangKhaoSatDto, cancellationToken);
            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Tạo mới thất bại";
                response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
                throw new ValidationException(validatorResult);
            }
        }

        var bangKhaoSat = _mapper.Map<BangKhaoSat>(request.BangKhaoSatDto);
        bangKhaoSat = await _surveyRepo.BangKhaoSat.Create(bangKhaoSat);
        await _surveyRepo.SaveAync();
        if (request.BangKhaoSatDto?.BangKhaoSatCauHoi != null)
        {
            var lstBangKhaoSatCauHoi = _mapper.Map<List<BangKhaoSatCauHoi>>(request.BangKhaoSatDto.BangKhaoSatCauHoi);
            lstBangKhaoSatCauHoi?.ForEach(x => x.IdBangKhaoSat = bangKhaoSat.Id);
            if (lstBangKhaoSatCauHoi != null)
            {
                await _surveyRepo.BangKhaoSatCauHoi.Creates(lstBangKhaoSatCauHoi);
                await _surveyRepo.SaveAync();
            }
        }

        response.Success = true;
        response.Message = "Tạo mới thành công";
        response.Id = bangKhaoSat.Id;
        return response;
    }
}