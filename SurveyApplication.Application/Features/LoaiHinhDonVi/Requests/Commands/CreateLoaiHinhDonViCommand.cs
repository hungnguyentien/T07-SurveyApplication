using MediatR;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.LoaiHinhDonVi.Requests.Commands;

public class CreateLoaiHinhDonViCommand : IRequest<BaseCommandResponse>
{
    public CreateLoaiHinhDonViDto? LoaiHinhDonViDto { get; set; }
}