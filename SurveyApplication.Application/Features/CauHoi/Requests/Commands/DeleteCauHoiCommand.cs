using MediatR;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.CauHoi.Requests.Commands
{
    public class DeleteCauHoiCommand: IRequest<BaseCommandResponse>
    {
        public List<int> Ids { get; set; }
    }
}
