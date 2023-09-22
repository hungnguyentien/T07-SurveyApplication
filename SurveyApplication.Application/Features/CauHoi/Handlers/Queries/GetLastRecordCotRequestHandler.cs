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
    public class GetLastRecordCotRequestHandler : BaseMasterFeatures, IRequestHandler<GetLastRecordCotRequest, string>
    {
        private readonly IMapper _mapper;

        public GetLastRecordCotRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
            surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<string> Handle(GetLastRecordCotRequest request, CancellationToken cancellationToken)
        {
            var cauHoi = await _surveyRepo.CauHoi.GetByIds(x => x.MaCauHoi == request.GenCauHoiDto.MaCauHoi);
            if (cauHoi.Count != 0) throw new Exception("Mã câu hỏi đã tồn tại");

            var maCot = request.GenCauHoiDto.MaCauHoi + "_Cot" + request.GenCauHoiDto.Length;
            return maCot;
        }
    }
}
