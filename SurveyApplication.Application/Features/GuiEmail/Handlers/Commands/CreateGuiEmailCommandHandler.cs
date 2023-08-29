using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.DotKhaoSat.Validators;
using SurveyApplication.Application.DTOs.GuiEmail.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.GuiEmails.Requests.Commands;
using SurveyApplication.Application.Responses;
using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.GuiEmails.Handlers.Commands
{
    
    public class CreateGuiEmailCommandHandler : IRequestHandler<CreatGuiEmailCommand, BaseCommandResponse>
    {
        private readonly IGuiEmailRepository _GuiEmailRepository;
        private readonly IMapper _mapper;

        public CreateGuiEmailCommandHandler(IGuiEmailRepository GuiEmailRepository, IMapper mapper)
        {
            _GuiEmailRepository = GuiEmailRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreatGuiEmailCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateGuiEmailDtoValidator(_GuiEmailRepository);
            var validatorResult = await validator.ValidateAsync(request.GuiEmailDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Tạo mới thất bại";
                response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
                throw new ValidationException(validatorResult);
            }
            var GuiEmail = _mapper.Map<GuiEmail>(request.GuiEmailDto);

            GuiEmail = await _GuiEmailRepository.Create(GuiEmail);

            response.Success = true;
            response.Message = "Tạo mới thành công";
            response.Id = GuiEmail.MaGuiEmail.ToString();
            return response;
        }
    }

}
