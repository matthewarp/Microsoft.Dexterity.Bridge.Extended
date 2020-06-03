using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended
{
    public static class DictionaryFormExtensions
    {
        private static readonly Dictionary<string, DictionaryFormExtended> _cache = new Dictionary<string, DictionaryFormExtended>();

        public static DictionaryFormExtended Extended(this DictionaryForm form)
        {
            string key = form.Key();
            if (!_cache.ContainsKey(key))
                _cache[key] = new DictionaryFormExtended(form);

            return _cache[key];
        }

        public static string Key(this DictionaryForm form) => $"{form.FullName} in {form.Dictionary.ProductId}";
    }
}
