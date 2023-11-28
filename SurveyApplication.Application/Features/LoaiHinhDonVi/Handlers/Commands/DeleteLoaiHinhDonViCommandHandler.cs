using AutoMapper;
using MediatR;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.LoaiHinhDonVi.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.LoaiHinhDonVi.Handlers.Commands;

public class DeleteLoaiHinhDonViCommandHandler : BaseMasterFeatures, IRequestHandler<DeleteLoaiHinhDonViCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public DeleteLoaiHinhDonViCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(DeleteLoaiHinhDonViCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();

        foreach (var item in request.Ids)
        {
            var loaiHinh = await _surveyRepo.LoaiHinhDonVi.SingleOrDefaultAsync(x => x.Id == item);

            var dotKhaoSat = await _surveyRepo.DotKhaoSat.GetAllListAsync(x => x.IdLoaiHinh == loaiHinh.Id);
            var bangKhaoSat = await _surveyRepo.BangKhaoSat.GetAllListAsync(x => x.IdLoaiHinh == loaiHinh.Id);
            var donVi = await _surveyRepo.DonVi.GetAllListAsync(x => x.IdLoaiHinh == loaiHinh.Id);

            if (dotKhaoSat.Count() != 0 || bangKhaoSat.Count() != 0 || donVi.Count() != 0)
            {
                response.Success = false;
                response.Message = "Đang có bản ghi liên quan, không thể xóa được!";
                return response;
            }
        }

        var lstLoaiHinh = await _surveyRepo.LoaiHinhDonVi.GetByIds(x => request.Ids.Contains(x.Id));

        if (lstLoaiHinh == null || lstLoaiHinh.Count == 0)
            throw new NotFoundException(nameof(LoaiHinhDonVi), request.Ids);

        foreach (var item in lstLoaiHinh)
            item.Deleted = true;

        await _surveyRepo.LoaiHinhDonVi.UpdateAsync(lstLoaiHinh);
        await _surveyRepo.SaveAync();
        return new BaseCommandResponse("Xóa thành công!");
    }
}