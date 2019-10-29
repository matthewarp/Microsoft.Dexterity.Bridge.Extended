using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended
{
    public static class DictionaryRoots
    {
        private static readonly Dictionary<int, DictionaryRoot> fCache = new Dictionary<int, DictionaryRoot>();

        private static readonly Dictionary<int, DictionaryRoot> mCache = new Dictionary<int, DictionaryRoot>();

        public static DictionaryRoot Get(int productId, bool isFormsDictionary)
        {
            if(isFormsDictionary)
            {
                if (!fCache.ContainsKey(productId))
                    fCache[productId] = new DictionaryRoot(productId, isFormsDictionary);

                return fCache[productId];
            }else
            {
                if (!mCache.ContainsKey(productId))
                    mCache[productId] = new DictionaryRoot(productId, isFormsDictionary);

                return mCache[productId];
            }
        }

        public static DictionaryRoot Main(int productId) => Get(productId, false);

        public static DictionaryRoot ModifiedForm(int productId) => Get(productId, true);
    }
}
