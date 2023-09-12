using AutoMapper;
using FluentValidation;
using MediatR;
using SurveyApplication.Application.DTOs.CauHoi.Validators;
using SurveyApplication.Application.Features.CauHoi.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.CauHoi.Handlers.Commands;

public class UpdateCauHoiCommandHandler : BaseMasterFeatures, IRequestHandler<UpdateCauHoiCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public UpdateCauHoiCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(UpdateCauHoiCommand request, CancellationToken cancellationToken)
    {
        if (await _surveyRepo.BangKhaoSatCauHoi.Exists(x => !x.Deleted && request.CauHoiDto.Id == x.IdCauHoi))
            throw new ValidationException("Câu hỏi đã được sử dụng không được sửa");

        var response = new BaseCommandResponse();
        var validator = new UpdateCauHoiDtoValidator(_surveyRepo, request.CauHoiDto.Id);
        var validatorResult = await validator.ValidateAsync(request.CauHoiDto, cancellationToken);
        if (!validatorResult.IsValid)
        {
            response.Success = false;
            response.Message = "Tạo mới thất bại";
            response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
            return response;
        }

        var cauHoi = await _surveyRepo.CauHoi.GetById(request.CauHoiDto.Id);
        _mapper.Map(request.CauHoiDto, cauHoi);
        await _surveyRepo.CauHoi.UpdateAsync(cauHoi);

        if (request.CauHoiDto.LstCot != null && request.CauHoiDto.LstCot.Any())
        {
            var lstCot = _mapper.Map<List<Cot>>(request.CauHoiDto.LstCot);
            lstCot.ForEach(x => x.IdCauHoi = cauHoi.Id);
            await _surveyRepo.Cot.UpdateAsync(lstCot.Where(x => x.Id > 0));
            await _surveyRepo.Cot.InsertAsync(lstCot.Where(x => x.Id == 0));
            await _surveyRepo.Cot.DeleteAsync(await _surveyRepo.Cot.GetAllListAsync(x =>
                x.IdCauHoi == cauHoi.Id && !lstCot.Select(c => c.Id).Contains(x.Id) && !x.Deleted));
        }

        if (request.CauHoiDto.LstHang != null && request.CauHoiDto.LstHang.Any())
        {
            var lstHang = _mapper.Map<List<Hang>>(request.CauHoiDto.LstHang);
            lstHang.ForEach(x => x.IdCauHoi = cauHoi.Id);
            await _surveyRepo.Hang.InsertAsync(lstHang.Where(x => x.Id > 0));
            await _surveyRepo.Hang.InsertAsync(lstHang.Where(x => x.Id == 0));
            await _surveyRepo.Hang.DeleteAsync(await _surveyRepo.Hang.GetAllListAsync(x =>
                x.IdCauHoi == cauHoi.Id && !lstHang.Select(c => c.Id).Contains(x.Id) && !x.Deleted));
        }

        await _surveyRepo.SaveAync();
        response.Message = "Cập nhật thành công!";
        response.Id = cauHoi?.Id ?? 0;
        return response;
    }
}