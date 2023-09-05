using MediatR;
using SurveyApplication.Application.DTOs.PhieuKhaoSat;
using SurveyApplication.Application.Responses;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Commands
{
    public class CreateKetQuaCommand : IRequest<BaseCommandResponse>
    {
        public CreateKetQuaDto? CreateKetQuaDto { get; set; }
    }
}
