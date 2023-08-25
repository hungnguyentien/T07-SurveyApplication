using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.NguoiDaiDien;
using SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.NguoiDaiDiens.Handlers.Commands
{
    public class UpdateNguoiDaiDienCommandHandler : IRequestHandler<UpdateNguoiDaiDienCommand, Unit>
    {
        private readonly INguoiDaiDienRepository _nguoiDaiDienRepository;
        private readonly IMapper _mapper;

        public UpdateNguoiDaiDienCommandHandler(INguoiDaiDienRepository nguoiDaiDienRepository, IMapper mapper)
        {
            _nguoiDaiDienRepository = nguoiDaiDienRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateNguoiDaiDienCommand request, CancellationToken cancellationToken)
        {
            var NguoiDaiDien = await _nguoiDaiDienRepository.GetById(request.NguoiDaiDienDto?.Id ?? 0);
            _mapper.Map(request.NguoiDaiDienDto, NguoiDaiDien);
            await _nguoiDaiDienRepository.Update(NguoiDaiDien);
            return Unit.Value;
        }
    }
}
