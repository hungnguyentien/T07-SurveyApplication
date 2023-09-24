using AutoMapper;
using MediatR;
using SurveyApplication.Application.Features.CauHoi.Requests.Queries;
using SurveyApplication.Application.Features.LoaiHinhDonVi.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.CauHoi.Handlers.Queries
{
    public class GetLastRecordHangRequestHandler : BaseMasterFeatures, IRequestHandler<GetLastRecordHangRequest, string>
    {
        private readonly IMapper _mapper;

        public GetLastRecordHangRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
            surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<string> Handle(GetLastRecordHangRequest request, CancellationToken cancellationToken)
        {
            var cauHoi = await _surveyRepo.CauHoi.GetByIds(x => x.MaCauHoi == request.GenCauHoiDto.MaCauHoi);
            if (cauHoi.Count != 0) throw new Exception("Mã câu hỏi đã tồn tại");

            var maHang = request.GenCauHoiDto.MaCauHoi + "_Hang" + request.GenCauHoiDto.Length;
            return maHang;
        }
    }
}
