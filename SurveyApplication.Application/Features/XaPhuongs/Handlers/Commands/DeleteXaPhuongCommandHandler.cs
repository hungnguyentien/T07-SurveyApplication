using AutoMapper;
using MediatR;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.XaPhuongs.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.XaPhuongs.Handlers.Commands
{
    public class DeleteXaPhuongCommandHandler : BaseMasterFeatures, IRequestHandler<DeleteXaPhuongCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;

        public DeleteXaPhuongCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteXaPhuongCommand request, CancellationToken cancellationToken)
        {
            var lstXaPhuong = await _surveyRepo.XaPhuong.GetByIds(x => request.Ids.Contains(x.Id));
            if (lstXaPhuong == null || lstXaPhuong.Count == 0)
                throw new NotFoundException(nameof(XaPhuong), request.Ids);

            foreach (var item in lstXaPhuong)
                item.Deleted = true;

            await _surveyRepo.XaPhuong.UpdateAsync(lstXaPhuong);
            await _surveyRepo.SaveAync();
            return new BaseCommandResponse("Xóa thành công!");
        }
    }
}
