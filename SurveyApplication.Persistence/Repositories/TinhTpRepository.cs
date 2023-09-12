﻿using Microsoft.EntityFrameworkCore;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Persistence.Repositories
{
    public class TinhTpRepository : GenericRepository<TinhTp>, ITinhTpRepository
    {
        private readonly SurveyApplicationDbContext _dbContext;

        public TinhTpRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsByCode(string code)
        {
            var entity = await _dbContext.TinhTp.AsNoTracking().FirstOrDefaultAsync(x => x.Code == code);
            return entity != null;
        }
    }
}
