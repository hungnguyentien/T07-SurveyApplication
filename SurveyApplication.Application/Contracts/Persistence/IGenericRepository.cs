using SurveyApplication.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using SurveyApplication.Application.DTOs.Common;
using System.Linq.Expressions;

namespace SurveyApplication.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<bool> Exists(int id);
        Task<IReadOnlyList<T>> GetAll();
        Task<T> Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task Deletes(IList<T> entites);
        Task<IList<T>> GetByIds(Expression<Func<T, bool>> conditions);
        Task<PagingDto<T>> GetByConditionsQuerieResponse<TOrderBy>(int pageIndex, int pageSize, Expression<Func<T, bool>> conditions,
            Expression<Func<T, TOrderBy>> orderBy);
        Task<PageCommandResponse<T>> GetByConditions<TOrderBy>(int pageIndex, int pageSize, Expression<Func<T, bool>> conditions, Expression<Func<T, TOrderBy>> orderBy);
    }
}
