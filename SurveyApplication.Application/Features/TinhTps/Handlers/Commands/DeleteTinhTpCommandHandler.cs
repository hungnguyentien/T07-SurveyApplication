using AutoMapper;
using MediatR;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.TinhTps.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.TinhTps.Handlers.Commands
{
    public class DeleteTinhTpCommandHandler : BaseMasterFeatures, IRequestHandler<DeleteTinhTpCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;

        public DeleteTinhTpCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteTinhTpCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            foreach (var item in request.Ids)
            {
                var tinhTp = await _surveyRepo.TinhTp.SingleOrDefaultAsync(x => x.Id == item);

                var quanHuyen = await _surveyRepo.QuanHuyen.GetAllListAsync(x => x.ParentCode == tinhTp.Code);

                if (quanHuyen.Count() != 0)
                {
                    response.Success = false;
                    response.Message = "Đang có bản ghi liên quan, không thể xóa được!";
                    return response;
                }
            }

            var lstTinhTp = await _surveyRepo.TinhTp.GetByIds(x => request.Ids.Contains(x.Id));

            if (lstTinhTp == null || lstTinhTp.Count == 0)
                throw new NotFoundException(nameof(TinhTp), request.Ids);

            foreach (var item in lstTinhTp)
                item.Deleted = true;

            await _surveyRepo.TinhTp.UpdateAsync(lstTinhTp);
            await _surveyRepo.SaveAync();
            return new BaseCommandResponse("Xóa thành công!");
        }
    }
}
