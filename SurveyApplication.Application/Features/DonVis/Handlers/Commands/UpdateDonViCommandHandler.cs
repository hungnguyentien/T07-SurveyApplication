using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.DonVi.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.DonVis.Requests.Commands;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.DonVis.Handlers.Commands
{
    public class UpdateDonViCommandHandler : IRequestHandler<UpdateDonViCommand, Unit>
    {
        private readonly IDonViRepository _donViRepository;
        private readonly IMapper _mapper;

        public UpdateDonViCommandHandler(IDonViRepository donViRepository, IMapper mapper)
        {
            _donViRepository = donViRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateDonViCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateDonViDtoValidator(_donViRepository);
            var validatorResult = await validator.ValidateAsync(request.DonViDto);
            if (validatorResult.IsValid == false)
            {
                throw new ValidationException(validatorResult);
            }

            var DonVi = await _donViRepository.GetById(request.DonViDto?.Id ?? 0);
            _mapper.Map(request.DonViDto, DonVi);
            await _donViRepository.Update(DonVi);
            return Unit.Value;
        }
    }
}
