﻿namespace SurveyApplication.Application.DTOs.CauHoi
{
    public class PhieuKhaoSatDto
    {
        public int IdBangKhaoSat { get; set; }
        public int TrangThai { get; set; }
        public List<CauHoiDto> LstCauHoi { get; set; }
        public string KqSurvey { get; set; }
    }

    public class EmailThongTinChungDto
    {
        public int? IdBangKhaoSat { get; set; }
        public int? IdDonVi { get; set; }
    }
}
