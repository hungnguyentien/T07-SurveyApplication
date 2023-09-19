using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.DTOs.BaoCaoCauHoi;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.DTOs.DotKhaoSat;
using SurveyApplication.Application.DTOs.GuiEmail;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.Features.BaoCaoCauHoi.Requests.Queries;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.BaoCaoCauHoi.Handlers.Queries
{
    public class GetDashBoardRequestHandler : BaseMasterFeatures, IRequestHandler<GetDashBoardRequest, DashBoardDto>
    {
        private readonly IMapper _mapper;

        public GetDashBoardRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<DashBoardDto> Handle(GetDashBoardRequest request, CancellationToken cancellationToken)
        {
            var tongDotKhaoSat = (from a in _surveyRepo.DotKhaoSat.GetAllQueryable()
                                join b in _surveyRepo.BangKhaoSat.GetAllQueryable() on a.Id equals b.IdDotKhaoSat
                                where (request.NgayBatDau == null || b.NgayBatDau >= request.NgayBatDau) &&
                                    (request.NgayKetThuc == null || b.NgayKetThuc <= request.NgayKetThuc) &&
                                    a.Deleted == false
                                select new DotKhaoSatDto
                                {
                                    Id = a.Id
                                }).ToList().Count();

            var tongBangKhaoSat = (from a in _surveyRepo.BangKhaoSat.GetAllQueryable()
                                where (request.NgayBatDau == null || a.NgayBatDau >= request.NgayBatDau) &&
                                    (request.NgayKetThuc == null || a.NgayKetThuc <= request.NgayKetThuc) &&
                                    a.Deleted == false
                                select new BangKhaoSatDto
                                {
                                    Id = a.Id
                                }).ToList().Count();

            var tongThamGiaKhaoSat = (from a in _surveyRepo.BangKhaoSat.GetAllQueryable()
                                      join b in _surveyRepo.GuiEmail.GetAllQueryable() on a.Id equals b.IdBangKhaoSat
                                      join c in _surveyRepo.KetQua.GetAllQueryable() on b.Id equals c.IdGuiEmail
                                      where (request.NgayBatDau == null || a.NgayBatDau >= request.NgayBatDau) &&
                                       (request.NgayKetThuc == null || a.NgayKetThuc <= request.NgayKetThuc) &&
                                       c.Deleted == false
                                   select new KetQua
                                   {
                                       Id = c.Id
                                   }).ToList().Count();

            var khaoSatTheoNhom = (from a in _surveyRepo.BangKhaoSat.GetAllQueryable()
                                   join b in _surveyRepo.LoaiHinhDonVi.GetAllQueryable() on a.IdLoaiHinh equals b.Id
                                   where (request.NgayBatDau == null || a.NgayBatDau >= request.NgayBatDau) &&
                                       (request.NgayKetThuc == null || a.NgayKetThuc <= request.NgayKetThuc) &&
                                       b.Deleted == false
                                   select new LoaiHinhDonViDto
                                   {
                                       Id = b.Id,
                                   }).ToList();

            var khaoSatTheoDot = (from a in _surveyRepo.BangKhaoSat.GetAllQueryable()
                                  join b in _surveyRepo.DotKhaoSat.GetAllQueryable() on a.IdDotKhaoSat equals b.Id
                                  where (request.NgayBatDau == null || a.NgayBatDau >= request.NgayBatDau) &&
                                   (request.NgayKetThuc == null || a.NgayKetThuc <= request.NgayKetThuc) &&
                                   b.Deleted == false
                                  select new DotKhaoSatDto
                                  {
                                      Id = b.Id
                                  }).ToList();

            var khaoSatTinhTp = (from a in _surveyRepo.BangKhaoSat.GetAllQueryable()
                                 join b in _surveyRepo.LoaiHinhDonVi.GetAllQueryable() on a.IdLoaiHinh equals b.Id
                                 join c in _surveyRepo.DonVi.GetAllQueryable() on b.Id equals c.IdLoaiHinh
                                 join d in _surveyRepo.TinhTp.GetAllQueryable() on c.IdTinhTp equals d.Id
                                 where (request.NgayBatDau == null || a.NgayBatDau >= request.NgayBatDau) &&
                                  (request.NgayKetThuc == null || a.NgayKetThuc <= request.NgayKetThuc) &&
                                  c.Deleted == false
                                 select new DonViDto
                                 {
                                     Id = c.Id,
                                     IdTinhTp = d.Id,
                                     TenTinhTp = d.Name,
                                 }).ToList();

            var tongKhaoSatTinhTp = khaoSatTinhTp.GroupBy(record => record.IdTinhTp).OrderByDescending(group => group.Count()).ToList();

            return new DashBoardDto
            {
                CountDotKhaoSat = tongDotKhaoSat,
                CountBangKhaoSat = tongBangKhaoSat,
                CountThamGia = tongThamGiaKhaoSat,
                CountDonViSo = khaoSatTheoNhom.Count(x => x.Id == 1) / khaoSatTheoNhom.Count() * 100,
                CountDonViBo = khaoSatTheoNhom.Count(x => x.Id == 2) / khaoSatTheoNhom.Count() * 100,
                CountDonViNganh = khaoSatTheoNhom.Count(x => x.Id == 3) / khaoSatTheoNhom.Count() * 100,
                CountDot1 = khaoSatTheoDot.Count(x => x.Id == 1) / khaoSatTheoDot.Count() * 100,
                CountDot2 = khaoSatTheoDot.Count(x => x.Id == 2) / khaoSatTheoDot.Count() * 100,
                CountDot3 = khaoSatTheoDot.Count(x => x.Id == 3) / khaoSatTheoDot.Count() * 100,


            };
        }
    }
}
