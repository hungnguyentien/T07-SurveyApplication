using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangfire.Domain.Interfaces
{
    public interface IBaseRepository<T>
    {
        #region Getting a list of entities

        IQueryable<T> GetAll();

        #endregion Getting a list of entities

        #region Insert

        Task<T> InsertAsync(T entity);

        Task InsertAsync(IEnumerable<T> entities);

        #endregion Insert

        #region Update

        Task UpdateAsync(T entity);

        Task UpdateAsync(IEnumerable<T> entities);

        #endregion Update
       
    }
}