using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WP.DataAccess.Entities
{
    public class Tenant : BaseEntity
    {
        public Guid UserAccountId { get; set; }

        public string CompanyName { get; set; } = null!;

        public string? ContactPerson { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public UserAccount UserAccount { get; set; } = null!;

        public ICollection<StorageLease> StorageLeases { get; set; } = new List<StorageLease>();
    }
}
