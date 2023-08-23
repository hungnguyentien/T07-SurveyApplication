﻿using MediatR;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Commands
{
    public class CreateLoaiHinhDonViCommand : IRequest<BaseCommandResponse>
    {
        public CreateLoaiHinhDonViDto? LoaiHinhDonViDto { get; set; }
    }
}
