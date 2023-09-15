﻿using MediatR;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.DonVis.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.DonVis.Handlers.Commands
{
    public class DeleteDonViCommandHandler : BaseMasterFeatures, IRequestHandler<DeleteDonViCommand, BaseCommandResponse>
    {
        public DeleteDonViCommandHandler(ISurveyRepositoryWrapper surveyRepository) : base(surveyRepository)
        {
        }

        public async Task<BaseCommandResponse> Handle(DeleteDonViCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            if (await _surveyRepo.GuiEmail.Exists(x => !x.Deleted && request.Ids.Contains(x.IdDonVi)))
            {
                response.Success = false;
                response.Message = "Đang có bản ghi liên quan, không thể xóa được!";
                return response;
            }

            var lstDonVi = await _surveyRepo.DonVi.GetByIds(x => request.Ids.Contains(x.Id));
            if (lstDonVi == null || lstDonVi.Count == 0)
                throw new NotFoundException(nameof(DonVi), request.Ids);

            foreach (var item in lstDonVi)
                item.Deleted = true;

            await _surveyRepo.DonVi.UpdateAsync(lstDonVi);
            await _surveyRepo.SaveAync();
            return new BaseCommandResponse("Xóa thành công!");
        }
    }
}
