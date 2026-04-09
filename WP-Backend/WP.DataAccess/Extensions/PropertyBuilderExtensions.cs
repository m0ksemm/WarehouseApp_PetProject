using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WP.DataAccess.Extensions
{
    public static class PropertyBuilderExtensions
    {
        public static PropertyBuilder<bool> HasInvertedConversion(this PropertyBuilder<bool> propertyBuilder)
        {
            ArgumentNullException.ThrowIfNull(propertyBuilder);

            return propertyBuilder.HasConversion<long>(v => v ? 0 : 1, v => v == 0);
        }

        public static PropertyBuilder<bool?> HasInvertedConversion(this PropertyBuilder<bool?> propertyBuilder)
        {
            return propertyBuilder.HasConversion<long?>(v => v.HasValue ? (v.Value ? 0 : 1) : null, v => v.HasValue ? v.Value == 0 : null);
        }

        public static PropertyBuilder<decimal> HasWeightConversion(this PropertyBuilder<decimal> propertyBuilder)
        {
            ArgumentNullException.ThrowIfNull(propertyBuilder);

            return propertyBuilder.HasConversion<decimal?>(v => v * 1000m, v => v != null ? v.Value / 1000m : 0);
        }
    }
}
