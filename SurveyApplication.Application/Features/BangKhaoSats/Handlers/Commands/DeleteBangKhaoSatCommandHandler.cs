using MediatR;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.BangKhaoSats.Handlers.Commands;

public class DeleteBangKhaoSatCommandHandler : BaseMasterFeatures, IRequestHandler<DeleteBangKhaoSatCommand>
{
    public DeleteBangKhaoSatCommandHandler(ISurveyRepositoryWrapper surveyRepository) : base(surveyRepository)
    {
    }

    public async Task<Unit> Handle(DeleteBangKhaoSatCommand request, CancellationToken cancellationToken)
    {
        if (await _surveyRepo.GuiEmail.Exists(x => x.IdBangKhaoSat == request.Id)) throw new FluentValidation.ValidationException("Bảng khảo sát đã được gửi mail");

        var bangKhaoSat = await _surveyRepo.BangKhaoSat.GetById(request.Id) ?? throw new NotFoundException(nameof(BangKhaoSat), request.Id);
        var lstRemove = await _surveyRepo.BangKhaoSatCauHoi.GetAllListAsync(x => x.IdBangKhaoSat == bangKhaoSat.Id);
        await _surveyRepo.BangKhaoSatCauHoi.DeleteAsync(lstRemove);
        await _surveyRepo.BangKhaoSat.DeleteAsync(bangKhaoSat);
        await _surveyRepo.SaveAync();
        return Unit.Value;
    }
}