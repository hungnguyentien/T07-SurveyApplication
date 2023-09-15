using MediatR;

namespace SurveyApplication.Application.Features.DotKhaoSats.Requests.Commands;

public class DeleteDotKhaoSatCommand : IRequest
{
    public int Id { get; set; }
}