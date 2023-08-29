using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Commands;
using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Handlers.Commands
{
    public class DeleteLoaiHinhDonViCommandHandler : IRequestHandler<DeleteLoaiHinhDonViCommand>
    {
        private readonly ILoaiHinhDonViRepository _LoaiHinhDonViRepository;
        private readonly IMapper _mapper;

        public DeleteLoaiHinhDonViCommandHandler(ILoaiHinhDonViRepository LoaiHinhDonViRepository, IMapper mapper)
        {
            _LoaiHinhDonViRepository = LoaiHinhDonViRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteLoaiHinhDonViCommand request, CancellationToken cancellationToken)
        {
            var LoaiHinhDonViRepository = await _LoaiHinhDonViRepository.GetById(request.Id);
            if (LoaiHinhDonViRepository == null)
            {
                throw new NotFoundException(nameof(LoaiHinhDonVi), request.Id);
            }
            await _LoaiHinhDonViRepository.Delete(LoaiHinhDonViRepository);
            return Unit.Value;
        }
    }
}
