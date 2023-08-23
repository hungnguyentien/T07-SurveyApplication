using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
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
            var bangKhaoSat = await _bangKhaoSatRepository.GetById(request.BangKhaoSatDto?.Id ?? 0);
            _mapper.Map(request.BangKhaoSatDto, bangKhaoSat);
            await _bangKhaoSatRepository.Update(bangKhaoSat);
            return Unit.Value;
        }
    }
}
