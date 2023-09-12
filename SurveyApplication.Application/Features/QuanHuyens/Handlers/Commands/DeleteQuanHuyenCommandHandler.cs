using AutoMapper;
using MediatR;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.QuanHuyens.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.QuanHuyens.Handlers.Commands
{
    public class DeleteQuanHuyenCommandHandler : BaseMasterFeatures, IRequestHandler<DeleteQuanHuyenCommand>
    {
        private readonly IMapper _mapper;

        public DeleteQuanHuyenCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteQuanHuyenCommand request, CancellationToken cancellationToken)
        {
            var QuanHuyenRepository = await _surveyRepo.QuanHuyen.GetById(request.Id);
            if (QuanHuyenRepository == null)
            {
                throw new NotFoundException(nameof(QuanHuyen), request.Id);
            }
            await _surveyRepo.QuanHuyen.Delete(QuanHuyenRepository);
            await _surveyRepo.SaveAync();
            return Unit.Value;
        }
    }
}
