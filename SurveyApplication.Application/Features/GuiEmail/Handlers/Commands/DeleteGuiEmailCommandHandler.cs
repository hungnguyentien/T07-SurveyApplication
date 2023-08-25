using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.Features.GuiEmails.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.GuiEmails.Handlers.Commands
{
   
    public class DeleteGuiEmailCommandHandler : IRequestHandler<DeleteGuiEmailCommand>
    {
        private readonly IGuiEmailRepository _GuiEmailRepository;
        private readonly IMapper _mapper;

        public DeleteGuiEmailCommandHandler(IGuiEmailRepository GuiEmailRepository, IMapper mapper)
        {
            _GuiEmailRepository = GuiEmailRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteGuiEmailCommand request, CancellationToken cancellationToken)
        {
            var GuiEmailRepository = await _GuiEmailRepository.GetById(request.Id);
            await _GuiEmailRepository.Delete(GuiEmailRepository);
            return Unit.Value;
        }
    }
}
