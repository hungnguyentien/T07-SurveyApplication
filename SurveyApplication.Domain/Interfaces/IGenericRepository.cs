using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SurveyApplication.Domain.Common;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<bool> Exists(int id);
        Task<IReadOnlyList<T>> GetAll();
        Task<T> Create(T entity);
        Task<IEnumerable<T>> Creates(IEnumerable<T> entities);
        Task Update(T entity);
        Task Delete(T entity);
        Task Deletes(IList<T> entites);
        Task<IList<T>> GetByIds(Expression<Func<T, bool>> conditions);
        Task<Paging<T>> GetByConditionsQueriesResponse(int pageIndex, int pageSize, Expression<Func<T, bool>> conditions,
            string orderBy);
        Task<PageCommandResponse<T>> GetByConditions<TOrderBy>(int pageIndex, int pageSize, Expression<Func<T, bool>> conditions, Expression<Func<T, TOrderBy>> orderBy);

        #region Getting a list of entities

        IEnumerable<T> GetAllList();
        Task<IEnumerable<T>> GetAllListAsync();
        IEnumerable<T> GetAllList(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllListAsync(Expression<Func<T, bool>> expression);
        IQueryable<T> GetAllQueryable();

        #endregion Getting a list of entities

        #region Getting single entity

        Task<T> GetAsync(object id);
        T Get(object id);
        T Single(Expression<Func<T, bool>> predicate);
        T SingleOrDefault(Expression<Func<T, bool>> predicate);
        Task<T> SingleAsync(Expression<Func<T, bool>> predicate);
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
        T? FirstOrDefault(Expression<Func<T, bool>> predicate);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        #endregion Getting single entity

        #region Insert

        Task<T> InsertAsync(T entity);
        Task InsertAsync(IEnumerable<T> entities);

        #endregion Insert

        #region Update

        Task UpdateAsync(T entity);
        Task UpdateAsync(IEnumerable<T> entities);

        #endregion Update

        #region Delete

        Task DeleteAsync(T entity);
        Task DeleteAsync(IEnumerable<T> entities);
        Task DeleteAsync(Expression<Func<T, bool>> predicate);

        #endregion Delete

        #region Other

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        int Count();
        Task<int> CountAsync();
        int Count(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        long LongCount();
        Task<long> LongCountAsync();
        long LongCount(Expression<Func<T, bool>> predicate);
        Task<long> LongCountAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> ExecuteStoreProc(string procName, object[] prs);
        IQueryable<T> WhereIf<TSource>(Expression<Func<T, bool>> predicate);
        Task<bool> Exists(Expression<Func<T, bool>> expression);

        #endregion Other
    }
}
