using MediatR;
using SurveyApplication.Application.DTOs.DotKhaoSat;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.DotKhaoSats.Requests.Commands
{
    public class CreateDotKhaoSatCommand : IRequest<BaseCommandResponse>
    {
        public CreateDotKhaoSatDto? DotKhaoSatDto { get; set; }
    }

}
