namespace WP.Infrastructure.Enums
{
    /// <summary>
    /// Application role identifiers.
    /// </summary>
    public enum Roles
    {
        /// <summary>
        /// System administrator with full access.
        /// </summary>
        SYSTEM_ADMIN = 1,

        /// <summary>
        /// Warehouse administrator.
        /// </summary>
        WAREHOUSE_ADMIN = 2,

        /// <summary>
        /// Warehouse manager.
        /// </summary>
        WAREHOUSE_MANAGER = 3,

        /// <summary>
        /// Loader / warehouse worker.
        /// </summary>
        LOADER = 4,

        /// <summary>
        /// Inventory operator.
        /// </summary>
        INVENTORY_OPERATOR = 5,

        /// <summary>
        /// Tenant company user.
        /// </summary>
        TENANT = 6,

        /// <summary>
        /// Tenant company manager.
        /// </summary>
        TENANT_MANAGER = 7,

        /// <summary>
        /// Read-only auditor.
        /// </summary>
        AUDITOR = 8
    }
}
