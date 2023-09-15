using MediatR;
using SurveyApplication.Application.DTOs.BangKhaoSat;

namespace SurveyApplication.Application.Features.BangKhaoSats.Requests.Commands;

public class UpdateBangKhaoSatCommand : IRequest<Unit>
{
    public UpdateBangKhaoSatDto? BangKhaoSatDto { get; set; }
}