using AutoMapper;
using MediatR;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.XaPhuongs.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.XaPhuongs.Handlers.Commands
{
    public class DeleteXaPhuongCommandHandler : BaseMasterFeatures, IRequestHandler<DeleteXaPhuongCommand>
    {
        private readonly IMapper _mapper;

        public DeleteXaPhuongCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteXaPhuongCommand request, CancellationToken cancellationToken)
        {
            var XaPhuongRepository = await _surveyRepo.XaPhuong.GetById(request.Id);
            if (XaPhuongRepository == null)
            {
                throw new NotFoundException(nameof(XaPhuong), request.Id);
            }
            await _surveyRepo.XaPhuong.Delete(XaPhuongRepository);
            await _surveyRepo.SaveAync();
            return Unit.Value;
        }
    }
}
