using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.Features.CauHoi.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.CauHoi.Handlers.Queries
{
    public class GetLastRecordCauHoiRequestHandler : BaseMasterFeatures, IRequestHandler<GetLastRecordCauHoiRequest, string>
    {
        public GetLastRecordCauHoiRequestHandler(ISurveyRepositoryWrapper surveyRepository) : base(surveyRepository)
        {
        }

        public async Task<string> Handle(GetLastRecordCauHoiRequest request, CancellationToken cancellationToken)
        {
            var lastEntity = await _surveyRepo.CauHoi.GetAllQueryable().OrderByDescending(e => e.Id).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            if (lastEntity == null) return "CH001";
            var prefix = lastEntity.MaCauHoi[..2];
            var isNumber = int.TryParse(prefix, out var currentNumber);
            if (!isNumber) return string.Empty;
            currentNumber++;
            var newNumber = currentNumber.ToString("D3");
            return prefix + newNumber;

        }
    }
}
