using MediatR;
using SurveyApplication.Application.DTOs.NguoiDaiDien;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Commands;

public class UpdateNguoiDaiDienCommand : IRequest<BaseCommandResponse>
{
    public UpdateNguoiDaiDienDto? NguoiDaiDienDto { get; set; }
}