using MediatR;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;

namespace SurveyApplication.Application.Features.LoaiHinhDonVi.Requests.Queries;

public class GetLoaiHinhDonViListRequest : IRequest<List<LoaiHinhDonViDto>>
{
}