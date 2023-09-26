using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Queries;
using SurveyApplication.Application.Features.LoaiHinhDonVi.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.LinhVucHoatDong.Handlers.Queries
{
    public class GetLastRecordLinhVucRequestHandler : BaseMasterFeatures, IRequestHandler<GetLastRecordLinhVucRequest, string>
    {
        private readonly IMapper _mapper;

        public GetLastRecordLinhVucRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<string> Handle(GetLastRecordLinhVucRequest request, CancellationToken cancellationToken)
        {
            var lastEntity = await _surveyRepo.LinhVucHoatDong.GetAllQueryable().OrderByDescending(e => e.Id).FirstOrDefaultAsync();

            if (lastEntity != null)
            {
                var prefix = lastEntity.MaLinhVuc.Substring(0, 2);
                var currentNumber = int.Parse(lastEntity.MaLinhVuc.Substring(2));

                currentNumber++;
                var newNumber = currentNumber.ToString("D3");

                return prefix + newNumber;
            }

            return "LV001";
        }
    }
}
