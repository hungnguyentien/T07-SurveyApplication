using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.Contracts.Persistence;
using System.Linq.Expressions;

namespace SurveyApplication.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly SurveyApplicationDbContext _dbContext;

        public GenericRepository(SurveyApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> Create(T entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Check tồn tại theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Exists(int id)
        {
            var entity = await GetById(id);
            return entity != null;
        }

        public async Task Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Tìm kiếm và phân trang
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="conditions"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<T>> GetByConditions(int pageIndex, int pageSize, Expression<Func<T, bool>> conditions,
            Expression<Func<T, bool>>? orderBy = null)
        {
            var result = _dbContext.Set<T>().AsNoTracking().Where(conditions);
            if (orderBy != null)
                result = result.OrderBy(orderBy);

            return await result.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}
