using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended
{
    public static class ScriptExtensions
    {
        private static readonly Dictionary<string, ScriptExtended> _cache = new Dictionary<string, ScriptExtended>();

        public static ScriptExtended Extended(this Script script)
        {
            string key = script.Key();
            if (!_cache.ContainsKey(key))
                _cache[key] = new ScriptExtended(script);

            return _cache[key];
        }

        public static string Key(this Script field) => $"{(field.IsFunction ? "FUNCTION" : "PROCEDURE")} {field.FullName} in {field.Dictionary.ProductId}";
    }
}
