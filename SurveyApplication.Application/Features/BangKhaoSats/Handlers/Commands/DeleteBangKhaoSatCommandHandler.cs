using MediatR;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.BangKhaoSats.Handlers.Commands
{
    public class DeleteBangKhaoSatCommandHandler : BaseMasterFeatures, IRequestHandler<DeleteBangKhaoSatCommand, BaseCommandResponse>
    {
        public DeleteBangKhaoSatCommandHandler(ISurveyRepositoryWrapper surveyRepository) : base(surveyRepository)
        {
        }

        public async Task<BaseCommandResponse> Handle(DeleteBangKhaoSatCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var lstBangKhaoSat = await _surveyRepo.BangKhaoSat.GetByIds(x => request.Ids.Contains(x.Id));

            if (lstBangKhaoSat == null || lstBangKhaoSat.Count == 0)
                throw new NotFoundException(nameof(BangKhaoSat), request.Ids);

            foreach (var item in lstBangKhaoSat)
                item.Deleted = true;

            await _surveyRepo.BangKhaoSat.UpdateAsync(lstBangKhaoSat);
            await _surveyRepo.SaveAync();
            return new BaseCommandResponse("Xóa thành công!");
        }
    }
}
