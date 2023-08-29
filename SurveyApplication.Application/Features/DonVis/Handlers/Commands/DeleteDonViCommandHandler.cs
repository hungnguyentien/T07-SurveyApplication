using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.DonVis.Requests.Commands;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Commands;
using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.DonVis.Handlers.Commands
{
    public class DeleteDonViCommandHandler : IRequestHandler<DeleteDonViCommand>
    {
        private readonly IDonViRepository _donViRepository;
        private readonly IMapper _mapper;

        public DeleteDonViCommandHandler(IDonViRepository donViRepository, IMapper mapper)
        {
            _donViRepository = donViRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteDonViCommand request, CancellationToken cancellationToken)
        {
            var DonViRepository = await _donViRepository.GetById(request.Id);

            if (DonViRepository == null)
            {
                throw new NotFoundException(nameof(DonVi), request.Id);
            }
            await _donViRepository.Delete(DonViRepository);
            return Unit.Value;
        }
    }
}
