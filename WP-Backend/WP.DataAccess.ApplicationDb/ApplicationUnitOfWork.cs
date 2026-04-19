namespace WP.DataAccess.ApplicationDb
{
    public class ApplicationUnitOfWork : BaseUnitOfWork<ApplicationDbContext>, IApplicationUnitOfWork
    {
        public ApplicationUnitOfWork(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
