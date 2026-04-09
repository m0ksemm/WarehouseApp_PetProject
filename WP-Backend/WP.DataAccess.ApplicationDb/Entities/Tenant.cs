using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WP.DataAccess.Entities
{
    public class Tenant : BaseEntity
    {
        public string CompanyName { get; set; } = null!;

        public string? ContactPerson { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public ICollection<StorageLease> Leases { get; set; } = new List<StorageLease>();

        public ICollection<SpaceReservation> Reservations { get; set; } = new List<SpaceReservation>();
    }
}
