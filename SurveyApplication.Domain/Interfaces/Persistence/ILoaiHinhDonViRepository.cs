﻿namespace SurveyApplication.Domain.Interfaces.Persistence;

public interface ILoaiHinhDonViRepository : IGenericRepository<LoaiHinhDonVi>
{
    Task<bool> ExistsByMaLoaiHinh(string maloaihinh);
    Task<bool> ExistsByName(string tenLoaiHinh);
}