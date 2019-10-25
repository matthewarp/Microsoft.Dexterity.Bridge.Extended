using System;

namespace Microsoft.Dexterity.Bridge.Extended
{
    public delegate void GenericAdditionalMenuHandler(DictionaryForm form, Guid eventTag);

    public static class GenericAdditionalMenus
    {
        public static event GenericAdditionalMenuHandler GenericAdditionalMenuClicked;

        public static Guid Add(short productId, string formName, string menuText, string acceleratorKey)
        {
            Guid eventTag = Guid.NewGuid();
            DictionaryRoot root = new DictionaryRoot(productId, false);
            root.Forms[formName].AddMenuHandler((sender, e) => FireEvent((DictionaryForm)sender, eventTag), menuText, acceleratorKey);

            return eventTag;
        }

        public static Guid GenericAdditionalMenu(this DictionaryForm form, string menuText, string acceleratorKey)
        {
            Guid eventTag = Guid.NewGuid();
            form.AddMenuHandler((sender, e) => FireEvent((DictionaryForm)sender, eventTag), menuText, acceleratorKey);

            return eventTag;
        }

        private static void FireEvent(DictionaryForm form, Guid eventTag)
        {
            GenericAdditionalMenuClicked?.Invoke(form, eventTag);
        }
    }
}
