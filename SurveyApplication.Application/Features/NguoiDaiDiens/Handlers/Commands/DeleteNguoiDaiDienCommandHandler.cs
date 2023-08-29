using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Commands;
using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.NguoiDaiDiens.Handlers.Commands
{
    public class DeleteNguoiDaiDienCommandHandler : IRequestHandler<DeleteNguoiDaiDienCommand>
    {
        private readonly INguoiDaiDienRepository _nguoiDaiDienRepository;
        private readonly IMapper _mapper;

        public DeleteNguoiDaiDienCommandHandler(INguoiDaiDienRepository nguoiDaiDienRepository, IMapper mapper)
        {
            _nguoiDaiDienRepository = nguoiDaiDienRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteNguoiDaiDienCommand request, CancellationToken cancellationToken)
        {
            var NguoiDaiDienRepository = await _nguoiDaiDienRepository.GetById(request.Id);
            if (NguoiDaiDienRepository == null)
            {
                throw new NotFoundException(nameof(NguoiDaiDien), request.Id);
            }
            await _nguoiDaiDienRepository.Delete(NguoiDaiDienRepository);
            return Unit.Value;
        }
    }
}
