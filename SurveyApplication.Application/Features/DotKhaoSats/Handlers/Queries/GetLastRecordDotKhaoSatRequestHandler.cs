using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.Features.DotKhaoSats.Requests.Queries;
using SurveyApplication.Application.Features.LoaiHinhDonVi.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.DotKhaoSats.Handlers.Queries
{
    public class GetLastRecordDotKhaoSatRequestHandler : BaseMasterFeatures, IRequestHandler<GetLastRecordDotKhaoSatRequest, string>
    {
        private readonly IMapper _mapper;

        public GetLastRecordDotKhaoSatRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
            surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<string> Handle(GetLastRecordDotKhaoSatRequest request, CancellationToken cancellationToken)
        {
            var lastEntity = await _surveyRepo.DotKhaoSat.GetAllQueryable().OrderByDescending(e => e.Id).FirstOrDefaultAsync();

            if (lastEntity != null)
            {
                var prefix = lastEntity.MaDotKhaoSat.Substring(0, 3);
                var currentNumber = int.Parse(lastEntity.MaDotKhaoSat.Substring(3));

                currentNumber++;
                var newNumber = currentNumber.ToString("D3");

                return prefix + newNumber;
            }

            return "DKS001";
        }
    }
}
