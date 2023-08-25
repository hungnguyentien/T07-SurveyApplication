using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.GuiEmail;
using SurveyApplication.Application.Features.GuiEmails.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.GuiEmails.Handlers.Queries
{
    
    public class GetGuiEmailListRequestHandler : IRequestHandler<GetGuiEmailListRequest, List<GuiEmailDto>>
    {
        private readonly IGuiEmailRepository _guiEmailRepository;
        private readonly IMapper _mapper;

        public GetGuiEmailListRequestHandler(IGuiEmailRepository guiEmailRepository, IMapper mapper)
        {
            _guiEmailRepository = guiEmailRepository;
            _mapper = mapper;
        }

        public async Task<List<GuiEmailDto>> Handle(GetGuiEmailListRequest request, CancellationToken cancellationToken)
        {
            var guiEmails = await _guiEmailRepository.GetAll();
            return _mapper.Map<List<GuiEmailDto>>(guiEmails);
        }
    }
}
