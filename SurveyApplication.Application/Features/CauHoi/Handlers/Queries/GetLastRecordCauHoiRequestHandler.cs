using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class GetLastRecordCauHoiRequestHandler : BaseMasterFeatures, IRequestHandler<GetLastRecordCauHoiRequest, string>
    {
        private readonly IMapper _mapper;

        public GetLastRecordCauHoiRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
            surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<string> Handle(GetLastRecordCauHoiRequest request, CancellationToken cancellationToken)
        {
            var lastEntity = await _surveyRepo.CauHoi.GetAllQueryable().OrderByDescending(e => e.Id).FirstOrDefaultAsync();

            if (lastEntity != null)
            {
                var prefix = lastEntity.MaCauHoi.Substring(0, 2);
                var currentNumber = int.Parse(lastEntity.MaCauHoi.Substring(2));

                currentNumber++;
                var newNumber = currentNumber.ToString("D3");

                return prefix + newNumber;
            }

            return "CH001";
        }
    }
}
