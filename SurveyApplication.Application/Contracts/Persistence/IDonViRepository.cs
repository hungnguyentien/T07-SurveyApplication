﻿using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Contracts.Persistence
{
    public interface IDonViRepository : IGenericRepository<DonVi>
    {
        Task<bool> ExistsByMaDonVi(string maDonVi);
    }
}
