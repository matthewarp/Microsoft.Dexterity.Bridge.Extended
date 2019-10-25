using System;

namespace Microsoft.Dexterity.Bridge.Extended
{
    public delegate void GenericAdditionalMenuHandler(DictionaryForm form, Guid eventTag);

    public static class GenericAdditionalMenus
    {
        public static event GenericAdditionalMenuHandler MenuClicked;

        public static Guid Register(short productId, string formName, string menuText, string acceleratorKey)
        {
            Guid eventTag = Guid.NewGuid();
            DictionaryRoot root = new DictionaryRoot(productId, false);
            root.Forms[formName].AddMenuHandler((sender, e) => OnMenuClicked((DictionaryForm)sender, eventTag), menuText, acceleratorKey);

            return eventTag;
        }

        public static Guid GenericAdditonalMenu(this DictionaryForm form, string menuText, string acceleratorKey)
        {
            Guid eventTag = Guid.NewGuid();
            form.AddMenuHandler((sender, e) => OnMenuClicked((DictionaryForm)sender, eventTag), menuText, acceleratorKey);

            return eventTag;
        }

        private static void OnMenuClicked(DictionaryForm form, Guid eventTag)
        {
            MenuClicked?.Invoke(form, eventTag);
        }
    }
}
