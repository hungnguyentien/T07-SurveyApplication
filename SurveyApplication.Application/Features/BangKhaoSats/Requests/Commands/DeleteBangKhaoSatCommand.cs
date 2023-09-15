using MediatR;

namespace SurveyApplication.Application.Features.BangKhaoSats.Requests.Commands;

public class DeleteBangKhaoSatCommand : IRequest
{
    public int Id { get; set; }
}