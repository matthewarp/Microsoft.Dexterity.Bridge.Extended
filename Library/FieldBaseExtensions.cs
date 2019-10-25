using System.Collections.Generic;

namespace Microsoft.Dexterity.Bridge.Extended
{
    public static class FieldBaseExtensions
    {
        private static Dictionary<string, FieldBaseWithEvents> _eventsCache = new Dictionary<string, FieldBaseWithEvents>();

        public static FieldBaseWithEvents Extended(this FieldBase field)
        {
            string key = field.Key();
            if (!_eventsCache.ContainsKey(key))
                _eventsCache[key] = new FieldBaseWithEvents(field);

            return _eventsCache[key];
        }

        public static object Value(this FieldBase field) => LocateForValue(field).Value;

        public static T Value<T>(this FieldBase field) => (T)LocateForValue(field).Value;

        public static string ValueToString(this FieldBase field) => LocateForValue(field).Value?.ToString();

        internal static FieldBaseWithEvents LocateForValue(FieldBase field)
        {
            string key = field.Key();

            if (_eventsCache.ContainsKey(key))
                return _eventsCache[key];

            return new FieldBaseWithEvents(field, false);
        }

        internal static string Key(this FieldBase field) => $"{field.FullName} in {field.Dictionary.ProductId}";
    }
}