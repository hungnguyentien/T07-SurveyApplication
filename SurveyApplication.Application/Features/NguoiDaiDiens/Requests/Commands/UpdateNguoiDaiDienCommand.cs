using MediatR;
using SurveyApplication.Application.DTOs.NguoiDaiDien;

namespace SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Commands;

public class UpdateNguoiDaiDienCommand : IRequest<Unit>
{
    public UpdateNguoiDaiDienDto? NguoiDaiDienDto { get; set; }
}