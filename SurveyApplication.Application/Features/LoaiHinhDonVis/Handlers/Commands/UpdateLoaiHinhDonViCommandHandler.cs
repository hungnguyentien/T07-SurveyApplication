using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Commands;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var LoaiHinhDonVi = await _LoaiHinhDonViRepository.GetById(request.LoaiHinhDonViDto?.Id ?? 0);
            _mapper.Map(request.LoaiHinhDonViDto, LoaiHinhDonVi);
            await _LoaiHinhDonViRepository.Update(LoaiHinhDonVi);
            return Unit.Value;
        }
    }
}
