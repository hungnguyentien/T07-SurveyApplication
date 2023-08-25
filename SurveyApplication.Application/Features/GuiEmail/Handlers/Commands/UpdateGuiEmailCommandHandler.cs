using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Commands;
using SurveyApplication.Application.Features.GuiEmails.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.GuiEmails.Handlers.Commands
{
   
    public class UpdateGuiEmailCommandHandler : IRequestHandler<UpdateGuiEmailCommand, Unit>
    {
        private readonly IGuiEmailRepository _guiEmailRepository;
        private readonly IMapper _mapper;

        public UpdateGuiEmailCommandHandler(IGuiEmailRepository guiEmailRepository, IMapper mapper)
        {
            _guiEmailRepository = guiEmailRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateGuiEmailCommand request, CancellationToken cancellationToken)
        {
            var guiEmail = await _guiEmailRepository.GetById(request.GuiEmailDto?.Id ?? 0);
            _mapper.Map(request.GuiEmailDto, guiEmail);
            await _guiEmailRepository.Update(guiEmail);
            return Unit.Value;
        }
    }
}
