using MediatR;
using SurveyApplication.Application.DTOs.NguoiDaiDien;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Queries;

public class GetNguoiDaiDienConditionsRequest : IRequest<BaseQuerieResponse<NguoiDaiDienDto>>
{
    public List<NguoiDaiDienDto> Data { get; set; }
    public int PageCount { get; set; }
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public string Keyword { get; set; }

    public NguoiDaiDienDto DonViNguoiDaiDienDtoDto { get; set; }
}