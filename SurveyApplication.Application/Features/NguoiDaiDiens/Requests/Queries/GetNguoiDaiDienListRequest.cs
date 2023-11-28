using MediatR;
using SurveyApplication.Application.DTOs.NguoiDaiDien;

namespace SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Queries;

public class GetNguoiDaiDienListRequest : IRequest<List<NguoiDaiDienDto>>
{
}