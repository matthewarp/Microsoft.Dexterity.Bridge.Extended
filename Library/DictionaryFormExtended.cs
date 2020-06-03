using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended
{
    public class DictionaryFormExtended
    {
        private static readonly PropertyInfo idInfo;

        public DictionaryForm Form { get; }

        public short Id { get; }

        static DictionaryFormExtended()
        {
            idInfo = typeof(DictionaryForm).GetProperty("Id", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        internal DictionaryFormExtended(DictionaryForm form)
        {
            Form = form;

            Id = (short)idInfo.GetValue(form);
        }
    }
}
