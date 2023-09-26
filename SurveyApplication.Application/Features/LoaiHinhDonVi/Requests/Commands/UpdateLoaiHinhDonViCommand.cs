using MediatR;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.LoaiHinhDonVi.Requests.Commands;

public class UpdateLoaiHinhDonViCommand : IRequest<BaseCommandResponse>
{
    public UpdateLoaiHinhDonViDto? LoaiHinhDonViDto { get; set; }
}