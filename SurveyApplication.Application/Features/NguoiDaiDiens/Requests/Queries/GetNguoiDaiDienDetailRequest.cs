using MediatR;
using SurveyApplication.Application.DTOs.NguoiDaiDien;

namespace SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Queries;

public class GetNguoiDaiDienDetailRequest : IRequest<NguoiDaiDienDto>
{
    public int Id { get; set; }
}