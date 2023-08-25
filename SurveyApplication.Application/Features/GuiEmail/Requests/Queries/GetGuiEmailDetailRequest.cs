using MediatR;
using SurveyApplication.Application.DTOs.GuiEmail;
using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.GuiEmails.Requests.Queries
{
    
    public class GetGuiEmailDetailRequest : IRequest<GuiEmailDto>
    {
        public int Id { get; set; }
    }
}
