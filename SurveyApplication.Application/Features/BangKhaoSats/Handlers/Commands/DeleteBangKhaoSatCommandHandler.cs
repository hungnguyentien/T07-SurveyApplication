using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Commands;
using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.BangKhaoSats.Handlers.Commands
{
    public class DeleteBangKhaoSatCommandHandler : IRequestHandler<DeleteBangKhaoSatCommand>
    {
        private readonly IBangKhaoSatRepository _bangKhaoSatRepository;
        private readonly IMapper _mapper;

        public DeleteBangKhaoSatCommandHandler(IBangKhaoSatRepository bangKhaoSatRepository, IMapper mapper)
        {
            _bangKhaoSatRepository = bangKhaoSatRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteBangKhaoSatCommand request, CancellationToken cancellationToken)
        {
            var bangKhaoSatRepository = await _bangKhaoSatRepository.GetById(request.Id);

            if (bangKhaoSatRepository == null)
            {
                throw new NotFoundException(nameof(BangKhaoSat), request.Id);
            }

            await _bangKhaoSatRepository.Delete(bangKhaoSatRepository);
            return Unit.Value;
        }
    }
}
