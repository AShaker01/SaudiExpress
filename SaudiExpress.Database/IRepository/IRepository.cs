using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiExpress.Database.IRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int id);

        TEntity GetById(Guid id);

        TEntity GetById(string id);

        IEnumerable<TEntity> GetAll();

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        Task<IEnumerable<TEntity>> GetAllByOrderAsync(Func<TEntity, object> predicate, string sort = "Ascending");

        ValueTask<TEntity> GetByIdAsync(int id);

        ValueTask<TEntity> GetByIdAsync(Guid id);

        ValueTask<TEntity> GetByIdAsync(string id);

        ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
    }
}
