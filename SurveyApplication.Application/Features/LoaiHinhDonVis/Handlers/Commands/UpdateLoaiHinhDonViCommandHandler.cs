using AutoMapper;
using MediatR;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Commands;
using SurveyApplication.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi.Validators;
using SurveyApplication.Application.Exceptions;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Handlers.Commands
{
    public class UpdateLoaiHinhDonViCommandHandler : IRequestHandler<UpdateLoaiHinhDonViCommand, Unit>
    {
        private readonly ILoaiHinhDonViRepository _loaiHinhDonViRepository;
        private readonly IMapper _mapper;

        public UpdateLoaiHinhDonViCommandHandler(ILoaiHinhDonViRepository loaiHinhDonViRepository, IMapper mapper)
        {
            _loaiHinhDonViRepository = loaiHinhDonViRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLoaiHinhDonViCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLoaiHinhDonViDtoValidator(_loaiHinhDonViRepository);
            var validationResult = await validator.ValidateAsync(request.LoaiHinhDonViDto);

            if (validationResult.IsValid == false)
            {
                throw new ValidationException(validationResult);
            }

            var loaiHinhDonVi = await _loaiHinhDonViRepository.GetById(request.LoaiHinhDonViDto?.Id ?? 0);
            _mapper.Map(request.LoaiHinhDonViDto, loaiHinhDonVi);
            await _loaiHinhDonViRepository.Update(loaiHinhDonVi);
            return Unit.Value;
        }
    }
}
