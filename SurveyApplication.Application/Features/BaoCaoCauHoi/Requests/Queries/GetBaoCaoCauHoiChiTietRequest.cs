using MediatR;
using SurveyApplication.Application.DTOs.BaoCaoCauHoi;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.BaoCaoCauHoi.Requests.Queries;

public class GetBaoCaoCauHoiChiTietRequest : IRequest<BaseQuerieResponse<BaoCaoCauHoiChiTietDto>>
{
    public int IdDotKhaoSat { get; set; }
    public int IdBangKhaoSat { get; set; }
    public int? IdLoaiHinhDonVi { get; set; }
    public int? IdDonVi { get; set; }
    public DateTime? NgayBatDau { get; set; }
    public DateTime? NgayKetThuc { get; set; }

    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public string? Keyword { get; set; }
}