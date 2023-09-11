using System.Collections.Generic;
using System.Threading.Tasks;
using Hangfire.Application.Models;

namespace Hangfire.Application.Interfaces
{
    public interface IBaseMasterService<TEntity>
        where TEntity : class
    {
        Task<IEnumerable<TEntity>> Gets();

        Task<TEntity> Get(object id);

        Task<ServiceResult> Create(TEntity model);

        Task<ServiceResult> Update(TEntity model);

        Task<ServiceResult> Delete(object id);
    }
}