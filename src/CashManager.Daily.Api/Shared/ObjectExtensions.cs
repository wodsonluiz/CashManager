namespace CashManager.Daily.Api.Shared
{
    public static class ObjectExtensions
    {
        public static bool TryValueObjectToString(this object entity, string key, out string value)
        {
            value = string.Empty;

            if(entity == null)
                return false;

            var type = entity.GetType();
            var property = type?.GetProperty(key);

            value = property?.GetValue(entity, null)?.ToString();

            if(string.IsNullOrWhiteSpace(value))
                return false;

            return true;
        }
    }
}