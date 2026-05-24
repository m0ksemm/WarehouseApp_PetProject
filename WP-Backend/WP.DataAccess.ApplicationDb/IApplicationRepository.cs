using WP.DataAccess.Interfaces;

namespace WP.DataAccess.ApplicationDb
{
    public interface IApplicationRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
    }
}
