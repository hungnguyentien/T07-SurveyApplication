using AutoMapper;
using MediatR;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.GuiEmail.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.GuiEmail.Handlers.Commands
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
            if (GuiEmailRepository == null)
            {
                throw new NotFoundException(nameof(GuiEmail), request.Id);
            }
            await _GuiEmailRepository.Delete(GuiEmailRepository);
            return Unit.Value;
        }
    }
}
