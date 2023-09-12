using MediatR;
using SurveyApplication.Application.DTOs.LinhVucHoatDong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Commands
{
    public class UpdateLinhVucHoatDongCommand : IRequest<Unit>
    {
        public UpdateLinhVucHoatDongDto? LinhVucHoatDongDto { get; set; }
    }
}
