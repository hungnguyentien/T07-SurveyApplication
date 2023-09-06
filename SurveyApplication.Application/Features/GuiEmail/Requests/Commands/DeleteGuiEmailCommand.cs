using MediatR;

namespace SurveyApplication.Application.Features.GuiEmail.Requests.Commands
{
    public class DeleteGuiEmailCommand : IRequest
    {
        public int Id { get; set; }
    }
}
