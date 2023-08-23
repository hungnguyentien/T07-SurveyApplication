using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Commands;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.BangKhaoSats.Handlers.Commands
{
    public class UpdateBangKhaoSatCommandHandler : IRequestHandler<UpdateBangKhaoSatCommand, Unit>
    {
        private readonly IBangKhaoSatRepository _bangKhaoSatRepository;
        private readonly IMapper _mapper;

        public UpdateBangKhaoSatCommandHandler(IBangKhaoSatRepository bangKhaoSatRepository, IMapper mapper)
        {
            _bangKhaoSatRepository = bangKhaoSatRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateBangKhaoSatCommand request, CancellationToken cancellationToken)
        {
            var loaiHinhDonVi = await _bangKhaoSatRepository.GetById(request.BangKhaoSatDto.MaBangKhaoSat);
            _mapper.Map(request.BangKhaoSatDto, loaiHinhDonVi);
            await _bangKhaoSatRepository.Update(loaiHinhDonVi);
            return Unit.Value;
        }
    }
}
