using MediatR;
using SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Handlers.Commands
{
    public class CreateLogNhanMailCommandHandler : BaseMasterFeatures, IRequestHandler<CreateLogNhanMailCommand, BaseCommandResponse>
    {
        public CreateLogNhanMailCommandHandler(ISurveyRepositoryWrapper surveyRepository) : base(surveyRepository)
        {
        }

        public async Task<BaseCommandResponse> Handle(CreateLogNhanMailCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var log = await _surveyRepo.LogNhanMail.FirstOrDefaultAsync(x => request.LogNhanMailDto != null && x.MaDoanhNghiep == request.LogNhanMailDto.MaDoanhNghiep && !x.Deleted);
            if (log != null || request.LogNhanMailDto == null) return response;
            if (string.IsNullOrEmpty(request.LogNhanMailDto.MaDoanhNghiep)) return response;
            await _surveyRepo.LogNhanMail.Create(new LogNhanMail
            {
                MaDoanhNghiep = request.LogNhanMailDto.MaDoanhNghiep,
                Data = request.LogNhanMailDto.Data
            });
            await _surveyRepo.SaveAync();
            return response;
        }
    }
}
