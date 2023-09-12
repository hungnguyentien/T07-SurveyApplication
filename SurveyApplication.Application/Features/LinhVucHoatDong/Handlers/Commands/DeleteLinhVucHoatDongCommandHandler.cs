using AutoMapper;
using MediatR;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Commands;
using SurveyApplication.Domain.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.LinhVucHoatDong.Handlers.Commands
{
    public class DeleteLinhVucHoatDongCommandHandler : BaseMasterFeatures, IRequestHandler<DeleteLinhVucHoatDongCommand>
    {
        private readonly IMapper _mapper;

        public DeleteLinhVucHoatDongCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteLinhVucHoatDongCommand request, CancellationToken cancellationToken)
        {
            var LinhVucHoatDongRepository = await _surveyRepo.LinhVucHoatDong.GetById(request.Id);
            if (LinhVucHoatDongRepository == null)
            {
                throw new NotFoundException(nameof(LinhVucHoatDong), request.Id);
            }
            await _surveyRepo.LinhVucHoatDong.Delete(LinhVucHoatDongRepository);
            await _surveyRepo.SaveAync();
            return Unit.Value;
        }
    }
}
