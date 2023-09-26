﻿namespace SurveyApplication.Domain.Interfaces.Persistence;

public interface IDonViRepository : IGenericRepository<DonVi>
{
    Task<bool> ExistsByMaDonVi(string maDonVi);
    Task<bool> ExistsByEmail(string email);
}