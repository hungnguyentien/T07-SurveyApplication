using AutoMapper;
using MediatR;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.CauHoi.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.CauHoi.Handlers.Commands
{
    public class DeleteCauHoiCommandHandler : BaseMasterFeatures, IRequestHandler<DeleteCauHoiCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        public DeleteCauHoiCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteCauHoiCommand request, CancellationToken cancellationToken)
        {
            var lstCauHoi = await _surveyRepo.CauHoi.GetByIds(x => request.Ids.Contains(x.Id));
            if (lstCauHoi == null || lstCauHoi.Count == 0)
                throw new NotFoundException(nameof(CauHoi), request.Ids);

            await _surveyRepo.CauHoi.DeleteAsync(lstCauHoi);
            await _surveyRepo.SaveAync();
            return new BaseCommandResponse("Xóa thành công!");
        }
    }
}
