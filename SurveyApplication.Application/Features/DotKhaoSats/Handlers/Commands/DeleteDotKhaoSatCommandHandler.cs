using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.DotKhaoSats.Requests.Commands;
using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.DotKhaoSats.Handlers.Commands
{
   
    public class DeleteDotKhaoSatCommandHandler : IRequestHandler<DeleteDotKhaoSatCommand>
    {
        private readonly IDotKhaoSatRepository _dotKhaoSatRepository;
        private readonly IMapper _mapper;

        public DeleteDotKhaoSatCommandHandler(IDotKhaoSatRepository dotKhaoSatRepository, IMapper mapper)
        {
            _dotKhaoSatRepository = dotKhaoSatRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteDotKhaoSatCommand request, CancellationToken cancellationToken)
        {
            var dotKhaoSatRepository = await _dotKhaoSatRepository.GetById(request.Id);
            if (dotKhaoSatRepository == null)
            {
                throw new NotFoundException(nameof(DotKhaoSat), request.Id);
            }
            await _dotKhaoSatRepository.Delete(dotKhaoSatRepository);
            return Unit.Value;
        }
    }
}
