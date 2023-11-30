using MediatR;
using SurveyApplication.Application.DTOs.PhieuKhaoSat;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Commands
{
    public class CreateLogNhanMailCommand : IRequest<BaseCommandResponse>
    {
        public LogNhanMailDto? LogNhanMailDto { get; set; }
    }
}
