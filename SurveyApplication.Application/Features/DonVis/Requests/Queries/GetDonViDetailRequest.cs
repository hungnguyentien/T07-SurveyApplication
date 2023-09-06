using MediatR;
using SurveyApplication.Application.DTOs.DonViAndNguoiDaiDien;

namespace SurveyApplication.Application.Features.DonVis.Requests.Queries
{
    public class GetDonViDetailRequest : IRequest<CreateDonViAndNguoiDaiDienDto>
    {
        public int Id { get; set; }
    }
}
