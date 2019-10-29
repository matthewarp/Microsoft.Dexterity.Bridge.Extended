using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended
{
    public static class DictionaryFormExtensions
    {
        public static string Key(this DictionaryForm form) => $"{form.FullName} in {form.Dictionary.ProductId}";
    }
}
