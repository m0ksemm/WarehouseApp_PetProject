using WP.DataAccess.Entities;

namespace WP.DataAccess.ApplicationDb.Entities
{
    public class RefreshToken : BaseEntity
    {
        public Guid UserId { get; set; }

        public string Token { get; set; }

        public long TokenExpires { get; set; }

        public UserAccount User { get; set; }
    }
}
