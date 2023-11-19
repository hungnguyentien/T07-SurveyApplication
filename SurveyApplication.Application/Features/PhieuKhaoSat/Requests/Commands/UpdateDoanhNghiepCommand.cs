using MediatR;
using SurveyApplication.Application.DTOs.PhieuKhaoSat;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Commands;

public class UpdateDoanhNghiepCommand : IRequest<BaseCommandResponse>
{
    public UpdateDoanhNghiepDto? UpdateDoanhNghiepDto { get; set; }
}