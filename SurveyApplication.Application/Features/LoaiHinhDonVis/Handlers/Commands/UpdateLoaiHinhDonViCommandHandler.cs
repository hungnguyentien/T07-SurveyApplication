using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Commands;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Handlers.Commands
{
    public class UpdateLoaiHinhDonViCommandHandler : IRequestHandler<UpdateLoaiHinhDonViCommand, Unit>
    {
        private readonly ILoaiHinhDonViRepository _LoaiHinhDonViRepository;
        private readonly IMapper _mapper;

        public UpdateLoaiHinhDonViCommandHandler(ILoaiHinhDonViRepository LoaiHinhDonViRepository, IMapper mapper)
        {
            _LoaiHinhDonViRepository = LoaiHinhDonViRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLoaiHinhDonViCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLoaiHinhDonViDtoValidator(_LoaiHinhDonViRepository);
            var validatorResult = await validator.ValidateAsync(request.LoaiHinhDonViDto);

            if (validatorResult.IsValid == false)
            {
                throw new ValidationException(validatorResult);
            }

            var LoaiHinhDonVi = await _LoaiHinhDonViRepository.GetById(request.LoaiHinhDonViDto?.Id ?? 0);
            _mapper.Map(request.LoaiHinhDonViDto, LoaiHinhDonVi);
            await _LoaiHinhDonViRepository.Update(LoaiHinhDonVi);
            return Unit.Value;
        }
    }
}
