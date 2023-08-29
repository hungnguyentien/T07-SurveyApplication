using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.BangKhaoSat.Validators;
using SurveyApplication.Application.DTOs.GuiEmail.Validators;
using SurveyApplication.Application.Exceptions;
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
            var validator = new UpdateGuiEmailDtoValidator(_guiEmailRepository);
            var validatorResult = await validator.ValidateAsync(request.GuiEmailDto);

            if (validatorResult.IsValid == false)
            {
                throw new ValidationException(validatorResult);
            }

            var guiEmail = await _guiEmailRepository.GetById(request.GuiEmailDto?.Id ?? 0);
            _mapper.Map(request.GuiEmailDto, guiEmail);
            await _guiEmailRepository.Update(guiEmail);
            return Unit.Value;
        }
    }
}
