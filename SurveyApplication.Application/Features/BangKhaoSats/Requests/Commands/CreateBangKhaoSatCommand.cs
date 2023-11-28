using MediatR;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.BangKhaoSats.Requests.Commands;

public class CreateBangKhaoSatCommand : IRequest<BaseCommandResponse>
{
    public CreateBangKhaoSatDto? BangKhaoSatDto { get; set; }
}