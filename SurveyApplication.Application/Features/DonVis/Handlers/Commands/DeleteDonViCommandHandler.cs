using AutoMapper;
using MediatR;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.DonVis.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.DonVis.Handlers.Commands
{
    public class DeleteDonViCommandHandler : BaseMasterFeatures, IRequestHandler<DeleteDonViCommand>
    {
        public DeleteDonViCommandHandler(ISurveyRepositoryWrapper surveyRepository) : base(surveyRepository)
        {
        }

        public async Task<Unit> Handle(DeleteDonViCommand request, CancellationToken cancellationToken)
        {
            var donVi = await _surveyRepo.DonVi.GetById(request.Id) ?? throw new NotFoundException(nameof(DonVi), request.Id);
            var lstNguoiDaiDien = await _surveyRepo.NguoiDaiDien.GetAllListAsync(x => x.IdDonVi == donVi.Id);
            await _surveyRepo.DonVi.DeleteAsync(donVi);
            await _surveyRepo.NguoiDaiDien.DeleteAsync(lstNguoiDaiDien);
            await _surveyRepo.SaveAync();
            return Unit.Value;
        }
    }
}
