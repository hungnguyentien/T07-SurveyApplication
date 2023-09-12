using MediatR;
using SurveyApplication.Application.DTOs.LinhVucHoatDong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Queries
{
    public class GetLinhVucHoatDongDetailRequest : IRequest<LinhVucHoatDongDto>
    {
        public int Id { get; set; }
    }
}
