using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using SurveyApplication.Domain.Common;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces;

namespace SurveyApplication.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected SurveyApplicationDbContext DbContext;

        public GenericRepository(SurveyApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<T> Create(T entity)
        {
            await DbContext.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> Creates(IEnumerable<T> entities)
        {
            await DbContext.Set<T>().AddRangeAsync(entities);
            await DbContext.SaveChangesAsync();
            return entities;
        }

        public async Task Delete(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task Deletes(IList<T> entites)
        {
            DbContext.Set<T>().RemoveRange(entites);
            await DbContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
        }

        #region Get ToListAsync cũ

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await DbContext.Set<T>().ToListAsync();
        }


        public async Task<T> GetById(int id)
        {
            return await DbContext.Set<T>().FindAsync(id);
        }

        public async Task<IList<T>> GetByIds(Expression<Func<T, bool>> conditions)
        {
            return await DbContext.Set<T>().AsNoTracking().Where(conditions).ToListAsync();
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

        /// <summary>
        /// Tìm kiếm và phân trang
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="conditions"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public async Task<PageCommandResponse<T>> GetByConditions<TOrderBy>(int pageIndex, int pageSize, Expression<Func<T, bool>> conditions, Expression<Func<T, TOrderBy>> orderBy)
        {
            var dbSet = DbContext.Set<T>();

            if (dbSet == null)
            {
                return new PageCommandResponse<T>
                {
                    PageSize = 0,
                    PageCount = 0,
                    PageIndex = pageIndex,
                    Data = new List<T>(),
                };
            }

            var result = dbSet.AsNoTracking().Where(conditions).OrderByDescending(orderBy);

            var totalCount = await dbSet.CountAsync();

            var pageCount = (int)Math.Ceiling(totalCount / (double)pageSize);

            var pageResults = await result.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            var response = new PageCommandResponse<T>
            {
                PageSize = pageSize,
                PageCount = totalCount,
                PageIndex = pageIndex,
                Data = pageResults,
            };

            return response;
        }

        /// <summary>
        /// Tìm kiếm và phân trang có tổng số bản ghi
        /// </summary>
        /// <typeparam name="TOrderBy"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="conditions"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public async Task<Paging<T>> GetByConditionsQueriesResponse(int pageIndex, int pageSize, Expression<Func<T, bool>> conditions,
            string orderBy)
        {
            if (string.IsNullOrEmpty(orderBy))
                orderBy = "CreatedBy DESC";

            var query = DbContext.Set<T>().AsNoTracking().Where(conditions);
            var items = await query.OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            var totalCount = await query.LongCountAsync();
            return new Paging<T>(items, totalCount, pageIndex, pageSize);
        }

        #endregion

        #region Getting a list of entities

        public IEnumerable<T> GetAllList()
        {
            return DbContext.Set<T>().ToList();
        }

        public async Task<IEnumerable<T>> GetAllListAsync()
        {
            return await DbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public IEnumerable<T> GetAllList(Expression<Func<T, bool>> expression)
        {
            return DbContext.Set<T>().AsNoTracking().Where(expression).ToList();
        }

        public async Task<IEnumerable<T>> GetAllListAsync(Expression<Func<T, bool>> expression)
        {
            return await DbContext.Set<T>().AsNoTracking().Where(expression).ToListAsync();
        }

        public IQueryable<T> GetAllQueryable()
        {
            return DbContext.Set<T>();
        }

        #endregion Getting a list of entities

        #region Getting single entity

        public async Task<T> GetAsync(object id)
        {
            return await DbContext.Set<T>().FindAsync(id);
        }

        public T Get(object id)
        {
            return DbContext.Set<T>().Find(id);
        }

        public T Single(Expression<Func<T, bool>> predicate)
        {
            return GetAllQueryable().Single(predicate);
        }

        public T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return GetAllQueryable().SingleOrDefault(predicate);
        }

        public async Task<T> SingleAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetAllQueryable().SingleAsync(predicate);
        }

        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetAllQueryable().SingleOrDefaultAsync(predicate);
        }

        public T? FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return GetAllQueryable().FirstOrDefault(predicate);
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetAllQueryable().FirstOrDefaultAsync(predicate);
        }

        #endregion Getting single entity

        #region Insert

        public async Task<T> InsertAsync(T entity)
        {
            var x = await DbContext.Set<T>().AddAsync(entity);
            return x.Entity;
        }

        public async Task InsertAsync(IEnumerable<T> entities)
        {
            await DbContext.Set<T>().AddRangeAsync(entities);
        }

        #endregion Insert

        #region Update

        public async Task UpdateAsync(T entity)
        {
            await Task.Run(() =>
            {
                DbContext.Set<T>().Update(entity);
            });
        }

        public async Task UpdateAsync(IEnumerable<T> entities)
        {
            await Task.Run(() =>
            {
                DbContext.Set<T>().UpdateRange(entities);
            });
        }

        #endregion Update

        #region Delete

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() =>
            {
                DbContext.Set<T>().Remove(entity);
            });
        }

        public async Task DeleteAsync(IEnumerable<T> entities)
        {
            await Task.Run(() =>
            {
                DbContext.Set<T>().RemoveRange(entities);
            });
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            var entities = await DbContext.Set<T>().Where(predicate).ToListAsync();
            DbContext.Set<T>().RemoveRange(entities);
        }

        #endregion Delete

        #region Other

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await DbContext.Set<T>().AsNoTracking().AnyAsync(expression);
        }

        public int Count()
        {
            return GetAllQueryable().AsNoTracking().Count();
        }

        public async Task<int> CountAsync()
        {
            return await GetAllQueryable().AsNoTracking().CountAsync();
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return GetAllQueryable().AsNoTracking().Count(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetAllQueryable().AsNoTracking().CountAsync(predicate);
        }

        public long LongCount()
        {
            return GetAllQueryable().AsNoTracking().LongCount();
        }

        public async Task<long> LongCountAsync()
        {
            return await GetAllQueryable().AsNoTracking().LongCountAsync();
        }

        public long LongCount(Expression<Func<T, bool>> predicate)
        {
            return GetAllQueryable().AsNoTracking().LongCount(predicate);
        }

        public async Task<long> LongCountAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetAllQueryable().AsNoTracking().LongCountAsync(predicate);
        }

        public IQueryable<T> ExecuteStoreProc(string procName, object[] prs)
        {
            return DbContext.Set<T>().FromSqlRaw(procName, prs).AsQueryable();
        }

        public IQueryable<T> WhereIf<TSource>(Expression<Func<T, bool>> predicate)
        {
            return GetAllQueryable().AsNoTracking().Where(predicate);
        }

        public async Task<bool> Exists(Expression<Func<T, bool>> expression)
        {
            var entity = await GetAllQueryable().FirstOrDefaultAsync(expression);
            return entity != null;
        }

        #endregion Other
    }
}