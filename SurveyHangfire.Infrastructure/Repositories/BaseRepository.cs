using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hangfire.Domain.Interfaces;

namespace DASHangfire.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected DbContext Context { get; set; }
        public BaseRepository(DbContext repositoryContext)
        {
            Context = repositoryContext;
        }

        #region Getting a list of entities

        public IQueryable<T> GetAll()
        {
            return Context.Set<T>();
        }
       
        #endregion Getting a list of entities

        #region Insert

        public async Task<T> InsertAsync(T entity)
        {
            var x = await Context.Set<T>().AddAsync(entity);
            return x.Entity;
        }

        public async Task InsertAsync(IEnumerable<T> entities)
        {
            await Context.Set<T>().AddRangeAsync(entities);
        }

        #endregion Insert

        #region Update

        public async Task UpdateAsync(T entity)
        {
            await Task.Run(() =>
            {
                Context.Set<T>().Update(entity);
            });
        }

        public async Task UpdateAsync(IEnumerable<T> entities)
        {
            await Task.Run(() =>
            {
                Context.Set<T>().UpdateRange(entities);
            });
        }

        #endregion Update

    }
}
