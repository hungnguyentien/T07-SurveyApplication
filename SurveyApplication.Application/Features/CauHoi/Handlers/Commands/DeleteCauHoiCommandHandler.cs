﻿using AutoMapper;
using MediatR;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.CauHoi.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using ValidationException = FluentValidation.ValidationException;

namespace SurveyApplication.Application.Features.CauHoi.Handlers.Commands;

public class DeleteCauHoiCommandHandler : BaseMasterFeatures, IRequestHandler<DeleteCauHoiCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public DeleteCauHoiCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(DeleteCauHoiCommand request, CancellationToken cancellationToken)
    {
        var lstCauHoi = await _surveyRepo.CauHoi.GetByIds(x => request.Ids.Contains(x.Id));
        if (lstCauHoi == null || lstCauHoi.Count == 0)
            throw new NotFoundException(nameof(CauHoi), request.Ids);

        if(await _surveyRepo.BangKhaoSatCauHoi.Exists(x => !x.Deleted && request.Ids.Contains(x.IdCauHoi)))
            throw new ValidationException("Câu hỏi đã được sử dụng");

        var lstCot = await _surveyRepo.Cot.GetByIds(x => request.Ids.Contains(x.IdCauHoi));

        var lstHang = await _surveyRepo.Hang.GetByIds(x => request.Ids.Contains(x.IdCauHoi));

        foreach (var item in lstCot)
            item.Deleted = true;

        foreach (var item in lstHang)
            item.Deleted = true;

        foreach (var item in lstCauHoi)
            item.Deleted = true;

        await _surveyRepo.CauHoi.UpdateAsync(lstCauHoi);
        await _surveyRepo.Cot.UpdateAsync(lstCot);
        await _surveyRepo.Hang.UpdateAsync(lstHang);
        await _surveyRepo.SaveAync();
        return new BaseCommandResponse("Xóa thành công!");
    }
}