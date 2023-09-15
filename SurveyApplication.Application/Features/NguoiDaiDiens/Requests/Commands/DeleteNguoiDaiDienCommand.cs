using MediatR;

namespace SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Commands;

public class DeleteNguoiDaiDienCommand : IRequest
{
    public int Id { get; set; }
}