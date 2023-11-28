﻿using MediatR;
using SurveyApplication.Application.DTOs.GuiEmail;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.GuiEmail.Requests.Commands;

public class SendKhaoSatCommand : IRequest<BaseCommandResponse>
{
    public CreateGuiEmailDto? GuiEmailDto { get; set; }
}