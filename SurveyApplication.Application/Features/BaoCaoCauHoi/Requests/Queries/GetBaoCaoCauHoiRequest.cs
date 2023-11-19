using MediatR;
using SurveyApplication.Application.DTOs.BaoCaoCauHoi;

namespace SurveyApplication.Application.Features.BaoCaoCauHoi.Requests.Queries;

public class GetBaoCaoCauHoiRequest : IRequest<ThongKeBaoCaoDto>
{
    public int IdDotKhaoSat { get; set; }
    public int IdBangKhaoSat { get; set; }
    public int? IdLoaiHinhDonVi { get; set; }
    public int? IdDonVi { get; set; }

    public string? NgayBatDau { get; set; }

    public string? NgayKetThuc { get; set; }
}