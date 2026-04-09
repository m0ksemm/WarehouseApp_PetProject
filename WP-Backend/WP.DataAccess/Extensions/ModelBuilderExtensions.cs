using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace WP.DataAccess.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder ConfigureDateTimeConversion(this ModelBuilder modelBuilder)
        {
            var emptyDateTime = DateTime.SpecifyKind(new DateTime(1900, 1, 1, 0, 0, 0), DateTimeKind.Utc);

            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
                v => v.HasValue && v.Value != emptyDateTime ? v.Value : null,
                v => v.HasValue && v.Value != emptyDateTime ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : null);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime))
                    {
                        property.SetValueConverter(dateTimeConverter);
                    }
                    else if (property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(nullableDateTimeConverter);
                    }
                }
            }

            return modelBuilder;
        }

        public static ModelBuilder AddIsoWeekTraslation(this ModelBuilder modelBuilder, Type baseType, string translationPlaceholderName)
        {
            var isoWeekMethodInfo = baseType
                .GetRuntimeMethod(translationPlaceholderName, [typeof(DateTime?)])
                    ?? throw new InvalidOperationException($"Method '{translationPlaceholderName}' not found in type '{baseType.FullName}'.");

            modelBuilder.HasDbFunction(isoWeekMethodInfo)
               .HasTranslation(args =>
                        new SqlFunctionExpression(
                            "DATEPART",
                            [new SqlFragmentExpression("isowk"), args.ToArray()[0]],
                            false,
                            [false, false],
                            typeof(int),
                            null));

            return modelBuilder;
        }
    }
}
