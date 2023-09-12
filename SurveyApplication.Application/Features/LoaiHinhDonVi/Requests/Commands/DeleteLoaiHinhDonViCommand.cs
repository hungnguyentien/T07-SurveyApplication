using MediatR;

namespace SurveyApplication.Application.Features.LoaiHinhDonVi.Requests.Commands;

public class DeleteLoaiHinhDonViCommand : IRequest
{
    public int Id { get; set; }
}