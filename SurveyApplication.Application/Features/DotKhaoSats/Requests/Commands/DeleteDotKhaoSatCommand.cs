using MediatR;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.DotKhaoSats.Requests.Commands
{
    public class DeleteDotKhaoSatCommand : IRequest<BaseCommandResponse>
    {
        public List<int> Ids { get; set; }
    }
}
