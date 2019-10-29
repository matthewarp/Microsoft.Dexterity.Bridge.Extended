using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended
{
    public static class DictionaryRootExtensions
    {
        private static Dictionary<int, DictionaryRootExtended> fCache = new Dictionary<int, DictionaryRootExtended>();

        private static Dictionary<int, DictionaryRootExtended> mCache = new Dictionary<int, DictionaryRootExtended>();

        public static DictionaryRootExtended Extended(this DictionaryRoot dic)
        {
            if (dic.IsFormsDictionary)
            {
                if (!fCache.ContainsKey(dic.ProductId))
                    fCache[dic.ProductId] = new DictionaryRootExtended(dic);

                return fCache[dic.ProductId];
            }else
            {
                if (!mCache.ContainsKey(dic.ProductId))
                    mCache[dic.ProductId] = new DictionaryRootExtended(dic);

                return mCache[dic.ProductId];
            }
        }
    }
}
