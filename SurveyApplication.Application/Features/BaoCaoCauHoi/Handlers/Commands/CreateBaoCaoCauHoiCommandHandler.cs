using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Application.Features.BaoCaoCauHoi.Requests.Commands;
using SurveyApplication.Domain.Common.Configurations;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Utility;

namespace SurveyApplication.Application.Features.BaoCaoCauHoi.Handlers.Commands
{
    public class CreateBaoCaoCauHoiCommandHandler : BaseMasterFeatures, IRequestHandler<CreateBaoCaoCauHoiCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private EmailSettings EmailSettings { get; }
        public CreateBaoCaoCauHoiCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper,
            IOptions<EmailSettings> emailSettings) : base(surveyRepository)
        {
            _mapper = mapper;
            EmailSettings = emailSettings.Value;
        }

        public async Task<BaseCommandResponse> Handle(CreateBaoCaoCauHoiCommand request, CancellationToken cancellationToken)
        {
            var idGuiEmail = JsonConvert.DeserializeObject<EmailThongTinChungDto>(StringUltils.DecryptWithKey(request.IdGuiEmail, EmailSettings.SecretKey))?.IdGuiEmail ?? 0;
            var guiEmail = await _surveyRepo.GuiEmail.GetById(idGuiEmail);
            var lstCauHoi = await (from a in _surveyRepo.BangKhaoSat.GetAllQueryable().AsNoTracking()
                                   join b in _surveyRepo.DotKhaoSat.GetAllQueryable().AsNoTracking() on a.IdDotKhaoSat equals b.Id
                                   join c in _surveyRepo.BangKhaoSatCauHoi.GetAllQueryable().AsNoTracking() on a.Id equals c.IdBangKhaoSat
                                   join d in _surveyRepo.CauHoi.GetAllQueryable().AsNoTracking() on c.IdCauHoi equals d.Id
                                   where a.Id == guiEmail.IdBangKhaoSat && !a.Deleted && !b.Deleted && !c.Deleted && !d.Deleted
                                   select new
                                   {
                                       IdBangKhaoSat = a.Id,
                                       IdDotKhaoSat = b.Id,
                                       IdCauHoi = d.Id,
                                       d.MaCauHoi,
                                       guiEmail.IdDonVi
                                   }).ToListAsync(cancellationToken: cancellationToken);

            var lstBaoCaoCauHoi = _mapper.Map<List<Domain.BaoCaoCauHoi>>(request.LstBaoCaoCauHoi);
            lstBaoCaoCauHoi.ForEach(x =>
            {
                var cauHoi = lstCauHoi.FirstOrDefault(q => q.MaCauHoi == x.MaCauHoi);
                if (cauHoi == null) return;
                x.IdDotKhaoSat = cauHoi.IdDotKhaoSat;
                x.IdBangKhaoSat = cauHoi.IdBangKhaoSat;
                x.IdCauHoi = cauHoi.IdCauHoi;
                x.IdDonVi = cauHoi.IdDonVi;
                x.DauThoiGian = DateTime.Now;
                x.IdGuiEmail = idGuiEmail;
            });
            await _surveyRepo.BaoCaoCauHoi.InsertAsync(lstBaoCaoCauHoi);
            await _surveyRepo.SaveAync();
            return new BaseCommandResponse("Sync Done!");
        }
    }
}
