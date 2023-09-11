using MediatR;
using SurveyApplication.Application.DTOs.CauHoi;

namespace SurveyApplication.Application.Features.CauHoi.Requests.Queries;

public class GetConfigCauHoiRequest : IRequest<PhieuKhaoSatDto>
{
    //public int IdBangKhaoSat { get; set; }
    //public int IdDonVi { get; set; }
    //public int IdNguoiDaiDien { get; set; }
    public int IdGuiEmail { get; set; }
}