using AutoMapper;
using MediatR;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.LinhVucHoatDong.Handlers.Commands;

public class DeleteLinhVucHoatDongCommandHandler : BaseMasterFeatures,
    IRequestHandler<DeleteLinhVucHoatDongCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public DeleteLinhVucHoatDongCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(DeleteLinhVucHoatDongCommand request,
        CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();

        foreach (var item in request.Ids)
        {
            var linhVuc = await _surveyRepo.LinhVucHoatDong.SingleOrDefaultAsync(x => x.Id == item);

            var donVi = await _surveyRepo.DonVi.GetAllListAsync(x => x.IdLinhVuc == linhVuc.Id);

            if (donVi.Count() != 0)
            {
                response.Success = false;
                response.Message = "Đang có bản ghi liên quan, không thể xóa được!";
                return response;
            }
        }

        var lstLinhVuc = await _surveyRepo.LinhVucHoatDong.GetByIds(x => request.Ids.Contains(x.Id));

        if (lstLinhVuc == null || lstLinhVuc.Count == 0)
            throw new NotFoundException(nameof(LinhVucHoatDong), request.Ids);

        foreach (var item in lstLinhVuc)
            item.Deleted = true;

        await _surveyRepo.LinhVucHoatDong.UpdateAsync(lstLinhVuc);
        await _surveyRepo.SaveAync();
        return new BaseCommandResponse("Xóa thành công!");
    }
}