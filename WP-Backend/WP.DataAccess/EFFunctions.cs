namespace WP.DataAccess
{
    public static class EFFunctions
    {
        public static int WeekIso(DateTime? date) => throw new InvalidOperationException($"{nameof(WeekIso)} cannot be called client side.");
    }
}
