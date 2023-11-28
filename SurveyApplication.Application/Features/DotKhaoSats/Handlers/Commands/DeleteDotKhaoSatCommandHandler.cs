using AutoMapper;
using MediatR;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.DotKhaoSats.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.DotKhaoSats.Handlers.Commands
{

    public class DeleteDotKhaoSatCommandHandler : BaseMasterFeatures, IRequestHandler<DeleteDotKhaoSatCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        public DeleteDotKhaoSatCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
            surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteDotKhaoSatCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            if (await _surveyRepo.BangKhaoSat.Exists(x => request.Ids.Contains(x.IdDotKhaoSat)))
            {
                response.Success = false;
                response.Message = "Đang có bản ghi liên quan, không thể xóa được!";
                return response;
            }

            var lstDotKhaoSat = await _surveyRepo.DotKhaoSat.GetByIds(x => request.Ids.Contains(x.Id));
            if (lstDotKhaoSat == null || lstDotKhaoSat.Count == 0)
                throw new NotFoundException(nameof(DotKhaoSat), request.Ids);

            foreach (var item in lstDotKhaoSat)
                item.Deleted = true;

            await _surveyRepo.DotKhaoSat.UpdateAsync(lstDotKhaoSat);
            await _surveyRepo.SaveAync();
            return new BaseCommandResponse("Xóa thành công!");
        }
    }
}
