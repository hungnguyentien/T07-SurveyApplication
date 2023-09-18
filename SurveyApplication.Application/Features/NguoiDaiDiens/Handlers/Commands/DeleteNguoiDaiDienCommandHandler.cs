using AutoMapper;
using MediatR;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.NguoiDaiDiens.Handlers.Commands
{
    public class DeleteNguoiDaiDienCommandHandler : BaseMasterFeatures, IRequestHandler<DeleteNguoiDaiDienCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;

        public DeleteNguoiDaiDienCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteNguoiDaiDienCommand request, CancellationToken cancellationToken)
        {
            //var response = new BaseCommandResponse();
            //foreach (var item in request.Ids)
            //{
            //    var nguoiDaiDien = await _surveyRepo.NguoiDaiDien.SingleOrDefaultAsync(x => x.Id == item);

            //    var ketQua = await _surveyRepo.KetQua.GetAllListAsync(x => x.IdNguoiDaiDien == nguoiDaiDien.Id);

            //    if (ketQua.Count() != 0)
            //    {
            //        response.Success = false;
            //        response.Message = "Đang có bản ghi liên quan, không thể xóa được!";
            //        return response;
            //    }
            //}

            var lstNguoiDaiDien = await _surveyRepo.NguoiDaiDien.GetByIds(x => request.Ids.Contains(x.Id));
            if (lstNguoiDaiDien == null || lstNguoiDaiDien.Count == 0)
                throw new NotFoundException(nameof(NguoiDaiDien), request.Ids);

            foreach (var item in lstNguoiDaiDien)
                item.Deleted = true;

            await _surveyRepo.NguoiDaiDien.UpdateAsync(lstNguoiDaiDien);
            await _surveyRepo.SaveAync();
            return new BaseCommandResponse("Xóa thành công!");
        }
    }
}
