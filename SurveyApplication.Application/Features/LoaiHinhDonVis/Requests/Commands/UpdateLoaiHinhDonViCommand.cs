using MediatR;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Commands;

public class UpdateLoaiHinhDonViCommand : IRequest<Unit>
{
    public UpdateLoaiHinhDonViDto? LoaiHinhDonViDto { get; set; }
}