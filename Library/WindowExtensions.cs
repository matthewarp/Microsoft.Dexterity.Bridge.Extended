using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended
{
    public static class WindowExtensions
    {
        private static Dictionary<string, WindowExtended> _cache = new Dictionary<string, WindowExtended>();

        public static WindowExtended Extended(this Window window)
        {
            string key = window.Key();
            if (!_cache.ContainsKey(key))
                _cache[key] = new WindowExtended(window);

            return _cache[key];
        }

        public static string Key(this Window window) => $"{window.FullName} in {window.Dictionary.ProductId}";
    }
}
