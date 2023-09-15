using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.DTOs.QuanHuyen;
using SurveyApplication.Application.DTOs.QuanHuyen.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.QuanHuyens.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.QuanHuyens.Handlers.Commands
{
    public class DeleteQuanHuyenCommandHandler : BaseMasterFeatures, IRequestHandler<DeleteQuanHuyenCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;

        public DeleteQuanHuyenCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteQuanHuyenCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            foreach (var item in request.Ids)
            {
                var quanHuyen = await _surveyRepo.QuanHuyen.SingleOrDefaultAsync(x => x.Id == item);

                var xaPhuong = await _surveyRepo.XaPhuong.GetAllListAsync(x => x.ParentCode == quanHuyen.Code);

                if (xaPhuong.Count() != 0)
                {
                    response.Success = false;
                    response.Message = "Đang có bản ghi liên quan, không thể xóa được!";
                    return response;
                }
            }

            var lstQuanHuyen = await _surveyRepo.QuanHuyen.GetByIds(x => request.Ids.Contains(x.Id));

            if (lstQuanHuyen == null || lstQuanHuyen.Count == 0)
                throw new NotFoundException(nameof(QuanHuyen), request.Ids);

            foreach (var item in lstQuanHuyen)
                item.Deleted = true;

            await _surveyRepo.QuanHuyen.UpdateAsync(lstQuanHuyen);
            await _surveyRepo.SaveAync();
            return new BaseCommandResponse("Xóa thành công!");
        }
    }
}
