using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(string id);
        Task<IReadOnlyList<T>> GetAll();
        Task<T> Create(T entity);
        Task Update(T entity);
        Task Delete(string id);
        Task<(IReadOnlyList<T> Items, int TotalCount)> Search(Expression<Func<T, bool>> filter, int pageNumber, int pageSize);
    }
}
