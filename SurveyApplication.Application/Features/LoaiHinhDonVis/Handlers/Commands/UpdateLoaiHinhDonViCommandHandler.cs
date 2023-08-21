using AutoMapper;
using MediatR;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Commands;
using SurveyApplication.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var loaiHinhDonVi = await _loaiHinhDonViRepository.GetById(request.LoaiHinhDonViDto.Maloaihinh);
            _mapper.Map(request.LoaiHinhDonViDto, loaiHinhDonVi);
            await _loaiHinhDonViRepository.Update(loaiHinhDonVi);
            return Unit.Value;
        }
    }
}
