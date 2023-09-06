using MediatR;
using SurveyApplication.Application.DTOs.NguoiDaiDien;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Commands
{
    public class CreateNguoiDaiDienCommand : IRequest<BaseCommandResponse>
    {
        public CreateNguoiDaiDienDto? NguoiDaiDienDto { get; set; }
    }
}
