using AutoMapper;
using MediatR;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.NguoiDaiDiens.Handlers.Commands
{
    public class DeleteNguoiDaiDienCommandHandler : BaseMasterFeatures, IRequestHandler<DeleteNguoiDaiDienCommand>
    {
        private readonly IMapper _mapper;

        public DeleteNguoiDaiDienCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteNguoiDaiDienCommand request, CancellationToken cancellationToken)
        {
            var NguoiDaiDienRepository = await _surveyRepo.NguoiDaiDien.GetById(request.Id);
            if (NguoiDaiDienRepository == null)
            {
                throw new NotFoundException(nameof(NguoiDaiDien), request.Id);
            }
            await _surveyRepo.NguoiDaiDien.Delete(NguoiDaiDienRepository);
            await _surveyRepo.SaveAync();
            return Unit.Value;
        }
    }
}
