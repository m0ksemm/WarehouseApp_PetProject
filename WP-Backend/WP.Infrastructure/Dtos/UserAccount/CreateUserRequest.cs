using System;
using System.Collections.Generic;
using System.Text;
using WP.Infrastructure.Enums;

namespace WP.Infrastructure.Dtos.UserAccount
{
    public class CreateUserRequest
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public UserType UserType { get; set; }

        public UserStatus UserStatus { get; set; }

        public string? Cellphone { get; set; }

        public string? WorkPhone { get; set; }

        public Guid? ManagerId { get; set; }

        public Guid? TenantId { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidUntil { get; set; }

        public List<string>? Roles { get; set; }
    }
}
