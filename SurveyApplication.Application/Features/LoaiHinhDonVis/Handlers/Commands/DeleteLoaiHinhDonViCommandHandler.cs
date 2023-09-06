using AutoMapper;
using MediatR;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Handlers.Commands
{
    public class DeleteLoaiHinhDonViCommandHandler : BaseMasterFeatures, IRequestHandler<DeleteLoaiHinhDonViCommand>
    {
        private readonly IMapper _mapper;

        public DeleteLoaiHinhDonViCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteLoaiHinhDonViCommand request, CancellationToken cancellationToken)
        {
            var LoaiHinhDonViRepository = await _surveyRepo.LoaiHinhDonVi.GetById(request.Id);
            if (LoaiHinhDonViRepository == null)
            {
                throw new NotFoundException(nameof(LoaiHinhDonVi), request.Id);
            }
            await _surveyRepo.LoaiHinhDonVi.Delete(LoaiHinhDonViRepository);
            await _surveyRepo.SaveAync();
            return Unit.Value;
        }
    }
}
