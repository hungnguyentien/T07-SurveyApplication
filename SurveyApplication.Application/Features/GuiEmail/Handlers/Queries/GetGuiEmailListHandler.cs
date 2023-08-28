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
    
    public class GetGuiEmailListHandler : IRequestHandler<GetGuiEmailDetailRequest, GuiEmailDto>
    {
        private readonly IGuiEmailRepository _guiEmailRepository;
        private readonly IMapper _mapper;

        public GetGuiEmailListHandler(IGuiEmailRepository guiEmailRepository, IMapper mapper)
        {
            _guiEmailRepository = guiEmailRepository;
            _mapper = mapper;
        }

        public async Task<GuiEmailDto> Handle(GetGuiEmailDetailRequest request, CancellationToken cancellationToken)
        {
            var guiEmailRepository = await _guiEmailRepository.GetById(request.Id);
            return _mapper.Map<GuiEmailDto>(guiEmailRepository);
        }
    }
}
