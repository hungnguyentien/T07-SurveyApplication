using AutoMapper;
using MediatR;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.TinhTps.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.TinhTps.Handlers.Commands
{
    public class DeleteTinhTpCommandHandler : BaseMasterFeatures, IRequestHandler<DeleteTinhTpCommand>
    {
        private readonly IMapper _mapper;

        public DeleteTinhTpCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteTinhTpCommand request, CancellationToken cancellationToken)
        {
            var TinhTpRepository = await _surveyRepo.TinhTp.GetById(request.Id);
            if (TinhTpRepository == null)
            {
                throw new NotFoundException(nameof(TinhTp), request.Id);
            }
            await _surveyRepo.TinhTp.Delete(TinhTpRepository);
            await _surveyRepo.SaveAync();
            return Unit.Value;
        }
    }
}
