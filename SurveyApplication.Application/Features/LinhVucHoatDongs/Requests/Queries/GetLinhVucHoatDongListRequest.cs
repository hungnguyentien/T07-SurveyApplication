using MediatR;
using SurveyApplication.Application.DTOs.LinhVucHoatDong;
using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.LinhVucHoatDongs.Requests.Queries
{
    public class GetLinhVucHoatDongListRequest : IRequest<List<LinhVucHoatDongDto>>
    {
    }
}
