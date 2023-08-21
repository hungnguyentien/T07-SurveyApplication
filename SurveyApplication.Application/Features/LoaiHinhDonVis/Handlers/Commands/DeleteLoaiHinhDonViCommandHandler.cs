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
    public class DeleteLoaiHinhDonViCommandHandler : IRequestHandler<DeleteLoaiHinhDonViCommand>
    {
        private readonly ILoaiHinhDonViRepository _loaiHinhDonViRepository;
        private readonly IMapper _mapper;

        public DeleteLoaiHinhDonViCommandHandler(ILoaiHinhDonViRepository loaiHinhDonViRepository, IMapper mapper)
        {
            _loaiHinhDonViRepository = loaiHinhDonViRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteLoaiHinhDonViCommand request, CancellationToken cancellationToken)
        {
            var loaiHinhDonVi = await _loaiHinhDonViRepository.GetById(request.Maloaihinh);
            await _loaiHinhDonViRepository.Delete(loaiHinhDonVi);
            return Unit.Value;
        }
    }
}
