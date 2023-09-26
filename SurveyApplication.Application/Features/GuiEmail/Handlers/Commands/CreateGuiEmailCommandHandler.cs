using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.DTOs.GuiEmail.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.GuiEmail.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.Application.Features.GuiEmail.Handlers.Commands;

public class CreateGuiEmailCommandHandler : BaseMasterFeatures,
    IRequestHandler<CreateGuiEmailCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public CreateGuiEmailCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(CreateGuiEmailCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new CreateGuiEmailDtoValidator(_surveyRepo.GuiEmail);
        if (request.GuiEmailDto != null)
        {
            var validatorResult = await validator.ValidateAsync(request.GuiEmailDto, cancellationToken);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Tạo mới thất bại";
                response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
                throw new ValidationException(validatorResult);
            }
        }

        var guiEmail = _mapper.Map<Domain.GuiEmail>(request.GuiEmailDto);
        guiEmail = await _surveyRepo.GuiEmail.Create(guiEmail);
        await _surveyRepo.SaveAync();
        var lstDotKhaoSat = await (from a in _surveyRepo.DotKhaoSat.GetAllQueryable()
                                   join b in _surveyRepo.BangKhaoSat.GetAllQueryable() on a.Id equals b.IdDotKhaoSat
                                   where a.TrangThai == (int)EnumDotKhaoSat.TrangThai.ChoKhaoSat && request.GuiEmailDto.LstBangKhaoSat.Contains(b.IdDotKhaoSat) && !a.Deleted && !b.Deleted
                                   select a).ToListAsync(cancellationToken: cancellationToken);

        if (lstDotKhaoSat.Any())
        {
            lstDotKhaoSat.ForEach(x => x.TrangThai = (int)EnumDotKhaoSat.TrangThai.DangKhaoSat);
            await _surveyRepo.DotKhaoSat.UpdateAsync(lstDotKhaoSat);
            await _surveyRepo.SaveAync();
        }

        response.Success = true;
        response.Message = "Tạo mới thành công";
        response.Id = guiEmail.Id;
        return response;
    }
}