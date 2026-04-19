using System;
using System.Collections.Generic;
using System.Text;

namespace WP.DataAccess.ApplicationDb.Entities
{
    public class ResetPasswordAttempt
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime ValidTo { get; set; }
    }
}
