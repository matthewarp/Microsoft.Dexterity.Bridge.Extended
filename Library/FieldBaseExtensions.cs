using System.Collections.Generic;

namespace Microsoft.Dexterity.Bridge.Extended
{
    public static class FieldBaseExtensions
    {
        private static readonly Dictionary<string, FieldBaseExtended> _cache = new Dictionary<string, FieldBaseExtended>();

        public static FieldBaseExtended Extended(this FieldBase field)
        {
            string key = field.Key();
            if (!_cache.ContainsKey(key))
                _cache[key] = new FieldBaseExtended(field);

            return _cache[key];
        }

        public static string Key(this FieldBase field) => $"{field.FullName} in {field.Dictionary.ProductId}";
    }
}