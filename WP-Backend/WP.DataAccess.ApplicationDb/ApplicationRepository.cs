namespace WP.DataAccess.ApplicationDb
{
    public class ApplicationRepository<TEntity> : BaseRepository<TEntity, ApplicationDbContext>, IApplicationRepository<TEntity>
        where TEntity : class
    {
        public ApplicationRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
