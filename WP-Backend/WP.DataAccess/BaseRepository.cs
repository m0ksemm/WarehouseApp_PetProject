using System.Collections;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WP.DataAccess.Interfaces;
using Z.EntityFramework.Plus;

namespace WP.DataAccess
{
    public class BaseRepository<TEntity, TContext> : IRepository<TEntity>, IQueryable<TEntity>, IEnumerable<TEntity>, IEnumerable, IQueryable, IAsyncEnumerable<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(TContext context)
        {
            _dbSet = context.Set<TEntity>();
        }   

        public Type ElementType => ((IQueryable)_dbSet).ElementType;

        public Expression Expression => ((IQueryable)_dbSet).Expression;

        public IQueryProvider Provider => ((IQueryable)_dbSet).Provider;

        public async Task<int> BatchDeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Queryable.Where(_dbSet, predicate).DeleteAsync();
        }

        public async Task<int> BatchUpdateAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> updateFactory)
        {
            return await Queryable.Where(_dbSet, predicate).UpdateAsync(updateFactory);
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> CreateManyAsync(IEnumerable<TEntity> entities)
        {
            TEntity[] entitiesArray = (entities as TEntity[]) ?? entities.ToArray();
            await _dbSet.AddRangeAsync(entitiesArray);
            return entitiesArray;
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteMany(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<TEntity> FindAsync(object[] keyValues)
        {
            return await _dbSet.FindAsync(keyValues);
        }

        public IQueryable<TEntity> FromSqlInterpolated(FormattableString sql)
        {
            return _dbSet.FromSqlInterpolated(sql);
        }

        public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return ((IAsyncEnumerable<TEntity>)_dbSet).GetAsyncEnumerator(cancellationToken);
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return ((IEnumerable<TEntity>)_dbSet).GetEnumerator();
        }

        public TEntity Update(TEntity entity)
        {
            _dbSet.Update(entity);
            return entity;
        }

        public IEnumerable<TEntity> UpdateMany(IEnumerable<TEntity> entities)
        {
            TEntity[] array = (entities as TEntity[]) ?? entities.ToArray();
            _dbSet.UpdateRange(array);
            return array;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
